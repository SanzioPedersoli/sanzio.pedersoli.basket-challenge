using UnityEngine;
using UnityEngine.Events;

public abstract class AIngamePlayerInput : MonoBehaviour
{
    public UnityEvent<float> OnDragFinished;

    public float ValuePerPixel = 0.01f;
    public float MaxDragDuration = 1f;

    protected bool isDragging = false;
    protected float dragStartTime;
    protected Vector2 lastPosition;
    protected float value = 0f;

    protected void Update()
    {
        UpdateInput();
    }

    protected abstract void UpdateInput();


    protected void StartDrag(Vector2 startPos)
    {
        isDragging = true;
        dragStartTime = Time.time;
        lastPosition = startPos;
        value = 0f;
    }

    protected void ContinueDrag(Vector2 currentPos)
    {
        if (Time.time - dragStartTime > MaxDragDuration)
        {
            EndDrag();
            return;
        }

        Vector2 delta = currentPos - lastPosition;

        if (delta.y > 0)
        {
            value += delta.y * ValuePerPixel;
        }

        lastPosition = currentPos;
    }

    protected void EndDrag()
    {
        isDragging = false;
        OnDragFinished?.Invoke(value);
    }

}