using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSouls : MonoBehaviour
{
    public int soulsLost;
    Souls souls;
    public void SetSoulsLost(int value)
    {
        soulsLost = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            souls = other.gameObject.GetComponent<Souls>();
            souls.IncrementSouls(soulsLost);
            Destroy(gameObject, 1f);
        }
    }
}
