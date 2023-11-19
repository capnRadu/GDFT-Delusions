using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject player;
    RGBShiftEffect rgbShift;
    PosterizeEffect posterize;
    ScanlinesEffect scanlines;
    BadTVEffect badTV;
    RippleEffect ripple;
    TiltShiftEffect tiltShift;

    [SerializeField] private GameObject fade;

    private void Start()
    {
        rgbShift = Camera.main.GetComponent<RGBShiftEffect>();
        posterize = Camera.main.GetComponent<PosterizeEffect>();
        scanlines = Camera.main.GetComponent<ScanlinesEffect>();
        badTV = Camera.main.GetComponent<BadTVEffect>();
        ripple = Camera.main.GetComponent<RippleEffect>();
        tiltShift = Camera.main.GetComponent<TiltShiftEffect>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Scene 1")
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            rgbShift.amount = Mathf.Lerp(0.1f, 0.0025f, distance / 4);
            rgbShift.speed = Mathf.Lerp(0.5f, 0.24f, distance / 4);

            posterize.level = (int)Mathf.Lerp(2, 166, distance / 4);

            scanlines.strength = Mathf.Lerp(2, 0.48f, distance / 4);
            scanlines.noise = Mathf.Lerp(2, 0.53f, distance / 4);

            badTV.thickDistort = Mathf.Lerp(2, 0.4f, distance / 4);
            badTV.fineDistort = Mathf.Lerp(8, 0.44f, distance / 4);

            ripple.strength = Mathf.Lerp(0.018f, 0, distance / 4);

            tiltShift.amount = Mathf.Lerp(0.01f, 0.001f, distance / 4);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fade.SetActive(true);
            Invoke("LoadScene", 1f);
        }
    }

    private void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "Scene 1") SceneManager.LoadScene("Scene 2");
        else SceneManager.LoadScene("Scene 3");
    }
}
