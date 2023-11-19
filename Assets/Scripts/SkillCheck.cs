using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheck : MonoBehaviour
{
    private bool isLmb = false;
    public float rotateSpeed = 200;

    [SerializeField] private GameObject fillImage;

    SUPERCharacterAIO playerController;
    SpawnCheck spawnCheckScript;
    private bool isMirror = false;

    RGBShiftEffect rgbShift;
    ScanlinesEffect scanlinesEffect;
    BadTVEffect badTVEffect;
    TiltShiftEffect tiltShift;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<SUPERCharacterAIO>();
        playerController.PausePlayer(PauseModes.FreezeInPlace);

        rgbShift = Camera.main.GetComponent<RGBShiftEffect>();
        scanlinesEffect = Camera.main.GetComponent<ScanlinesEffect>();
        badTVEffect = Camera.main.GetComponent<BadTVEffect>();
        tiltShift = Camera.main.GetComponent<TiltShiftEffect>();

        spawnCheckScript = GameObject.FindWithTag("Player").GetComponent<SpawnCheck>();
        rotateSpeed += spawnCheckScript.skillCheckSpeed;
        Debug.Log(rotateSpeed);

        if (GameObject.Find("InteractText"))
        {
            GameObject.Find("InteractText").SetActive(false);
        }

        if (Camera.main.GetComponent<MirrorEffect>().on)
        {
            Camera.main.GetComponent<MirrorEffect>().on = false;
            isMirror = true;
        }

        fillImage.transform.Rotate(0, 0, WrapAngle(Random.Range(0, 361)));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isLmb = true;

            var rotation = /*TransformUtils.GetInspectorRotation(transform);*/ transform.rotation.eulerAngles;
            var rotation2 = /*TransformUtils.GetInspectorRotation(fillImage.transform);*/ fillImage.transform.rotation.eulerAngles;

            if ((int) rotation.z >= (int) rotation2.z && (int)rotation.z <= (int)rotation2.z + 36)
            {
                Debug.Log("pass");

                GameObject.Find("PassSFX").GetComponent<AudioSource>().Play();

                //spawnCheckScript.skillCheckSpeed += 15;
                spawnCheckScript.skillCheckSpeed += 10;

                GameObject.Find("StaticSFX").GetComponent<AudioSource>().volume -= 0.005f * spawnCheckScript.passMultiplier;

                if (rgbShift.amount - 0.01f * spawnCheckScript.passMultiplier * rgbShift.baseAmount >= 0.001f) rgbShift.amount -= 0.01f * spawnCheckScript.passMultiplier * rgbShift.baseAmount;
                if (rgbShift.speed - 0.01f * spawnCheckScript.passMultiplier * rgbShift.baseSpeed >= 0.001f) rgbShift.speed -= 0.01f * spawnCheckScript.passMultiplier * rgbShift.baseSpeed;

                scanlinesEffect.amount += 10 * (int) spawnCheckScript.passMultiplier * 10;
                if (scanlinesEffect.strength - 0.01f * spawnCheckScript.passMultiplier * scanlinesEffect.baseStrength >= 0.001f) scanlinesEffect.strength -= 0.01f * spawnCheckScript.passMultiplier * scanlinesEffect.baseStrength;
                if (scanlinesEffect.speed - 0.01f * spawnCheckScript.passMultiplier * scanlinesEffect.baseSpeed >= 0.001f) scanlinesEffect.speed -= 0.01f * spawnCheckScript.passMultiplier * scanlinesEffect.baseSpeed;

                if (badTVEffect.thickDistort - 0.01f * spawnCheckScript.passMultiplier * badTVEffect.baseThickDistort >= 0.01f) badTVEffect.thickDistort -= 0.01f * spawnCheckScript.passMultiplier * badTVEffect.baseThickDistort;
                if (badTVEffect.fineDistort - 0.01f * spawnCheckScript.passMultiplier * badTVEffect.baseFineDistort >= 0.01f) badTVEffect.fineDistort -= 0.01f * spawnCheckScript.passMultiplier * badTVEffect.baseFineDistort;

                if (tiltShift.amount - 0.01f * spawnCheckScript.passMultiplier * tiltShift.baseAmount >= 0.001f) tiltShift.amount -= 0.01f * spawnCheckScript.passMultiplier * tiltShift.baseAmount;

                //spawnCheckScript.passMultiplier += 0.05f;
                //spawnCheckScript.failMultiplier = 0.1f;

                if (spawnCheckScript.failStreak != 1)
                {
                    spawnCheckScript.passMultiplier = 0.1f;
                    spawnCheckScript.failMultiplier = 0.1f;
                    spawnCheckScript.failStreak = 1;
                }
                else if (spawnCheckScript.passStreak == 1 && spawnCheckScript.failMultiplier != 0.1f)
                {
                    spawnCheckScript.failMultiplier = 0.1f;
                }

                spawnCheckScript.passMultiplier += 0.1f;

                if (spawnCheckScript.passMultiplier > 0.2f)
                {
                    spawnCheckScript.failMultiplier += 0.4f;
                    spawnCheckScript.passStreak += 1;
                }
            }
            else
            {
                GameObject.Find("FailSFX").GetComponent<AudioSource>().Play();

                //spawnCheckScript.skillCheckSpeed -= 2;
                spawnCheckScript.skillCheckSpeed -= 10;

                GameObject.Find("StaticSFX").GetComponent<AudioSource>().volume += 0.01f * spawnCheckScript.failMultiplier;

                if (rgbShift.amount + 0.01f * spawnCheckScript.failMultiplier * rgbShift.baseAmount <= 0.1f) rgbShift.amount += 0.01f * spawnCheckScript.failMultiplier * rgbShift.baseAmount;
                if (rgbShift.speed + 0.01f * spawnCheckScript.failMultiplier * rgbShift.baseSpeed <= 10f) rgbShift.speed += 0.01f * spawnCheckScript.failMultiplier * rgbShift.baseSpeed;

                scanlinesEffect.amount -= 30 * (int)spawnCheckScript.failMultiplier * 10;
                if (scanlinesEffect.strength + 0.01f * spawnCheckScript.failMultiplier * scanlinesEffect.baseStrength <= 2f) scanlinesEffect.strength += 0.01f * spawnCheckScript.failMultiplier * scanlinesEffect.baseStrength;
                if (scanlinesEffect.speed + 0.01f * spawnCheckScript.failMultiplier * scanlinesEffect.baseSpeed <= 10f) scanlinesEffect.speed += 0.01f * spawnCheckScript.failMultiplier * scanlinesEffect.baseSpeed;

                if (badTVEffect.thickDistort + 0.01f * spawnCheckScript.failMultiplier * badTVEffect.baseThickDistort <= 10f) badTVEffect.thickDistort += 0.01f * spawnCheckScript.failMultiplier * badTVEffect.baseThickDistort;
                if (badTVEffect.fineDistort + 0.01f * spawnCheckScript.failMultiplier * badTVEffect.baseFineDistort <= 10f) badTVEffect.fineDistort += 0.01f * spawnCheckScript.failMultiplier * badTVEffect.baseFineDistort;

                if (tiltShift.amount + 0.01f * spawnCheckScript.failMultiplier * tiltShift.baseAmount <= 0.02f) tiltShift.amount += 0.01f * spawnCheckScript.failMultiplier * tiltShift.baseAmount;

                //spawnCheckScript.failMultiplier += 0.25f;
                //spawnCheckScript.passMultiplier = 0.1f;

                if (spawnCheckScript.passStreak != 1)
                {
                    spawnCheckScript.failMultiplier = 0.1f;
                    spawnCheckScript.passMultiplier = 0.1f;
                    spawnCheckScript.passStreak = 1;
                }
                else if (spawnCheckScript.failStreak == 1 && spawnCheckScript.passMultiplier != 0.1f)
                {
                    spawnCheckScript.passMultiplier = 0.1f;
                } 

                spawnCheckScript.failMultiplier += 0.1f;

                if (spawnCheckScript.failMultiplier > 0.2f)
                {
                    spawnCheckScript.passMultiplier += 0.1f;
                    spawnCheckScript.failStreak += 1;
                }

            }

            Debug.Log(spawnCheckScript.skillCheckSpeed);

            playerController.UnpausePlayer();
            if (isMirror)
            {
                Camera.main.GetComponent<MirrorEffect>().on = true;
                isMirror = false;
            }
            Destroy(transform.parent.gameObject.transform.parent.gameObject);
        }
        
        RotateArrow();
    }

    private void RotateArrow()
    {
        if (!isLmb)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
        }
    }

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    private static float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }
}
