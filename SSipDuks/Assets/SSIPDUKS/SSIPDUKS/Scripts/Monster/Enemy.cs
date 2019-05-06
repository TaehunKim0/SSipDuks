using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool hasSpawn;
    private Move moveS;
    private Collider2D col;
    private Renderer render;

    private Weapon myWeapons;

    private void Awake()
    {
        myWeapons = gameObject.GetComponent<Weapon>();
        moveS = GetComponent<Move>();
        render = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hasSpawn = false;
        col.enabled = false;

        if(myWeapons != null)
          myWeapons.enabled = false;
    }

    private void Spawn()
    {
        hasSpawn = true;

        col.enabled = true;
        myWeapons.enabled = true;
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
            if (myWeapons != null && myWeapons.CanAttack())
            {
                myWeapons.Attack(true);
            }

            if (render.IsVisibleFrom(Camera.main) == false)
            {
                //화면 밖에 나가면
                string itemName = gameObject.GetComponent<Health>().itemName;
                ObjectPool.Instance.PushToPool(itemName, gameObject);
            }
        }
    }
}
