using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private Text leftTextScore;
    [SerializeField] private Text rightTextScore;

    [SerializeField] private Goal leftGoal;
    [SerializeField] private Goal rightGoal;

    [SerializeField] private GameManager gameManager;
    public char whoScored;


    private int leftScore = 0;

    private int rightScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        RefreshScore();
    }

    private void RefreshScore()//this post the score.
    {
       leftTextScore.text = $"{leftScore}";
       rightTextScore.text = $"{rightScore}";

       if (leftScore == 10)
       {
           leftTextScore.color = Color.yellow;
       }
       if (rightScore == 10)
       {
           rightTextScore.color = Color.yellow;
       }

       if (leftScore == 11 || rightScore == 11)
       {
           resetScore();
       }
    }
    public void AddScore(Goal scoringSide)
    {
        if (scoringSide == leftGoal)
        {
            whoScored = 'L';
            rightScore += 1;
        }
        else
        {
            whoScored = 'R';
            leftScore += 1;
        }
        RefreshScore();
    }

    public void resetScore()
    {
        leftScore = 0;
        leftTextScore.color = Color.white;
        
        rightScore = 0;
        rightTextScore.color = Color.white; 
        
        RefreshScore();
    }
}
