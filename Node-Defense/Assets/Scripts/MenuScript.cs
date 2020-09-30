using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string gameScene;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private AudioSource soundButton;
    [SerializeField] private AudioClip hoverAudio;
    [SerializeField] private AudioClip clickAudio;
    [SerializeField] public bool hasClicked;

    public void OnClickPlay()
    {
        SceneManager.LoadScene(gameScene);
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
