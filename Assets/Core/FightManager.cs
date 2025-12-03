using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance { get; private set; }
    [SerializeField] private Character playerPrefab;
    [SerializeField] private List<Character> enemiesPrefab;

    private IFighter playerFighter;
    private IFighter enemyFighter;

    private int turnCount;
    private bool fightStarted;

    void Awake()
    {
        Instance = this;
    }
    private void SpawnPlayer()
    {
        playerFighter = Instantiate(playerPrefab);
        playerFighter.Initialize();
    }

    private void SpawnEnemy()
    {
        enemyFighter = Instantiate(enemiesPrefab[UnityEngine.Random.Range(0, enemiesPrefab.Count)]);
        enemyFighter.Initialize();
    }
}
public enum FightTimeline
{
    StartFight,
    EndFight,
    StartTableTurn,
    EndTableTurn,
    StartCharacterTurn,
    EndCharacterTurn,
}
