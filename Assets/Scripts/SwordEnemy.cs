using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class SwordEnemy : MonoBehaviour
{
    //pos -0.03  -0.036 -0.035
    //rot   -119.845  140.209 118.811
    bool drawn = false;
    NavMeshAgent nav;
    Animator animator;
    [SerializeField] float spottingDistance;
    Transform player;
    Vector3 localPos;
    Vector3 localRot;
    Rigidbody rb;
    [SerializeField] Transform sword;
    [SerializeField] Transform bone;
    [SerializeField] RuntimeAnimatorController weaponController;
    [SerializeField] float closeAttackDist;
    [SerializeField] float dashSpeed = 3f;
    [SerializeField] float dashTime = 4f;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] float farAttack;
    float activeCooldown;
    bool isaWay;
    Vector3 pos;    
    float thisCoolDow = 0;
    public enum State
    {
        Calm, Agressive
    }
    public State state = State.Calm;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        localPos = new Vector3(-0.03f, -0.036f, -0.035f);
        localRot = new Vector3(-119.845f, 140.209f, 118.811f);
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (CloseTo(player, spottingDistance))
        {
            DrawSword();
            state = State.Agressive;
        }
        if(player.GetComponent<Health>().DeathState() == true)
        {
            state = State.Calm;
        }
        if (state == State.Agressive)
        {            
            isaWay = Vector3.Distance(transform.position, player.position) > spottingDistance;
            if (isaWay)
            {
                Dash();
            }
            else
            {
                AttackBehavior();
            }
            Debug.Log(isaWay);
            if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Great Sword High Spin Attack 1"))
            {
                nav.SetDestination(player.position * dashSpeed);
            }
        }
        if(state == State.Calm)
        {
            animator.SetTrigger("Idle");
        }
    }
        void Dash()
        {
        transform.LookAt(player);
        nav.isStopped = false;
        if (!(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Stand To Roll 1")))
        {
            animator.SetTrigger("Roll");
        }
        else
        {
         nav.SetDestination(player.position * dashSpeed);
        }
    }
    public void RollAnimEnemy()
    {
        if (isaWay)
        {
            animator.SetTrigger("Roll");
        }
    }
        void DrawSword()
        {
            if (drawn == false)
            {
                animator.SetTrigger("Draw");
                animator.runtimeAnimatorController = weaponController as RuntimeAnimatorController;
                sword.parent = bone.transform;
                sword.transform.localPosition = localPos;
                sword.transform.localEulerAngles = localRot;
                drawn = true;
            }
            //animator.ResetTrigger("Draw");
        }
        void AttackBehavior()
        {
            transform.LookAt(player);

            if (activeCooldown > 0f)
            {
                activeCooldown -= 1f * Time.deltaTime;
            }
            else
            {
                if (CloseTo(player, closeAttackDist))
                {
                    animator.SetTrigger("AttackClose");
                }
                else
                {
                    animator.SetTrigger("AttackFar");                    
                }
                activeCooldown = attackCooldown;
            }

        }
    

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spottingDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, closeAttackDist);
    }
        private Vector3 GetPos()
        {
            return player.position;
        }
        private bool CloseTo(Transform selectedObj, float dist)
        {
            return Vector3.Distance(transform.position, selectedObj.position) <= dist;
        }

}

