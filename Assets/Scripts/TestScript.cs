using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This is a script aimed at testing the A* pathfinding system
/// </summary>
public class TestScript : MonoBehaviour
{
    Tilemap tilemap;
    TileBase[] tiles;
    NavGrid grid;

    public List<TileBase> walkable;
    public List<TileBase> unWalkable;
    private AStar pathFinder;
    List<Vector3> path;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] tiles = tilemap.GetTilesBlock(bounds);

        grid = new NavGrid(bounds.size.x, bounds.size.y, tilemap.cellSize.x, tilemap.origin);

        for(int x=0; x < bounds.size.x; x++)
        {
            for(int y=0; y < bounds.size.y; y++)
            {
                TileBase tile = tiles[x + y*bounds.size.x];
                if(tile == null || walkable.Contains(tile))
                {
                    grid.AddNode(new Node(grid, x, y), x, y);
                }
            }
        }

        grid.CalculateNeighbours();

        pathFinder = new AStar(grid);
        Vector3 start = new Vector3(-1.5f, -3.5f, 0);
        Vector3 end = new Vector3(4.5f, -0.5f, 0);
        path = pathFinder.FindPath(start, end);
    }
}
