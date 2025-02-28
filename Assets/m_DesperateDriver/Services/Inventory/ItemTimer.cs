using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ItemTimer : MonoBehaviour
{
    [SerializeField] private ItemValue itemValue;
    private int timer;
    public int Timer { get { return timer; } private set => timer = value; }
    [NonSerialized] public UnityEvent OnTimerEnded = new UnityEvent();

    public void SetupTimer(int duration)
    {
        Timer = duration;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (Timer > 0)
        {
            yield return new WaitForSeconds(1); 
            Timer--; 
            if (Timer <= 0)
            {
                TimerEnded();
            }
        }
    }

    void TimerEnded()
    {
        itemValue.Value = 0;
        OnTimerEnded.Invoke();
        Debug.Log("Timer ended!");
    }
}
