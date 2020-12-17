using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{

    [SerializeField] public PowerUpType type;
    [SerializeField] public Text levelText;
    [SerializeField] public AudioSource audioSource;
    public bool inStack;

    public void PlaySelect()
    {
        audioSource.Play();
    }

}

public enum PowerUpType
{
    FireWall = 1,
    Antivirus = 2
};