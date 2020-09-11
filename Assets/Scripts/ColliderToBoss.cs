using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToBoss : MonoBehaviour
{
    bool passed = false;
    Transform player;
    [SerializeField] ColliderToBoss toBoss;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (!passed)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), player.GetComponent<Collider>(),true);           
        }
        else
        {
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), player.GetComponent<Collider>(), false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            toBoss.SetPass(true);
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
