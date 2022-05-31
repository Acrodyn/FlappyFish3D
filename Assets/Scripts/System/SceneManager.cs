using UnityEngine;

public class SceneManager : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	public void LoadScene(string scene)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void LoadMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
