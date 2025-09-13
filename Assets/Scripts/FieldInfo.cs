using UnityEngine.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldInfo", menuName = "ScriptableObjects/Field info", order = 1)]
public class FieldInfo : ScriptableObject
{
    public Transform Target;
    public Collider PlacementArea;
    public string SceneName;
}