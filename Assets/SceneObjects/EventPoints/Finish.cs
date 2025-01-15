using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Finish : MonoBehaviour
{
    const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            Debug.Log("Win!");
        }
    }
}
