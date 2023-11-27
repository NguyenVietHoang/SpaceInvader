using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControler : MonoBehaviour
{
    public void SetScore(int _newScore)
    {
        if(_newScore > GetHighScore())
        {
            PlayerPrefs.SetInt("HighScore", _newScore);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
}
