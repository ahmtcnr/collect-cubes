using System;
using System.Collections.Generic;
using UnityEngine;
using AI.States;
using Random = UnityEngine.Random;

public class EnemyController : CollectorControllerBase
{
 
    public PathfindingManager _pathfindingManager;
    public EnemySettings _enemySettings;
    [SerializeField] private Transform _ghostTransform;
    public Stack<Node> ReturnNodes = new Stack<Node>();
    public override event Action<Vector3> OnPointerDrag;

    [SerializeField] private float _targetZOffset;
    
    private BaseState _currentState;

    private Idle _idle;
    private SearchForCollectable _searchForCollectables;
    private ReturnToBase _returnToBase;
    private SetPath _setPath;
    

    [SerializeField] [TextArea(15, 20)] private string _stateLogs;

    private void Awake()
    {
        CreateStates();
    }
    private void Update()
    {
        _currentState?.UpdateState();
    }

    private BaseState GetState(AIState? target)
    {
        switch (target)
        {
            case AIState.idle:
                return _idle;
            case AIState.returnToBase:
                return _returnToBase;
            case AIState.searchForCollectable:
                return _searchForCollectables;
            case AIState.setPath:
                return _setPath;
            case null:
                return null;
            default: return _idle;
        }
    }

    private void CreateStates()
    {
        _idle = new Idle(this);
        _searchForCollectables = new SearchForCollectable(this);
        _setPath = new SetPath(this);
        _returnToBase = new ReturnToBase(this);
    }

    protected override void HandleGameState(GameState state)
    {
        switch (state)
        {
            case GameState.StartGame:
                SwitchStates(AIState.idle);
                break;
            case GameState.EndGame:
                SwitchStates(null);
                break;
        }
    }

    public void SwitchStates(AIState? targetState)
    {
        _currentState?.ExitState();
        var state = GetState(targetState);
        state?.EnterState();
        _currentState = state;

        _stateLogs += _currentState?.GetType().Name + "\n";
    }

    public void SetGhostObjectPosition(Vector3 target)
    {
        target.y = 0;
        target.z += _targetZOffset;
        OnPointerDrag?.Invoke(target - _ghostTransform.position);
        //_ghostTransform.position = target;
    }
}
public enum AIState
{
    idle,
    searchForCollectable,
    returnToBase,
    setPath,
}