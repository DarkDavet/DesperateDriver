using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GasStation : MonoBehaviour
{
    [SerializeField] private GameEvent m_GasStationEvent;

    private GameObject vfxEffect;
    private const string playerTag = "Player";
    private Coroutine fillTankCoroutine;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            //vfxEffect = PoolManager.Instance.GetObject("RedExplosionVFX", transform.position, transform.rotation);
            if (fillTankCoroutine != null)
            {
                StopCoroutine(fillTankCoroutine);
            }
            fillTankCoroutine = StartCoroutine(FillTankCoroutine());
        }
    }

    private IEnumerator FillTankCoroutine()
    {
       while (true)
        {
            m_GasStationEvent.Raise();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            if (fillTankCoroutine != null)
            {
                StopCoroutine(fillTankCoroutine);
                fillTankCoroutine = null;
            }
        }
    }
}
