using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public decimal scoreEarned = 0m;

    // Start is called before the first frame update
    void Start()
    {
        Workshift.OnScoreAdded += AddScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreEarned == 0m)
        {
            scoreText.text = "-";
        }
        else
        {
            scoreText.text = scoreEarned.ToString("0.00");
        }
    }

    private void AddScore(decimal scoreAdded)
    {
        scoreEarned += scoreAdded;
    }
}
