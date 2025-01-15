using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BottleItem : MonoBehaviour
{
    [SerializeField] private GameEvent m_Event;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            m_Event.Raise();
            gameObject.SetActive(false);
        }
    }
}
