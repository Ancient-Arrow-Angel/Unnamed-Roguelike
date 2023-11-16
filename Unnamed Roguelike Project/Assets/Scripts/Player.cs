using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float MaxHealth = 100;
    public float MaxMana = 100;
    public float Damage = 10;
    public float Speed = 10;
    public float Knockback = 50;
    public float KnockbackResist = 2;

    [Header("Active Stats")]
    public float Health;
    public float Mana;

    public float Coins;


    public int EqippedID = 0;
    public int OtherID = 0;
    int InterID = 0;
    public int PreID = 0;


    [Header("Pre Stuff")]
    public Point Pointer;

    public Volume DamVolume;
    public float IFrames = 1;

    public GameObject OOMUI;
    public GameObject Inventory;
    //public SlotMaker MenuItems;

    public Bar Healthbar;
    public TextMeshProUGUI HealthText;

    public Bar Manabar;
    public TextMeshProUGUI ManaText;


    float CurrentIFrames;

    public bool FacingRight;
    public bool Changed = true;
    public bool InputActive = true;
    public Vector2 KnockbackDir;

    [ColorUsage(true, true)]
    public Color FlashColor = Color.white;
    public float FlashTime = 0.25f;
    SpriteRenderer[] Frames;
    Material[] mats;
    Coroutine DamageFlashCoroutine;

    Vector2 movement;
    Rigidbody2D rb;
    Collider2D Coll;


    void Start()
    {
        Changed = true;
        Frames = GetComponentsInChildren<SpriteRenderer>();
        Init();
        rb = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();

        Health = MaxHealth;
        Healthbar.SetMaxValue((int)MaxHealth);

        Mana = MaxMana;
        Manabar.SetMaxValue((int)MaxMana);

        HealthText.text = Health.ToString() + "%";
        ManaText.text = Mana.ToString() + "%";
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (EqippedID != PreID)
        {
            Changed = true;
        }
        else
        {
            PreID = EqippedID;
        }

        DamVolume.weight -= Time.deltaTime * 3;

        CurrentIFrames -= Time.deltaTime;

        if (CurrentIFrames > 0)
        {
            Coll.enabled = false;
        }
        else
        {
            Coll.enabled = true;
        }

        //if (!Inventory.activeInHierarchy)
        //{
        //    InputActive = true;
        //    Time.timeScale = 1;
        //    movement.x = Input.GetAxisRaw("Horizontal");
        //    movement.y = Input.GetAxisRaw("Vertical");

        //}
        //else
        //{
        //    InputActive = false;
        //    Time.timeScale = 0.1f;
        //}


        if (Input.GetKeyDown(KeyCode.E))
        {
            InterID = EqippedID;
            EqippedID = OtherID;
            OtherID = InterID;
            PreID = OtherID;
            Changed = true;
        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    //OOMUI.SetActive(!OOMUI.active);
        //    Inventory.SetActive(!Inventory.activeInHierarchy);
        //}
    }

    void FixedUpdate()
    {
        KnockbackDir.x = KnockbackDir.x / KnockbackResist;
        KnockbackDir.y = KnockbackDir.y / KnockbackResist;
        rb.MovePosition((rb.position + movement * Speed * Time.fixedDeltaTime) + KnockbackDir * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        Healthbar.SetValue((int)Health);
        CallDamageFlash();
        DamVolume.weight = 1;
        CurrentIFrames = IFrames;

        HealthText.text = Health.ToString() + "%";
    }
    public void UseMana(float UsedMana)
    {
        Mana -= UsedMana;
        if(Mana > MaxMana)
        {
            Mana = MaxMana;
        }

        Manabar.SetValue((int)Mana);

        ManaText.text = Mana.ToString() + "%";
    }


    private void Init()
    {
        mats = new Material[Frames.Length];

        for (int i = 0; i < Frames.Length; i++)
        {
            mats[i] = Frames[i].material;
        }
    }

    public void CallDamageFlash()
    {
        DamageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    IEnumerator DamageFlasher()
    {
        SetFlashColor();

        float CurrentFlashAmount = 0;
        float elapsedTime = 0;
        while (elapsedTime < FlashTime)
        {
            elapsedTime += Time.deltaTime;

            CurrentFlashAmount = Mathf.Lerp(1, 0, (elapsedTime / FlashTime));
            SetFlashAmount(CurrentFlashAmount);
            yield return null;
        }
    }

    private void SetFlashColor()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_FlashColor", FlashColor);
        }
    }

    void SetFlashAmount(float amount)
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat("_FlashAmount", amount);
        }
    }
}