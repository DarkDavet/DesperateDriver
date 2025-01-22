using UnityEngine;

[RequireComponent (typeof(Collider))]
public class MoneyItem : MonoBehaviour, IResetable
{
    [SerializeField] private GameEvent m_MoneyAddEvent;
    [SerializeField] private GameObject vfxEffect;
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            m_MoneyAddEvent.Raise();
            gameObject.SetActive(false);
            Instantiate(vfxEffect, transform.position, transform.rotation);
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
