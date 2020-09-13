using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public enum Stages
    {
        NoStage,Stage1,Stage2,Stage3
    }
    public Stages stage = Stages.NoStage;
    [Header("UI")]
    public Image healthBar;
    [Header("Floats")]
    public float currentHealth = 100f;
    public float farAttackDist = 10f;
    public float closeAttackDist = 5f;
    public float farAttackCD = 5f;
    public float closeAttackCD = 3f;
    [Header("GameObjects")]
    public GameObject stage1projectile;
    public GameObject stage2Fork;
    [Header("Transforms")]
    public Transform instanceTransform;
    public Transform bone;
    //private
    Transform player;
    ColliderToBoss toBossColl;
    private float maxHealth;
    CanvasGroup canvasGroup;
    float actualFarAttackCD;
    float actualCloseAttackCD;
    bool enteredArena = false;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth;
        canvasGroup = healthBar.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        toBossColl = GameObject.FindGameObjectWithTag("CollBoss1").GetComponent<ColliderToBoss>();
        healthBar = GetComponent<Image>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar == null)
        {
            healthBar = GameObject.FindGameObjectWithTag("HUDIM").GetComponent<Image>();
        }
        StageSwitch();
        if(stage == Stages.Stage1)
        {
            Stage1Behavior();
        }
        if(stage == Stages.Stage2)
        {
            Stage2Behavior();
        }
        if(stage == Stages.Stage3)
        {
            Stage3Behaviour();
        }
    }
    void Stage1Behavior()
    {
        //rotate towards player
        RotateTowardsPlayer(true);
        if(CloseTo(player,farAttackDist) && !CloseTo(player, closeAttackDist))
        {
            //instantiate stuff here
            if (actualFarAttackCD > 0f)
            {
                actualFarAttackCD -= 1f * Time.deltaTime;
            }
            else
            {
                int randAreaCastChance = Random.Range(1, 8);
                if (randAreaCastChance != 5)
                {
                    animator.SetTrigger("Cast");
                }
                else
                {
                    animator.SetTrigger("AreaCast");
                }
                actualFarAttackCD = farAttackCD;
            }           
        }
        else if (CloseTo(player, farAttackDist) && CloseTo(player, closeAttackDist))
        {
            if (actualCloseAttackCD> 0f)
            {
                RotateTowardsPlayer(true);
                actualCloseAttackCD -= 1f * Time.deltaTime;
            }
            else
            {
                RotateTowardsPlayer(false);
                int randomNum = Random.Range(1, 3);
                if(randomNum != 1) {
                    animator.SetTrigger("atck1");
                }
                else
                {
                    animator.SetTrigger("atck2");
                }
                
                actualCloseAttackCD= closeAttackCD;
            }
        }
    }
    public void CastStage1Projectiles()
    {
        Instantiate(stage1projectile,instanceTransform.position,Quaternion.identity);
    }
    void RotateTowardsPlayer(bool isTrue) {
        if (isTrue)
        {
            Vector3 relativePos = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;
        }
        else
        {
            transform.LookAt(this.gameObject.transform.forward);
        }
    }

    void Stage2Behavior()
    {
        //takes out his fork
    }
    void Stage3Behaviour()
    {

    }
    void StageSwitch()
    {
        if (!enteredArena)
        {
          if(toBossColl.GetPass() == true)
            {
                enteredArena = true;
                stage = Stages.Stage1;
            }  
        }
    }
    private bool CloseTo(Transform obj, float byDistance)
    {
        return Vector3.Distance(transform.position, obj.position) <= byDistance;
    }
    public CanvasGroup GetCanvasGroup()
    {
        return canvasGroup;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position,closeAttackDist);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, farAttackDist);
    }
}
