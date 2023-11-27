using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameView : BaseView
{
    [SerializeField]
    private TextMeshProUGUI scoreTxt;

    public void SetScore(int _score)
    {
        scoreTxt.text = _score.ToString();
    }
}
