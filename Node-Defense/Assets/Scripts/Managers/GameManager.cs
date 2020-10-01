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
        hasWon = false;
    }

    public void Win() // <====={ SCENE WIN }
    {
        SceneManager.LoadScene(conditionScene);
        hasWon = true;
    }
}
