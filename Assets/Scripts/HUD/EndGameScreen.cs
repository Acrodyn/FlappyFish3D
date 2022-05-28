using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Editor]
    [SerializeField] private TextMeshProUGUI CurrentScore;
    [SerializeField] private TextMeshProUGUI BestScore;
    // ------------------------------------------------------------------------------------------------------------------------------
    public void OnGameEnd()
	{
        CurrentScore.text = Core.ScoreManager.LastScore.ToString();
        BestScore.text = Core.ScoreManager.BestScore.ToString();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
