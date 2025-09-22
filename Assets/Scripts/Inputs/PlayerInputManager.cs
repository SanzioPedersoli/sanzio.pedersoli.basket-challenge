using System;
using UnityEngine;

#if UNITY_EDITOR || UNITY_STANDALONE
using IngamePlayerInput = IngamePlayerMouseInput;
#elif UNITY_ANDROID || UNITY_IOS
    using IngamePlayerInput = IngamePlayerTouchInput;
#endif

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(InputRangeMaker))]
public class PlayerInputManager : MonoBehaviour
{
    public event Action<float> DragFinished;
    public event Action<float> ValueChanged;

    [SerializeField] private float valuePerPixel = 0.01f;
    [SerializeField] private float maxDragDuration = 1f;

    private Player player;
    private InputRangeMaker inputRangeMaker;

    private void Awake()
    {
        inputRangeMaker = GetComponent<InputRangeMaker>();
        player = GetComponent<Player>();
        var input = gameObject.AddComponent<IngamePlayerInput>();

        input.ValuePerPixel = valuePerPixel;
        input.MaxDragDuration = maxDragDuration;

        input.ValueChanged += OnValueChanged;
        input.DragFinished += OnDragFinished;        
    }

    private void OnValueChanged(float newValue)
    {
        if (!player.IsReadyToShoot) return;
        ValueChanged?.Invoke(newValue);
    }

    private void OnDragFinished(float value)
    {
        if (!player.IsReadyToShoot) return;
        var error = inputRangeMaker.GetErrorFromRange(value);
        player.StartShot(error);
        DragFinished?.Invoke(error);
    }
}
