using System;
using UnityEngine;

public class InputRangeMaker : MonoBehaviour
{
    public event Action<float> NewRangeSet;

    [SerializeField] private FieldManager fieldManager;
    [SerializeField] private Transform startTransform;

    private float currentRange;

    public float CurrentRange 
    {
        get => currentRange; 
        set 
        { 
            currentRange = value;
            NewRangeSet?.Invoke(value);
        } 
    }

    public void CalculateNewRange()
    {
        CurrentRange = Vector3.Distance(startTransform.position, fieldManager.FieldInfo.Target.position);
    }

    public float GetErrorFromRange(float range)
    {
        var error = -(currentRange - range) / currentRange;
        return error;
    }
}