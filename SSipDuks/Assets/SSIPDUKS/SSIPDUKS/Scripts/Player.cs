using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform GameOverPanel;

    private Animator m_Animator;
    public Vector2 Speed = new Vector2(30, 30);
    public Vector2 MoveMent;
    private Rigidbody2D myRigid;

    public GameObject LazerPrefab;
    private GameObject Lazer;

    public GameObject BombPrefab;

    private AudioSource audio;
    public AudioClip sound;


    private int Mana = 0;

    public void PlusMana(int ma)
    {
        Mana += ma;
    }
    public void MinusMana(int ma)
    {
        Mana -= ma;
    }
    public int GetMana()
    {
        return Mana;
    }

    void OnDestroy()
    {
        if(GameOverPanel != null)
        {
            GameOverPanel.GetComponent<CanvasGroup>().alpha = 1;
            GameOverPanel.GetComponent<CanvasGroup>().interactable = true;
            GameObject g = GameObject.Find("UI");
            g.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = sound;
        audio.loop = false;
    }

    ~Player()
    {
        OnDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            m_Animator.SetInteger("Direction", -1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_Animator.SetInteger("Direction", 1);
        }
        else
            m_Animator.SetInteger("Direction", 0);

        MoveMent = new Vector2(Speed.x * x, Speed.y * y);

        bool shoot = Input.GetButton("Fire1");

        if (shoot)
        {
            Weapon tempWeapon = GetComponent<Weapon>();
            if (tempWeapon != null)
            {
                tempWeapon.Attack(false);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(GetMana() >= 300)
            {
                MinusMana(300);

                Instantiate(BombPrefab);

            }

            if(GetMana() >= 200)
            {
                MinusMana(200);
                Lazer = Instantiate(LazerPrefab);
                audio.PlayOneShot(sound,1);
            }

            if (GetMana() >= 100)
            {
                Weapon tempWeapon = GetComponent<Weapon>();

                if (tempWeapon != null)
                {
                    tempWeapon.Shotgun();
                    MinusMana(100);

                }

            }

            

            if (Mana < 0)
                Mana = 0;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            PlusMana(100);
        }

        if (Lazer != null)
        {
            Vector3 vec = new Vector3(30f, -0.5f, 0f);
            Lazer.gameObject.transform.position = gameObject.transform.position + vec;
        }

        float dist = (transform.position - Camera.main.transform.position).z;

        float leftborder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightborder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topborder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        float bottomborder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftborder, rightborder)
            , Mathf.Clamp(transform.position.y, bottomborder, topborder)
            , transform.position.z
             );
    }

    private void FixedUpdate()
    {
        myRigid.velocity = MoveMent;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool PlayerDamage = false;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            Health health = enemy.gameObject.GetComponent<Health>();
            if(health != null)
            {
                health.Damage(health.hp);
                PlayerDamage = true;
            }
        }

        if(PlayerDamage)
        {
            Health health = gameObject.GetComponent<Health>();
            if(health != null)
                health.Damage(25);
        }

    }
}
