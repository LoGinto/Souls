using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToBoss : MonoBehaviour
{
    bool passed = false;
    Transform player;
    Boss boss;
    [SerializeField] GameObject wall;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wall.SetActive(false);
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wall.SetActive(true);
            boss.GetCanvasGroup().alpha = 1f;
            passed = true;
            Debug.Log("passed to boss");
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.7f);
    }
    public bool GetPass()
    {
        return passed;
    }
    public void SetPass(bool value)
    {
        passed = value;
    }
}
