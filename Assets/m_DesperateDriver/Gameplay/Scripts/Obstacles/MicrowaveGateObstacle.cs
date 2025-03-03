using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MicrowaveGateObstacle : MonoBehaviour
{
    [SerializeField] private int timeDamage;
    private List<ItemTimer> itemTimers;

    private GameObject vfxEffect;
    private const string playerTag = "Player";

    private void Awake()
    {
        itemTimers = new List<ItemTimer>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            //vfxEffect = PoolManager.Instance.GetObject("RedExplosionVFX", transform.position, transform.rotation);
            if (CheckAvailableTimers())
            {
                foreach (ItemTimer timer in itemTimers)
                {
                    timer.DecreaseTimer(timeDamage);
                }
            }
        }
    }

    private bool CheckAvailableTimers()
    {
        var timers = FindObjectsByType<ItemTimer>(FindObjectsSortMode.None);
        itemTimers.Clear();
        foreach (var timer in timers)
        {
            itemTimers.Add(timer);
        }
        if (itemTimers.Count > 0)
        {
            return true;
        }
        return false;
    }
}
