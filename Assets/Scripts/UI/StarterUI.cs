using UnityEngine;

public class StarterUI : MonoBehaviour
{
    [SerializeField] private Starter starter;
    [SerializeField] private Animator[] animators;

    private void Awake()
    {
        starter.NewCountdownNumberReached += OnNewCountDownReached;
    }

    private void OnNewCountDownReached(int currentNumber)
    {
        animators[animators.Length - (currentNumber+1)].SetTrigger("Blink");
    }
}
