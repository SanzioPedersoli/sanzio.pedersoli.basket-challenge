using UnityEngine;

public class FieldInfo : MonoBehaviour
{
    [SerializeReference] public FieldManager FieldManager;
    [SerializeReference] public Transform Target;
    [SerializeReference] public Collider PlacementArea;

    private void Awake()
    {
        FieldManager.FieldInfo = this;
    }
}