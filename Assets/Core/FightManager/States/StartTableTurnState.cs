using System;
using System.Collections;
using UnityEngine;

public class StartTableTurnState<T> : FightManagerState<T>
{
    public StartTableTurnState(T _stateType, FightManager _fightManager, Action _enter = null, Action _update = null, Action _fixedUpdate = null, Action _exit = null)
            : base(_stateType, _fightManager, _enter, _update, _fixedUpdate, _exit)
    {
    }

    public override void OnEnter()
    {
        fightManager.TurnCount++;
        Debug.Log($"Start Turn [{fightManager.TurnCount}]");
        fightManager.StartCoroutine(FighterTurn());
        base.OnEnter();
    }
    IEnumerator FighterTurn()
    {
        int i = 0;
        foreach (IFighter _fighter in fightManager.fighters)
        {
            _fighter.StartTurn();
            Debug.Log($"Fighter[{i}] : Is Playing");
            yield return new WaitWhile(() => _fighter.IsPlaying);
            Debug.Log($"Fighter[{i}] : End Turn");
            i++;
        }
        fightManager.StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

        fightManager.StateMachine.ChangeState(FightTimeline.EndTableTurn);
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}