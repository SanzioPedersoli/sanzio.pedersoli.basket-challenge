using UnityEngine;

public class IngamePlayerMouseInput : AIngamePlayerInput
{
    protected override void UpdateInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(Input.mousePosition);
        }
        else if (isDragging && Input.GetMouseButton(0))
        {
            ContinueDrag(Input.mousePosition);
        }
        else if (isDragging && Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }
}