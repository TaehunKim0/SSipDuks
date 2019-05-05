using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public int Damage = 25;
    public bool isEnemyShot = false;

    public string poolItemName = string.Empty;
    public float moveSpeed = 10f;
    public float lifeTime = 3f;
    public float _elapsedTime = 0f;

    private bool ShotToPlayer = false;

    public void SetShotToPlayer(bool value)
    {
        if(value)
        {
            GameObject player = GameObject.Find("Player");

            Vector2 v = player.transform.position - gameObject.transform.position;
            Vector2 dir = v / v.magnitude;

            gameObject.GetComponent<Move>().SetDirection(dir);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 20);

    }

    // Update is called once per frame
    void Update()
    {
        if (GetTimer() > lifeTime)
        {
            SetTimer();
            ObjectPool.Instance.PushToPool(poolItemName, gameObject);
        }
    }

    public void PushPool()
    {
        ObjectPool.Instance.PushToPool(poolItemName, gameObject);
    }

    float GetTimer()
    {
        return (_elapsedTime += Time.deltaTime);
    }
    void SetTimer()
    {
        _elapsedTime = 0f;
    }
}
