using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private MatchManager matchManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!matchManager.GameMode.IsGameOn) return;
        if (other.CompareTag(TagHelper.Ball) && other.TryGetComponent(out Ball ball))
        {
            if (!ball.isInGame) return;

            matchManager.AddScore(ball.owner, ball.GetScore());
            ball.OnScore();
            ball.isInGame = false;
        }
    }
}