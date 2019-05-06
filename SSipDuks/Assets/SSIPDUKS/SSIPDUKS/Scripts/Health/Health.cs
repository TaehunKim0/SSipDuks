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

    private AudioSource audio;
    public AudioClip sound;

    private void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = sound;
        audio.loop = false;
    }

    public void Damage(int value)
    {
        hp -= value;

        if(isEnemy)
        {
            GameObject temp = ObjectPool.Instance.PopFromPool("BulletExplode");
            temp.SetActive(true);
            temp.transform.position = gameObject.transform.position;

            AL.ALUtil.Shaker.Instance.Shake();            
        }

        if (hp <= 0)
        {
            if (MemoryDelete == false)
            {
                ObjectPool.Instance.PushToPool(itemName, gameObject);

                ScoreSystem.score += 1;
                GameObject p = GameObject.Find("Player");
                p.gameObject.GetComponent<Player>().PlusMana(10);

                //audio.PlayOneShot(sound, 1);
            }
            else
            {
                ScoreSystem.score += 1;
                GameObject p = GameObject.Find("Player");
                p.gameObject.GetComponent<Player>().PlusMana(30);

                Destroy(gameObject);
            }
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(gameObject.tag != collision.tag)
        {
            Damage(1);
        }
    }
}
