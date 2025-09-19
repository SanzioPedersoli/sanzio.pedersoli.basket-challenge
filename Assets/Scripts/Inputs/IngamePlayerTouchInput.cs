using UnityEngine;

public class IngamePlayerTouchInput : AIngamePlayerInput
{
    protected override void UpdateInput()
    {
        if (Input.touchCount < 0) return;
        
        Touch t = Input.GetTouch(0);

        if (t.phase == TouchPhase.Began)
        {
            StartDrag(t.position);
        }
        else if (isDragging && (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary))
        {
            ContinueDrag(t.position);
        }
        else if (isDragging && (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled))
        {
            EndDrag();
        }        
    }
}