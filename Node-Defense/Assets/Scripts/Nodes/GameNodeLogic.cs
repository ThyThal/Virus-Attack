using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNodeLogic : MonoBehaviour
{
    public void Selection()
    {
        Debug.Log("SELECTION");
        if (PowerUpManager.instance.activePowerUp != null)
        {
            PowerUpManager.instance.activePowerUp.transform.position = (Vector2)this.transform.position + (Vector2.up);
            this.GetComponent<GameNode>().PowerUp = PowerUpManager.instance.activePowerUp;
            PowerUpManager.instance.activePowerUp = null;
        }
    }
}
