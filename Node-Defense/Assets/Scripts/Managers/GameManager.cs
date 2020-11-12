using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {

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
        scoreArray.Add(score);
        hasWon = false;
    }

    public void Win() // <====={ SCENE WIN }
    {
        SceneManager.LoadScene(conditionScene);
        hasWon = true;
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
