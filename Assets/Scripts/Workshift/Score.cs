using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Splash splash;
    public decimal scoreEarned = 0m;
    private decimal bonusScoreEarned = 0m;

    // Start is called before the first frame update
    void Start()
    {
        Workshift.OnScoreAdded += AddScore;
        Workshift.OnBonusScoreAdded += AddBonusScore;
        Timer.OnTimerEnded += DisplayScore;
        Exit.OnDayEnded += ResetBonusScore;
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

    private void ResetBonusScore()
    {
        bonusScoreEarned = 0m;
    }

    private void AddScore(decimal scoreAdded)
    {
        scoreEarned += scoreAdded;
    }

    private void AddBonusScore(decimal scoreAdded)
    {
        bonusScoreEarned += scoreAdded;
        scoreEarned += scoreAdded;
    }

    private void DisplayScore()
    {
        string bonusScoreText = bonusScoreEarned.ToString("0.00");
        string regularScoreText = (scoreEarned - bonusScoreEarned).ToString("0.00");

        splash.DisplayScore(regularScoreText, bonusScoreText);
    }
}
