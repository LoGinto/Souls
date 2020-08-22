using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDraw : MonoBehaviour
{
    Animator animator;
    public Transform hammer;
    public Transform bone;
    bool drawn;
    [SerializeField] Transform chest;
    Vector3 localPos;
    Vector3 euler;
    Vector3 noWeaponLocalPos;
    Vector3 noWeaponEuler;
    public RuntimeAnimatorController swordOvverride;
    public RuntimeAnimatorController noWeapon;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        localPos = new Vector3(0.067f, -0.002f, -0.02f);
        euler = new Vector3(-31.342f, 252.869f, -5.85f);
        noWeaponLocalPos = new Vector3(-0.08749656f, 0.1914029f, -0.368622f);
        noWeaponEuler = new Vector3(-0.624f, 111.357f, 86.635f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            drawn = !drawn;
            if (drawn)
            {
                animator.runtimeAnimatorController = swordOvverride as RuntimeAnimatorController;
                animator.SetTrigger("Draw");
                hammer.parent = bone.transform;
                hammer.transform.localPosition = localPos;
                hammer.transform.localEulerAngles = euler;
                //animator.runtimeAnimatorController = swordOvverride as RuntimeAnimatorController;       
            }
            else
            {
                animator.runtimeAnimatorController = noWeapon as RuntimeAnimatorController;
                animator.SetTrigger("Draw");                                
            }          
        }
        
    }    
    public void Sheathe()
    {
        //animation event
        hammer.parent = chest.transform;
        hammer.transform.localPosition = noWeaponLocalPos;
        hammer.transform.localEulerAngles = noWeaponEuler;
    }
}
