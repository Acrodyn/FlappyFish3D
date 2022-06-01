using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SerializationManager : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private string FilePath;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		FilePath = Application.persistentDataPath + "/FlappyFishData.ffd"; ;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void SaveBiomData(Dictionary<TransitionManager.Bioms, int> biomScores)
	{
		BiomData biomData = new BiomData(biomScores);
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(FilePath, FileMode.Create);
		formatter.Serialize(stream, biomData);
		stream.Close();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public BiomData LoadBiomData()
	{
		BiomData biomData = new BiomData();

		if (File.Exists(FilePath))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(FilePath, FileMode.Open);
			biomData = formatter.Deserialize(stream) as BiomData;
			stream.Close();
		}

		return biomData;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
