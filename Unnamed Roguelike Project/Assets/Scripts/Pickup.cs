using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int ID = 0;
    Player player;
    ItemHandle HeldItem;
    bool Set;

    // Start is called before the first frame update
    void Start()
    {
        HeldItem = GameObject.Find("Held Item").GetComponent<ItemHandle>();
        GetComponent<SpriteRenderer>().sprite = HeldItem.Items[ID].icon;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    //private void Update()
    //{
    //    Collider2D checkRadius = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player Entity"));

    //    if (checkRadius && Input.GetKey(KeyCode.F))
    //    {
    //        if (HeldItem.Items[ID].Weapon)
    //        {
    //            if (player.MenuItems.SlotIDs[2] == 0 && !Set)
    //            {
    //                player.MenuItems.SlotIDs[2] = ID;
    //                player.Changed = true;
    //                Set = true;
    //                Destroy(this.gameObject);
    //            }
    //            else if (player.MenuItems.SlotIDs[1] == 0 && !Set)
    //            {
    //                player.MenuItems.SlotIDs[1] = ID;
    //                Set = true;
    //                Destroy(this.gameObject);
    //            }
    //        }
    //        else if (!HeldItem.Items[ID].Weapon)
    //        {
    //            for (int i = 3; i < 8; i++)
    //            {
    //                if (player.MenuItems.SlotIDs[i] == 0 && !Set)
    //                {
    //                    player.MenuItems.SlotIDs[i] = ID;
    //                    Set = true;
    //                    Destroy(this.gameObject);
    //                    return;
    //                }
    //            }
    //        }
    //        for (int i = 8; i < player.MenuItems.SlotIDs.Length; i++)
    //        {
    //            if (player.MenuItems.SlotIDs[i] == 0 && !Set)
    //            {
    //                player.MenuItems.SlotIDs[i] = ID;
    //                Set = true;
    //                Destroy(this.gameObject);
    //                return;
    //            }
    //        }
    //    }
    //}
}
