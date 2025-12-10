using System;
using UnityEngine;

public class EndFightState<T> : FightManagerState<T>
{
    public EndFightState(T _stateType, FightManager _fightManager, Action _enter = null, Action _update = null, Action _fixedUpdate = null, Action _exit = null)
        : base(_stateType, _fightManager, _enter, _update, _fixedUpdate, _exit)
    {
    }
}
