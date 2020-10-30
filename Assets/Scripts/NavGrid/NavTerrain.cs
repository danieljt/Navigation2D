using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
Convert tilemap data to a navigation grid. This component should be added to 
a tilemap. Beware of having many tilemaps as any navgridagents will only search
til they find the first tilemap.
**/
public class NavTerrain : MonoBehaviour
{
    public List<TileBase> walkable;
    public List<TileBase> unWalkable;
    public bool EdgeSmoothing;
    Tilemap tilemap;
    TileBase[] tiles;
    BoundsInt bounds;
    NavGrid grid;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
        tiles = tilemap.GetTilesBlock(bounds);
        grid = new NavGrid(bounds.size.x, bounds.size.y, tilemap.cellSize.x, tilemap.transform.position + tilemap.origin);
        CreateNavGrid();     
    }

	/// <summary>
	/// Create the navegrid
	/// and save
	/// </summary>
    private void CreateNavGrid()
    {
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

        if(EdgeSmoothing)
        {
            grid.CalculateNeighboursNoEdges();
        }

        else
        {
            grid.CalculateNeighbours();
        }
    }

    public NavGrid GetGrid()
    {
        return grid;
    }
}
