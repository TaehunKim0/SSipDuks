using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public int Damage = 25;
    public bool isEnemyShot = false;

    public string poolItemName = "Bullet";
    public float moveSpeed = 10f;
    public float lifeTime = 3f;
    public float _elapsedTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
        //Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetTimer() > lifeTime)
        {
            SetTimer();
            ObjectPool.Instance.PushToPool(poolItemName, gameObject);
        }
    }

    void PushPool()
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
