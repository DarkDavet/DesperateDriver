using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusStateController : MonoBehaviour
{
    private Dictionary<Type, StatusState> _statesMap = new();
    private StatusState StateCurrent { get; set; }

    public void AddState(StatusState state)
    {
        _statesMap.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : StatusState
    {
        var type = typeof(T);

        if ( StateCurrent != null && StateCurrent.GetType() == type)
        {
            return;
        }

        if (_statesMap.TryGetValue(type, out var newState))
        {
            StateCurrent?.Exit();
            StateCurrent = newState;
            StateCurrent.Enter();
        }
    }

    public void Update()
    {
        StateCurrent?.Update();
    }
}
