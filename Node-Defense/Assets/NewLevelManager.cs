using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelManager : MonoBehaviour
{
    [SerializeField] public NewLevelManager Instance;
    [SerializeField] private Transform nodesParent;
    [SerializeField] private List<GameObject> nodesPrefabs;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Start()
    {
        
    }

    private void CreateInternet()
    {

    }

    private void CreateNode()
    {

    }

    private void CreateServer()
    {

    }
}
