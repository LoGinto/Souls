using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCast : MonoBehaviour
{
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        StartCoroutine(CastDamage());
    }

    IEnumerator CastDamage()
    {
        yield return new WaitForSeconds(3f);
        boxCollider.enabled = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Casted to player");
        }
    }
}
