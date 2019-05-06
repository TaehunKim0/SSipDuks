using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Slider slider1;
    [SerializeField] private Slider slider2;
    [SerializeField] private Slider slider3;

     [SerializeField] public Image gauge1;
     [SerializeField] public Image gauge2;
     [SerializeField] public Image gauge3;

    public Player player;
    private int offset = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
           

            if (slider1.value < player.GetMana())
                slider1.value += offset;
            else if (slider1.value > player.GetMana())
                slider1.value -= offset;

            if (player.GetMana() >= 100)
            {
                if (slider2.value < player.GetMana())
                    slider2.value += offset;
                else if (slider2.value > player.GetMana())
                    slider2.value -= offset;

                gauge1.gameObject.SetActive(true);

            }
            else
            {
                slider2.value -= offset;
                gauge1.gameObject.SetActive(false);
            }

            if (player.GetMana() >= 200)
            {
                if (slider3.value < player.GetMana())
                    slider3.value += offset;
                else if (slider3.value > player.GetMana())
                    slider3.value -= offset;

                gauge2.gameObject.SetActive(true);
            }
            else
            {
                slider3.value -= offset;
                gauge2.gameObject.SetActive(false);
            }

            if(player.GetMana() >= 300)
            {
                gauge3.gameObject.SetActive(true);
            }
            else
                gauge3.gameObject.SetActive(false);

        }

        
    }
}