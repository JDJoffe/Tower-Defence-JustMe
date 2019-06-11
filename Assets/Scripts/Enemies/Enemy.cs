using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    public Transform target;

    private NavMeshAgent agent;
    private int health = 0;
    // Use this for initialization
    void Start()
    {
        //health is set to max health
        health = maxHealth;
        //get the navmesh component
        agent = GetComponent<NavMeshAgent>();
    }
    //call to damage enemy
    public void TakeDamage(int damage)
    {
        health -= damage;
            if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
