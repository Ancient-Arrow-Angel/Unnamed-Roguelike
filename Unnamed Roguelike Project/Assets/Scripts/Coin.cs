using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Amount;
    public GameObject CollectPart;
    public GameObject Jumper;
    public float Speed = 1;
    public float Top = 2;

    bool TopReached = false;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)).normalized * Random.Range(-7, 7);
    }

    private void Update()
    {
        if (!TopReached)
        {
            Jumper.transform.localPosition = new Vector2(0, Jumper.transform.localPosition.y + Time.deltaTime * Speed);
            if (Jumper.transform.localPosition.y >= Top)
            {
                TopReached = true;
            }
        }
        else if (Jumper.transform.localPosition.y > 0 && TopReached)
        {
            Jumper.transform.localPosition = new Vector2(0, Jumper.transform.localPosition.y - Time.deltaTime * Speed);
        }
        else
        {
            Jumper.transform.localPosition = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Player>().Coins += Amount;
            Instantiate(CollectPart, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}