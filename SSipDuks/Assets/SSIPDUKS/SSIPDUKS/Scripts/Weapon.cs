using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shotPrefab;
    public int ShootingRate;

    private int CoolTime = 100;
    private string bulletName;

    // Update is called once per frame
    void Update()
    {
        CoolTime -= 1;
        if (CoolTime < 0)
            CoolTime = 0;
    }

    public bool CanAttack()
    {
        if (CoolTime <= 0)
            return true;

        return false;
    }

    public void Shotgun()
    {
        Vector3 vec = new Vector3(5f, -0.5f, 0f);
        bulletName = "Bullet";

        //// 추적 총알 취소
        for (float i = 0.5f; i >= -0.5f; i -= 0.1f)
        {
            //총알 생성
            GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);

            bullet.gameObject.GetComponent<Shot>().ShotAngle(i);
            bullet.gameObject.transform.position = transform.position + vec;
            bullet.gameObject.SetActive(true);
        }

        ////총알 생성
        //GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);

        //bullet.gameObject.GetComponent<Shot>().ShotAngle(0.5f);
        //bullet.gameObject.transform.position = transform.position + vec;
        //bullet.gameObject.SetActive(true);


        //GameObject bullet2 = ObjectPool.Instance.PopFromPool(bulletName);

        //bullet2.gameObject.GetComponent<Shot>().ShotAngle(0);
        //bullet2.gameObject.transform.position = transform.position + vec;
        //bullet2.gameObject.SetActive(true);


        //GameObject bulletd = ObjectPool.Instance.PopFromPool(bulletName);

        //bulletd.gameObject.GetComponent<Shot>().ShotAngle(-0.5f);
        //bulletd.gameObject.transform.position = transform.position + vec;
        //bulletd.gameObject.SetActive(true);


        //총알 발사 이펙트
        GameObject temp = ObjectPool.Instance.PopFromPool("BulletShot");
        temp.SetActive(true);
        temp.transform.position = gameObject.transform.position + vec;
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack())
        {
            if (gameObject.GetComponent<Health>().isEnemy == false)
                bulletName = "Bullet";
            else
                bulletName = "EBullet";

            //적이 아니면
            if (gameObject.GetComponent<Health>().isEnemy == false)
            {
                Vector3 vec = new Vector3(5f, -0.5f, 0f);

                //총알 생성
                GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);

                //추적 총알 취소
                bullet.gameObject.GetComponent<Shot>().SetShotToPlayer(false);
                bullet.gameObject.transform.position = transform.position + vec;
                bullet.gameObject.SetActive(true);
                

                //총알 발사 이펙트
                GameObject temp = ObjectPool.Instance.PopFromPool("BulletShot");
                temp.SetActive(true);
                temp.transform.position = gameObject.transform.position + vec;

                CoolTime = ShootingRate;
            }

            //적이면
            else
            {
                //적 총알 생성
                GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);

                bullet.gameObject.transform.position = transform.position;
                bullet.gameObject.SetActive(true);
                bullet.gameObject.GetComponent<Shot>().SetShotToPlayer(true);

                CoolTime = ShootingRate;
            }
        }
    }
}