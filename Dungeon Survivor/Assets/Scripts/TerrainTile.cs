using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] private Vector2Int tilePosition;

    void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);
    }
}
