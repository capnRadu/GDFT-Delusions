using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPower : MonoBehaviour
{
    [SerializeField] private AudioSource lightSwitchSound;

    [SerializeField] private Light bulbLight;
    [SerializeField] private Material bulbMaterial;

    private float lightIntensity = 0.4f;
    private float emissionIntensity = 1.41f;

    public bool isOn = true;

    private void Awake()
    {
        if (isOn)
        {
            bulbMaterial.SetColor("_EmissionColor", Color.white * emissionIntensity);
            bulbLight.intensity = lightIntensity;
        } else if (!isOn)
        {
            bulbMaterial.SetColor("_EmissionColor", Color.white * 0);
            bulbLight.intensity = 0;
        }
    }

    public void ToggleLight()
    {
        if (!isOn)
        {
            bulbLight.intensity = lightIntensity;
            bulbMaterial.SetColor("_EmissionColor", Color.white * emissionIntensity);
            isOn = true;
        }
        else if (isOn)
        {
            bulbLight.intensity = 0;
            bulbMaterial.SetColor("_EmissionColor", Color.white * 0);
            isOn = false;
        }

        lightSwitchSound.Play();
    }
}
