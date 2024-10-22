using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VakoomDrinkingState : IState
{
    public bool isFull = false;
    public void OnEnter(VakoomStateController sc)
    {
        Drink(sc);
    }

    public void UpdateState(VakoomStateController sc)
    {
        if (isFull)
        {
            sc.ChangeState(sc.vakoomIdleState);
        }
        else
        {
            sc.ChangeState(sc.vakoomPatrolState);
        }
    }

    public void OnExit(VakoomStateController sc)
    {

    }
    private void Drink(VakoomStateController sc)
    {
        sc.StartCoroutine(DrinkCoroutine(sc));
    }

    private IEnumerator DrinkCoroutine(VakoomStateController sc)
    {
        yield return new WaitForSeconds(2f); // Temps de boisson
        sc.vakoomPatrolState.isDrinking = false; // Fin de la boisson, on peut se balader à nouveau
        // Optionnel : choisir une nouvelle direction ou un nouveau lac
        
    }
}
