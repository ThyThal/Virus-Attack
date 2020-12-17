using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    static public ManagerUI instance;
    [SerializeField] private int _round;
    [SerializeField] private Text round;
    [SerializeField] private Text score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _round = 0;
        round.text = $"Get Ready!";
        score.text = $"Score: 0";
    }


    public void UpdateRound()
    {
        _round += 1;
        round.text = $"Round: {_round}";
    }
    public void UpdateScore()
    {
        score.text = $"Score: {GameManager.Instance.score}";
    }
}

