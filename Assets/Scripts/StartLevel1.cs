using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel1 : MonoBehaviour
{
    public bool enableOutline = false;
    private bool isDialogue = false;

    [SerializeField] private GameObject dialogue;
    Dialogue dialogueScript;

    private void Start()
    {
        dialogueScript = dialogue.GetComponent<Dialogue>();
    }

    private void OnTriggerEnter(Collider other)
    {
        enableOutline = true;
        //Destroy(gameObject);

        if (gameObject.name == "Start Level 2" && GameObject.FindGameObjectsWithTag("Unorganized 2").Length > 0)
        {
            Camera.main.GetComponent<MirrorEffect>().on = true;
        }

        if (gameObject.name == "Start Level 3" && GameObject.FindGameObjectsWithTag("Unorganized 3").Length > 0)
        {
            RenderSettings.fog = true;

            Camera.main.GetComponent<RippleEffect>().strength = 0.005f;
            Camera.main.GetComponent<RippleEffect>().amount = 11;
            Camera.main.GetComponent<RippleEffect>().speed = 9.5f;

            Camera.main.GetComponent<HueSaturationEffect>().speed = 10;

            if (!isDialogue)
            {
                PlayText();
                isDialogue = true;
            }
        }
    }

    private void PlayText()
    {
        dialogueScript.lines = new string[3];
        dialogueScript.lines[0] = "The last house... The rooms here are not just untidy, they're corrupted.";
        dialogueScript.lines[1] = "Cleaning takes on a new meaning here, a desperate attempt to purify, to erase the stains of darkness.";
        dialogueScript.lines[2] = "I must find my way to the garage...";
        dialogueScript.StartDialogue();
    }
}
