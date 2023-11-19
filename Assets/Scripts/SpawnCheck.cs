using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnCheck : MonoBehaviour
{
    [SerializeField] private GameObject skillCheck;
    public float skillCheckSpeed = 0;

    public float passMultiplier = 0.1f;
    public int passStreak = 1;
    public float failMultiplier = 0.1f;
    public int failStreak = 1;

    [SerializeField] private TextMeshProUGUI speedRecord;
    private float skillCheckRecord = 0;

    private void Update()
    {
        if (skillCheckSpeed >= skillCheckRecord)
        {
            skillCheckRecord = skillCheckSpeed;
        }

        speedRecord.text = "Skill Record: " + (skillCheckRecord + 200);
    }

    public void StartSkillCheck()
    {
        var newCheck = Instantiate(skillCheck);
        newCheck.GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
