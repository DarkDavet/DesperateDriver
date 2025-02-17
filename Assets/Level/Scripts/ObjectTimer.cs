using System.Collections;
using UnityEngine;

public class ObjectTimer : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private string objectName;
    private Coroutine lifeTimerCoroutine;

    private void OnEnable()
    {
        lifeTimerCoroutine = StartCoroutine(LifeTimer());
    }

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);

        PoolManager.Instance.ReleaseObject(objectName, gameObject);
    }
}

