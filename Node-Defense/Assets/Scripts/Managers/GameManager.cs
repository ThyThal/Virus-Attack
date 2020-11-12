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
    [SerializeField] public MenuScript menuScript;

    [SerializeField] public List<int> scoreArray;
    [SerializeField] public int score;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
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
        SceneManager.LoadScene(conditionScene);

        if (scoreArray.Count < 5)
        {
            scoreArray.Add(score);
        }
        else
        {
            scoreArray[scoreArray.Count - 1] = score;
        }

        if (scoreArray.Count > 1)
            SortScores();

        hasWon = false;
    }

    public void Win() // <====={ SCENE WIN }
    {
        SceneManager.LoadScene(conditionScene);
        

        if (scoreArray.Count < 5)
        {
            scoreArray.Add(score);
        }
        else
        {
            scoreArray[scoreArray.Count -1] = score;
        }

        if (scoreArray.Count > 1)
            SortScores();

        hasWon = true;
    }

    public void SortScores()
    {
        int[] aux = scoreArray.ToArray();
        QuickSort.quickSort(aux, scoreArray.IndexOf(scoreArray.First()), scoreArray.LastIndexOf(scoreArray.Last()));
        scoreArray.Clear();
        scoreArray = aux.OfType<int>().ToList();
        scoreArray.Reverse();
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
}
