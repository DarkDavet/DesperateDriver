using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpeedControlObstacle : MonoBehaviour
{
    [SerializeField] private int speedLimit;
    [SerializeField] private TextMeshProUGUI speedLimittext;
    [SerializeField] private GameEvent m_LoseEvent;
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private int fineSum;

    private Speedometer speedometer;

    private GameObject vfxEffect;
    private const string playerTag = "Player";

    private void Start()
    {
        speedometer = FindAnyObjectByType<Speedometer>();
        speedLimittext.text = speedLimit.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            //vfxEffect = PoolManager.Instance.GetObject("RedExplosionVFX", transform.position, transform.rotation);
            CheckSpeed();
        }
    }

    private void CheckSpeed()
    {
        if (speedometer.CurrentSpeed > speedLimit)
        {
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
