using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeFinderUI : MonoBehaviour
{
    [SerializeField] private Slider currentRangeSlider;
    [SerializeField] private Slider targetSlider;
    [SerializeField] private float maxDistanceFromTarget = 15;

    private InputRangeMaker rangeMaker;
    private PlayerInputManager playerInput;

    private void Start()
    {
        rangeMaker = FindObjectOfType<InputRangeMaker>();
        playerInput = FindObjectOfType<PlayerInputManager>();
        rangeMaker.NewRangeSet += OnNewRangeSet;
        playerInput.ValueChanged += OnValueChanged;
        playerInput.DragFinished += OnDragFinished;
    }

    private void OnDragFinished(float obj)
    {

    }

    private void OnValueChanged(float distance)
    {
        currentRangeSlider.value = distance / maxDistanceFromTarget;
    }

    private void OnNewRangeSet(float distance)
    {
        if (distance > maxDistanceFromTarget) maxDistanceFromTarget = distance;
        currentRangeSlider.value = 0;
        targetSlider.value = distance / maxDistanceFromTarget;
    }
}
