using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomNavMeshMover : MonoBehaviour
{
    private NavMeshAgent agent;
    public float range = 5f; // Range within which to move
    public float waitTime = 2f; // Time to wait at each destination before moving again

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToRandomDestination();
    }

    private void Update()
    {
        // Check if the agent has reached its destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Invoke("MoveToRandomDestination", waitTime); // Wait for 'waitTime' seconds before moving again
        }
    }

    private void MoveToRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
