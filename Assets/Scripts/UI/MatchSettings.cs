using UnityEngine;

[CreateAssetMenu(fileName = "MatchSettings", menuName = "ScriptableObjects/Match Settings", order = 1)]
public class MatchSettings : ScriptableObject
{
    public int numberOfBots;
    public string field;
    public AGameMode gameMode;
}