using cakeslice;
using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject holdArea;
    [SerializeField] private GameObject player;
    SUPERCharacterAIO characterController;
    ChairSit chairScript;

    private bool outlineEnable = false;

    private Rigidbody rigidbody;
    private MeshCollider meshCollider;
    private bool pickedUp = false;

    [SerializeField] private GameObject destinationObject;
    [SerializeField] private GameObject placePoint = null;

    [SerializeField] private GameObject startLevel;
    StartLevel1 startLevelScript;

    RGBShiftEffect rgbShift;
    ScanlinesEffect scanlinesEffect;
    BadTVEffect badTVEffect;
    TiltShiftEffect tiltShift;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
        characterController = player.GetComponent<SUPERCharacterAIO>();
        chairScript = player.GetComponent<ChairSit>();

        GetComponent<Outline>().eraseRenderer = true;

        if (SceneManager.GetActiveScene().name == "Scene 2")
        {
            startLevelScript = startLevel.GetComponent<StartLevel1>();
        }

        rgbShift = Camera.main.GetComponent<RGBShiftEffect>();
        scanlinesEffect = Camera.main.GetComponent<ScanlinesEffect>();
        badTVEffect = Camera.main.GetComponent<BadTVEffect>();
        tiltShift = Camera.main.GetComponent<TiltShiftEffect>();
    }

    private void Update()
    {
        if (!outlineEnable)
        {
            if ((chairScript && chairScript.enableOutline) || (SceneManager.GetActiveScene().name == "Scene 2" && startLevelScript.enableOutline))
            {
                GetComponent<Outline>().eraseRenderer = false;
                outlineEnable = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && pickedUp && !characterController.raycastHit)
        {
            rigidbody.transform.parent = null;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;

            destinationObject.GetComponent<Outline>().eraseRenderer = true;

            pickedUp = false;
            characterController.pickedUpObject = null;
        }
    }

    public void PickUpObject()
    {
        if (!pickedUp && !characterController.pickedUpObject)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.transform.parent = holdArea.transform;
            rigidbody.transform.position = holdArea.transform.position;

            if (meshCollider) meshCollider.convex = true;

            destinationObject.GetComponent<Outline>().eraseRenderer = false;

            pickedUp = true;
            characterController.pickedUpObject = gameObject;
        }
    }

    public void DestroyObject()
    {
        if(pickedUp)
        {
            if (placePoint)
            {
                placePoint.SetActive(true);
            }

            Destroy(gameObject);
            destinationObject.GetComponent<Outline>().eraseRenderer = true;

            if (GameObject.Find("InteractText"))
            {
                GameObject.Find("InteractText").SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Scene 1")
        {
            GameObject.Find("PassSFX").GetComponent<AudioSource>().Play();
        }
    }

    public void DestroyObject2(GameObject callingObject)
    {
        if (pickedUp)
        {
            if (placePoint)
            {
                placePoint.SetActive(true);
            }

            if ((gameObject.layer == 9 && callingObject.gameObject.CompareTag("Compost")) ||
                (gameObject.layer == 10 && callingObject.gameObject.CompareTag("Garbage")) ||
                (gameObject.layer == 11 && callingObject.gameObject.CompareTag("Recycle")))
            {
                rgbShift.amount -= 0.2f * rgbShift.amount;
                rgbShift.speed -= 0.2f * rgbShift.speed;

                scanlinesEffect.strength -= 0.2f * scanlinesEffect.strength;
                scanlinesEffect.speed -= 0.2f * scanlinesEffect.speed;

                badTVEffect.thickDistort -= 0.2f * badTVEffect.thickDistort;
                badTVEffect.fineDistort -= 0.2f * badTVEffect.fineDistort;

                tiltShift.amount -= 0.2f * tiltShift.amount;

                Debug.Log("correct");
            }
            else
            {
                rgbShift.amount += 0.2f * rgbShift.amount;
                rgbShift.speed += 0.2f * rgbShift.speed;

                scanlinesEffect.strength += 0.2f * scanlinesEffect.strength;
                scanlinesEffect.speed += 0.2f * scanlinesEffect.speed;

                badTVEffect.thickDistort += 0.2f * badTVEffect.thickDistort;
                badTVEffect.fineDistort -= 0.2f * badTVEffect.fineDistort;

                tiltShift.amount += 0.2f * tiltShift.amount;

                Debug.Log("wrong");
            }

            Destroy(gameObject);
            destinationObject.GetComponent<Outline>().eraseRenderer = true;

            if (GameObject.Find("InteractText"))
            {
                GameObject.Find("InteractText").SetActive(false);
            }
        }
    }
}
