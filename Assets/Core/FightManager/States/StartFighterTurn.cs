using System;
public class StartFighterTurn<T> : FightManagerState<T>
{
    public StartFighterTurn(T _stateType, FightManager _fightManager, Action _enter = null, Action _update = null, Action _fixedUpdate = null, Action _exit = null)
            : base(_stateType, _fightManager, _enter, _update, _fixedUpdate, _exit)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}
