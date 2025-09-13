using System;
using UnityEngine;

/// <summary> Utility class used to perform ballistic calculations to get optimal shots info </summary>
public static class BallisticCalculatorUtility
{
    public const float g = 9.81f;

    [Serializable]
    public struct BallisticCalculationData
    {
        public Vector3 Start;
        public Vector3 Target;
        public float Speed;
    }

    /// <summary>Utility function that calculate the velocity vector for a shot. See the <see href="https://en.wikipedia.org/wiki/Projectile_motion?utm_source=chatgpt.com#Angle_%CE%B8_required_to_hit_coordinate_(x,_y)">formulas and references</see> used.</summary>
    /// <returns>True if the shot is possible with the given speed</returns>
    public static bool SolveBallisticVelocity(BallisticCalculationData inputs, out Vector3 resultVelocity)
    {
        resultVelocity = Vector3.zero;
        Vector3 distanceVector = inputs.Target - inputs.Start;
        Vector3 horizontalDistanceVector = new(distanceVector.x, 0f, distanceVector.z);
        Vector2 distances = new(horizontalDistanceVector.magnitude, distanceVector.y);

        if (distances.x <= 0.0001f) return SolveVerticalShot(out resultVelocity, distances);        

        var quadraticSpeed = inputs.Speed * inputs.Speed;
        var quadraticHorizontalDistance = distances.x * distances.x;

        var underSqrt = quadraticSpeed * quadraticSpeed - g * (g * quadraticHorizontalDistance + 2f * distances.y * quadraticSpeed);

        if (underSqrt < 0f) return false;

        float sqrtVal = Mathf.Sqrt(Mathf.Max(0f, underSqrt));
        float theta = Mathf.Atan2(quadraticSpeed + sqrtVal, g * distances.x);

        var horizontalVelocity = inputs.Speed * Mathf.Cos(theta);
        var verticalVelocity = Vector3.up * (inputs.Speed * Mathf.Sin(theta));
        resultVelocity = horizontalDistanceVector.normalized * horizontalVelocity + verticalVelocity;        
        return true;
    }

    private static bool SolveVerticalShot(out Vector3 resultVelocity, Vector2 distances)
    {
        if (Mathf.Abs(distances.y) > 0.01f)
        {
            if (distances.y > 0)
            {
                resultVelocity = Vector3.up * Mathf.Sqrt(2f * g * Mathf.Abs(distances.y));
                return true;
            }
        }
        resultVelocity = Vector3.zero;
        return true;
    }
}