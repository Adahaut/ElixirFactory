using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VakoomStateController : MonoBehaviour
{
    IState currentState;

    public VakoomPatrolState vakoomPatrolState = new VakoomPatrolState();
    public VakoomDrinkingState vakoomDrinkingState = new VakoomDrinkingState();
    public VakoomIdleState vakoomIdleState = new VakoomIdleState();

    public float speed = 1f;

    private void Start()
    {
        ChangeState(vakoomPatrolState);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEnter(this);
    }
}

public interface IState
{
    public void OnEnter(VakoomStateController controller);

    public void UpdateState(VakoomStateController controller);

    public void OnExit(VakoomStateController controller);
}
