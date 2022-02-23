using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float jumpForce = 10f;
    [SerializeField]
    float raydistance=5f;
    [SerializeField]
    Color raycolor = Color.red;
    [SerializeField]
    LayerMask groundlayer;
    [SerializeField]
    Score score;

    Rigidbody2D rb2d;
    SpriteRenderer spr;
    GameInputs gameInputs;
    Animator anim;
    [SerializeField]
    Vector3 rayOrigin;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gameInputs = new GameInputs();
    }
    void OnEnable(){
        gameInputs.Enable();
    }

    void OnDisable(){
        gameInputs.Disable();
    }
    void Start()
    {
    }

    void FixedUpdate()
    {
        rb2d.position += Horizontal * Vector2.right * moveSpeed * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.right * Axis.x * moveSpeed * Time.deltaTime);
        spr.flipX = FlipSpriteX;       
        //if(JumpButton && IsGrounding)
        //{
        //    rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //    anim.SetTrigger("jump");
        //}

    }

    void LateUpdate()
    {
        anim.SetFloat("AxisX", Mathf.Abs(Horizontal));
        anim.SetBool("ground", IsGrounding);
    }

    Vector2 Axis => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    bool JumpButton => Input.GetButtonDown("Jump");
    bool IsGrounding => Physics2D.Raycast(transform.position, Vector2.down, raydistance, groundlayer);
    bool FlipSpriteX => Horizontal > 0f ? false : Horizontal < 0f ? true : spr.flipX;
    void OnDrawGizmosSelected()
    {
        Gizmos.color=raycolor;
        Gizmos.DrawRay(transform.position + rayOrigin , Vector2.down*raydistance);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            GameManager.instance.GetScore.AddPoints(coin.GetPoints);
            Destroy(other.gameObject);
            Debug.Log(coin.GetPoints);
            
        }
    }
    float Horizontal => gameInputs.Gameplay.Horizontal.ReadValue<float>();
}
