using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    void Start()
    {
        if(GameObject.Find("Player").GetComponent<Player>().FacingRight)
            GetComponent<Rigidbody2D>().velocity = transform.right * Speed;
        else
            GetComponent<Rigidbody2D>().velocity = -transform.right * Speed;
    }
}