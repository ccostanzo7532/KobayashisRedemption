using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    public Slider slider;
    public Gradient colorchange;
    public Image colorfill;


    public void maxHP(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;

        colorfill.color = colorchange.Evaluate(1f);
    }
    public void setHP(int hp)
    {
        slider.value = hp;
        colorfill.color = colorchange.Evaluate(slider.normalizedValue);
    }
}
