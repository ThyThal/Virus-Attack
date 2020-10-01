using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRespowner : MonoBehaviour
{
    public const int RESPAWN_TIME = 5;

    public List<GameObject> prefabs;
    public Transform parent;

    private float timer;
    public bool inGame;


    void Update()
    {
        if (inGame)
        {
            if (timer >= RESPAWN_TIME)
            {
                Respown();
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private void Respown()
    {
        //instanciar objeto
        var powerUp = Instantiate(prefabs[Random.Range(0, prefabs.Count)], parent);

        //guardarlo en la pila
        PowerUpManager.instance.StackPowerUp(powerUp);
    }
}
