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

    public enum Player
    {
        Player,
        Enemy1,
        Enemy2,
    
    };

    public Player choosePlayer;

    private void Awake()
    {
        if (choosePlayer == Player.Player)
        {
            player = FindObjectOfType<PlayerInput>().transform;
        }
        else if (choosePlayer == Player.Enemy1)
        {
            player = FindObjectsOfType<EnemyMovement>()[0].transform;
        }
        else if (choosePlayer == Player.Enemy2)
        {
            player = FindObjectsOfType<EnemyMovement>()[1].transform;
        }
    }

    private void Start()
    {
        sliderParent = GetComponentInParent<SliderParent>();
        slider = GetComponent<Slider>();
        startPosition = player.transform.position;
    }

    private void Update()
    {
        if (player!=null)
        {
            slider.value = Mathf.Abs(player.position.z - startPosition.z) / Mathf.Abs(sliderParent.finishLine.position.z - startPosition.z);
        }
    }

}
