using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Rendering;
using UnityEngine;

public class Slime : MonoBehaviour
{
    EnemyStats Stats;
    bool EnemyNear = false;
    public LayerMask EnemyMask;

    // Start is called before the first frame update
    void Start()
    {
        Stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //EnemyNear = (Physics2D.OverlapCircle(transform.position, 1.5f, EnemyMask));

        //if (EnemyNear)
        //{
        //    Stats.movement = GameObject.Find("Player").transform.position - transform.position;

        //}
        //else
        //{
        //    Stats.movement = GameObject.Find("Player").transform.position - transform.position;
        //}
        Stats.movement = GameObject.Find("Player").transform.position - transform.position;
        Stats.movement.Normalize();
    }
}