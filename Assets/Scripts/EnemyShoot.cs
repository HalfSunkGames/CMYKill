using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    bool canAttack;

    public float attackRate = 1f;

    private Transform canonPos;

    public GameObject canon;
    public GameObject grenade;

    private Quaternion rotation = new Quaternion();

    // Start is called before the first frame update
    void Start()
    {
        canonPos = canon.GetComponent<Transform>();
        canAttack = true;
        Attack();
    }

    // Update is called once per frame
    void Attack()
    {
        if (canAttack && !IsInvoking("AttackDelay"))
        {
            canAttack = false;
            Invoke("AttackDelay", attackRate);
            rotation = canon.transform.rotation;
            Instantiate (grenade, canonPos.position, rotation);
        }
    }

    void AttackDelay()//Se invoca de shot con un delay determinar la cadencia
    {
        if (!canAttack)
        {
            canAttack = true;
            Attack();
        }
        else
        {
            CancelInvoke("AttackDelay");
        }
    }
}
