using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance { get; private set; }
    [SerializeField] private Character playerPrefab;
    [SerializeField] private List<Character> enemiesPrefab;

    private IFighter playerFighter;
    private IFighter enemyFighter;
    public List<IFighter> fighters = new();
    private int turnCount;
    public int TurnCount { get => turnCount; set => turnCount = value; }

    private GenericStateMachine<FightTimeline> stateMachine;
    public GenericStateMachine<FightTimeline> StateMachine { get => stateMachine; }

    void Awake()
    {
        Instance = this;
        SetupStateMachine();
    }

    private void SetupStateMachine()
    {
        FightManagerState<FightTimeline> _initState = new InitializationState<FightTimeline>(FightTimeline.Initialization, this);
        FightManagerState<FightTimeline> _startFight = new StartFightState<FightTimeline>(FightTimeline.StartFight, this);
        FightManagerState<FightTimeline> _endFight = new EndFightState<FightTimeline>(FightTimeline.EndFight, this);
        FightManagerState<FightTimeline> _startTableTurn = new StartTableTurnState<FightTimeline>(FightTimeline.StartTableTurn, this);
        FightManagerState<FightTimeline> _endTableTurn = new EndTableTurnState<FightTimeline>(FightTimeline.EndTableTurn, this);

        stateMachine ??= new();
        stateMachine.AddState(_initState);
        stateMachine.AddState(_startFight);
        stateMachine.AddState(_endFight);
        stateMachine.AddState(_startTableTurn);
        stateMachine.AddState(_endTableTurn);

        Initialized();
    }
    public void StartFight()
    {
        if (stateMachine.CurrentStateEqual(FightTimeline.Initialization))
            stateMachine.ChangeState(FightTimeline.StartFight);
    }
    private void CreateFighter(Character _prefab, ref IFighter _fighter)
    {
        if (_fighter != null) return;
        if (_prefab == null)
            throw new System.Exception("A Character Prefab in Fight Manager is Null");

        _fighter = Instantiate(_prefab);
        _fighter.Initialize();

        fighters.Add(_fighter);
    }
    public void Initialized()
    {
        CreateFighter(playerPrefab, ref playerFighter);
        CreateFighter(enemiesPrefab[Random.Range(0, enemiesPrefab.Count)], ref enemyFighter);
        stateMachine.SetState(FightTimeline.Initialization);
    }
}
public enum FightTimeline
{
    Initialization,
    StartFight,
    EndFight,
    StartTableTurn,
    EndTableTurn,
    StartFighterTurn,
    EndFighterTurn,
}
