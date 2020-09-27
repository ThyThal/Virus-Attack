using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNode : MonoBehaviour, IGameNode
{
    [SerializeField] private int treePosition;
    [SerializeField] private PowerUp powerUp;
    [SerializeField] int Type;
    [SerializeField] int life;
    [SerializeField] List<Virus> virus;

    public int TreePosition
    {
        get
        {
            return treePosition;
        }

        set
        {
            treePosition = value;
        }
    }

    public PowerUp PowerUp
    {
        get
        {
            return powerUp;
        }

        set
        {
            powerUp = value;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
