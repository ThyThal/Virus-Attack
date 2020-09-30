using System;
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
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject helpMenu;
    [SerializeField] private Button playButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private AudioSource soundButton;
    [SerializeField] private AudioClip hoverAudio;
    [SerializeField] private AudioClip clickAudio;
    [SerializeField] public bool hasClicked;

    private void Awake()
    {
        playButton.onClick.AddListener(OnClickPlay);
        helpButton.onClick.AddListener(OnClickHelp);
        exitButton.onClick.AddListener(OnClickExit);
        backButton.onClick.AddListener(OnClickBack);
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void OnClickHelp()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
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
        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
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
