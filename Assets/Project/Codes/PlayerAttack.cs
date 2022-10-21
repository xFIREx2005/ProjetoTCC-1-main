using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 0.9f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemylayers;

    public int attackDamage = 20;
    public bool attackHit;

    public bool rotateEnemy;
    public Transform enemyTrans;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if(Input.GetMouseButtonDown(0))
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if(noOfClicks == 1)
            {
                attackDamage = 20;
                PlayerController.mov = false;
                anim.SetBool("running", false);
                anim.SetBool("sprint", false);
                anim.SetBool("1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
        

        if(attackHit == true)
        {
            attack();
        }

        if(rotateEnemy == true)
        {
            RotateEnemy();
        }
    }

    public void RotateEnemy()
    {
        if (enemyTrans)
        {
            Vector3 pos = enemyTrans.position - transform.position;
            pos.y = 0;

            Quaternion rot = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 4 * Time.deltaTime);
        }
    }

    public void return1()
    {
        if(noOfClicks >= 2)
        {
            attackDamage = 25;
            anim.SetBool("2", true);
        }
        else
        {
            anim.SetBool("1", false);
            noOfClicks = 0;
        }
    }
    public void return2()
    {
        if (noOfClicks >= 3)
        {
            attackDamage = 35;
            anim.SetBool("3", true);
        }
        else
        {
            anim.SetBool("2", false);
            anim.SetBool("1", false);
            noOfClicks = 0;
        }
        
    }
    public void return3()
    {
            anim.SetBool("3", false);
            anim.SetBool("2", false);
            anim.SetBool("1", false);
            noOfClicks = 0;
    }


    public void attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemylayers);
        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
            enemyTrans = enemy.transform;
            rotateEnemy = true;
            StartCoroutine(RotateTime());
        }
    }

    public IEnumerator RotateTime()
    {
        yield return new WaitForSeconds(1);
        rotateEnemy = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        
    }

    public void AttackTrue()
    {
        attackHit = true;
    }
    public void AttackFalse()
    {
        attackHit = false;
    }

    public void MovTrue()
    {
        PlayerController.mov = true;
    }
    public void MovFalse()
    {
        PlayerController.mov = false;
    }
    
}
