using UnityEngine;

public class TransitionManager : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	public enum Bioms
	{
		Biom1,
		Biom2,
		Biom3
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Properties]
	public bool HasAIAssistant => _hasAIAssistant;
	public float DifficultyScale => _difficultyScale;
	public bool IsSceneLoadedFromMenu => _isSceneLoadedFromMenu;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private bool _hasAIAssistant = false;
	private float _difficultyScale = 1f;
	private bool _isSceneLoadedFromMenu = false;
	// ------------------------------------------------------------------------------------------------------------------------------
	public void LoadScene(string scene)
	{
		_isSceneLoadedFromMenu = true;
		UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void LoadMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void ActivateAIAssistant(bool activateAssistant)
	{
		_hasAIAssistant = activateAssistant;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void SetDifficultyScale(float difficultyScale)
	{
		_difficultyScale = difficultyScale;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
