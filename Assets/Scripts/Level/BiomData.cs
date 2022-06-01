using System;
using System.Collections.Generic;

[System.Serializable]
public class BiomData
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Code - public]
    public int[] BestScores;
    // ------------------------------------------------------------------------------------------------------------------------------
    public BiomData()
    {
        BestScores = new int[Enum.GetNames(typeof(TransitionManager.Bioms)).Length];
        int index = 0;
        foreach (TransitionManager.Bioms biom in Enum.GetValues(typeof(TransitionManager.Bioms)))
        {
            BestScores[index] = 0;
            ++index;
        }
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public BiomData(Dictionary<TransitionManager.Bioms, int> biomScores)
	{
        BestScores = new int[Enum.GetNames(typeof(TransitionManager.Bioms)).Length];
        int index = 0;
        foreach (TransitionManager.Bioms biom in Enum.GetValues(typeof(TransitionManager.Bioms)))
        {
            BestScores[index] = (biomScores.ContainsKey(biom)) ? biomScores[biom] : 0;
            ++index;
        }
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    public Dictionary<TransitionManager.Bioms, int> GetDictionary()
	{
        Dictionary<TransitionManager.Bioms, int> dictionary = new Dictionary<TransitionManager.Bioms, int>();
        foreach (TransitionManager.Bioms biom in Enum.GetValues(typeof(TransitionManager.Bioms)))
        {
            dictionary.Add(biom, BestScores[(int)biom]);
        }

        return dictionary;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
