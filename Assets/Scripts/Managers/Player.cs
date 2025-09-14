using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent BallShot;

    private bool isReadyToShoot = false;

    [SerializeField] private MatchManager matchManager;
    [SerializeField] private Shooter shooter;
    [SerializeField] private PlayerBallManager ballManager;
    [SerializeField] private PlayerMover mover;

    private void Awake()
    {
        ballManager.NewBallReady.AddListener(InitialaizeShot);
        matchManager.RegisterPlayer(this);
    }

    private void InitialaizeShot(Ball ball)
    {
        mover.PlacePlayerRandomly();
        ball.owner = this;
        shooter.Projectile = ball.Rigidbody;
        shooter.LockProjectile();
        BallShot.AddListener(() => { ball.isInGame = true; });
        isReadyToShoot = true;
    }

    [Button]
    public void StartShot(float error = 0f)
    {
        if (!isReadyToShoot) return;
        isReadyToShoot = false;
        BallShot?.Invoke();
        BallShot.RemoveAllListeners();
        shooter.UnlockAndShoot(error);
    }

    private void OnDestroy()
    {
        if (matchManager != null) matchManager.UnRegisterPlayer(this);
    }
}