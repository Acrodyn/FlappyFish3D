using UnityEngine;
using TMPro;

public class PointCounter : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private TextMeshProUGUI CounterText;
	// ------------------------------------------------------------------------------------------------------------------------------
	public void OnPointsChanged(EventMessage message)
	{
		EventIntMessage pointsMessage = (EventIntMessage)message;
		CounterText.text = pointsMessage.MessageInt.ToString();

		// effect?
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void OnGameReset()
	{
		CounterText.text = "0";
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
