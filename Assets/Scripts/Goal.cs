using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isGoalReached { get; private set;}

    private void Awake() {
        isGoalReached = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Pawn")
        {
            isGoalReached = true;
            print("Pawn");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Pawn")
        {
            isGoalReached = false;
            print("NoPawn");
        }
    }
}
