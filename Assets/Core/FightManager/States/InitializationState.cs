using System;
using System.Collections;
using UnityEngine;

public class InitializationState<T> : FightManagerState<T>
{
    public InitializationState(T _stateType, FightManager _fightManager, Action _enter = null, Action _update = null, Action _fixedUpdate = null, Action _exit = null)
        : base(_stateType, _fightManager, _enter, _update, _fixedUpdate, _exit)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Start Initialisation");
        fightManager.StartCoroutine(Initialisation());
    }
    IEnumerator Initialisation()
    {
        Debug.Log("Initialisation ONLOAD");
        yield return new WaitForSeconds(1f);
        fightManager.StartFight();
    }
    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("End Initialisation");
    }
}
