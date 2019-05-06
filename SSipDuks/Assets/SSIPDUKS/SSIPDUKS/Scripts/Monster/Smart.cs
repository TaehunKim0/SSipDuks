using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smart : MonoBehaviour
{
    private float deltaX;
    private float deltaY;
    private float elapsedTime;
    private float time;

    public void Init(Vector3 pos)
    {
        transform.position = pos;
        deltaY = 0f;
        elapsedTime = 0f;
        time = 0f;
        gameObject.SetActive(true);
    }

    void SmartMove()
    {
        if(elapsedTime <= 1.5f)
        {
            Vector3 vec = new Vector3(-0.1f, 0f, 0f);
            gameObject.transform.Translate(vec);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        SmartMove();
    }
}
