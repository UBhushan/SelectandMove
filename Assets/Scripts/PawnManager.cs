using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    [SerializeField] List<Pawn> pawnList;
    [SerializeField] int totalMoveCount;

    private Vector3 direction;
    private bool canMove;
    private int currentMoveCount;

    private void Awake() {
        currentMoveCount = 0;
    }

    private void Update() {

        CheckCanMove();

        if(currentMoveCount < totalMoveCount)
        {
            TakeInput();
        }
        
    }

    private void TakeInput()
    {
        if(Input.GetKeyDown(KeyCode.W) && canMove) 
        { 
            direction = Vector3.up;
            TryMove(direction);
        }
        else if(Input.GetKeyDown(KeyCode.S) && canMove) 
        { 
            direction = Vector3.down;
            TryMove(direction);
        }
        else if(Input.GetKeyDown(KeyCode.A) && canMove) 
        { 
            direction = Vector3.left;
            TryMove(direction);
        }
        else if(Input.GetKeyDown(KeyCode.D) && canMove) 
        { 
            direction = Vector3.right;
            TryMove(direction);
        }
    }

    private void CheckCanMove()
    {
        canMove = true;

        foreach (Pawn p in pawnList)
        {
            if(p.isMoving)
            {
                canMove = false;
                return;
            }

        }     

    }

    private void TryMove(Vector3 dir)
    {
        foreach (Pawn p in pawnList)
        {
            if(p.GetIsSelected())
            {
                p.TryMovePawn(dir);
            }

        }
        currentMoveCount++;
    }

    
}
