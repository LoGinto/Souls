using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Souls : MonoBehaviour
{
    public int soulsCount = 1;
    [SerializeField] Text soulsText;
    
    private void Update()
    {
        soulsText.text = soulsCount.ToString();
    }
    public int GetSoulsCount()
    {
        return soulsCount;
    }
    public void IncrementSouls(int souls)
    {
        soulsCount += soulsCount;
    }
    public void NullifySouls()
    {
        soulsCount = 0;
    }
}
