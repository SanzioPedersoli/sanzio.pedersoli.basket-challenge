using UnityEngine;
using UnityEngine.Events;

public class PlayerBallManager : MonoBehaviour
{
    public UnityEvent<Ball> NewBallReady;

    public Ball BallPrefab;

    private void Start()
    {
        CreateNewBall();
    }

    public void CreateNewBall()
    {
        var inst = Instantiate(BallPrefab);
        NewBallReady?.Invoke(inst);
        RegisterBall(inst);
    }

    private void RegisterBall(Ball inst)
    {
        inst.BecameOutOfGame.AddListener(CreateNewBall);
    }
}
