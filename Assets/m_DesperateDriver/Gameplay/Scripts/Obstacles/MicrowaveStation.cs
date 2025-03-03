using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MicrowaveStation : MonoBehaviour
{
    [SerializeField] private int timeDamage;
    private GameObject vfxEffect;
    private const string playerTag = "Player";
    private Coroutine heatUpCoroutine;
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
                heatUpCoroutine = StartCoroutine(HeatUpCoroutine());
            }
            
        }
    }

    private IEnumerator HeatUpCoroutine()
    {
        while (true)
        {
            OnMicrowaveStationUse();
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void OnMicrowaveStationUse()
    {
        foreach (ItemTimer timer in itemTimers)
        {
            timer.DecreaseTimer(timeDamage);
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
            StopCoroutine(heatUpCoroutine);
        }
    }
}
