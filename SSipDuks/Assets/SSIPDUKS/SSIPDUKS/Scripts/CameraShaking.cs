using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 originPos;

    public float shakeTimer = 0; //흔들림 효과 시간 
    public float shakeAmount; //흔들림 범위

    private Vector3 CurrentPos;

    private void Start()
    {
        CurrentPos = transform.position;
    }

    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmount = shakePwr;
        shakeTimer = shakeDur;
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShakeCamera(0.5f, 0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shakeTimer >= 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = transform.position + new Vector3(ShakePos.x, 0f, -4f);

            shakeTimer -= Time.deltaTime;
        }

        if(shakeTimer <= 0)
        {
            transform.position = CurrentPos;
        }
    }
}
