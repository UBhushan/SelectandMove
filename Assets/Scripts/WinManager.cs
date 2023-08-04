using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField] List<Goal> goalList;

    private bool didWin;

    private void LateUpdate() {
        CheckWin();

        if(didWin)
        {
            print("Won");
        }
    }

    private void CheckWin()
    {
        didWin = true;

        foreach(Goal g in goalList)
        {
            if(!g.isGoalReached)
            {
                didWin = false;
            }

        }     

    }
}
