using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    private Camera cam;
    private Vector2 raycastPosition;
    private RaycastHit2D hit;
    private Pawn pawn;
    

    private void Awake() {
        cam = Camera.main;
    }

    private void Update() {

        if(Input.GetMouseButtonDown(0))
        {
            raycastPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(raycastPosition, Vector2.zero);
            if(hit)
            {
                hit.collider.TryGetComponent<Pawn>(out pawn);
                pawn.SetIsSelected(true);
                print("Selected");
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            raycastPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(raycastPosition, Vector2.zero);
            if(hit)
            {
                hit.collider.TryGetComponent<Pawn>(out pawn);
                pawn.SetIsSelected(false);
                print("DeSelected");
            }
        }
    }
}
