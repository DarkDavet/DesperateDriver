using System;
using UnityEngine;

public class GameEventListener: MonoBehaviour, IGameEventListener
{
    public GameEvent m_Event;
    public Action EventHandler;

    public void Subscribe()
    {
        m_Event.AddListener(this);
    }

    public void Unsubscribe()
    {
        m_Event.RemoveListener(this);
    }
    public void OnEventRaised()
    {
        EventHandler?.Invoke();
    }
}
