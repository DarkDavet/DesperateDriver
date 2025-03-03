using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ForbiddenPlaceObstacle : MonoBehaviour
{
    [SerializeField] private GameEvent m_LoseEvent;
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private int fineSum;

    private Speedometer speedometer;

    private GameObject vfxEffect;
    private const string playerTag = "Player";

    private void Start()
    {
        speedometer = FindAnyObjectByType<Speedometer>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            //vfxEffect = PoolManager.Instance.GetObject("RedExplosionVFX", transform.position, transform.rotation);
            PayFine();
        }
    }

    private void PayFine()
    {
        if (!levelInventory.RequestPayment(fineSum))
        {
            m_LoseEvent.Raise();
        }
    }
}
