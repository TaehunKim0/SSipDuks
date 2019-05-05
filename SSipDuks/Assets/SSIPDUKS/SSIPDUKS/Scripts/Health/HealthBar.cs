using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public Player player;

    private void Update()
    {
        if (player != null)
        {
            if (slider.value != player.gameObject.GetComponent<Health>().hp)
                slider.value -= 1;
        }
        else
        {
            if (slider.value != 0)
                slider.value -= 1;
        }
    }
}