using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{
    public void Selection()
    {
        if (this.GetComponent<PowerUp>().inStack) 
        {
            PowerUpManager.instance.GetPowerUp();
        }
    }
}
