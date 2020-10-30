using UnityEngine;

/**
Grid class to hold information about generic nodes
**/
public class NavGrid
{
    public int width;
    public int height;
    public float cellSize;
    public Vector3 position;
    Node[,] grid;

    public NavGrid(int width, int height, float cellSize, Vector3 position)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.position = position;
        grid = new Node[width,height];
    }

    public void CalculateNeighbours()
    {
        for(int i=0; i<width; i++)
        {
            for(int j=0; j<height; j++)
            {
                if(GetNode(i,j) != null)
                {
                    GetNode(i,j).CalculateNeighbours();
                }
            }
        }
    }

    public void CalculateNeighboursNoEdges()
    {
        for(int i=0; i<width; i++)
        {
            for(int j=0; j<height; j++)
            {
                if(GetNode(i,j) != null)
                {
                    GetNode(i,j).CalculateNeighboursNoEdges();
                }
            }
        }
    }

    public void AddNode(Node node, int x, int y)
    {
        if(x >= 0 && x < width && y >= 0 && y < height)
        {
            grid[x,y] = node;
        }
    }

    public Node GetNode(int x, int y)
    {
        if(x >= 0 && x < width && y >= 0 && y < height)
        {
            return grid[x,y];
        }
        return default(Node);
    } 

    public void WorldToGridCoordinates(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.x - position.x)/cellSize);
        y = Mathf.FloorToInt((worldPosition.y - position.y)/cellSize);
    }
    
    public Vector3 GridToWorldCoordinates(int x, int y)
    {
        return new Vector3(x,y)*cellSize + position;
    }
   
    public float GetCellSize()
    {
        return cellSize;
    }
    
    #region Helper Methods
    public override string ToString()
    {
        string outString = "";
        outString += "\n";

        for(int i=0; i<width; i++)
        {
            for(int j=0; j<height; j++)
            {
                
                if(GetNode(i,j) != null)
                {
                    outString += "IIIIIIIIII ";
                }
                else
                {
                    outString += "XXXX ";
                }
                
                
            }
            outString += "\n";
        }

        return outString;
    }
    #endregion
}
