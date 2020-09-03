using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnabler : MonoBehaviour
{
    public GameObject weaponToEnable;
    public void TurnOn()
    {
        weaponToEnable.GetComponent<BoxCollider>().enabled = true;
    }
    public void TurnOff()
    {
        weaponToEnable.GetComponent<BoxCollider>().enabled = false;
    }
}
