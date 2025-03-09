using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PokemonMovement : MonoBehaviour
{
    public float walkRadius;
    [SerializeField] NavMeshAgent agent;

    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        StartCoroutine(walk());
    }

    IEnumerator walk()
    {
        FindRandomWalkPoint();

        yield return new WaitForSeconds(Random.Range(1, 5));

        StartCoroutine(walk());
    }

    private void FindRandomWalkPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        Vector3 randomPoint = transform.position + randomDirection;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, walkRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
