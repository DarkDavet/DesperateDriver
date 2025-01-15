using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<IGameEventListener> m_EventListeners = new();

    public void Raise()
    {
        for (int i = m_EventListeners.Count - 1; i >= 0; i--)
            m_EventListeners[i].OnEventRaised();
    }

    public void AddListener(IGameEventListener listener)
    {
        if (!m_EventListeners.Contains(listener))
        {
            m_EventListeners.Add(listener);
        }
    }

    public void RemoveListener(IGameEventListener listener)
    {
        if (m_EventListeners.Contains(listener))
        {
            m_EventListeners.Remove(listener);
        }
    }
}
