using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    Image staminaBar;
    private float currentStamina;
    private float maxStamina;
    Locomotion locomotion;
    // Start is called before the first frame update
    void Start()
    {
        staminaBar = GetComponent<Image>();
        locomotion = FindObjectOfType<Locomotion>();
        maxStamina = locomotion.GetStamina();
    }

    // Update is called once per frame
    void Update()
    {
        currentStamina = locomotion.GetStamina();
        staminaBar.fillAmount = currentStamina /maxStamina;
    }
}
