using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameEvent m_LoseEvent;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            m_LoseEvent.Raise();
        }
    }
}
