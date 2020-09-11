using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectiles : MonoBehaviour
{
    Transform player;
    Vector3 snapPosition;
    [SerializeField] float speed = 3f;
    [SerializeField] float waitTime = 2.5f;
    bool casted = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Waito());
    }
    IEnumerator Waito()
    {
        yield return new WaitForSeconds(waitTime);
        snapPosition = player.position;
        casted = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       if(casted == true)
        {
            transform.LookAt(snapPosition);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("I hit player");
        }
        Destroy(gameObject, 0.5f);
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 0.5f);
    }
}
