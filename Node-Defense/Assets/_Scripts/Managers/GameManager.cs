using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public string gameScene;
    [SerializeField] public string menuScene;
    [SerializeField] public string conditionScene;
    [SerializeField] public bool hasWon;
    [SerializeField] private bool isDead;
    [SerializeField] public int deadVirus;
    [SerializeField] public MenuScript menuScript;
    [SerializeField] public Animator transition;
    [SerializeField] private AudioSource _dieSource;

    [SerializeField] public List<int> scoreArray;
    public Database database;
    [SerializeField] public int score;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            database = new Database();
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        #if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Q)) // <==========[DEBUG SCORE] 
        {
            ScoreDebug();
        }

        if (Input.GetKeyDown(KeyCode.P)) // <==========[AUTOMATIC WIN] 
        {
            Win();
        }

        
        if (Input.GetKeyDown(KeyCode.O)) // <==========[AUTOMATIC LOSE] 
        {
            GameOver();
        }
        
        #endif
    }

    public void GameOver() // <====={ SCENE LOSE }
    {
        if (!isDead)
        {
            StartCoroutine(TransitionScene(conditionScene));
            isDead = true;

            RankingModel ranking = new RankingModel("Perdio", WaveManager.instance.currentWave, score);
            database.AddRankingRecord(ranking);
            GetScores();
            SortScores();

            isDead = true;
            hasWon = false;
        }
    }

    public void Win() // <====={ SCENE WIN }
    {
        StartCoroutine(TransitionScene(conditionScene));

        RankingModel ranking = new RankingModel("Gano", WaveManager.instance.currentWave, score);
        database.AddRankingRecord(ranking);
        GetScores();
        SortScores();

        isDead = false;
        hasWon = true;
    }

    public void GetScores()
    {
        List<RankingModel> allRanking = database.GetAllRankingRecords();
        scoreArray.Clear();
        foreach (RankingModel ranking in allRanking) {
            scoreArray.Add(ranking.ScoreValue);
        }
    }

    public void SortScores()
    {
        int[] aux = scoreArray.ToArray();
        QuickSort.quickSort(aux, scoreArray.IndexOf(scoreArray.First()), scoreArray.LastIndexOf(scoreArray.Last()));
        scoreArray.Clear();
        scoreArray = aux.OfType<int>().ToList();
        scoreArray.Reverse();
        if (scoreArray.Count > 5)
            scoreArray.RemoveRange(5, scoreArray.Count - 5);
    }

    public void ScoreUpdate(int s)
    {
        score += s;
        ManagerUI.instance.UpdateScore();
    }

    private void ScoreDebug()
    {
        score += Random.Range(25, 137);
        ManagerUI.instance.UpdateScore();
    }

    public void PlayEnemyDie()
    {
        deadVirus = deadVirus + 1;
        if (_dieSource != null)
            _dieSource.Play();
    }

    public IEnumerator TransitionScene(string scene)
    {
        if (scene == "GameScene")
        {
            isDead = false;
            deadVirus = 0;
        }

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}
