using UnityEngine;
using System.Collections;

public class BadTVEffect : MonoBehaviour {

	public bool on = false;

	[Range(0.01f, 10f)]
	public float thickDistort = 3f;
	public float baseThickDistort = 10f;

	[Range(0.01f, 10f)]
	public float fineDistort = 1f;
	public float baseFineDistort = 10f;

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
        //Mathf.Clamp(thickDistort, 0.01f, 10f);
        //Mathf.Clamp(fineDistort, 0.01f, 10f);

        if (on)
		{
			material.SetFloat("_ThickDistort", thickDistort);
			material.SetFloat("_FineDistort", fineDistort);
		}
	}
}