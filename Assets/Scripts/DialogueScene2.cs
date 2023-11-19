using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScene2 : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    Dialogue dialogueScript;

    private void Start()
    {
        dialogueScript = dialogue.GetComponent<Dialogue>();
        PlayText();
    }

    private void PlayText()
    {
        dialogueScript.lines = new string[11];
        dialogueScript.lines[0] = "Everything is... different. It's like my world, but twisted.";
        dialogueScript.lines[1] = "Distorted...";
        dialogueScript.lines[2] = "I can feel it... every fiber of my being.";
        dialogueScript.lines[3] = "It's like an insatiable need.";
        dialogueScript.lines[4] = "The compulsion to clean everything, it's overwhelming...";
        dialogueScript.lines[5] = "Look at these houses, like mine but not.";
        dialogueScript.lines[6] = "The disorder is palpable, creeping up on me.";
        dialogueScript.lines[7] = "I must clean. It's the only way.";
        dialogueScript.lines[8] = "The only way to make sense of this distorted reflection of my reality.";
        dialogueScript.lines[9] = "Each room echoes with disorder, every object out of place taunts me.";
        dialogueScript.lines[10] = "I know it, deep down, that if I can bring order to this twisted dimension, I can find my way back home.";
        dialogueScript.StartDialogue();
    }
}
