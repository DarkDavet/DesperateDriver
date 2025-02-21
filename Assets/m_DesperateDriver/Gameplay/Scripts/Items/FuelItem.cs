using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FuelItem : MonoBehaviour
{
    [SerializeField] private GameEvent m_FuelPickEvent;

    private GameObject vfxEffect;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            //vfxEffect = PoolManager.Instance.GetObject("RedExplosionVFX", transform.position, transform.rotation);
            m_FuelPickEvent.Raise();
            gameObject.SetActive(false);
        }
    }

    public void ResetObject()
    {
        if (!gameObject.activeSelf && gameObject.GetComponent<FuelItem>() != null)
        {
            gameObject.SetActive(true);
        }
    }
}
