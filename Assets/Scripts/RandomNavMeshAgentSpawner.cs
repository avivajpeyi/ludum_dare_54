using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshAgentSpawner : MonoBehaviour
{
    public NavMeshAgent agentPrefab; // Drag your NavMeshAgent prefab here
    public int numberOfAgents = 10; // Number of agents to spawn
    public Vector3 areaSize = new Vector3(10, 0, 10); // Size of the area within which to spawn agents

    void Start()
    {
        for (int i = 0; i < numberOfAgents; i++)
        {
            SpawnAgentOnRandomNavMeshLocation();
        }
    }

    void SpawnAgentOnRandomNavMeshLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * areaSize.magnitude;
        randomDirection += transform.position; // Starting point for the random location search

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, areaSize.magnitude, NavMesh.AllAreas))
        {
            NavMeshAgent agentInstance = Instantiate(agentPrefab, hit.position, Quaternion.identity);
        }
    }
}