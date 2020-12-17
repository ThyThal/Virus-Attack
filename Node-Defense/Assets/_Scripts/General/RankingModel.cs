using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingModel 
{
    public int Id;
    public string NameValue;
    public int WaveValue;
    public int ScoreValue;

    public RankingModel(string name, int wave, int score)
    {
        NameValue = name;
        WaveValue = wave;
        ScoreValue = score;
    }
}
