using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float persistince;
    public bool PlayerBased = true;
    public bool Neutral = false;
    public float AttackMultyplyer;
    public float HitMana = 0;

    public float NeutralDamage;
    public float NeutralKnockback;

    // Update is called once per frame
    void Update()
    {
        persistince -= Time.deltaTime;

        if(persistince <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Neutral)
        {
            if (col.tag == "Enemy")
            {
                col.GetComponent<EnemyStats>().TakeDamage(NeutralDamage);

                col.GetComponent<EnemyStats>().KnockbackDir = (col.GetComponent<BoxCollider2D>().offset
                    + (Vector2)col.transform.position - (Vector2)transform.position).normalized
                    * NeutralKnockback;
            }
            
            if (col.tag == "Player")
            {
                col.GetComponent<Player>().TakeDamage(NeutralDamage);

                col.GetComponent<Player>().KnockbackDir = (col.GetComponent<BoxCollider2D>().offset
                    + (Vector2)col.transform.position - (Vector2)transform.position).normalized
                    * NeutralKnockback;
            }
        }
        else if (PlayerBased && col.tag == "Enemy")
        {
            col.GetComponent<EnemyStats>().TakeDamage(GameObject.Find("Player").GetComponent<Player>().Damage
                + GameObject.Find("Held Item").GetComponent<ItemHandle>().Items[GameObject.Find("Player").GetComponent<Player>().EqippedID].WeaponDamage
                * AttackMultyplyer);

            col.GetComponent<EnemyStats>().KnockbackDir = (col.GetComponent<BoxCollider2D>().offset
                + (Vector2)col.transform.position - (Vector2)transform.parent.transform.position).normalized * GameObject.Find("Player").GetComponent<Player>().Knockback;

            GameObject.Find("Player").GetComponent<Player>().UseMana(-HitMana);
        }
        else if (!PlayerBased && col.tag == "Player")
        {
            col.GetComponent<Player>().TakeDamage(transform.parent.GetComponent<EnemyStats>().Damage);

            col.GetComponent<Player>().KnockbackDir = (col.GetComponent<BoxCollider2D>().offset
                + (Vector2)col.transform.position - (Vector2)transform.parent.transform.position).normalized
                * transform.parent.GetComponent<EnemyStats>().Knockback;
        }
    }
}