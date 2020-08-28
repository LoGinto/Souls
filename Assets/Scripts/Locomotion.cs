using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    Animator animator;
    Camera myCamera;
    [SerializeField] float speed = 2f;
    [SerializeField] float delayBeforeInvinsible = 0.1f;
    [SerializeField] float invisibleDuration = 1f;
    CharacterController characterController;
    Melee melee;
    [SerializeField] float coolDown = 1f;
    float activeCooldown;
    bool isRollin;
    Health health;
    bool space;
    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        melee = GetComponent<Melee>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (melee.GetNumberOfClicks() == 0)
        {
            if (!isRollin)
            {
                Move();
            }
            Roll();  
        }
       space = Input.GetKey(KeyCode.Space);
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Stand To Roll"))
        {
            AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);
            health.Invinsible(delayBeforeInvinsible,currInfo.normalizedTime-delayBeforeInvinsible);
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
        if (verticalAxis == 0 && horizontalAxis == 0 || isRollin)//if player doesn't move
        {           
            animator.SetBool("Run", false);
        }
    }
    void Roll()
    {
        Camera kamera = myCamera;
        float verticalAxis = Input.GetAxis("Vertical");//varies between 1 and -1
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector3 cameraForward = Vector3.Scale(kamera.transform.forward, new Vector3(1, 0, 1)).normalized; // forward direction in 2d
        Vector3 updatedVector = verticalAxis * cameraForward + horizontalAxis * kamera.transform.right;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRollin = true;            
            animator.SetBool("Run", false);
            if (activeCooldown <= 0)
            {
                if (!(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Stand To Roll")))
                {
                    if (verticalAxis != 0 || horizontalAxis != 0)
                    {
                        animator.SetTrigger("Roll");
                        transform.LookAt(updatedVector + transform.position);
                    }
                }
                
            }
            else
            {
                activeCooldown -= Time.deltaTime;
            }
        }
        else
        {
            isRollin = false;
        }
        
    }
    
    public void RollAnim()
    {
        if (space)
        {
            animator.SetTrigger("Roll");
        }
    }
}
