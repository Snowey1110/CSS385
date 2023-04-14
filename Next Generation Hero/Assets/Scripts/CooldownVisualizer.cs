using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownVisualizer : MonoBehaviour
{
    public float cooldownDuration = 0.2f;
    private Image cooldownImage;
    private float cooldownEndTime;
    void Start()
    {
        cooldownImage = GetComponent<Image>();
        cooldownEndTime = Time.time;
    }

    void Update()
    {
        if (Time.time < cooldownEndTime)
        {
            cooldownImage.fillAmount = 1 - (cooldownEndTime - Time.time) / cooldownDuration;
        }
        else
        {
            cooldownImage.fillAmount = 0;
        }
    }

    public void StartCooldown()
    {
        cooldownEndTime = Time.time + cooldownDuration;
    }
}
