using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WizardEnemy : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;
    [SerializeField] private float spottingDist;
    [SerializeField] private float farAttackDistance;
    [SerializeField] private float tooClose;
    [SerializeField] private float coolDown;
    [SerializeField] private float areaCastCooldown = 5f;
    [SerializeField] Transform boneOfProjectile;
    float activeCooldown;
    float areaCooldown;
    public GameObject[] projectiles;
    public GameObject areaCastEffect;
    int projectileIndex;
    public enum State
    {
        Calm, Agressive
    }
    public State state = State.Calm;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();  
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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
            if (CloseTo(player,farAttackDistance) && !CloseTo(player,tooClose))
            {
                RangeAttack();
            }
            else if (CloseTo(player, tooClose) && CloseTo(player, farAttackDistance))
            {
                NearbyBehavior();
            }
        }
    }
    void RangeAttack()
    {
        Debug.Log("Range attack");
        if (activeCooldown > 0f)
        {
            activeCooldown -= 1f * Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("Cast");
            activeCooldown = coolDown;
        }

    }
    public void CastProjectile()
    {
        projectileIndex++;
        if (projectileIndex >= projectiles.Length)
        {
            projectileIndex = 0;
        }
        GameObject instance =Instantiate(projectiles[projectileIndex], boneOfProjectile.position, Quaternion.identity);
        Destroy(instance, 2.5f);
    }
    void NearbyBehavior()
    {
        Debug.Log("Close behavior");
        if (areaCooldown > 0f)
        {
            areaCooldown -= 1f * Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("AreaCast");
            areaCooldown = areaCastCooldown;
        }
        
    }
    public void AreaCast()
    {
        GameObject castInstance = Instantiate(areaCastEffect, boneOfProjectile.position, Quaternion.identity);
        Destroy(castInstance, 3.5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spottingDist);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, tooClose);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, farAttackDistance);
    }
    private bool CloseTo(Transform obj,float byDistance)
    {
        return Vector3.Distance(transform.position, obj.position) <= byDistance;
    }
}
