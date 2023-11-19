using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeLock : MonoBehaviour
{
    private int digitOne = 0;
    private int digitTwo = 0;
    private int digitThree = 0;
    private int digitFour = 0;

    public int finalCode = 0;

    [SerializeField] private TextMeshProUGUI textOne;
    [SerializeField] private TextMeshProUGUI textTwo;
    [SerializeField] private TextMeshProUGUI textThree;
    [SerializeField] private TextMeshProUGUI textFour;

    [SerializeField] private GameObject player;
    SUPERCharacterAIO playerController;

    private void Start()
    {
        playerController = player.GetComponent<SUPERCharacterAIO>();
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        textOne.text = "";
        textOne.text += digitOne;

        textTwo.text = "";
        textTwo.text += digitTwo;

        textThree.text = "";
        textThree.text += digitThree;

        textFour.text = "";
        textFour.text += digitFour;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerController.UnpausePlayer();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void ChangeDigit (TextMeshProUGUI digitText)
    {
        if (digitText == textOne)
        {
            if (digitOne == 9) digitOne = 0;
            else digitOne += 1;
        }
        if (digitText == textTwo)
        {
            if (digitTwo == 9) digitTwo = 0;
            else digitTwo += 1;
        }
        if (digitText == textThree)
        {
            if (digitThree == 9) digitThree = 0;
            else digitThree += 1;
        }
        if (digitText == textFour)
        {
            if (digitFour == 9) digitFour = 0;
            else digitFour += 1;
        }

        finalCode = digitOne * 1000 + digitTwo * 100 + digitThree * 10 + digitFour;
        Debug.Log(finalCode);
    }

    public void PauseEverything()
    {
        if (GameObject.Find("InteractText"))
        {
            GameObject.Find("InteractText").SetActive(false);
        }

        player.GetComponent<SUPERCharacterAIO>().PausePlayer(PauseModes.FreezeInPlace);
    }
}
