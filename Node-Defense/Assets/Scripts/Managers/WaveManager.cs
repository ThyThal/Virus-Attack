using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    static public WaveManager instance;

    [Header("Scale Difficulty")]
    [SerializeField] private float difficultyIncrease = 1;

    [Header("Variables")]
    [SerializeField] private int totalVirus;
    [SerializeField] public int currentWave;
    [SerializeField] private int nextWave;
    [SerializeField] public float searchTimer;
    [SerializeField] private float originalTimer;
    [SerializeField] private SpawnState state = SpawnState.WAITING;
    [SerializeField] private bool foundEnemy;
    [SerializeField] private bool newWave;

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

    }

    public void Update()
    {
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

        state = SpawnState.WAITING;
        newWave = false;
    }

    public void SpawnEnemy(GameObject virus)
    {
        var stats = virus.GetComponent<Virus>();
        stats.speed += difficultyIncrease;
        stats.damage += difficultyIncrease;

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
        nextWave += 1;
    }

    private void IncreaseStats()
    {
        difficultyIncrease += difficultyIncrease;
    }
}
