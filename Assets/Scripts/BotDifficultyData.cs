using UnityEngine;
using UnityEngine.Scripting;
using Random = UnityEngine.Random;

[Preserve]
[CreateAssetMenu(fileName = "BotDifficultyData", menuName = "ScriptableObjects/BotDifficultyData", order = 1)]
public class BotDifficultyData : ScriptableObject
{
    [SerializeField] private AnimationCurve errorCurve;
    [SerializeField] private Vector2 minMaxWaitTime;

    public float GetCurrentWait()
    {
        return Random.Range(minMaxWaitTime.x, minMaxWaitTime.y); 
    }

    public float GetError()
    {
        return errorCurve.Evaluate(Random.Range(0,1));
    }
}