using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_MongeLevel1 : EnemyController
{
    public enum chooseState
    {
        atkEstocada, atkBey, stop
    };
    chooseState _chooseState = chooseState.stop;

    public bool ifStop;
    public bool ifFollow;
    public int chooseAtk;
    public float timerEsp;
    public bool ifAtk;

    private void Start()
    {
        maxHelth = 300;
        ifDamage = true;
        currentHealth = maxHelth;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        ifStop = true;
        ifFollow = false;

    }

    private void Update()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (ifAttack == true)
        {
            Attack();
        }
        if (ifFollow == true)
        {
            Follow();
        }
        else
        {
            _navMesh.speed = 0;
            anim.SetBool("walk", false);
        }
        States();
        HealthBar();
    }

    public void Follow()
    {
        _navMesh.speed = 2;
        _navMesh.SetDestination(player.transform.position);
        anim.SetBool("walk", true);
    }

    public void States()
    {
        switch (_chooseState)
        {
            case chooseState.atkEstocada:
                if (ifAtk == true)
                {
                    ifFollow = true;
                    if (dist <= 2f)
                    {
                        this.transform.LookAt(player.transform);
                        ifFollow = false;
                        anim.SetBool("estoc", true);
                        ifAtk = false;
                    }
                }
                break;

            case chooseState.atkBey:
                if (ifAtk == true)
                {
                    anim.SetBool("bey", true);
                    StartCoroutine(TimerBey());
                    ifAtk = false;
                }
               
                break;

            case chooseState.stop:
                _navMesh.speed = 0;
                anim.SetBool("bey", false);
                anim.SetBool("estoc", false);
                if (ifStop == true)
                {
                    chooseAtk = Random.Range(1, 3);
                    timerEsp = Random.Range(2, 4);
                    StartCoroutine(TimerEsp());
                    ifStop = false;
                }
                break;
        }
    }

    public IEnumerator TimerEsp()
    {
        Debug.Log("timerEsp");
        yield return new WaitForSeconds(timerEsp);
        if (chooseAtk == 1) { _chooseState = chooseState.atkEstocada; ifAtk = true; }
        if (chooseAtk == 2) { _chooseState = chooseState.atkBey; ifAtk = true; }
    }
    public IEnumerator TimerBey()
    {
        yield return new WaitForSeconds(Random.Range(3,6));
        CancelAtk();
    }
    public void CancelAtk()
    {
        ifStop = true;
        ifDamage = true;
        _chooseState = chooseState.stop;
    }
}
