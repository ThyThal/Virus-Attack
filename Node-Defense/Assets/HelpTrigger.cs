using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private MenuScript _menuScript;

    private void Start()
    {
        _menuScript = _menu.GetComponent<MenuScript>();
    }

    public void TriggerShow()
    {
        _menuScript.MainMenuShow();
    }

    public void TriggerHide()
    {
        _menuScript.MainMenuHide();
    }

}

