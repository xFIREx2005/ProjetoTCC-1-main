using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Kitsune : EnemyController
{
    public enum chooseState
    {
        atkHorizontal, atkClone, atkFire, stop
    };
    chooseState _chooseState = chooseState.stop;

    public bool atkH;
    public bool atkV;
    public bool ifStop;
    public bool ifFollow;
    public int chooseAtk;
    public float timerEsp;
    public bool ifAtk;
    public int randomFire;

    public Transform posInicial;
    public GameObject kitsuneSpawn;
    public GameObject[] fireKitsune;

    private void Start()
    {
        maxHelth = 300;
        ifDamage = true;
        currentHealth = maxHelth;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        ifStop = true;
        ifFollow = true;

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
            case chooseState.atkHorizontal:
                if (ifAtk == true)
                {
                    ifFollow = true;
                    if (dist <= 2f)
                    {
                        this.transform.LookAt(player.transform);
                        ifFollow = false;
                        anim.SetBool("atkH", true);
                        ifAtk = false;
                    }
                }
                break;

            case chooseState.atkClone:
                ifFollow = false;
                anim.SetBool("clone", true);
                break;

            case chooseState.atkFire:
                ifFollow = false;
                anim.SetBool("fire", true);
                randomFire = Random.Range(0, 5);
                break;

            case chooseState.stop:
                _navMesh.speed = 0;
                anim.SetBool("fire", false);
                anim.SetBool("atkH", false);
                anim.SetBool("walk", false);
                if (ifStop == true)
                {
                    chooseAtk = Random.Range(1, 4);
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
        if (chooseAtk == 1) { _chooseState = chooseState.atkHorizontal; ifAtk = true; }
        if (chooseAtk == 2) { _chooseState = chooseState.atkClone; ifAtk = true; }
        if (chooseAtk == 3) { _chooseState = chooseState.atkFire; ifAtk = true; }
    }

    public void AtkClone()
    {
        kitsuneSpawn.GetComponent<Kitsune_Spawns>().Spawn();
        anim.SetBool("clone", false);
        gameObject.SetActive(false);
    }

    public void SpawnFire()
    {
        fireKitsune[randomFire].SetActive(true);
    }

    public void CancelAtk()
    {
        ifStop = true;
        ifDamage = true;
        _chooseState = chooseState.stop;
    }
}

