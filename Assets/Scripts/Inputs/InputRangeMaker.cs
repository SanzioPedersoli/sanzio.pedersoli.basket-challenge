using UnityEngine;

public class InputRangeMaker : MonoBehaviour
{
    [SerializeField] private FieldInfo fieldInfo;
    [SerializeField] private Transform startTransform;

    private float currentRange;

    public void CalculateNewRange()
    {
        currentRange = Vector3.Distance(startTransform.position, fieldInfo.Target.position);
        print($"New Current Range: {currentRange}");
    }

    public float GetErrorFromRange(float range)
    {
        var error = Mathf.Abs(currentRange - range) / currentRange;
        print($"Current Range: {currentRange}, Range: {range}, Error: {error}");
        return error;
    }
}