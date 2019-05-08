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
    private Vector2 LastDir;

    public bool UseAngle = false;

    public void ShotAngle(float degree)
    {
        Move temp = gameObject.GetComponent<Move>();
        temp.SetAngle(degree);
    }

    public void SetShotToPlayer(bool value)
    {
        if(value)
        {
            GameObject player = GameObject.Find("Player");
            Move temp = gameObject.GetComponent<Move>();
            temp.IsUseAngle(false);

            if(player != null)
            {
                Vector3 v = player.transform.position - gameObject.transform.position;
                v = Vector3.Normalize(v);
                v = new Vector3(-1f * moveSpeed, 0f, 0f);

                gameObject.GetComponentInParent<Move>().SetDirection(v);
                LastDir = v;
            }
            else
            {
                gameObject.GetComponentInParent<Move>().SetDirection(LastDir);
            }
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
