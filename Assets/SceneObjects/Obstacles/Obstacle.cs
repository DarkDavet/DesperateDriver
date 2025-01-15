using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            LogicManager.Instance.Lose();
        }
    }
}
