using System;
using System.Collections;
using UnityEngine;
public class StartFightState<T> : FightManagerState<T>
{
    public StartFightState(T _stateType, FightManager _fightManager, Action _enter = null, Action _update = null, Action _fixedUpdate = null, Action _exit = null)
        : base(_stateType, _fightManager, _enter, _update, _fixedUpdate, _exit)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("Start Fight");
        fightManager.TurnCount = -1;
        DoTurn();
        base.OnEnter();
    }
    public void DoTurn()
    {
        fightManager.StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);

        fightManager.StateMachine.ChangeState(FightTimeline.StartTableTurn);
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}
