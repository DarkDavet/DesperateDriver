using System.Collections;
using UnityEngine;

public class ObjectTimer : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private string objectName;
    private PoolManager objectPool;

    private void OnEnable()
    {
        objectPool = FindFirstObjectByType<PoolManager>();
        StartCoroutine(LifeTimer());
    }

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        objectPool.ReleaseObject(objectName ,gameObject);
    }
}
