using System;
using UnityEngine;

public abstract class AIngamePlayerInput : MonoBehaviour
{
    public event Action<float> DragFinished;
    public event Action<float> ValueChanged;

    public float ValuePerPixel = 0.01f;
    public float MaxDragDuration = 1f;

    protected bool isDragging = false;
    protected float dragStartTime;
    protected Vector2 lastPosition;

    private float inputValue = 0f;
    protected float Value 
    {
        get 
        { 
            return inputValue; 
        } 
        set
        {
            inputValue = value;
            ValueChanged?.Invoke(inputValue);
        }
    }

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
        Value = 0f;
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
            Value += delta.y * ValuePerPixel;
        }

        lastPosition = currentPos;
    }

    protected void EndDrag()
    {
        isDragging = false;
        DragFinished?.Invoke(Value);
    }

}