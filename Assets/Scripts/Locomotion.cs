using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    Animator animator;
    Camera myCamera;
    [SerializeField] float speed = 2f;
    CharacterController characterController;
    Melee melee;
    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        melee = GetComponent<Melee>();
    }

    // Update is called once per frame
    void Update()
    {
        if (melee.GetNumberOfClicks() == 0)
        {
            Move();
        }
       
    }
    void Move()
    {
        Camera kamera = myCamera;
        float verticalAxis = Input.GetAxis("Vertical");//varies between 1 and -1
        float horizontalAxis = Input.GetAxis("Horizontal");
        //create a new vector
        Vector3 cameraForward = Vector3.Scale(kamera.transform.forward, new Vector3(1, 0, 1)).normalized; // forward direction in 2d
        Vector3 updatedVector = verticalAxis * cameraForward + horizontalAxis * kamera.transform.right;
        transform.LookAt(updatedVector + transform.position);
        Vector3 actualMovement = updatedVector * speed * Time.deltaTime;
        animator.SetBool("Run", true);
        characterController.Move(actualMovement);//actual movement
        if (verticalAxis == 0 && horizontalAxis == 0)//if player doesn't move
        {           
            animator.SetBool("Run", false);
        }
    }
}
