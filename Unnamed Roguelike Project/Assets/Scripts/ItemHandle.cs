using UnityEngine;

public class ItemHandle : MonoBehaviour
{
    public Item[] Items;
    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = Items[player.EqippedID].icon;

        if (player.Changed)
        {
            player.Speed -= Items[player.PreID].WeaponSpeed;
            player.Knockback -= Items[player.PreID].WeaponKnockback;
            player.KnockbackResist -= Items[player.PreID].WeaponKnockbackResist;

            player.Speed += Items[player.EqippedID].WeaponSpeed;
            player.Knockback += Items[player.EqippedID].WeaponKnockback;
            player.KnockbackResist += Items[player.EqippedID].WeaponKnockbackResist;
        }

        if (Input.anyKeyDown/* && !player.MenuItems.isActiveAndEnabled*/)
        {
            for (int i = 0; i < Items[player.EqippedID].Skills.Length; i++)
            {
                GameObject JustSpawned = null;

                if (Input.GetKeyDown(Items[player.EqippedID].Skills[i].Activate) && player.Mana >= Items[player.EqippedID].Skills[i].ManaUse)
                {
                    player.UseMana(Items[player.EqippedID].Skills[i].ManaUse);

                    if (Items[player.EqippedID].Skills[i].Centered)
                    {
                        JustSpawned = Instantiate(Items[player.EqippedID].Skills[i].Attack, player.transform.position, transform.rotation);
                    }
                    else
                    {
                        if (player.FacingRight)
                        {
                            JustSpawned = Instantiate(Items[player.EqippedID].Skills[i].Attack, transform.position, transform.parent.transform.rotation);
                        }
                        else if (player.FacingRight == false)
                        {
                            JustSpawned = Instantiate(Items[player.EqippedID].Skills[i].Attack, transform.position, transform.parent.transform.rotation);
                            JustSpawned.transform.localScale = new Vector3(-1, 1, 1);
                        }
                    }

                    if(JustSpawned.GetComponentInChildren<Hitbox>() != null)
                    {
                        JustSpawned.GetComponentInChildren<Hitbox>().AttackMultyplyer = Items[player.EqippedID].Skills[i].DamMultiplyer;
                        JustSpawned.GetComponentInChildren<Hitbox>().HitMana = Items[player.EqippedID].Skills[i].HitMana;
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite icon;

    [Header("Weapon Stats")]
    public bool Weapon = true;
    public float WeaponDamage;
    public float WeaponMana;
    public float WeaponSpeed;
    public float WeaponKnockback;
    public float WeaponKnockbackResist;

    [Header("Accsesory Stats")]
    public float ArmorDamage;
    public float ArmorSpeed;
    public float ArmorKnockback;
    public float ArmorKnockbackResist;

    public Skill[] Skills;
}

[System.Serializable]
public class Skill
{
    public string name;
    public string description;
    public KeyCode Activate;
    public GameObject Attack;
    public float DamMultiplyer = 1;
    public float ManaUse = 0;
    public float HitMana = 0;
    public bool Centered;
}