using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Shooter shooter;
    [SerializeField] private PlayerBallManager ballManager;
    [SerializeField] private PlayerMover mover;

    private void Awake()
    {
        ballManager.NewBallReady.AddListener(InitialaizeShot);
    }

    private void InitialaizeShot(Ball ball)
    {
        mover.PlacePlayerRandomly();
        shooter.Projectile = ball.Rigidbody;
        shooter.LockProjectile();
    }
}