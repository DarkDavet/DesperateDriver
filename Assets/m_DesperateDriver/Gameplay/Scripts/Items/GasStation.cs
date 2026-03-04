using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GasStation : MonoBehaviour
{
    [SerializeField] private GameEvent m_GasStationEvent;
    [SerializeField] private float fillInterval = 0.2f;
    [SerializeField] private float cooldownAfterExit = 2.0f;

    private Coroutine fillCoroutine;
    private bool isFillDelay = false;
    private bool isCarHere = false;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            var fuelTank = collider.GetComponent<FuelTank>();
            if (fuelTank != null) fuelTank.IsInsideGasStation = true;

            isCarHere = true;

            if (!isFillDelay && fillCoroutine == null)
            {
                fillCoroutine = StartCoroutine(FillTankCoroutine());
            }
        }
    }

    private IEnumerator FillTankCoroutine()
    {
        while (isCarHere)
        {
            m_GasStationEvent?.Raise();
            yield return new WaitForSeconds(fillInterval);
        }
        fillCoroutine = null;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isCarHere = false;

            var fuelTank = collider.GetComponent<FuelTank>();
            if (fuelTank != null) fuelTank.IsInsideGasStation = false;

            if (fillCoroutine != null)
            {
                StopCoroutine(fillCoroutine);
                fillCoroutine = null;
            }

            if (!isFillDelay) StartCoroutine(DelayAfterFill());
        }
    }

    private IEnumerator DelayAfterFill()
    {
        isFillDelay = true;
        yield return new WaitForSeconds(cooldownAfterExit);
        isFillDelay = false;
    }
}
