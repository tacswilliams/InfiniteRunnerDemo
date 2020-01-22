using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScoreTracker : MonoBehaviour
{
    public static BestScoreTracker INSANCE;
    public int bestScore;
    // Start is called before the first frame update
    void Awake()
    {
        if (INSANCE == null)
        {
            INSANCE = this;
            bestScore = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }  
    }

    public void SetBestScore(int newScore)
    {
        bestScore = newScore;
    }

}
