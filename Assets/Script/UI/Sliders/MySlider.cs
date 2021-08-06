using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MySlider : MonoBehaviour
{
    private SliderParent sliderParent;
    private Slider slider;
    public Transform player;
    private Vector3 startPosition;

    private void Awake()
    {
        player = FindObjectOfType<PlayerInput>().transform;
    }

    private void Start()
    {
        sliderParent = GetComponentInParent<SliderParent>();
        slider = GetComponent<Slider>();
        startPosition = player.transform.position;
    }

    private void Update()
    {
        slider.value = Mathf.Abs(player.position.z - startPosition.z) / Mathf.Abs(sliderParent.finishLine.position.z - startPosition.z);
    }

}
