using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI text;


    [SerializeField] private AudioSource soundButton;
    [SerializeField] private AudioClip hoverAudio;
    [SerializeField] private AudioClip clickAudio;
    [SerializeField] public bool hasClicked;

    void Awake()
    {
        if(GameManager.Instance != null)
        {
            

            if (GameManager.Instance.hasWon == true)
            {
                if (text == null) return;
                Debug.Log("Win");
                conditionText = "YOU WIN";
                text.text = conditionText;
            }

            if (GameManager.Instance.hasWon == false)
            {
                if (text == null) return;
                Debug.Log("Lose");
                conditionText = "GAME OVER";
                text.text = conditionText;
            }

            GameManager.Instance.menuScript = this.GetComponent<MenuScript>();

            for (int i = 0; i < GameManager.Instance.scoreArray.Count; i++) // Load Scores
            {
                scoresPositions[i].text = GameManager.Instance.scoreArray[i].ToString();
            }
        }
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
