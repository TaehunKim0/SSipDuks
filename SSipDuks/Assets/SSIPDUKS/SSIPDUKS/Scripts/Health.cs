using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp = 100;
    public bool isEnemy = true;

    public void Damage(int value)
    {
        hp -= value;

        if (hp <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Shot tempshot = collision.GetComponent<Shot>();
        if(tempshot != null)
        {
            if(tempshot.isEnemyShot != isEnemy)
            {
                Damage(tempshot.Damage);
                
            }
        }
    }
}
