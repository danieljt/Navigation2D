using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Class for the Astar algorithm in a grid based level. This is a dirty and easy implementation, subject to great 
/// efficiency upgrades.
///
/// TODO
/// - Bake the grid in an asset so it can be used by multiple agents instead of copying itself to each agent.
/// - Put the costs in the grid itself so we don't have to do the calculations here
///</summary>
public class AStar
{
    private int moveStraightCost;
    private int moveDiagonalCost;

    private NavGrid grid;
    private List<Node> closedList;
    private List<Node> openList;

    public AStar(NavGrid grid)
    {
        this.grid = grid;
        moveStraightCost = 10;
        moveDiagonalCost = 14;
    }

    ///<summary>
    /// Find the path between the start and end locations in worls space
    /// Method converts to grid coordinates
    ///</summary>
    public List<Vector3> FindPath(Vector3 startposition, Vector3 endPosition)
    {
        grid.WorldToGridCoordinates(startposition, out int xStart, out int yStart);
        grid.WorldToGridCoordinates(endPosition, out int xEnd, out int yEnd);
        List<Node> path = FindPath(xStart, yStart, xEnd, yEnd);
        if(path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorList = new List<Vector3>();
            foreach(Node node in path)
            {
                vectorList.Add(grid.GridToWorldCoordinates(node.x, node.y) + new Vector3(0.5f,0.5f,0));
            } 

            return vectorList;
        }
    }

    ///<summary>
    /// Finds the path between the start and the end grid position. Uses the A* pathfinding algorithm
    /// If no path is found, this method returns null.
    ///</summary>
    public List<Node> FindPath(int xStart, int yStart, int xEnd, int yEnd)
    {
        Node startNode = grid.GetNode(xStart, yStart);
        Node endNode = grid.GetNode(xEnd, yEnd);

        if(startNode == null || endNode == null)
        {
            return null;
        }
        
        // Open and closed lists keep track of the nodes that can and cannot be traversed by the algorithm
        // 
        openList = new List<Node>(){startNode};
        closedList = new List<Node>();

        for(int i=0; i<grid.width; i++)
        {
            for(int j=0; j<grid.height; j++)
            {
                Node currentNode = grid.GetNode(i,j);
                if(currentNode != null)
                {
                    currentNode.gCost = int.MaxValue;
                    currentNode.CalculateFCost();
                    currentNode.previousNode = null;
                }
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        // Run the algorithm itself
        while(openList.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openList);

            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(Node neighbourNode in currentNode.GetNeighbours())
            {
                if(closedList.Contains(neighbourNode))
                {
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);

                if(tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.previousNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if(!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        return null;
    }

    ///<summary>
    /// Add all nodes to the path. The path is reversed at the end
    ///</summary>
    private List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currentNode = endNode;
        while(currentNode.previousNode != null)
        {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }
        path.Reverse();
        return path;
    }

    ///<summary>
    /// Get the node with the lowest F cost and return it
    ///</summary>
    private Node GetLowestFCostNode(List<Node> nodeList)
    {
        Node lowestFCostNode = nodeList[0];
        for(int i = 1; i<nodeList.Count; i++)
        {
            if(nodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = nodeList[i];
            }
        }
        return lowestFCostNode;
    }

    ///<summary>
    /// Calculate the distance move cost between the start and the end node.
    ///</summary>
    private int CalculateDistanceCost(Node start, Node end)
    {
        int xDistance = Mathf.Abs(start.x - end.x);
        int yDistance = Mathf.Abs(start.y - end.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return moveDiagonalCost * Mathf.Min(xDistance,yDistance) + moveStraightCost*remaining;
    }

    #region Helper Methods
    public NavGrid GetGrid()
    {
        return grid;
    }
    #endregion
}
