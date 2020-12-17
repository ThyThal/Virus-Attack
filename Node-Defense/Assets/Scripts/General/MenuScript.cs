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

    [Header("Menus")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _helpMenu;
    [SerializeField] private Animator _helpAnimator;
    [SerializeField] private bool _isHelp;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private AudioSource soundButton;
    [SerializeField] private AudioClip hoverAudio;
    [SerializeField] private AudioClip clickAudio;
    [SerializeField] public bool hasClicked;

    [SerializeField] public Animator transition;

    private static readonly int Show = Animator.StringToHash("Show");

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
        ClickSound();
        StartCoroutine(TransitionScene(GameManager.Instance.gameScene));
    }

    IEnumerator TransitionScene(string scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    public void OnClickMenu()
    {
        ClickSound();
        StartCoroutine(TransitionScene(GameManager.Instance.menuScene));
    }

    public void OnClickHelp()
    {
        _isHelp = true;
        _helpAnimator.SetBool(Show, true);
        ClickSound();
    }

    public void OnClickExit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
        ClickSound();
        Application.Quit();
    }

    public void OnClickBack()
    {
        _helpAnimator.SetBool(Show, false);
        _isHelp = false;
        ClickSound();
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

    public void MainMenuHide()
    {
        _mainMenu.SetActive(false);
    }

    public void MainMenuShow()
    {
        _mainMenu.SetActive(true);
    }
}
