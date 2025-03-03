using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FridgeStation : MonoBehaviour
{
    [SerializeField] private int timeBoost;
    private GameObject vfxEffect;
    private const string playerTag = "Player";
    private Coroutine freezeCoroutine;
    private List<ItemTimer> itemTimers;

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
                freezeCoroutine = StartCoroutine(FreezeCoroutine());
            }

        }
    }

    private IEnumerator FreezeCoroutine()
    {
        while (true)
        {
            OnFridgeStationUse();
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void OnFridgeStationUse()
    {
        foreach (ItemTimer timer in itemTimers)
        {
            timer.IncreaseTimer(timeBoost);
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

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            StopCoroutine(freezeCoroutine);
        }
    }
}
