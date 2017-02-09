using System;
using System.Collections;
using System.Collections.Generic;
using Core.Map;
using UnityEngine;

[ExecuteInEditMode]
public class DayNightColorShift : MonoBehaviour
{
    public Material Material;

    [Header("Day values")]
    public float[] DayValues;
    [Header("Evening values")]
    public float[] EveningValues;
    [Header("Night values")]
    public float[] NightValues;
    [Header("Morning values")]
    public float[] MorningValues;


    // Use this for initialization
    void Awake()
    {
        FindObjectOfType<DayNight>().DayStateChanged += OnDayStateChanged; ;
    }

    private void OnDayStateChanged(EDayTime obj)
    {
        switch (obj)
        {
            case EDayTime.Morning:
                Material.SetInt("_HueShift", Convert.ToInt32(MorningValues[0]));
                Material.SetFloat("_Sat", MorningValues[1]);
                Material.SetFloat("Val", MorningValues[1]);
                break;
            case EDayTime.Day:
                Material.SetInt("_HueShift", Convert.ToInt32(DayValues[0]));
                Material.SetFloat("_Sat", DayValues[1]);
                Material.SetFloat("Val", DayValues[1]);
                break;
            case EDayTime.Evening:
                Material.SetInt("_HueShift", Convert.ToInt32(EveningValues[0]));
                Material.SetFloat("_Sat", EveningValues[1]);
                Material.SetFloat("Val", EveningValues[1]);
                break;
            case EDayTime.Night:
                Material.SetInt("_HueShift", Convert.ToInt32(NightValues[0]));
                Material.SetFloat("_Sat", NightValues[1]);
                Material.SetFloat("Val", NightValues[1]);
                break;
            default:
                throw new ArgumentOutOfRangeException("obj", obj, null);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
