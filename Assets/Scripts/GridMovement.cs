using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    #region  Requirements
    /*

    */
    #endregion

    [SerializeField] private float timeToMove = 0.25f;

    private bool isMoving;
    private bool canMoveTo;
    private Vector3 targetPosition;
    private Vector3 originalPosition;

    private void Update() {

        if(Input.GetKey(KeyCode.W) && !isMoving) 
        { 
            StartCoroutine(TryMove(Vector3.up));
        }
        if(Input.GetKey(KeyCode.S) && !isMoving) 
        { 
            StartCoroutine(TryMove(Vector3.down));
        }
        if(Input.GetKey(KeyCode.A) && !isMoving) 
        { 
            StartCoroutine(TryMove(Vector3.left));
        }
        if(Input.GetKey(KeyCode.D) && !isMoving) 
        { 
            StartCoroutine(TryMove(Vector3.right));
        }

    }

    private IEnumerator TryMove(Vector3 direction)
    {
        isMoving = true;

        originalPosition = transform.position;
        targetPosition = originalPosition + direction;
        targetPosition.x = Mathf.Floor(targetPosition.x) + 0.5f;
        targetPosition.y = Mathf.Floor(targetPosition.y) + 0.5f;
        canMoveTo = CanMoveTo(direction);

        float elapsedTime = 0f;

        while(canMoveTo && elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime/timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }

    private bool CanMoveTo(Vector3 direction) 
    {
        return true;
    }
}
