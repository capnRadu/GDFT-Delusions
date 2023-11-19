using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeSound : MonoBehaviour
{
    [SerializeField] private GameObject fridgeDoor;
    [SerializeField] private AudioSource fridgeSound;
    [SerializeField] private Light fridgeLight;

    Door doorScript;

    private void Start()
    {
        doorScript = fridgeDoor.GetComponent<Door>();
    }

    public void FridgePower()
    {
        if (!doorScript.doorOpen)
        {
            fridgeSound.Play();
            fridgeLight.intensity = 1;
        }
        else if (doorScript.doorOpen)
        {
            fridgeSound.Stop();
            fridgeLight.intensity = 0;
        }
    }
}
