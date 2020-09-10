using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NinjaEnemy : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;
    Rigidbody rb;
    [SerializeField] private float spottingDist = 5f;
    [SerializeField] private float tooClose = 2f;
    [SerializeField] private float dashSpeed = 6f;
    [SerializeField] float attackCD = 4f;
    float actualCoolDown;
    bool dashed;
    public enum State
    {
        Calm, Agressive
    }
    public State state = State.Calm;
    // Start is called before the first frame update
    void Start()
    {
        dashed = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CloseTo(player, spottingDist))
        {
            state = State.Agressive;
        }
        if(state == State.Agressive)
        {
            //transform.LookAt(Vector3.Scale(player.position, new Vector3(1, 1, 0)));          
            transform.LookAt(player);
            if (!CloseTo(player, spottingDist))
            {
                dashed = false;
            }
            if (dashed&&CloseTo(player,tooClose))
            {
                Attack();
            }
            if (CloseTo(player, spottingDist) && !CloseTo(player, tooClose))
            {
                agent.isStopped = false;
                animator.SetBool("Walk", true);
                agent.SetDestination(player.position);

            }
            else if (CloseTo(player, tooClose))
            {
                agent.isStopped = true;
                animator.SetBool("Walk", false);
            }
        }
    }
   
    void Attack()
    {
        if (actualCoolDown > 0f)
        {
            actualCoolDown -= 1f * Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("SwordCombo");
            actualCoolDown = attackCD;
        }
        
    }
    void FixedUpdate()
    {
        if (state == State.Agressive)
        {
            if (!CloseTo(player, tooClose) && CloseTo(player, spottingDist))
            {
                if (!dashed)
                {
                    Dash();
                }
            }
            if (CloseTo(player, tooClose) && !dashed)
            {
                animator.SetTrigger("SwordCast");
                dashed = true;
            }
        }
    }
    void Dash()
    {
        Vector3 pos = player.position - transform.position;
        animator.SetTrigger("ReadyToDash");
        rb.AddForce(pos * dashSpeed * Time.deltaTime, ForceMode.Impulse);       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spottingDist);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, tooClose);
    }
    private bool CloseTo(Transform obj, float byDistance)
    {
        return Vector3.Distance(transform.position, obj.position) <= byDistance;
    }
    private void OnCollisionStay(Collision collision)
    {
        rb.angularVelocity = new Vector3(0,0,0);   
    }
}
