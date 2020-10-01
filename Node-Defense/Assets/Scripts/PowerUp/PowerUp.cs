using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] public PowerUpType type;
    public bool inStack;


}

public enum PowerUpType
{
    FireWall = 1,
    Antivirus = 2
};