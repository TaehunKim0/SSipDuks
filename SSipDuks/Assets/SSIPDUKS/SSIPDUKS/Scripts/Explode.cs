using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public string itemName = string.Empty;
    private float elapsedTime = 0f;
    public float DestroyTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= DestroyTime)
        {
            if (itemName != string.Empty)
                ObjectPool.Instance.PushToPool(itemName, gameObject);
            else
                Destroy(gameObject);

            elapsedTime = 0;
        }

    }
}