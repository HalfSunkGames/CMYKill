using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField] private DamageType damageType;
    [SerializeField] private LayerMask lmask;
    [SerializeField] private LayerMask othersLMask;
    [SerializeField]private Transform meleeStart;
    [SerializeField]private Transform meleeEnd;
    private bool isTouching;
    private bool isTouchingOthers;
    private Collider[] enemys;
    private Collider[] otherEnemys;
    private float damageRadius = 2.3f;
    [SerializeField] private GameObject damageView;
    [SerializeField] private List <GameObject> trailParticles;
    [SerializeField] private ParticleSystem[] sparksC;
    [SerializeField] private ParticleSystem[] sparksM;
    [SerializeField] private ParticleSystem[] sparksY;

    private bool isAttacking = false;
    // Start is called before the first frame update
    void Awake()
    {
        trailParticles[0].SetActive(false);
        trailParticles[1].SetActive(false);
        trailParticles[2].SetActive(false);
    }

    public void GetType(DamageType dType)
    {
        switch (dType)
        {
            case DamageType.C:
                lmask = LayerMask.GetMask("Cyan");
                othersLMask = LayerMask.GetMask("Magenta", "Yellow");
                damageType = DamageType.C;
                break;
            case DamageType.M:
                lmask = LayerMask.GetMask("Magenta");
                othersLMask = LayerMask.GetMask("Cyan", "Yellow");
                damageType = DamageType.M;
                break;
            case DamageType.Y:
                lmask = LayerMask.GetMask("Yellow");
                othersLMask = LayerMask.GetMask("Magenta", "Cyan");
                damageType = DamageType.Y;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            isTouching = Physics.CheckCapsule(meleeStart.position, meleeEnd.position, damageRadius, lmask);
            
            if (isTouching == true)
            {
                enemys = Physics.OverlapCapsule(meleeStart.position, meleeEnd.position, damageRadius, lmask);
                foreach (Collider enemy in enemys)
                {
                    enemy.GetComponent<EnemyBehaviour>().Damage();
                }
            }

            isTouchingOthers = Physics.CheckCapsule(meleeStart.position, meleeEnd.position, damageRadius, othersLMask);

            if (isTouchingOthers == true)
            {
                otherEnemys = Physics.OverlapCapsule(meleeStart.position, meleeEnd.position, damageRadius, othersLMask);

                //foreach (Collider otherEnemy in otherEnemys)
                //{                   
                //    otherEnemy.GetComponent<EnemyBehaviour>().KnockBack();
                //}
            }
        }


    }

    public void Melee()
    {
        isAttacking = true;
        //MeleeArea.Instance.StartAttack();
        Invoke("EndMelee", 0.5f);




        switch (damageType)
        {
            case DamageType.C:
                damageView.GetComponent<Renderer>().material.color = new Color(0, 1, 1, 0.3f);
                trailParticles[0].SetActive(true);
                foreach (ParticleSystem spark in sparksC)
                {
                    spark.Play();
                }
                break;
            case DamageType.M:
                damageView.GetComponent<Renderer>().material.color = new Color(1, 0, 1, 0.3f);
                trailParticles[1].SetActive(true);
                foreach (ParticleSystem spark in sparksM)
                {
                    spark.Play();
                }
                break;
            case DamageType.Y:
                damageView.GetComponent<Renderer>().material.color = new Color(1, 0.92f, 0.016f, 0.3f);
                trailParticles[2].SetActive(true);
                foreach (ParticleSystem spark in sparksY)
                {
                    spark.Play();
                }
                break;
            default:
                break;
        }
    }

    void EndMelee() // Esto será por animacion en lugar de por Invoke
    {
        trailParticles[0].SetActive(false);
        trailParticles[1].SetActive(false);
        trailParticles[2].SetActive(false);
        isAttacking = false;
    }
}
