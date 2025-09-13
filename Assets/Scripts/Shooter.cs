using Sirenix.OdinInspector;
using UnityEngine;
using static BallisticCalculatorUtility;

public class Shooter : MonoBehaviour
{
    [Header("Set on runtime")]   
    public Rigidbody Projectile;

    [Header("Set by hand")]
    [SerializeField] private FieldManager fieldManager;
    [SerializeField] private Transform staringPoint;
    [SerializeField] private float startSpeed = 10f;
    [SerializeField] private float speedIncrement = 0.5f;
    [Tooltip("This curve determine how a different error affect the ballistically perfect shot.")]
    [SerializeField] private AnimationCurve errorCurve;

    private Transform target;

    private void Awake()
    {
        LockProjectile();
        target = fieldManager.FieldInfo.Target;
    }

    [Button]
    public void LockProjectile()
    {
        Projectile.isKinematic = true;
        Projectile.transform.position = staringPoint.position;
        Projectile.transform.parent = staringPoint;
    }

    public void UnlockProjectile()
    {
        Projectile.isKinematic = false;
        Projectile.transform.parent = null;
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
        if (target == null) throw new MissingReferenceException($"The target for the {nameof(Shooter)} {name} is missing.");
        if (staringPoint == null) throw new MissingReferenceException($"The staring point for the {nameof(Shooter)} {name} is missing.");

        var data = new BallisticCalculationData()
        {
            Start = staringPoint.position,
            Target = target.position,
            Speed = startSpeed
        };

        bool isBallisticallyPossible;
        do        
        {
            isBallisticallyPossible = SolveBallisticVelocity(data, out var perfectVelocity);
            if (isBallisticallyPossible)
            {
                Projectile.velocity = perfectVelocity * errorCurve.Evaluate(error);
                return;
            }
            data.Speed += speedIncrement;
        }
        while (!isBallisticallyPossible);              
    }
}
