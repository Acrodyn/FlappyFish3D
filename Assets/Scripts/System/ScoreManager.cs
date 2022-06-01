using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Editor]
    [SerializeField] private int DefaultPointsPerSuccess;
    [SerializeField] private ObserverEvent CurrentPointsChangedEvent;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Properties]
    public int LastScore => _lastScore;
    public int BestScore => _bestScore;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Code - private]
    private int _currentScore;
    private int _bestScore;
    private int _lastScore;
    private Dictionary<TransitionManager.Bioms, int> _biomScores;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
        _biomScores = Core.SerializationManager.LoadBiomData().GetDictionary();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void UpdateBestScore()
	{
        _bestScore = _biomScores[Core.ActiveLevelController.ActiveBiom];
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void UpdateScore()
	{
        float receivedPoints = DefaultPointsPerSuccess * Core.ActiveLevelController.ModifierValue;
        _currentScore += (int)receivedPoints;

        EventIntMessage message = new EventIntMessage();
        message.MessageInt = _currentScore;
        CurrentPointsChangedEvent.Trigger(message);
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void OnGameEnd()
	{
        if (_currentScore > _bestScore)
 		{
            _bestScore = _currentScore;
            _biomScores[Core.ActiveLevelController.ActiveBiom] = _bestScore;
            Core.SerializationManager.SaveBiomData(_biomScores);
		}

        _lastScore = _currentScore;
        _currentScore = 0;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
