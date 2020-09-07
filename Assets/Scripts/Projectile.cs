using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Transform player;
    Vector3 snapPosition;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        snapPosition = player.position;
    }
    private void FixedUpdate()
    {
        transform.LookAt(snapPosition);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("I hit player");
        }
        Destroy(gameObject, 0.5f);
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject,0.5f);
    }
}
