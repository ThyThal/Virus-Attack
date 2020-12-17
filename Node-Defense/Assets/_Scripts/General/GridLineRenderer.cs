using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLineRenderer : MonoBehaviour
{
    [Header("Grid Info.")]
    [SerializeField] private Transform _gridTransform;
    [SerializeField] private Vector3 _gridPosition;

    [Header("Line Renderer Info.")]
    [SerializeField] private GameObject _lineRendererHolder;
    [SerializeField] private GameObject _lineEdge;
    [SerializeField] private float _lineRendererScale;


    private void Awake()
    {
        _gridPosition = Camera.main.ScreenToWorldPoint(_gridTransform.position);
        InstantiateHolder();
    }

    private void InstantiateHolder()
    {
        var nodeEdge = Instantiate(_lineRendererHolder, this.transform);
        nodeEdge.transform.position = _gridPosition;
    }

}
