using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLogo : MonoBehaviour
{
    
    private void Start()
    {
        Vector3 pos = new Vector3(34f, 0.4f, 0f);
        gameObject.transform.Translate(pos);
    }
    //5~-1
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(pos);

        if (targetScreenPos.x < Screen.width || targetScreenPos.x > 0 || targetScreenPos.y < Screen.height || targetScreenPos.y > 0)
        {
            if (pos.x <= 5f && pos.x >= -1f)
            {
                gameObject.transform.Translate(-5f * Time.deltaTime, 0f, 0f);
            }
            else
            {
                gameObject.transform.Translate(-60f * Time.deltaTime, 0f, 0f);
            }
        }
       
        if(targetScreenPos.x + 788< 0)
            Destroy(gameObject);
        
    }
}
