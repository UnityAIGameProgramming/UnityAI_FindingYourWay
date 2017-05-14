﻿using UnityEngine;
using System.Collections;

public class AStar
{
    public static PriorityQueue closedList, openList;

    private static float HeuristicEstimateCost(Node curNode, Node goalNode)
    {
        Vector3 vecCost = curNode.position - goalNode.position;

        return vecCost.magnitude;
    }

    public static ArrayList FindPath(Node start, Node goal) // method massively too long!
    {
        openList = new PriorityQueue();
        openList.Push(start);
        start.nodeTotalCost = 0.0f;
        start.estimatedCost = HeuristicEstimateCost(start, goal);

        closedList = new PriorityQueue();
        Node node = null;

        while (openList.Length != 0)
        {
            node = openList.First();

            // Check if the current node is the goal node
            if (node.position == goal.position)
            {
                return CalculatePath(node);
            }

            // Create an ArrayList to store the neighbouring nodes
            ArrayList neighbours = new ArrayList();

            GridManager.instance.GetNeighbours(node, neighbours);

            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = (Node)neighbours[i];

                if (!closedList.Contains(neighbourNode))
                {
                    float cost = HeuristicEstimateCost(node, neighbourNode);

                    float totalCost = node.nodeTotalCost + cost;
                    float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);

                    neighbourNode.nodeTotalCost = totalCost;
                    neighbourNode.parent = node;
                    neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Push(neighbourNode);
                    }
                }
            }

            // Push the current node to the closed list
            closedList.Push(node);

            // remove it from the openList
            openList.Remove(node);
        }

        if (node.position != goal.position)
        {
            Debug.LogError("Goal Not Found");
            return null;
        }
        return CalculatePath(node);
    }

    private static ArrayList CalculatePath(Node node)
    {
        ArrayList list = new ArrayList();

        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }

        list.Reverse();
        return list;
    }
}
