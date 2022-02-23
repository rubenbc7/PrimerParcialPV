using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text tmpText;
    int score = 0;

    void Awake()
    {
        tmpText = GetComponent<Text>();
    }

    public void AddPoints(int points)
    {
        score += points;
        tmpText.text = $"Score: {score} pts";
    }    
}
