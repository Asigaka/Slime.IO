using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SlimeStomach), typeof(NavMeshAgent))]
public class SlimeAI : MonoBehaviour
{
    [SerializeField] private float checkRadius = 40;
    [SerializeField] private float randomPositionOffset = 10;

    private NavMeshAgent agent;
    private SlimeStomach stomach;
    [SerializeField] private FoodObject foodTarget;
    [SerializeField] private SlimeStomach slimeTarget;
    [SerializeField] private Vector3 randomTarget;
    [SerializeField] private bool walkRandom;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stomach = GetComponent<SlimeStomach>();
    }

    private void Update()
    {
        CheckSphereCast();
        PursueTarget();
    }

    private void PursueTarget()
    {
        if (slimeTarget)
        {
            agent.SetDestination(slimeTarget.transform.position);
            walkRandom = false;
        }
        else if (foodTarget)
        {
            agent.SetDestination(foodTarget.transform.position);
            walkRandom = false;
        }
        else
        {
            if (walkRandom)
            {
                if (agent.remainingDistance <= 0)
                {
                    agent.SetDestination(GetRandomOnMap());
                    walkRandom = true;
                }
            }
            else
            {
                agent.SetDestination(GetRandomOnMap());
                walkRandom = true;
            }
        }
    }

    private void CheckSphereCast()
    {
        if (foodTarget == null || slimeTarget == null)
        {
            Collider[] collidersAround = Physics.OverlapSphere(transform.position, checkRadius);

            foodTarget = null;
            slimeTarget = null;

            List<Collider> colliderList = collidersAround.ToList();
            if (colliderList.Count > 0)
            {
                colliderList.Sort(delegate (Collider t1, Collider t2)
                {
                    return Vector3.Distance(t1.transform.position, transform.position).CompareTo(Vector3.Distance(t2.transform.position, transform.position));
                });

                for (int i = 0; i < colliderList.Count; i++)
                {
                    FoodObject food = colliderList[i].GetComponent<FoodObject>();
                    SlimeStomach enemy = colliderList[i].GetComponent<SlimeStomach>();

                    if (enemy && enemy.StomachFullness < stomach.StomachFullness)
                    {
                        slimeTarget = enemy;
                        return;
                    }
                    else if (food && food.Info.StomachFullness <= stomach.StomachFullness)
                    {
                        foodTarget = food;
                        return;
                    }
                }
            }
        }
    }

    private Vector3 GetRandomOnMap()
    {
        float xOffset = Random.Range(-randomPositionOffset, randomPositionOffset);
        float zOffset = Random.Range(-randomPositionOffset, randomPositionOffset);
        randomTarget = new Vector3(transform.position.x + xOffset, 0, transform.position.z + zOffset);
        return randomTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
