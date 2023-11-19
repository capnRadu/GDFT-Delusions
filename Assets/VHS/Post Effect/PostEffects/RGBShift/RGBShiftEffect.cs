using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBShiftEffect : MonoBehaviour {


	public bool on = false;

	[Range(0.001f, 0.1f)]
	public float amount = 0.05f;
	public float baseAmount = 0.1f;

	[Range(0.001f, 10f)]
	public float speed = 5f;
	public float baseSpeed = 10f;

	public Material material;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if(on)
		{
			Graphics.Blit(src, dest, material);
		}
		else
		{
			Graphics.Blit(src, dest);
		}
	}


	void Update()
	{
		//Mathf.Clamp(amount, 0.001f, 0.1f);
        //Mathf.Clamp(amount, 0.001f, 10f);

        if (on)
		{
			material.SetFloat("_Amount", amount);
			material.SetFloat("_Speed", speed);
		}
	}
}
