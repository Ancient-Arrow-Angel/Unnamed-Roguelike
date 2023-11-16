using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject Spawn;
    public GameObject Parts;
    public int Num = 1;
    public int PerCoin;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Hitbox")
        {
            if (Spawn != null)
            {
                for (int i = 0; i < Num; i++)
                {
                    GameObject JustSpawned = Instantiate(Spawn, transform.position, transform.rotation);
                    if(PerCoin > 0)
                    {
                        JustSpawned.GetComponent<Coin>().Amount = PerCoin;
                        JustSpawned.name = ("DONE");
                    }
                }
            }
            if(Parts != null)
            {
                Instantiate(Parts, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }
}