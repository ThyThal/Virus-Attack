using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] public string conditionText;
    [SerializeField] public List<Text> scoresPositions;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Text text;


    [SerializeField] private AudioSource soundButton;
    [SerializeField] private AudioClip hoverAudio;
    [SerializeField] private AudioClip clickAudio;
    [SerializeField] public bool hasClicked;


    public Queue scores;

    private void Awake()
    {
        if (GameManager.Instance.hasWon == true)
        {
            Debug.Log("Win");
            conditionText = "You Win";
            text.text = conditionText;
        }

        if (GameManager.Instance.hasWon == false)
        {
            Debug.Log("Lose");
            conditionText = "Game Over";
            text.text = conditionText;
        }
    }

    private void Start()
    {
        GameManager.Instance.menuScript = this.GetComponent<MenuScript>();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(GameManager.Instance.gameScene);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene(GameManager.Instance.menuScene);
    }

    public void OnClickHelp()
    {

    }

    public void OnClickExit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();

        #endif
        Application.Quit();
    }

    public void OnClickBack()
    {

    }

    public void ClickSound()
    {
        if (!hasClicked)
        {
            soundButton.PlayOneShot(clickAudio);
        }
    }

    public void HoverSound()
    {
        if (!hasClicked)
        {
            soundButton.PlayOneShot(hoverAudio);
        }
    }
}
