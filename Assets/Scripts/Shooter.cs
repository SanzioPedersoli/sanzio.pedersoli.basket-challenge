using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;
using UnityEngine;
using static BallisticCalculatorUtility;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Rigidbody projectile;
    [SerializeField] private Transform target;
    [SerializeField] private float startSpeed = 10f;
    [SerializeField] private float speedIncrement = 0.5f;

    [Tooltip("This curve determine how a different error affect the ballistically perfect shot.")]
    [SerializeField] private AnimationCurve errorCurve;

    private void Awake()
    {
        LockProjectile();
    }

    [Button]
    public void LockProjectile()
    {
        projectile.isKinematic = true;
        projectile.transform.position = transform.position;
        projectile.transform.parent = transform;

    }

    public void UnlockProjectile()
    {
        projectile.isKinematic = false;
        projectile.transform.parent = null;
    }

    [Button]
    public void UnlockAndShoot(float error = 0) 
    { 
        UnlockProjectile();
        Shoot(error);
    }

    /// <summary> Adjust speed and angle to get a shot, applies any desired error. </summary>
    /// <param name="error">The amount of undershoot or overshoot. The error value should go from -1 to 1 for better results. 0 error means a perfect shot.</param>
    public void Shoot(float error = 0)
    {
        if (target == null) throw new MissingReferenceException($"The target for the shooter {name} is missing.");

        var data = new BallisticCalculationData()
        {
            Start = transform.position,
            Target = target.position,
            Speed = startSpeed
        };

        bool isBallisticallyPossible;
        do        
        {
            isBallisticallyPossible = SolveBallisticVelocity(data, out var perfectVelocity);
            if (isBallisticallyPossible)
            {
                projectile.velocity = perfectVelocity * errorCurve.Evaluate(error);
                return;
            }
            data.Speed += speedIncrement;
        }
        while (!isBallisticallyPossible);              
    }
}
