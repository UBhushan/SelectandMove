using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    //Color    
    [SerializeField] Color canBeMovedColor;
    [SerializeField] Color cannotBeMovedColor;
    [SerializeField] Color neutralColor;

    private SpriteRenderer sprite;

    //Move
    public PawnMovement movability {get; private set;}

    [SerializeField] private float timeToMove = 0.25f;
    [SerializeField] private bool isSelected;
    [SerializeField] private GameObject isSelectedSprite;

    private Vector3 direction;
    private Vector3 targetPosition;
    private Vector3 originalPosition;

    public bool isMoving { get; private set;}
    

    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        movability = PawnMovement.undetermined;
        SetIsSelected(isSelected);
    }


    public void TryMovePawn(Vector3 dir)
    {
        direction = dir;
        StartCoroutine(TryMove());   
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void SetIsSelected(bool selectionBool)
    {
        if(selectionBool == true)
        {
            isSelected = true;
            isSelectedSprite.SetActive(true);
            movability = PawnMovement.undetermined;
        }
        else
        {
            isSelected = false;
            isSelectedSprite.SetActive(false);
            movability = PawnMovement.cannotBeMoved;
        }
    }

    private IEnumerator TryMove()
    {
        isMoving = true;
        
        while(movability == PawnMovement.undetermined)
        {
            CheckCanMovement();
            yield return new WaitForSeconds(0.01f);
        }

        if(movability == PawnMovement.canBeMoved)
        {
            StartCoroutine(Move(direction));
        }
        if(movability == PawnMovement.cannotBeMoved)
        {
            isMoving = false;
        }
        SetColor();
        movability = PawnMovement.undetermined;

    }


    private void CheckCanMovement()
    {
        RaycastHit2D hit2D =  Physics2D.CircleCast(transform.position + direction, 0.4f, Vector2.zero);
        if(!hit2D)
        {
            movability = PawnMovement.canBeMoved;
        }
        else if(hit2D.collider.gameObject.tag == "Blocked")
        {
            movability = PawnMovement.cannotBeMoved;
        }
        else if(hit2D.collider.gameObject.tag == "Pawn")
        {
            if(hit2D.collider.TryGetComponent<Pawn>(out Pawn hitPawn))
            {
                movability = hitPawn.movability;
            }
        }
        
    }

    private IEnumerator Move(Vector3 direction)
    {

        originalPosition = transform.position;
        targetPosition = originalPosition + direction;
        targetPosition.x = Mathf.Floor(targetPosition.x) + 0.5f;
        targetPosition.y = Mathf.Floor(targetPosition.y) + 0.5f;

        float elapsedTime = 0f;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime/timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetColor();
        isMoving = false;
    }
    

    private void SetColor()
    {
        if(movability == PawnMovement.canBeMoved)
        {
            sprite.color = canBeMovedColor;
        }
        else if(movability == PawnMovement.cannotBeMoved)
        {
            sprite.color = cannotBeMovedColor;
        }
        else if(movability == PawnMovement.undetermined)
        {
            sprite.color = neutralColor;
        }
    }
}

public enum PawnMovement
{
    canBeMoved,
    undetermined,
    cannotBeMoved
}
