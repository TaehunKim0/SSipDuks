using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp = 100;
    public bool isEnemy = true;
    public string itemName = string.Empty;
    public bool MemoryDelete = false;
    public GameObject expolodePrefab;

    public void Damage(int value)
    {
        hp -= value;

        if (hp <= 0)
        {
            if (MemoryDelete == false)
            {
                ObjectPool.Instance.PushToPool(itemName, gameObject);
                //Destroy(gameObject);
                //Instantiate(expolodePrefab, transform.position, Quaternion.identity);

                GameObject temp = ObjectPool.Instance.PopFromPool("BulletExplode");
                temp.SetActive(true);
                temp.transform.position = gameObject.transform.position;

                ScoreSystem.score += 1;
            }
            else
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Shot tempshot = collision.GetComponent<Shot>();
        if(tempshot != null)
        {
            if(tempshot.isEnemyShot != isEnemy)
            {
                Damage(tempshot.Damage);
                tempshot.PushPool();
            }
        }
    }
}
