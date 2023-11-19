using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SUPERCharacter;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;

    [SerializeField] private AudioSource doorOpenSFX;
    [SerializeField] private AudioSource doorCloseSFX;
    [SerializeField] private AudioSource doorLockedSFX;

    [SerializeField] private float openAngle = -90;
    public bool doorOpen = false;
    public bool doorLocked = false;
    private bool reverse = false;
    private bool canInteract = true;
    private bool lockedText = true;
    private bool startLevelLock = false;

    [SerializeField] private GameObject dialogue;
    Dialogue dialogueScript;

    private bool showLightTip = false;

    [SerializeField] private GameObject startLevel;
    StartLevel1 startLevelScript;

    private void Start()
    {
        dialogueScript = dialogue.GetComponent<Dialogue>();
        
        if (SceneManager.GetActiveScene().name == "Scene 2")
        {
            startLevelScript = startLevel.GetComponent<StartLevel1>();
        }
    }

    private void Update()
    {
        var remainingUnorganized = GameObject.FindGameObjectsWithTag("Unorganized");
        var remainingUnorganized1 = GameObject.FindGameObjectsWithTag("Unorganized 1");
        var remainingUnorganized2 = GameObject.FindGameObjectsWithTag("Unorganized 2");
        var remainingUnorganized3 = GameObject.FindGameObjectsWithTag("Unorganized 3");


        if (gameObject.name == "Door_008" && remainingUnorganized.Length == 0 && doorLocked && SceneManager.GetActiveScene().name == "Scene 1")
        {
            doorLocked = false;
            PlayText();
        }

        if (gameObject.name == "Door" && startLevelScript.enableOutline && !startLevelLock && SceneManager.GetActiveScene().name == "Scene 2")
        {
            startLevelLock = true;
            doorLocked = true;
            canInteract = false;
            StartCoroutine(CloseDoor());
            doorOpen = false;
        }

        if (gameObject.CompareTag("Door Entrance 1") && remainingUnorganized1.Length == 0 && doorLocked && SceneManager.GetActiveScene().name == "Scene 2")
        {
            doorLocked = false;
            PlayText4();
        }

        if (gameObject.CompareTag("Door Entrance 2") && remainingUnorganized2.Length == 0 && doorLocked && SceneManager.GetActiveScene().name == "Scene 2")
        {
            doorLocked = false;
            PlayText4();
        }

        if (gameObject.CompareTag("Door Entrance 3") && remainingUnorganized1.Length == 0 && remainingUnorganized2.Length == 0 && remainingUnorganized3.Length != 0 && !startLevelScript.enableOutline && doorLocked && SceneManager.GetActiveScene().name == "Scene 2")
        {
            doorLocked = false;
        }

        if (gameObject.name == "Door_008" && SceneManager.GetActiveScene().name == "Scene 2" && GameObject.FindWithTag("Lock") && GameObject.FindWithTag("Lock").GetComponentInChildren<CodeLock>().finalCode == 2632 && doorLocked)
        {
            doorLocked = false;
            canInteract = false;
            StartCoroutine(OpenDoor());
            doorOpen = true;
            GameObject.FindWithTag("Player").GetComponent<SUPERCharacterAIO>().UnpausePlayer();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameObject.FindWithTag("Lock").SetActive(false);   
        }
    }

    public bool Interact()
    {
        if (canInteract && !doorLocked && !GameObject.Find("Dialogue"))
        {
            if (gameObject.name != "Door_008" && !doorLocked)
            {
                OnInteract.Invoke();
            }
            canInteract = false;

            if (!doorOpen)
            {
                StartCoroutine(OpenDoor());
                doorOpen = true;

                if (gameObject.name == "Door_008" && SceneManager.GetActiveScene().name == "Scene 1")
                {
                    Invoke("PlayText2", 1f);
                }

                if (gameObject.CompareTag("Door Entrance 2") && GameObject.FindGameObjectsWithTag("Unorganized 2").Length == 0)
                {
                    Camera.main.GetComponent<MirrorEffect>().on = false;
                }

                return true;
            }

            if (doorOpen)
            {
                StartCoroutine(CloseDoor());
                doorOpen = false;
                return false;
            }
        } else if (doorLocked && lockedText && !GameObject.Find("Dialogue"))
        {
            lockedText = false;
            StartCoroutine(LockedDoor());
        }

        return false;
    }

    IEnumerator OpenDoor()
    {
        float alpha = 0;
        float newY = transform.localEulerAngles.y + (reverse ? -openAngle : openAngle);
        doorOpenSFX.Play();

        while (alpha < 1)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(90, newY, 0), alpha);
            alpha += Time.deltaTime;
            reverse = true;
            doorOpen = true;
            yield return new WaitForEndOfFrame();
        }
        canInteract = true;
        yield return null;
    }

    IEnumerator CloseDoor()
    {
        float alpha = 0;
        float newY = transform.localEulerAngles.y + (reverse ? -openAngle : openAngle);
        doorCloseSFX.Play();

        while (alpha < 1)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(90, newY, 0), alpha);
            alpha += Time.deltaTime;
            reverse = false;
            doorOpen = false;
            yield return new WaitForEndOfFrame();
        }
        canInteract = true;
        yield return null;
    }

    IEnumerator LockedDoor()
    {
        doorLockedSFX.Play();

        if (gameObject.CompareTag("Door Entrance 3") && (GameObject.FindGameObjectsWithTag("Unorganized 1").Length != 0 || GameObject.FindGameObjectsWithTag("Unorganized 2").Length != 0))
        {
            PlayText5();
        }

        if (gameObject.name == "Door_008" && SceneManager.GetActiveScene().name == "Scene 2" && doorLocked)
        {
            OnInteract.Invoke();
        }

        yield return new WaitForSeconds(4);
        lockedText = true;
    }

    private void PlayText()
    {
        dialogueScript.lines = new string[1];
        dialogueScript.lines[0] = "To the garage now.";
        dialogueScript.StartDialogue();
    }

    private void PlayText2()
    {
        dialogueScript.lines = new string[1];
        dialogueScript.lines[0] = "Whoa...";
        dialogueScript.StartDialogue();
    }

    public void PlayText3()
    {
        if (!showLightTip)
        {
            dialogueScript.lines = new string[1];
            dialogueScript.lines[0] = "I should power the lights on...";
            dialogueScript.StartDialogue();

            showLightTip = true;
        }
    }

    private void PlayText4()
    {
        dialogueScript.lines = new string[1];
        dialogueScript.lines[0] = "To the next house...";
        dialogueScript.StartDialogue();
    }

    private void PlayText5()
    {
        dialogueScript.lines = new string[1];
        dialogueScript.lines[0] = "I must clean the other houses first.";
        dialogueScript.StartDialogue();
    }
}
