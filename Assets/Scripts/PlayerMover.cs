using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Collider placementArea;
    [SerializeField] private Transform target;

    [Button]
    public void PlacePlayerRandomly()
    {
        if (placementArea == null) throw new MissingReferenceException($"The placement Area for the {nameof(PlayerMover)} {name} is missing.");
        if (target == null) throw new MissingReferenceException($"The target for the {nameof(PlayerMover)} {name} is missing.");
        
        Vector3 randomPosition = GetRandomPointInCollider(placementArea);
        transform.position = randomPosition;
        FaceTarget();
    }

    Vector3 GetRandomPointInCollider(Collider col)
    {
        Bounds bounds = col.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        float y = transform.position.y;
        return new(x, y, z);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero) transform.rotation = Quaternion.LookRotation(direction);        
    }
}
