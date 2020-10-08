using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI currentScoreText;

    public static ScoreManager instance;

    int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = null;
        currentScore = 0;
    }

    public void AddScore()
    {
        currentScore += 1;
        currentScoreText.text = currentScore.ToString();
    }
}
