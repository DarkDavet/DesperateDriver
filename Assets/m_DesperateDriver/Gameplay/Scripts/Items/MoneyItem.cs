using UnityEngine;

[RequireComponent (typeof(Collider))]
public class MoneyItem : MonoBehaviour, IResetable
{
    [SerializeField] private GameEvent m_MoneyAddEvent;

    private GameObject vfxEffect;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            vfxEffect = PoolManager.Instance.GetObject("GreenExplosionVFX", transform.position, transform.rotation);
            m_MoneyAddEvent.Raise();
            gameObject.SetActive(false);
        }
    }

    public void ResetObject()
    {
        if (!gameObject.activeSelf && gameObject.GetComponent<MoneyItem>() != null)
        {
            gameObject.SetActive(true);
        }
    }
}
