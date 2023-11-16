using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SlotMaker : MonoBehaviour
{
    public ItemHandle IHS;
    public GameObject Icon;
    public GameObject WeaponIcon;

    public GameObject Slot;
    public GameObject WeaponSlot;
    public GameObject ArmorSlot;

    public Transform SlotKeep;

    public Image CursorSprite;
    public int CursorID;

    public int InterID;

    public int[] SlotIDs;
    public Transform[] Slots;

    public int XSlots;
    public int YSlots;

    public int XSlotsGap;
    public int YSlotsGap;

    public float SlotSize;
    public float WeaponSlotSize;

    Vector2 Pos;

    void Start()
    {
        SlotIDs = new int[XSlots * YSlots + 3];
        Slots = new Transform[XSlots * YSlots + 2];

        Pos = new Vector2(-XSlotsGap * 1f, YSlotsGap * 1.5f);
        Instantiate(WeaponIcon, Pos + (Vector2)transform.position, transform.rotation, transform);
        Instantiate(WeaponSlot, Pos + (Vector2)transform.position, transform.rotation, SlotKeep);
        Pos = new Vector2(-XSlotsGap * 3f, YSlotsGap * 1.5f);
        Instantiate(WeaponIcon, Pos + (Vector2)transform.position, transform.rotation, transform);
        Instantiate(WeaponSlot, Pos + (Vector2)transform.position, transform.rotation, SlotKeep);

        for (int i = 0; i < XSlots * YSlots; i++)
        {
            Pos = new Vector2(-i % XSlots * XSlotsGap, -i / YSlots * YSlotsGap);

            Instantiate(Icon, Pos + (Vector2)transform.position, transform.rotation, transform);
            if(i > 4)
            {
                Instantiate(Slot, Pos + (Vector2)transform.position, transform.rotation, SlotKeep);
            }
            else
            {
                Instantiate(ArmorSlot, Pos + (Vector2)transform.position, transform.rotation, SlotKeep);
            }
        }

        Slots = GetComponentsInChildren<Transform>();

        UpdateInventor();
    }

    private void Update()
    {
        CursorSprite.sprite = IHS.Items[CursorID].icon;
        if(CursorID == 0)
        {
            CursorSprite.color = new Color(255,255,255,0);
        }
        else
        {
            CursorSprite.color = new Color(255, 255, 255, 255);
        }
        UpdateInventor();

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            for (int i = 3; i < XSlots * YSlots + 3; i++)
            {
                if (mousePos.x <= Slots[i].transform.position.x + SlotSize &&
                    mousePos.x >= Slots[i].transform.position.x - SlotSize &&
                    mousePos.y <= Slots[i].transform.position.y + SlotSize &&
                    mousePos.y >= Slots[i].transform.position.y - SlotSize)
                {
                    if(CursorID != 0)
                    {
                        if(i > 7)
                        {
                            InterID = CursorID;
                            CursorID = SlotIDs[i];
                            SlotIDs[i] = InterID;
                        }
                        else if (!IHS.Items[CursorID].Weapon)
                        {
                            InterID = CursorID;
                            CursorID = SlotIDs[i];
                            SlotIDs[i] = InterID;
                        }
                    }
                    else
                    {
                        InterID = CursorID;
                        CursorID = SlotIDs[i];
                        SlotIDs[i] = InterID;
                    }

                    return;
                }
            }
            for (int i = 1; i < 3; i++)
            {
                if (mousePos.x <= Slots[i].transform.position.x + WeaponSlotSize &&
                    mousePos.x >= Slots[i].transform.position.x - WeaponSlotSize &&
                    mousePos.y <= Slots[i].transform.position.y + WeaponSlotSize &&
                    mousePos.y >= Slots[i].transform.position.y - WeaponSlotSize)
                {
                    if (CursorID != 0)
                    {
                        if (IHS.Items[CursorID].Weapon)
                        {
                            InterID = CursorID;
                            CursorID = SlotIDs[i];
                            SlotIDs[i] = InterID;
                        }
                    }
                    else
                    {
                        InterID = CursorID;
                        CursorID = SlotIDs[i];
                        SlotIDs[i] = InterID;
                    }

                    return;
                }
            }
        }
    }

    public void UpdateInventor()
    {
        for (int i = 1; i < Slots.Length; i++)
        {
            Slots[i].GetComponent<Image>().sprite = IHS.Items[SlotIDs[i]].icon;
            if (SlotIDs[i] == 0)
            {
                Slots[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }
            else
            {
                Slots[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
        }
    }
}