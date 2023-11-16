using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    public float MaxHealth = 100;
    public float Damage = 10;
    public float Speed = 4;
    public float Knockback = 50;
    public float KnockbackResist = 2;
    public int DroppedCoins = 0;
    public int PerCoin = 0;

    [Header("Active Stats")]
    public float Health;

    [Header("Pre Stuff")]
    public Bar Healthbar;
    public GameObject HitPart;
    public GameObject GoldCoin;

    [ColorUsage(true, true)]
    public Color FlashColor = Color.white;
    public float FlashTime = 0.25f;
    SpriteRenderer[] Frames;
    Material[] mats;
    Coroutine DamageFlashCoroutine;


    public Vector2 KnockbackDir;
    public Vector2 movement;

    Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        Frames = GetComponentsInChildren<SpriteRenderer>();
        Init();
        rb = GetComponent<Rigidbody2D>();
        Health = MaxHealth;
        Healthbar.SetMaxValue((int)MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        if(Health <= 0)
        {
            Instantiate(HitPart, transform.position, transform.rotation);
            Instantiate(HitPart, transform.position, transform.rotation);
            for (int i = 0; i < DroppedCoins; ++i)
            {
                Instantiate(GoldCoin, transform.position, transform.rotation);
                GameObject.Find("Gold Coin(Clone)").GetComponent<Coin>().Amount = PerCoin;
                GameObject.Find("Gold Coin(Clone)").name = "DONE";
            }

            Destroy(this.gameObject);
        }
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
        Healthbar.SetValue((int)Health);
        Instantiate(HitPart, transform.position, transform.rotation);
        CallDamageFlash();
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
        for (int i = 0;i < mats.Length; i++)
        {
            mats[i].SetColor("_FlashColor", FlashColor);
        }
    }

    void SetFlashAmount(float amount)
    {
       for(int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat("_FlashAmount", amount);
        }
    }
}