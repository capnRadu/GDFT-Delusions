using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFade : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;

    [SerializeField] private AudioSource notificationSound;
    [SerializeField] private Animator notificationAnimator;

    [SerializeField] private GameObject player;
    SUPERCharacterAIO playerController;
    ChairSit sitScript;

    [SerializeField] private GameObject dialogue;
    Dialogue dialogueScript;

    private bool skip = false;

    private void Awake()
    {
        if (!skip && (SceneManager.GetActiveScene().name == "Scene 1" || SceneManager.GetActiveScene().name == "Scene 3"))
        {
            playerController = player.GetComponent<SUPERCharacterAIO>();
            sitScript = player.GetComponent<ChairSit>();
            playerController.controllerPaused = true;

            dialogueScript = dialogue.GetComponent<Dialogue>();
        }
    }

    private void Start()
    {
        if (!skip)
        {
            if (SceneManager.GetActiveScene().name == "Scene 1")
            {
                Invoke("FadeAnimation", 5.46f);
                Invoke("PlayText1", 6f);
                Invoke("PlayNotification", 24f);
                Invoke("PlayText2", 25f);
                Invoke("EnableMovement", 35f);
            }

            if (SceneManager.GetActiveScene().name == "Scene 3")
            {
                Invoke("FadeAnimation", 5.46f);
                Invoke("PlayNotification", 6f);
                Invoke("PlayText3", 7.2f);
                Invoke("FadeAnimation2", 25f);
                Invoke("Menu", 30f);
            }
        }
    }

    private void FadeAnimation()
    {
        playerController.controllerPaused = false;
        fadeAnimator.enabled = true;
    }

    private void FadeAnimation2()
    {
        GameObject.Find("Fade End").GetComponent<Animator>().enabled = true;
        GameObject.Find("TypingSFX").GetComponent<AudioSource>().Play();
        GameObject.Find("TypingSFX").GetComponent<AudioSource>().loop = true;
    }

    private void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Scene 0");
    }

    private void PlayText1()
    {
        dialogueScript.lines = new string[5];
        dialogueScript.lines[0] = "Damn it...";
        dialogueScript.lines[1] = "I tried to work for hours but I can't get myself to concentrate.";
        dialogueScript.lines[2] = "I can't write a single line of code with this filth around me.";
        dialogueScript.lines[3] = "It's like a constant itch in my mind, and I can't ignore it.";
        dialogueScript.lines[4] = "I need to clean up, or it's going to drive me crazy..";
        dialogueScript.StartDialogue();
    }

    private void PlayNotification()
    {
        notificationSound.Play();
        notificationAnimator.enabled = true;
    }

    private void PlayText2()
    {
        dialogueScript.lines = new string[3];
        dialogueScript.lines[0] = "You know what?";
        dialogueScript.lines[1] = "This is the last time I'm going to be bothered by this.";
        dialogueScript.lines[2] = "I'll clean up once and for all, and then I'll finally have peace to focus on my work.";   
        dialogueScript.StartDialogue();
    }

    private void PlayText3()
    {
        dialogueScript.lines = new string[5];
        dialogueScript.lines[0] = "The echoes of that twisted dimension still linger, but this time, I resist.";
        dialogueScript.lines[1] = "The notification beckons, a reminder of the compulsion I once succumbed to.";
        dialogueScript.lines[2] = "But now... I choose differently.";
        dialogueScript.lines[3] = "The twisted dimension is left behind; the portal remains closed.";
        dialogueScript.lines[4] = "It's a victory.";
        dialogueScript.StartDialogue();
    }

    private void EnableMovement()
    {
        sitScript.stand = true;
    }
}
