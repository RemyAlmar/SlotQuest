using System;
using UnityEngine;

namespace StateMachineSystem
{
    public class GenericBaseState<T> : IGenericState<T>
    {
        private readonly T stateType;
        public T StateType => stateType;

        public Action EnterBehaviour { get; set; }
        public Action UpdateBehaviour { get; set; }
        public Action FixedUpdateBehaviour { get; set; }
        public Action ExitBehaviour { get; set; }

        public GenericBaseState(T _stateType, Action _enter = null, Action _update = null, Action _fixedUpdate = null, Action _exit = null)
        {
            this.stateType = _stateType;

            EnterBehaviour = _enter;
            UpdateBehaviour = _update;
            FixedUpdateBehaviour = _fixedUpdate;
            ExitBehaviour = _exit;
        }

        public virtual void OnEnter() => EnterBehaviour?.Invoke();
        public virtual void Update() => UpdateBehaviour?.Invoke();
        public virtual void FixedUpdate() => FixedUpdateBehaviour?.Invoke();
        public virtual void OnExit() => ExitBehaviour?.Invoke();
    }
}