using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    static public ManagerUI instance;
    [SerializeField] private Text round;
    [SerializeField] private Text score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        round.text = $"Round: 0";
        score.text = $"Score: 0";
    }


    public void UpdateRound()
    {
        round.text = $"Round: {WaveManager.instance.currentWave + 1}";
    }
    public void UpdateScore()
    {
        score.text = $"Score: {GameManager.Instance.score}";
    }
}

