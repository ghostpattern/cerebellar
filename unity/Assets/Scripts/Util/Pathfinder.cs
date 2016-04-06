using System.Collections.Generic;
using UnityEngine;

public class PathInfo
{
    public float Cost;
    public IPathNode Parent;
    public List<IPathNode> ConnectedNodes { get; set; }
}

public interface IPathNode
{
    PathInfo PathInfo { get; }
    float CalculatePathCost(IPathNode pathNode);
}

public class Pathfinder
{
    public static List<IPathNode> FindPath(IPathNode startNode, IPathNode endNode, List<IPathNode> allNodes)
    {
        foreach(IPathNode node in allNodes)
        {
            node.PathInfo.Cost = Mathf.Infinity;
            node.PathInfo.Parent = null;
        }

        startNode.PathInfo.Cost = 0.0f;
        List<IPathNode> openNodes = new List<IPathNode> { startNode };
        List<IPathNode> closedNodes = new List<IPathNode>();


        while(openNodes.Count > 0 && openNodes[0] != endNode)
        {
            IPathNode currNode = openNodes[0];

            openNodes.RemoveAt(0);
            closedNodes.Add(currNode);

            foreach(IPathNode connectedNode in currNode.PathInfo.ConnectedNodes)
            {
                float cost = currNode.PathInfo.Cost + connectedNode.CalculatePathCost(currNode);
                if(openNodes.Contains(connectedNode) && cost < connectedNode.PathInfo.Cost)
                {
                    openNodes.Remove(connectedNode);
                }

                if(closedNodes.Contains(connectedNode) && cost < connectedNode.PathInfo.Cost)
                {
                    closedNodes.Remove(connectedNode);
                }

                if(openNodes.Contains(connectedNode) == false && closedNodes.Contains(connectedNode) == false)
                {
                    connectedNode.PathInfo.Cost = cost;

                    bool inserted = false;
                    for(int i = 0; i < openNodes.Count; i++)
                    {
                        IPathNode openNode = openNodes[i];
                        if(openNode.PathInfo.Cost > cost)
                        {
                            openNodes.Insert(i, connectedNode);
                            inserted = true;
                            break;
                        }
                    }

                    if(inserted == false)
                    {
                        openNodes.Add(connectedNode);
                    }

                    connectedNode.PathInfo.Parent = currNode;
                }
            }
        }

        IPathNode traceNode = endNode;
        List<IPathNode> path = new List<IPathNode>();

        while(traceNode != startNode)
        {
            path.Insert(0, traceNode);
            traceNode = traceNode.PathInfo.Parent;
        }

        return path;
    }
}