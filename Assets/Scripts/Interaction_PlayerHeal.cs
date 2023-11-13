using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_PlayerHeal : Interaction
{
    //This Interaction Module allows the player to heal its units via tap interactions on their screens
    public int _healPerTouch;
    public override void Interact()
    {
        base.Interact();
        Vector2 pos = Vector2.zero;
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                pos = Input.GetTouch(0).position;
            else if (Input.GetMouseButtonDown(0))
                pos = Input.mousePosition;

            if (pos != Vector2.zero)
            {
                Ray tapRay = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(tapRay, out hit, LayerMask.GetMask("Unit")))
                {
                    Unit _target = hit.collider.gameObject.GetComponent<Unit>();

                    if (_target != null && _target.IsPlayerSide)
                        _target.Heal(_healPerTouch);
                }
            }
        }
    }
}
