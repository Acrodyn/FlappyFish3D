using UnityEngine;
using TMPro;

public class BiomMenuRepresentation : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private TransitionManager.Bioms Biom;
	[SerializeField] private TextMeshProUGUI Score;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		Score.text = Core.ScoreManager.GetBestScore(Biom).ToString();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
