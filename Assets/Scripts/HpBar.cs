using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform offset;

    private Player player;
    private void Start()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        slider.value = player.currentHealth / player.maxHealth;
        slider.transform.position = Camera.main.WorldToScreenPoint(offset.position);
    }
}