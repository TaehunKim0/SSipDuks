using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool hasSpawn;
    private Move moveS;
    private Collider2D col;
    private Renderer render;

    private Weapon[] myWeapons;

    private void Awake()
    {
        myWeapons = GetComponentsInChildren<Weapon>();
        moveS = GetComponent<Move>();
        render = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hasSpawn = false;
        col.enabled = false;
        moveS.enabled = true;
        for (int i = 0; i < myWeapons.Length; i++)
        {
            myWeapons[i].enabled = false;
        }
    }

    private void Spawn()
    {
        hasSpawn = true;
        col.enabled = true;
        moveS.enabled = true;
        for (int i = 0; i < myWeapons.Length; i++)
        {
            myWeapons[i].enabled = true;
        }
    }

    void Update()
    {
        if (hasSpawn == false)
        {
            if (render.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
            for (int i = 0; i < myWeapons.Length; i++)
            {
                if (myWeapons[i] != null && myWeapons[i].CanAttack)
                {
                    myWeapons[i].Attack(true);
                }
            }

            if (render.IsVisibleFrom(Camera.main) == false)
            {
               // Destroy(gameObject); // 화면밖으로 나면 삭제
            }
        }


    }
}
