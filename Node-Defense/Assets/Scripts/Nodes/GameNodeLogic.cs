using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNodeLogic : MonoBehaviour
{
    private Text levelText;

    public void Selection()
    {
        var gameNode = this.GetComponent<GameNode>();

        if (PowerUpManager.instance.activePowerUp != null && gameNode.Type != GameNodeType.Internet)
        {
            if (gameNode.PowerUp == null)
            {
                levelText = PowerUpManager.instance.activePowerUp.levelText;
                PowerUpManager.instance.activePowerUp.transform.position = (Vector2)this.transform.position + (Vector2.up);
                gameNode._sourceUpgrade.PlayOneShot(gameNode._upgradeAudio);
                gameNode.PowerUp = PowerUpManager.instance.activePowerUp;
                gameNode.powerUpLevel += 1;
                levelText.text = $"Level: {gameNode.powerUpLevel}";
                PowerUpManager.instance.DeActivePowerUp();
                return;
            }
        }

        if (gameNode.PowerUp.type == PowerUpManager.instance.activePowerUp.type && gameNode.Type != GameNodeType.Internet)
        {
            gameNode._sourceUpgrade.PlayOneShot(gameNode._upgradeAudio);
            gameNode.powerUpLevel += 1;
            levelText.text = $"Level: {gameNode.powerUpLevel}";
            PowerUpManager.instance.activePowerUp.gameObject.SetActive(false);
            PowerUpManager.instance.DeActiveDestroyPowerUp();
        }
    }
}
