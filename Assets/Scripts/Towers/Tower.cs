using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public int damage = 10;
    public float attackDelay = 1f;
    public float attackRange = 2f;
    protected Enemy currentEnemy;

    private float attackTimer = 0f;


    void OnDrawGizmosSelected()
    {
        // Draw the attack sphere around Tower
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public virtual void Aim(Enemy e)
    {
        print("I am aiming at '" + e.name + "'");
    }
    // Attacks at a given enemy only when 'attacking'
    public virtual void Attack(Enemy e)
    {
        print("I am attacking '" + e.name + "'");
    }

    void DetectEnemies()
    {
        //reset currentEnemy
        currentEnemy = null;

        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();

            if( enemy)
            {
                currentEnemy = enemy;
            }
        }
    }
    //protected accessable to other child scripts
    //virtual able to change what this function does in derived classes
    protected virtual void Update()
    {
        DetectEnemies();
        // count up the timer
        attackTimer += Time.deltaTime;
        //if there is an enemy
        if (currentEnemy)
        {
            //aim at the enemy 
            Aim(currentEnemy);

            if (attackTimer >= attackDelay)
            {
                //attack
                Attack(currentEnemy);
                attackTimer = 0f;
            }
        }
    }
}
