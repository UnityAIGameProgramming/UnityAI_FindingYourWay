using UnityEngine;
        navAgents = FindObjectsOfType(typeof(UnityEngine.AI.NavMeshAgent)) as UnityEngine.AI.NavMeshAgent[];
        foreach (UnityEngine.AI.NavMeshAgent agent in navAgents)
        {
            agent.destination = targetPosition;
        }

        //Get the point of the hit position when the mouse is being clicked
        if (Input.GetMouseButtonDown(button))
        {
            {