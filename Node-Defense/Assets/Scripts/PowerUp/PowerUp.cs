using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{

    [SerializeField] public PowerUpType type;
    [SerializeField] public Text levelText;
    public bool inStack;


}

public enum PowerUpType
{
    FireWall = 1,
    Antivirus = 2
};