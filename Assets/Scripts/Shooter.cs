using UnityEngine;
using static BallisticCalculatorUtility;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 50f;

    public void Shoot()
    {
        if (target == null) throw new MissingReferenceException($"The target for the shooter {name} is missing.");

        var data = new BallisticCalculationData()
        {
            Start = transform.position,
            Target = target.position,
            Speed = speed
        };

        var isBallisticallyPossible = SolveBallisticVelocity(data, out Vector3 resultVelocity);

        if (!isBallisticallyPossible)
        {
            Debug.LogWarning("Nessuna soluzione: target fuori portata per la velocità data.");
            return;
        }

        rb.velocity = resultVelocity;        
    }
}
