﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    static public WaveManager instance;

    [Header("Scale Difficulty")]
    [SerializeField] private ProgressBar _healthBar;
    [SerializeField] private Animator _levelIncrease;
    [SerializeField] private float difficultyIncrease = 1;
    [SerializeField] private float originalDifficultyIncrease;
    
    [Header("Variables")]
    [SerializeField] private int totalVirus;
    [SerializeField] public int currentWave;
    [SerializeField] private int nextWave;
    [SerializeField] public float searchTimer;
    [SerializeField] private float originalTimer;
    [SerializeField] private SpawnState state = SpawnState.WAITING;
    [SerializeField] private bool foundEnemy;
    [SerializeField] private bool newWave;
    [SerializeField] private int waveCounter = 0;

    public enum SpawnState { SPAWNING, WAITING }
    [SerializeField] public Wave[] waves;

    [System.Serializable]
    public class Wave
    {
        [SerializeField] public int virusAmount;
        [SerializeField] public float spawnerRate;
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        originalTimer = searchTimer;
        originalDifficultyIncrease = difficultyIncrease;
    }

    public void Update()
    {
        _healthBar.current = GameManager.Instance.deadVirus;
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive() && !newWave)
            {
                WaveCompleted();
                newWave = true;
            }

            else
            {
                return;
            }
        }

        if (state != SpawnState.SPAWNING)
        {
            if (currentWave == 0 && GameManager.Instance.deadVirus == 15)
            {
                GameManager.Instance.deadVirus = 0;
                _levelIncrease.SetTrigger("ShowMessage");
            }

            StartCoroutine(SpawnWave(waves[currentWave]));
        }
    }

    IEnumerator SpawnWave(Wave waveNumber)
    {
        ManagerUI.instance.UpdateRound();
        state = SpawnState.SPAWNING;
        newWave = true;

        totalVirus = waveNumber.virusAmount;
        VirusManager.instance.InstanstiateQueue(totalVirus);

        for (int i = 0; i < totalVirus; i++)
        {
            SpawnEnemy(VirusManager.instance.GetItem());
            
            yield return new WaitForSeconds(1f * waveNumber.spawnerRate);
        }

        waveCounter = waveCounter + 1;
        state = SpawnState.WAITING;
        newWave = false;
    }

    public void SpawnEnemy(GameObject virus)
    {
        var stats = virus.GetComponent<Virus>();

        if (waveCounter >= 5)
        {
            stats.speed += difficultyIncrease/10;
            stats.defense += difficultyIncrease * 5;
            stats.damage += difficultyIncrease + difficultyIncrease/3;
            waveCounter = 0;
        }

        LevelManager.instance.SpawnVirus(virus);
    }

    public bool EnemyIsAlive()
    {
        searchTimer -= Time.deltaTime;
        if (searchTimer <= 0f)
        {
            //Debug.Log("Search");
            if (GameObject.FindGameObjectWithTag("Virus") == null)
            {
                //Debug.Log("Nothing is Alive");
                foundEnemy = false;
                return false;
            }

            if (GameObject.FindGameObjectWithTag("Virus") != null)
            {
                //Debug.Log("Enemy Alive");
                foundEnemy = true;
            }
            searchTimer = originalTimer;
        }
        return true;
    }

    public void WaveCompleted()
    {
        currentWave = nextWave;
        if (nextWave + 1 > waves.Length - 1)
        {
            IncreaseStats();
            nextWave = 0;
        }

        else
        {
            nextWave += 1;
        }
    }

    private void IncreaseStats()
    {
        nextWave = 0;
        difficultyIncrease += originalDifficultyIncrease;
    }

}
