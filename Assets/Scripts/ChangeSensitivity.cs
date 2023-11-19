using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeSensitivity : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sensitivityText;
    private bool pressKey = false;

    SUPERCharacterAIO characterController;

    private void Start()
    {
        characterController = GetComponent<SUPERCharacterAIO>();
        sensitivityText.text = "";
        sensitivityText.text += characterController.Sensitivity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp) && characterController.Sensitivity < 20 && !pressKey)
        {
            pressKey = true;
            sensitivityText.gameObject.SetActive(true);
            characterController.Sensitivity += 1;
            sensitivityText.text = "";
            sensitivityText.text += characterController.Sensitivity;
            StartCoroutine(HideSensitivityText());
        }
        else if (Input.GetKeyDown(KeyCode.PageDown) && characterController.Sensitivity > 1 && !pressKey)
        {
            pressKey = true;
            sensitivityText.gameObject.SetActive(true);
            characterController.Sensitivity -= 1;
            sensitivityText.text = "";
            sensitivityText.text += characterController.Sensitivity;
            StartCoroutine(HideSensitivityText());
        }

        if (Input.GetKeyDown(KeyCode.C) && GameObject.Find("Dialogue"))
        {
            GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>().text = "";
            GameObject.Find("Dialogue").SetActive(false);
        }
    }

    IEnumerator HideSensitivityText()
    {
        yield return new WaitForSeconds(0.3f);

        sensitivityText.gameObject.SetActive(false);
        pressKey = false;
    }
}
