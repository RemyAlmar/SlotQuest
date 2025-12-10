using System;
using System.Collections.Generic;

public class GenericStateMachine<T>
{
    private GenericStateNode current;
    private Dictionary<T, GenericStateNode> nodes = new();
    private HashSet<IGenericTransition<T>> anyTransitions = new();
    public GenericStateNode Current => current;
    public bool CurrentStateEqual(T _stateType) => _stateType.Equals(current.StateType);
    public void Update()
    {
        IGenericTransition<T> _transition = GetTransition();
        if (_transition != null)
            ChangeState(_transition.To);

        current.State?.Update();
    }
    public void FixedUpdate() => current.State?.FixedUpdate();

    public void SetState(T _stateType)
    {
        current = nodes[_stateType];
        current?.State?.OnEnter();
    }
    public void SetState(IGenericState<T> state)
    {
        current = nodes[state.StateType];
        current?.State?.OnEnter();
    }

    public void ChangeState(T _newState)
    {
        if (_newState.Equals(current.StateType) || nodes[_newState] == null) return;

        IGenericState<T> _previousState = current.State;
        IGenericState<T> _nextState = nodes[_newState].State;

        _previousState.OnExit();
        _nextState.OnEnter();

        current = nodes[_newState];

    }
    public void ChangeState(IGenericState<T> _newState)
    {
        if (_newState == current?.State) return;

        IGenericState<T> _previousState = current.State;
        IGenericState<T> _nextState = nodes[_newState.StateType].State;

        _previousState.OnExit();
        _nextState.OnEnter();

        current = nodes[_newState.StateType];
    }

    private IGenericTransition<T> GetTransition()
    {
        foreach (IGenericTransition<T> _transition in anyTransitions)
        {
            if (_transition.Condition.Evaluate())
                return _transition;
        }
        foreach (IGenericTransition<T> _transition in current.Transitions)
        {
            if (_transition.Condition.Evaluate())
                return _transition;
        }

        return null;
    }

    public void AddTransition(IGenericState<T> _from, IGenericState<T> _to, IPredicate _condition)
    {
        GetOrAddNode(_from).AddTransition(GetOrAddNode(_to).State, _condition);
    }

    public void AddAnyTransition(IGenericState<T> _to, IPredicate _condition)
    {
        anyTransitions.Add(new GenericTransition<T>(GetOrAddNode(_to).State, _condition));
    }

    public void AddState(IGenericState<T> _state)
    {
        GetOrAddNode(_state);
    }
    private GenericStateNode GetOrAddNode(IGenericState<T> _state)
    {
        GenericStateNode _node = nodes.GetValueOrDefault(_state.StateType);

        if (_node == null)
        {
            _node = new(_state);
            nodes.Add(_state.StateType, _node);
        }

        return _node;
    }

    public class GenericStateNode
    {
        public T StateType => State.StateType;
        public IGenericState<T> State { get; }
        public HashSet<IGenericTransition<T>> Transitions { get; }
        public GenericStateNode(IGenericState<T> state)
        {
            State = state;
            Transitions = new();
        }

        public void AddTransition(IGenericState<T> _to, IPredicate _condition)
        {
            Transitions.Add(new GenericTransition<T>(_to, _condition));
        }
    }
}