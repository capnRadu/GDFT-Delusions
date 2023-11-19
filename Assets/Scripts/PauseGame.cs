using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPause = false;
    private bool isMirror = false;

    [SerializeField] private GameObject dialogue;
    private bool isDialogue = false;

    [SerializeField] private GameObject speedRecord;
    private bool isRecord = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause && !GameObject.FindWithTag("Skill Check") && !GameObject.FindWithTag("Lock"))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            AudioListener.pause = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPause = true;

            if (Camera.main.GetComponent<MirrorEffect>().on)
            {
                Camera.main.GetComponent<MirrorEffect>().on = false;
                isMirror = true;
            }

            if (dialogue.gameObject.activeSelf)
            {
                dialogue.GetComponent<TextMeshProUGUI>().enabled = false;
                isDialogue = true;
            }

            if (speedRecord && speedRecord.gameObject.activeSelf)
            {
                speedRecord.SetActive(false);
                isRecord = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            AudioListener.pause = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPause = false;

            if (isMirror)
            {
                Camera.main.GetComponent<MirrorEffect>().on = true;
                isMirror = false;
            }

            if (isDialogue)
            {
                dialogue.GetComponent<TextMeshProUGUI>().enabled = true;
                isDialogue = false;
            }

            if (isRecord)
            {
                speedRecord.SetActive(true);
                isRecord = false;
            }
        }
    }
}
