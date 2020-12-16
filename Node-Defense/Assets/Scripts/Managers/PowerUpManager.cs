using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{

    static public PowerUpManager instance;

    private int STACK_LIMIT;
    private List<Vector2> stackPositions;
    [SerializeField] public Stack powerUps;
    [SerializeField] public PowerUp activePowerUp; // power up seleccionado de pila y activo hasta aplicar en arma o muro
    [SerializeField] public Transform powerUpActivePosition;
    [SerializeField] public List<Transform> positions;

    void Awake()
    {
        instance = this;
        STACK_LIMIT = positions.Count; 
        powerUps = new Stack();
        stackPositions = new List<Vector2>();

        for (int i = 0; i < STACK_LIMIT; i++)
        {
            stackPositions.Add(positions[i].localPosition);
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
        activePowerUp.transform.position = powerUpActivePosition.position;
        activePowerUp.inStack = false;
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

    public void DeActivePowerUp()
    {
        if (PowerUpIsActive())
        {
            activePowerUp = null;

            UpdateInteractableState(true);
        }
    }

    public void DeActiveDestroyPowerUp()
    {
        if (PowerUpIsActive())
        {
            activePowerUp.gameObject.SetActive(false);
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
