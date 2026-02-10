using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ItemTimer : MonoBehaviour
{
    [SerializeField] private ItemValue itemValue;
    private int timer;
    private int duration;
    private bool isTimerLaunched;
    public bool IsTimerLaunched { get => isTimerLaunched; set => isTimerLaunched = value; }
    public int Timer { get { return timer; } private set => timer = Mathf.Clamp(value, 0, duration + 1); }
    [NonSerialized] public UnityEvent OnTimerEnded = new UnityEvent();

    public void SetupTimer(int duration)
    {
        IsTimerLaunched = true;
        this.duration = duration;
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
        IsTimerLaunched = false;
        itemValue.Value = 0;
        OnTimerEnded.Invoke();
        Debug.Log("Timer ended!");
    }

    public void DecreaseTimer(int duration)
    {
        Timer -= duration;
    }

    public void IncreaseTimer(int duration)
    {
        Timer += duration;
    }
}
