using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountSlider : MonoBehaviour
{
   public Slider           slider;
    public bool intec;


    public void FixedUpdate()
    {
        if(intec == false) 
        {
            GetComponent<TextMeshProUGUI>().text = (slider.value * 100).ToString("0");
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "x"+ slider.value.ToString("0");
        }
       
    }
}
