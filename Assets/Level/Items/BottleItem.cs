using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BottleItem : MonoBehaviour, IResetable
{
    [SerializeField] private GameEvent m_BottlePickEvent;
    [SerializeField] private GameObject vfxEffect;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            m_BottlePickEvent.Raise();
            gameObject.SetActive(false);
            Instantiate(vfxEffect, transform.position, transform.rotation);
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
