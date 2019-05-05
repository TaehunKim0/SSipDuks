using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shotPrefab;
    public float ShootingRate = 0.25f;

    private float ShootCoolDown;
    private string bulletName;

    // Start is called before the first frame update
    void Start()
    {
        ShootCoolDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShootCoolDown > 0 )
        {
            ShootCoolDown -= Time.deltaTime;
        }
    }

    public bool CanAttack
    {
        get
        {
            return ShootCoolDown <= 0f;
        }
    }

    public void Attack(bool isEnemy)
    {
        if(CanAttack)
        {
            ShootCoolDown = ShootingRate;

            if(gameObject.GetComponent<Health>().isEnemy == false)
            {
                bulletName = "Bullet";             
            }
            else
            {
                bulletName = "EBullet";

            }
            GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);

            //Transform shotTransform = Instantiate(shotPrefab) as Transform;
            //shotTransform.position = transform.position;

            //Shot tempShot = shotTransform.gameObject.GetComponent<Shot>();
            //if (tempShot != null)
            //{   
            //    tempShot.isEnemyShot = isEnemy;
            //}

            //Move tempMove = shotTransform.gameObject.GetComponent<Move>();
            //if (tempMove != null)
            //{
            //    Move move = gameObject.GetComponent<Move>();
            //    if (move != null)
            //        tempMove.Direction.x = move.Direction.x;

            //}
        }
    }
    
}
