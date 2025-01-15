using UnityEngine;

[RequireComponent (typeof(Collider))]
public class MoneyItem : MonoBehaviour
{
    [SerializeField] private GameEvent m_Event;
    [SerializeField] private ParticleSystem particle;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            m_Event.Raise();
            gameObject.SetActive(false);
            particle.Play();
        }
    }
}
