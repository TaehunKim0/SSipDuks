using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitomi : MonoBehaviour
{
    private float deltaY;
    private float elapsedTime;
    private float time;
    private bool Trans;
    private float i = 0f;
    private float Max =360f;
    private float x = 1f;

    public void Init(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        elapsedTime = 0f;
        deltaY = 0f;
        time = 0f;
        Trans = false;
    }

    void HitomoiAttack()
    {
        if(elapsedTime >= 3f)
        {
            for (i = 0; i <= Max; i += 10)
            {
                //총알 생성
                GameObject bullet = ObjectPool.Instance.PopFromPool("EBullet");

                bullet.gameObject.GetComponent<Shot>().ShotAngle(i * x);
                bullet.gameObject.GetComponent<Shot>().GetComponent<Move>().Speed = new Vector2(10f, 10f);
                bullet.gameObject.transform.position = transform.position;
                bullet.gameObject.SetActive(true);
            }

            x++;
            Max += 10f;

            elapsedTime = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (Trans == false)
        {
            if (gameObject.transform.position.y < -0.1f)
            {
                gameObject.transform.Translate(new Vector2(0f, 0.1f));
            }
            else
            {
                Trans = true;
            }
        }
        else
        {
            HitomoiAttack();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.tag != collision.gameObject.tag)
        {
            if (gameObject.tag != collision.tag)
            {
                gameObject.GetComponent<Health>().Damage(1);
            }
        }
    }
}
