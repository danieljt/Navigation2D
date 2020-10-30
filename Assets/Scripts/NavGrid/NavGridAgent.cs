using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NavGridAgent : MonoBehaviour
{
    NavGrid navGrid;
    AStar aStar;
    List<Vector3> path;

    private void Start()
    {
        SetNavGrid();
        aStar = new AStar(navGrid);
    }

    private void SetNavGrid()
    {
        Tilemap tilemap = FindObjectOfType<Tilemap>();
        if(tilemap != null)
        {
            NavTerrain navterrain = tilemap.GetComponent<NavTerrain>();
            if(navterrain != null)
            {
                navGrid = navterrain.GetGrid();
            }
        }
    }

    public List<Vector3> SetDestination(Vector3 target)
    {
        return aStar.FindPath(transform.position, target);
    }
}
