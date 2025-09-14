using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR || UNITY_STANDALONE
using IngamePlayerInput = IngamePlayerMouseInput;
#elif UNITY_ANDROID || UNITY_IOS
    using IngamePlayerInput = IngamePlayerTouchInput;
#endif

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private float valuePerPixel = 0.01f;
    [SerializeField] private float maxDragDuration = 1f;

    private void Awake()
    {
        var input = gameObject.AddComponent<IngamePlayerInput>();
        input.ValuePerPixel = valuePerPixel;
        input.MaxDragDuration = maxDragDuration;
    }
}
