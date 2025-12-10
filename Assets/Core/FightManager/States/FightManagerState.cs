using StateMachineSystem;
using System;

public abstract class FightManagerState<T> : GenericBaseState<T>
{
    protected FightManager fightManager;
    public FightManagerState(T _stateType, FightManager _fightManager, Action _enter = null, Action _update = null, Action _fixedUpdate = null, Action _exit = null)
        : base(_stateType, _enter, _update, _fixedUpdate, _exit)
    {
        fightManager = _fightManager;
    }
}
