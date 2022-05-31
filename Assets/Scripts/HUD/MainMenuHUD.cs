using UnityEngine;
using UnityEngine.UI;

public class MainMenuHUD : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private Toggle AIAssistToggle;
	[SerializeField] private Slider DifficultySlider;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		AIAssistToggle.isOn = Core.TransitionManager.HasAIAssistant;
		DifficultySlider.value = Core.TransitionManager.DifficultyScale;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void OnAIAssistantToggled()
	{
		Core.TransitionManager.ActivateAIAssistant(AIAssistToggle.isOn);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void OnDifficultySliderChanged()
	{
		Core.TransitionManager.SetDifficultyScale(DifficultySlider.value);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
