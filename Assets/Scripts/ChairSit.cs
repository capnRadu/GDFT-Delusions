using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ChairSit : MonoBehaviour
{
    [SerializeField] private GameObject chair;
    [SerializeField] private GameObject sitPoint;
    [SerializeField] private AudioSource officeChairSFX;
    [SerializeField] private GameObject standPoint;
    SUPERCharacterAIO controllerScript;

    public bool stand = false;
    private bool sit = true;
    public bool enableOutline = false;

    private bool skip = false;

    [SerializeField] private GameObject eyeFade;

    private void Awake()
    {
        if (!skip && (SceneManager.GetActiveScene().name == "Scene 1" || SceneManager.GetActiveScene().name == "Scene 3"))
        {
            controllerScript = GetComponent<SUPERCharacterAIO>();
            Physics.IgnoreCollision(chair.GetComponent<MeshCollider>(), GetComponent<Collider>(), true);
        }
    }

    private void Start()
    {
        if (!skip)
        {
            controllerScript.movementPaused = true;
            transform.position = sitPoint.transform.position;
            transform.rotation = Quaternion.Euler(0, 88, 0);
        }
    }

    private void Update()
    {
        if (!skip)
        {
            if (stand && sit)
            {
                StartCoroutine(SitOnChair());
            }
        }
    }

    IEnumerator SitOnChair()
    {
        float alpha = 0;
        officeChairSFX.Play();

        eyeFade.SetActive(true);
        while (alpha < 1)
        {
            transform.position = Vector3.Lerp(sitPoint.transform.position, standPoint.transform.position, alpha);
            alpha += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        sit = false;
        controllerScript.movementPaused = false;
        Physics.IgnoreCollision(chair.GetComponent<MeshCollider>(), GetComponent<Collider>(), false);
        enableOutline = true;
        yield return null;
    }
}
