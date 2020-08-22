using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    Animator animator;
    bool isAttacking;
    public float lastClickedTime = 0;
    public float maxComboDelay = 0.9f;
    public int numberOfClicks = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfClicks = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            lastClickedTime = Time.time;
            numberOfClicks++;
            if(numberOfClicks == 1)
            {
                animator.SetBool("Attack1",true);
            }
            numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);

        }
    }
    public void Return1()
    {
        if (numberOfClicks >= 2)
        {
            animator.SetBool("Attack2", true);
        }
        else
        {
            animator.SetBool("Attack1", false);
            numberOfClicks = 0;
        }
    }
    public void Return2()
    {
        if (numberOfClicks >= 3)
        {
            animator.SetBool("Attack3", true);
        }
        else
        {
            animator.SetBool("Attack2", false);
            numberOfClicks = 0;
        }
    }
    public void Return3()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        numberOfClicks = 0;
    }
    public int GetNumberOfClicks()
    {
        return numberOfClicks;
    }
}
