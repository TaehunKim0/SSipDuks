using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObject
{
    public string poolItemName = string.Empty; //객체를 검색할 때 사용할 이름
    public GameObject prefab = null;//오브젝트 풀에 저장할 프리팹
    public int poolCount = 0;//초기화할 때 생성할 객체 수   

    [SerializeField]
    private List<GameObject> poolList = new List<GameObject>();

    //pool 객체를 초기화할 때 처음 한번만 호출 Poolcount 만큼 객체를 생성해서   리스트에 추가
    public void Initialize(Transform parent = null)
    {
        for (int ix = 0; ix < poolCount; ++ix)
        {
            poolList.Add(CreateItem(parent));
        }
    }
    //사용한 객체를 다시 오브젝트 풀에 반환할 때 
    public void PushToPool(GameObject item, Transform parent = null)
    {
        item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);
    }
    //객체가 필요할 때 오브젝트 풀에 요청하는 용도 객체를 반환
    public GameObject PopFromPool(Transform parent = null)
    {
        if (poolList.Count == 0)
            poolList.Add(CreateItem(parent));

        GameObject item = poolList[0];
        poolList.RemoveAt(0);

        return item;
    }
    //prefab에 지정된 게임 오브젝트를 생성하는 역할
    private GameObject CreateItem(Transform parent = null)
    {
        GameObject item = Object.Instantiate(prefab) as GameObject;
        item.name = poolItemName;
        item.transform.SetParent(parent);
        item.SetActive(false);

        return item;
    }
}
