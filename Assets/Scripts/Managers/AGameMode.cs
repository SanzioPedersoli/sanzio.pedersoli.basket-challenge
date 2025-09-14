using UnityEngine;
using UnityEngine.Events;

public abstract class AGameMode : MonoBehaviour
{
    public UnityEvent GameStarted;
    public UnityEvent GameIsOver;

    [SerializeField] protected MatchManager MatchManager;
    public abstract Player[] GetPodium();
    public virtual void StartGame()
    {
        GameStarted?.Invoke();
    }
}