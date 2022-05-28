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
    private float _modifierDuration;
    private float _modifierValue;
    private bool _isModifierActive;
    // ------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        CheckModifier();
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    public void UpdateScore()
	{
        float receivedPoints = DefaultPointsPerSuccess;

        if (_isModifierActive)
		{
            receivedPoints *= _modifierValue; 
		}

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
		}

        _lastScore = _currentScore;
        _currentScore = 0;
        ResetModifierData();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void ApplyModifierData(ModifierData data)
	{
        _isModifierActive = true;
        _modifierDuration = data.ModifierDuration;
        _modifierValue = data.ModifierdPointScale;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void ResetModifierData()
	{
        _isModifierActive = false;
        _modifierDuration = 0f;
        _modifierValue = 1f;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    private void CheckModifier()
	{
        if (!_isModifierActive)
        {
            return;
        }

        _modifierDuration -= Time.deltaTime;
        if (_modifierDuration <= 0f)
		{
            ResetModifierData();
		}
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
