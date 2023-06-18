using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    //public DamageType dT;
    public float speed;
    [SerializeField]private Transform target;
    private NavMeshAgent navMesh;
    public float knockForce = -50f;
    public bool knock;

    private Bounds bndFloor;
    private Vector3 movePoint;

    private bool right;
    public float strifeMin;

    [SerializeField] private GameObject deathParticle;
    [SerializeField] private GameObject spawnParticle;

    private void Awake()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = speed;

        bndFloor = GameObject.Find("Plane").GetComponent<Renderer>().bounds;
        Instantiate(spawnParticle, transform.position, Quaternion.identity);
        AudioVFXController.Instance.PlayEnemySpawn();

        if (gameObject.layer == 13)
            SetRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == 12)
            if (navMesh.isActiveAndEnabled)
                navMesh.SetDestination(target.position);

        if (gameObject.layer == 13)
        {
            float dist = Vector3.Distance(movePoint, transform.position);
            if (dist < 0.5f)
                SetRandomDestination(); navMesh.speed = speed * 2;
        }

        if (gameObject.layer == 11)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            if (dist < strifeMin)
            {
                navMesh.SetDestination(target.position);
            }
            else
                Strafe();
        }
            

        if (knock == true)
        {
            navMesh.enabled = false;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;

            rb.velocity = Vector3.zero;
            Vector3 backFrom = target.position - this.transform.position;
            transform.LookAt(backFrom);
            rb.AddForce(transform.forward * knockForce);
            knock = false;
        }

        float deadDist = Vector3.Distance(target.position, transform.position);
        if (deadDist <1.1f)
        {
            GameObject.Find("StatusController").GetComponent<PlayerStatus>().Damage();
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
            
    }

    public void Damage()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        ScoreStatus.Instance.killedEnemys++;
        UIController.Instance.SetEnemiesHUD(ScoreStatus.Instance.killedEnemys);
        AudioVFXController.Instance.PlayEnemyDeath();
        Destroy(this.gameObject);
    }

    public void KnockBack()
    {
        knock = true;
        Invoke("EndKnockBack", 0.3f);
    }

    private void EndKnockBack()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        navMesh.enabled = true;
        rb.isKinematic = true;
    }

    private void SetRandomDestination()
    {
        float rx = Random.Range(bndFloor.min.x +5, bndFloor.max.x -5);
        float rz = Random.Range(bndFloor.min.z +5, bndFloor.max.z -5);
        movePoint = new Vector3(rx, this.transform.position.y, rz);
        navMesh.SetDestination(movePoint);
    }

    private void Strafe()
    {
        if (right)
        {
            var offsetPlayer = transform.position - target.transform.position;
            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            navMesh.SetDestination(transform.position + dir);
        }
        else
        {
            var offsetPlayer = target.transform.position - transform.position;
            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            navMesh.SetDestination(transform.position + dir);
        }
    }
}
