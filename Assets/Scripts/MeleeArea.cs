using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeArea : MonoBehaviour
{
    public static MeleeArea Instance;
    [SerializeField] private Transform mStart;
    [SerializeField] private Transform mEnd;
    public float step = 0.5f;
    private bool goAttack = false;


    private void Awake()
    {
        Instance = this;
    }

    public void StartAttack()
    {
        StartCoroutine(AttackAnim());
    }

    private IEnumerator AttackAnim()
    {
        goAttack = true;
        yield return new WaitForSeconds(0.4f);
        goAttack = false;
    }

    private void Update()
    {
        if (goAttack)
            Attack();
        else
            GoBack();

    }
    private void Attack()
    {
        transform.position = Vector3.MoveTowards(mStart.position, mEnd.position, step);
    }

    private void GoBack()
    {
        transform.position = mStart.position;
    }

}
