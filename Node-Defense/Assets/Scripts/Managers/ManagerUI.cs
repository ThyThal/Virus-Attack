using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    [SerializeField] public static ManagerUI Instance;
    [SerializeField] private Text round;
    [SerializeField] private Text score;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        round.text = $"Round: 0";
        score.text = $"Score: 0";
    }


    public void UpdateRound()
    {
        round.text = $"Round: {WaveManager.Instance.currentWave + 1}";
    }
    public void UpdateScore()
    {
        score.text = $"Score: {GameManager.Instance.score}";
    }
}

