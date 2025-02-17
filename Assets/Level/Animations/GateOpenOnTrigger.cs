using UnityEngine;

public class GateOpenOnTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string animTriggerName = "GateOpenTrigger";  
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            animator.SetTrigger(animTriggerName);
        }
    }
}
