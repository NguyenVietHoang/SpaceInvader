using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverView : BaseView
{
    [SerializeField]
    private TextMeshProUGUI currentScoreTxt;
    [SerializeField]
    private TextMeshProUGUI highScoreTxt;

    public void SetScore(int _currentScore, int _highScore)
    {
        currentScoreTxt.text = _currentScore.ToString();
        highScoreTxt.text = _highScore.ToString();
    }
}
