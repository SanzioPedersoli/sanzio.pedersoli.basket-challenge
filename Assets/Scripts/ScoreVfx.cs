using UnityEngine;

public class ScoreVfx : MonoBehaviour
{
    [SerializeField] private ParticleSystem scoreVfx;
    [SerializeField] private MatchManager matchManager;

    private Player pg;

    private void Start()
    {
        pg = FindObjectOfType<PlayerInputManager>().GetComponent<Player>();
        matchManager.ScoreUpdated += OnScoreUpdated;
    }

    private void OnScoreUpdated((Player player, float info) info)
    {
        if (info.player == pg)
        {
            scoreVfx.Play();
        }
    }
}
