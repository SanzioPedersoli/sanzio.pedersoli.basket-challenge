using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MatchManager", menuName = "ScriptableObjects/Managers/Match Manager", order = 1)]
public class MatchManager : ScriptableObject
{
    public GameMode gameMode;

    public UnityAction<Player> PlayerRegistered;
    public UnityAction<Player> PlayerUnRegistered;
    public UnityAction<(Player, float)> ScoreUpdated;

    private List<Player> players = new();
    private Dictionary<Player, float> scores = new();

    public void RegisterPlayer(Player newPlayer)
    {
        players.Add(newPlayer);
        scores.Add(newPlayer, 0f);
        Debug.Log("Player registered");
        PlayerRegistered?.Invoke(newPlayer);
    }

    public void UnRegisterPlayer(Player oldPlayer)
    {
        if (!players.Contains(oldPlayer)) players.Add(oldPlayer);
        PlayerUnRegistered?.Invoke(oldPlayer);
    }

    public void AddScore(Player player, float amount) => SetScore(player, scores[player] + amount);    

    private void SetScore(Player player, float newScore)
    {
        scores[player] = newScore;
        ScoreUpdated?.Invoke((player, newScore));
        Debug.Log($"Scored! new Score: {newScore}");
    }

    public void ResetAllScores() 
    { 
        foreach (Player player in players) ResetPlayerScore(player); 
    }

    public float GetScore(Player player) => scores[player];

    public Dictionary<Player, float> GetAllScores() => new(scores);

    public void ResetPlayerScore(Player player) => SetScore(player, 0);
}