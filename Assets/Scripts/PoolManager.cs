using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;

        [HideInInspector]
        public ObjectPool<GameObject> pool;
    }

    public List<Pool> pools;

    private void Start()
    {
        foreach (var pool in pools)
        {
            pool.pool = new ObjectPool<GameObject>
        (
            createFunc: () => Instantiate(pool.prefab), // Create function
            actionOnGet: obj => obj.gameObject.SetActive(true), // On get
            actionOnRelease: obj => obj.gameObject.SetActive(false), // On release
            actionOnDestroy: obj => Destroy(obj.gameObject), // On destroy
            defaultCapacity: 10, // Initial capacity
            maxSize: 100 // Max size
        );
        }
    }

    public GameObject GetObject(string tag, Vector3 position, Quaternion rotation)
    {
        foreach(var pool in pools)
        {
            if (pool.tag == tag)
            {
                GameObject pooledObject = pool.pool.Get();
                pooledObject.transform.position = position;
                pooledObject.transform.rotation = rotation;

                return pooledObject;
            }
        }
        return null;
    }

    public void ReleaseObject(string tag, GameObject obj)
    {
        foreach (var pool in pools)
        {
            if (pool.tag == tag)
            {
                pool.pool.Release(obj);
            }  
        }
    }
}
