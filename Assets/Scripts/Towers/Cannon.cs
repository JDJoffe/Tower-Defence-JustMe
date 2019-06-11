using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Cannon : Tower
{
    public Transform orb;
    public float lineDelay = 0.2f;
    public LineRenderer line;

     void Reset()
    {
        line = GetComponent<LineRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        //if current enemy is null
        if (currentEnemy == null)
        {
            //disable line
           // line.enabled = false;
        }
    }
    //delay after damaging
    IEnumerator DisableLine()
    {
        yield return new WaitForSeconds(lineDelay);
        line.enabled = false;
    }
    public override void Aim(Enemy e)
    {
        //get orb to look at enemy
        orb.LookAt(e.transform);
        //create line orb from enemy
        line.SetPosition(0, orb.position);
        line.SetPosition(1, e.transform.position);
    }
    public override void Attack(Enemy e)
    {
        //enable line
        line.enabled = true;
        //deal damage to enemy
        e.TakeDamage(damage);
        //run and disable line on a delay
        StartCoroutine(DisableLine());
    }
}
