using System;
using UnityEngine;
using UnityEngine.Pool;
public class PoolTool : MonoBehaviour
{
    public GameObject objPrefab;
    public ObjectPool<GameObject> pool;
    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(objPrefab, transform),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
           actionOnDestroy: (obj) => Destroy(obj),
           collectionCheck: false,
           defaultCapacity: 10,
           maxSize: 20
            );
      
    }
    private void PreFillPool(int count)
    {
        var PreFillArray = new GameObject[count];
        for (int i = 0; i < count; i++) {
            PreFillArray[i] = pool.Get();
        }

        foreach (var item in PreFillArray)
        {
            pool.Release(item);
        }
    }

    public GameObject GetGameObjectFromPool()
    {
        return pool.Get();
    }
    public void ReturnObjectToPool(GameObject obj)
    {
        pool.Release(obj);
    }
}
