using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BottleItem : MonoBehaviour, IResetable
{
    [SerializeField] private GameEvent m_BottlePickEvent;
    [SerializeField] private PoolManager objectPool;
    
    private GameObject vfxEffect;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            vfxEffect = objectPool.GetObject("RedExplosionVFX", transform.position, transform.rotation);
            m_BottlePickEvent.Raise();
            gameObject.SetActive(false);
        }
    }

    public void ResetObject()
    {
        if (!gameObject.activeSelf && gameObject.GetComponent<BottleItem>() != null)
        {
            gameObject.SetActive(true);
        }
    }
}
