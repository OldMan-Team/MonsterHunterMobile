using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherInfoUI : MonoBehaviour
{
    public GameObject HPui;
    public GameObject Chargeui;

    public void SetHP(float hp)
    {
        HPui.GetComponent<Slider>().value = hp;
    }

    public void SetCharge(float ch)
    {
        Chargeui.GetComponent<Slider>().value = ch;
    }
}
