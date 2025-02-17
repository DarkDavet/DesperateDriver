using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Finish : MonoBehaviour
{
    [SerializeField] private GameEvent m_WinEvent;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            m_WinEvent.Raise();
        }
    }
}
