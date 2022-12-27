using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class newenemy : MonoBehaviour
{
    public EnemyStatus status; // !!!!!! <------- 
    public Transform target;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject healPool;

    public float maxHp = 100;
    public float hp = 100;

    private NavMeshAgent agent;
    private IEnumerator fireCoroutine;
    private bool isAttacking = false;

    public int healCount = 2;


   
    public enum EnemyStatus
    {
        Idle, Chase, Attack, Escape, Heal, GetHeals
    }

    void Start()
    {
        
        status = EnemyStatus.Idle;

        
        agent = GetComponent<NavMeshAgent>();

       
        fireCoroutine = FireBullet();
    }

    void Update()
    {
        

        
        float d = Vector3.Distance(transform.position, target.transform.position);
        

        
        if (status == EnemyStatus.Idle)
        {
           
            if (d < 10)
            {
                status = EnemyStatus.Chase;
                return;
            }

            
            if (hp < 50)
            {
                status = EnemyStatus.Heal;
                return;
            }

           
            Idle();
        }

        
        if (status == EnemyStatus.Chase)
        {
           
            if (d > 12)
            {
                status = EnemyStatus.Idle;
                return;
            }

           
            if (hp < 50)
            {
                status = EnemyStatus.Heal;
                return;
            }

           
            if (d < 5)
            {
                status = EnemyStatus.Attack;
                return;
            }

           
            Chase();
        }

       
        if (status == EnemyStatus.Attack)
        {
        
            if (d > 5)
            {
                status = EnemyStatus.Chase;
                StopCoroutine(fireCoroutine);
                isAttacking = false;
                return;
            }

            
            if (hp < 50)
            {
                status = EnemyStatus.Heal;
                StopCoroutine(fireCoroutine);
                isAttacking = false;
                return;
            }

           
            Attack();
        }

        
        if (status == EnemyStatus.Escape)
        {
            Escape();
        }

      
        if (status == EnemyStatus.Heal)
        {
           
            if (hp >= 100)
            {
                status = EnemyStatus.Idle;
                return;
            }

            
            if (healCount == 0)
            {
                status = EnemyStatus.GetHeals;
                return;
            }

           
            Heal();
        }

    
        if (status == EnemyStatus.GetHeals)
        {
           
            if (healCount >= 2)
            {
                status = EnemyStatus.Idle;
                return;
            }

            
            GetHeals();
        }
    }

   
    private void Idle()
    {
        agent.isStopped = true;
    }

   
    private void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    
    private void Attack()
    {
        agent.isStopped = true;

        
        transform.LookAt(target);

        if (!isAttacking)
        {
            StartCoroutine(fireCoroutine);
            isAttacking = true;
        }
    }

    IEnumerator FireBullet()
    {
       
        while (true)
        {
           
            Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);

           
            yield return new WaitForSeconds(1);
        }
    }

    
    private void Escape()
    {

    }

  
    private void GetHeals()
    {
        agent.isStopped = false;
        agent.SetDestination(healPool.transform.position);
    }

    private void Heal()
    {
        if (healCount > 0)
        {
            hp = 100;
            healCount = healCount - 1;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            hp = hp - 10;

            if (hp < 0)
            {
                Destroy(gameObject);
            }
        }

        if (other.tag == "HealPool")
        {
            healCount = 2;
        }
    }




}
