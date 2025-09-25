using System.Collections.Generic;
using UnityEngine;

public abstract class AMatchUI<GameMode> : MonoBehaviour
where GameMode : AGameMode
{
    [SerializeField] protected Dictionary<Player,PlayerUI> playerUIsByPlayer = new();
    [SerializeField] protected MatchManager matchManager;
    [SerializeField] protected PlayerUI playerUIPrefab;
    [SerializeField] private Transform playerUIContainer;

    protected GameMode gameMode;

    protected virtual void Awake()
    {
        gameMode = (GameMode) matchManager.GameMode;
        matchManager.PlayerRegistered += OnPlayerRegistered;
        matchManager.PlayerUnRegistered += OnPlayerUnRegistered;
        matchManager.ScoreUpdated += OnScoreUpdated;
    }

    protected virtual void OnScoreUpdated((Player player, float score) scoreInfo)
    {
        playerUIsByPlayer[scoreInfo.player].SetScore(scoreInfo.score);
    }

    protected virtual void OnPlayerUnRegistered(Player player)
    {
        playerUIsByPlayer.Remove(player);
    }

    protected virtual void OnPlayerRegistered(Player player)
    {
        var inst = Instantiate(playerUIPrefab, playerUIContainer);
        inst.onFireBonusInjecter = player.GetComponent<OnFireBonusInjecter>();
        playerUIsByPlayer.Add(player, inst);
        if (player.TryGetComponent<PlayerInputManager>(out _))
        {
            inst.MakePG();
        }
    }
}