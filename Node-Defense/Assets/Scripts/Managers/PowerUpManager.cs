using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    private const int STACK_LIMIT = 5;
    private const float STACK_POSITION_DIFERENCESS = 50f;
    private const float STACK_POSITION_INIT = -100f;

    static public PowerUpManager instance;

    public Stack powerUps;
    public PowerUp activePowerUp; // power up seleccionado de pila y activo hasta aplicar en arma o muro
    private Vector2 powerUpActivePosition;
    public List<Vector2> stackPositions;


    void Awake()
    {
        instance = this;
        powerUps = new Stack();
        powerUpActivePosition = new Vector2(-100, 200);

        for (int i = 0; i < STACK_LIMIT; i++)
        {
            stackPositions.Add(new Vector2(0, stackPositions.Count == 0 ? STACK_POSITION_INIT : stackPositions[i - 1].y + STACK_POSITION_DIFERENCESS));
        }
    }

    /* Almacena el power up en la pila */
    public void StackPowerUp(GameObject powerUp)
    {
        if (powerUps.Count < STACK_LIMIT)
        {
            UpdateInteractableState(false);
            if (PowerUpIsActive())
            {
                powerUp.GetComponent<Button>().interactable = false;
            }

            powerUps.Push(powerUp); /* AGREGA OBJETO A LA PILA */

            powerUp.GetComponent<PowerUp>().inStack = true;
            powerUp.transform.localPosition = GetStackPosition();
        }
        else
        {
            Destroy(powerUp);
        }
    }

    /* Setea el último power up en la pila a activo para aplicar */
    public void GetPowerUp()
    {
        var powerUp = powerUps.Pop() as GameObject; /* OBTIENE OBJETO Y REMUEVE DE LA PILA */
        activePowerUp = powerUp.GetComponent<PowerUp>();
        activePowerUp.transform.localPosition = powerUpActivePosition;
    }

    /* Si hay un power up activado destruye el power up sin aplicarlo a un objeto  */
    public void DeleteActivePowerUp()
    {
        if (PowerUpIsActive())
        {
            Destroy(activePowerUp.gameObject);
            activePowerUp = null;

            UpdateInteractableState(true);
        }
    }

    private bool PowerUpIsActive()
    {
        return activePowerUp != null;
    }

    /* Posiciona el power up en pantalla según la dimensión de la pila */
    private Vector2 GetStackPosition()
    {
        return stackPositions[powerUps.Count - 1];
    }

    /* Recorre la pila desabilitando botones para dejar solo el último ingresado como activo */
    private void UpdateInteractableState(bool interactable)
    {
        if (powerUps.Count != 0)
        {
            var powerUp = powerUps.Peek() as GameObject; /* OBTIENE OBJETO DE LA PILA SIN REMOVER */
            powerUp.GetComponent<Button>().interactable = interactable;
        }
    }
}
