using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Current Wave Info")]
    [SerializeField] private GameObject virus;
    [SerializeField] public int totalVirus;

    [Header("Variables")]
    [SerializeField] private int currentWave;
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
        [SerializeField] public GameObject enemyToSpawn;
        [SerializeField] public int minVirus;
        [SerializeField] public int maxVirus;
        [SerializeField] public float spawnerRate;
    }

    public void Start()
    {
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
        state = SpawnState.SPAWNING;
        newWave = true;
        totalVirus = Random.Range(waveNumber.minVirus, waveNumber.maxVirus);
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
            nextWave = 0;
        }
        nextWave += 1;
    }
}
