using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naco : MonoBehaviour
{
    private float deltaY;
    private float elapsedTime;
    private float time;
    private bool Up;
    private bool Trans;

    public void Init(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        elapsedTime = 0f;
        deltaY = 0f;
        time = 0f;
        Trans = false;
        Up = false;
    }

    void NacoAttack()
    {
        time += Time.deltaTime;

        if (time >= 1f)
        {
            for (float i = 0f; i <= 360f; i+=40)
            {
                //총알 생성
                GameObject bullet = ObjectPool.Instance.PopFromPool("EBullet");

                bullet.gameObject.GetComponent<Shot>().ShotAngle(i);
                bullet.gameObject.GetComponent<Shot>().GetComponent<Move>().Speed = new Vector2(10f, 10f);
                bullet.gameObject.transform.position = transform.position;
                bullet.gameObject.SetActive(true);
            }
            time = 0f;
        }
    }

    void NacoMove()
    {
        if (Trans == false)
        {
            if (transform.position.x >= 11.8f)
            {
                transform.Translate(new Vector2(-0.08f, 0f));

            }
            else
            {
                Trans = true;
                elapsedTime = 0;
            }
        }
        else
        {
            if(elapsedTime >= 0f)
            {
                Up = true;
            }
            
            if(elapsedTime >= 2f)
            {
                Up = false;
            }

            if(elapsedTime >= 4f)
            {
                elapsedTime = 0f;
            }


            if(Up)
            {
                transform.Translate(new Vector2(0f, 0.1f));
            }
            else
            {
                transform.Translate(new Vector2(0f, -0.1f));
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        NacoMove();

        if (Trans)
            NacoAttack();   
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(gameObject.tag != collision.gameObject.tag)
        {
            if (gameObject.tag != collision.tag)
            {
                gameObject.GetComponent<Health>().Damage(1);
            }
        }
    }

}
