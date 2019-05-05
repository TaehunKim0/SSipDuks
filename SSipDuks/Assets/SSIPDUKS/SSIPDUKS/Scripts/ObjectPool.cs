using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<PoolObject> objectPool = new List<PoolObject>();
    //오브젝트 풀 초기화 각각 objectPool 안의 PoolObject의 Initialize 호출
    void Awake()
    {
        for (int ix = 0; ix < objectPool.Count; ++ix)
        {
            objectPool[ix].Initialize(transform);
        }
    }
    //사용한 객체를 objectPool에 반환할 때 사용할 함수 (반환할 객체의 pool 오브젝트 이름, 반환할 객체, )
    public bool PushToPool(string itemName, GameObject item, Transform parent = null)
    {
        PoolObject pool = GetPoolItem(itemName);
        if (pool == null)
            return false;

        pool.PushToPool(item, parent == null ? transform : parent);
        return true;
    }
    //필요할 객체를 오브젝트 풀에 요청할 때 사용 (요청할 객체의 pool 오브젝트 이름, 부모 계층 관계를 설정할 정보)
    public GameObject PopFromPool(string itemName, Transform parent = null)
    {
        PoolObject pool = GetPoolItem(itemName);
        if (pool == null)
            return null;

        return pool.PopFromPool(parent);
    }
    //itemName 과 같은 오브젝트 풀을 검색하고, 검색에 성공하면 그 결과 리턴
    PoolObject GetPoolItem(string itemName)
    {
        for (int ix = 0; ix < objectPool.Count; ++ix)
        {
            if (objectPool[ix].poolItemName.Equals(itemName))
                return objectPool[ix];
        }

        Debug.LogWarning("There's no matched pool list.");
        return null;
    }
}