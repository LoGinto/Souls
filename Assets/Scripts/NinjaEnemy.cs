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
            transform.LookAt(player);
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
}
