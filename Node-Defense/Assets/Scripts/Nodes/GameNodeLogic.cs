using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNodeLogic : MonoBehaviour
{
    public void Selection()
    {
        if (PowerUpManager.instance.activePowerUp != null && this.GetComponent<GameNode>().Type != GameNodeType.Internet && this.GetComponent<GameNode>().PowerUp == null)
        {
            PowerUpManager.instance.activePowerUp.transform.position = (Vector2)this.transform.position + (Vector2.up);
            this.GetComponent<GameNode>().PowerUp = PowerUpManager.instance.activePowerUp;
            PowerUpManager.instance.DeActivePowerUp();
        }
    }
}
