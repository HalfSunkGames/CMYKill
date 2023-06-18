using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType 
{
    C = 1,
    M, 
    Y
};

public class ShotDamage : MonoBehaviour
{
    private float damageRadius = 0.5f;
    [SerializeField]private LayerMask lmask;
    [SerializeField]private LayerMask othersLMask;
    private bool isTouching;
    private bool isTouchingOther;
    private Collider[] enemys;
    private Collider[] otherEnemys;
    [SerializeField]private float shotSpeed;
    [SerializeField] private DamageType damageType;
    // Start is called before the first frame update
    void Awake()
    {       
        switch (damageType)
        {
            case DamageType.C:
                lmask = LayerMask.GetMask("Cyan");
                othersLMask = LayerMask.GetMask("Magenta", "Yellow");
                break;
            case DamageType.M:
                lmask = LayerMask.GetMask("Magenta");
                othersLMask = LayerMask.GetMask("Cyan", "Yellow");
                break;
            case DamageType.Y:
                lmask = LayerMask.GetMask("Yellow");
                othersLMask = LayerMask.GetMask("Magenta", "Cyan");
                break;
            default:
                break;
        }

        Rigidbody shotRB = GetComponent<Rigidbody>();
        shotRB.AddRelativeForce(Vector3.forward * shotSpeed);
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        isTouching = Physics.CheckSphere(transform.position, damageRadius, lmask);
        enemys = Physics.OverlapSphere(transform.position, damageRadius, lmask);
        if (isTouching == true)
        {
            foreach (Collider enemy in enemys)
            {
                Destroy(this.gameObject);
                enemy.GetComponent<EnemyBehaviour>().Damage();
            }
        }  

        isTouchingOther = Physics.CheckSphere(transform.position, damageRadius, othersLMask);
        if (isTouchingOther == true)
        {
            otherEnemys = Physics.OverlapSphere(transform.position, damageRadius, othersLMask);

            //foreach (Collider otherEnemy in otherEnemys)
            //{
            //    Destroy(this.gameObject);
            //    otherEnemy.GetComponent<EnemyBehaviour>().KnockBack();
            //}
        }
            
    }
}
