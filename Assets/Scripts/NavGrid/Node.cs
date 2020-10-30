using System.Collections.Generic;

public class Node
{
    public int x;
    public int y;
    public NavGrid grid;
    public bool isWalkable;
    public bool allowDiagonalMovement;
    public int gCost;
    public int hCost;
    public int fCost;
    public List<Node> neighbours;
    public Node previousNode;

    public Node(NavGrid grid, int x, int y)
    {
        this.x = x;
        this.y = y;
        this.grid = grid;
        neighbours = new List<Node>();
    }

    // Calculate this nodes 8 neighbour nodes in the
    // grid. 
    public void CalculateNeighbours()
    {    
        if(x + 1 < grid.width)
        {
            // Middle right neighbour
            if(grid.GetNode(x+1,y) != null)
            {
                neighbours.Add(grid.GetNode(x+1,y));
            }

            // Top right neighbour
            if(y + 1 < grid.height)
            {
                if(grid.GetNode(x+1, y+1) != null)
                {
                    neighbours.Add(grid.GetNode(x+1,y+1));
                }
            }

            // Bottom right neighbour
            if(y - 1 >= 0)
            {
                if(grid.GetNode(x+1, y-1) != null)
                {
                    neighbours.Add(grid.GetNode(x+1,y-1));
                }
            }
        }

        if(x - 1 >= 0)
        {
            // middle left neigbour
            if(grid.GetNode(x-1,y) != null)
            {
                neighbours.Add(grid.GetNode(x-1,y));
            }

            // Top left neighbour
            if(y + 1 < grid.height)
            {
                if(grid.GetNode(x-1,y+1) != null)
                {
                    neighbours.Add(grid.GetNode(x-1,y+1));
                }
            }

            // Bottom left neighbour
            if(y - 1 >= 0)
            {
                if(grid.GetNode(x-1,y-1) != null)
                {
                    neighbours.Add(grid.GetNode(x-1,y-1));
                }
            }
        }

        // Top middle
        if(y + 1 < grid.height)
        {
            if(grid.GetNode(x,y+1) != null)
            {
                neighbours.Add(grid.GetNode(x,y+1));
            }
        }

        // Bottom middle
        if(y - 1 >= 0)
        {
            if(grid.GetNode(x,y-1) != null)
            {
                neighbours.Add(grid.GetNode(x,y-1));
            }
        }
    }

    public void CalculateNeighboursNoEdges()
    {
        Node topNode;
        Node bottomNode;
        Node leftNode;
        Node rightNode;
        bool top = false;
        bool bottom = false;
        bool left = false;
        bool right = false;

        if(y + 1 < grid.height)
        {
            top = true;
            if(grid.GetNode(x,y+1) != null)
            {
                topNode = grid.GetNode(x,y+1);
                neighbours.Add(grid.GetNode(x,y+1));
            }
        }

        if(y - 1 >= 0)
        {
            bottom = true;
            if(grid.GetNode(x,y-1) != null)
            {
                bottomNode = grid.GetNode(x,y-1);
                neighbours.Add(grid.GetNode(x,y-1));
            }
        }

        if(x + 1 < grid.width)
        {
            right = true;
            if(grid.GetNode(x+1,y) != null)
            {
                rightNode = grid.GetNode(x+1,y);
                neighbours.Add(grid.GetNode(x+1,y));
            }
        }

        if(x - 1 >= 0)
        {
            left = true;
            if(grid.GetNode(x-1,y) != null)
            {
                leftNode = grid.GetNode(x-1,y);
                neighbours.Add(grid.GetNode(x-1,y));
            }
        }

        if(top && left)
        {
            if(grid.GetNode(x-1,y) != null && grid.GetNode(x,y+1) != null)
            {
                if(grid.GetNode(x-1,y+1) != null)
                {
                    neighbours.Add(grid.GetNode(x-1,y+1));
                }
            }
        }

        if(top && right)
        {
            if(grid.GetNode(x+1,y) != null && grid.GetNode(x,y+1) != null)
            {
                if(grid.GetNode(x+1,y+1) != null)
                {
                    neighbours.Add(grid.GetNode(x+1,y+1));
                }
            }
        }

        if(bottom && left)
        {
            if(grid.GetNode(x-1,y) != null && grid.GetNode(x,y-1) != null)
            {
                if(grid.GetNode(x-1,y-1) != null)
                {
                    neighbours.Add(grid.GetNode(x-1,y-1));
                }
            }
        }

        if(bottom && right)
        {
            if(grid.GetNode(x+1,y) != null && grid.GetNode(x,y-1) != null)
            {
                if(grid.GetNode(x+1,y-1) != null)
                {
                    neighbours.Add(grid.GetNode(x+1,y-1));
                }
            }
        }
    }

    public List<Node> GetNeighbours()
    {
        return neighbours;
    }

    public int CalculateFCost()
    {
        fCost =  gCost + hCost;
        return fCost;
    }

    #region Helper Methods
    public override string ToString()
    {
        return x + "   " + y;
    } 
    #endregion
}
