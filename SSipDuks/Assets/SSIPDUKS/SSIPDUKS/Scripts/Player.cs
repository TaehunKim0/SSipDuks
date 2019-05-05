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

    void OnDestroy()
    {
        if(GameOverPanel != null)
        {
            GameOverPanel.GetComponent<CanvasGroup>().alpha = 1;
            GameOverPanel.GetComponent<CanvasGroup>().interactable = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
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
