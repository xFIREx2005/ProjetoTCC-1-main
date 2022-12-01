using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_BigBig : EnemyController
{
    public enum chooseState
    {
        atkHorizontal, atkVertical, atkMeditar, stop
    };
    chooseState _chooseState = chooseState.stop;

    public bool atkH;
    public bool atkV;
    public GameObject areaAtkV;
    public bool ifStop;
    public bool ifFollow;
    public int chooseAtk;
    public float timerEsp;
    public bool ifAtk;
    public Transform rangeSpawn1;
    public Transform rangeSpawn2;
    public float posSpawnX;
    public float posSpawnZ;
    public int numSpawn;
    public GameObject EnemySpawn;
    public GameObject[] Enemys;
    GameObject[] enemyExist;

    private void Start()
    {
        maxHelth = 600;
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
        enemyExist = GameObject.FindGameObjectsWithTag("EnemySpawn");
        Enemys = GameObject.FindGameObjectsWithTag("EnemySpawn");
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (ifAttack == true)
        {
            Attack();
        }
        if(ifFollow == true)
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
        _navMesh.speed = 3;
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
                    if (dist <= 5)
                    {
                        this.transform.LookAt(player.transform);
                        ifFollow = false;
                        anim.SetBool("atkH", true);
                        ifAtk = false;
                    }
                }
                break;

            case chooseState.atkVertical:
                if (ifAtk == true)
                {
                    ifFollow = true;
                    if (dist <= 8)
                    {
                        this.transform.LookAt(player.transform);
                        ifFollow = false;
                        anim.SetBool("atkV", true);
                        ifDamage = false;
                        ifAtk = false;
                    }
                }
                break;
            case chooseState.atkMeditar:
                if (enemyExist.Length == 0)
                {
                    if (Enemys.Length == 0)
                    {
                        numSpawn = Random.Range(2, 4);
                        for (int i = 0; i < numSpawn; i++)
                        {
                            posSpawnX = Random.Range(rangeSpawn1.position.x, rangeSpawn2.position.x);
                            posSpawnZ = Random.Range(rangeSpawn1.position.z, rangeSpawn2.position.z);
                            Instantiate(EnemySpawn, new Vector3(posSpawnX, rangeSpawn1.position.y, posSpawnZ), transform.rotation);
                        }
                    }
                }
                CancelAtk();
                break;

            case chooseState.stop:
                _navMesh.speed = 0;
                anim.SetBool("atkV", false);
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
        if (chooseAtk == 1) {_chooseState = chooseState.atkHorizontal; ifAtk = true; }
        if (chooseAtk == 2) {_chooseState = chooseState.atkVertical; ifAtk = true; }
        if (chooseAtk == 3) {_chooseState = chooseState.atkMeditar; ifAtk = true; }
    }

    public void CancelAtk()
    {
        ifStop = true;
        ifDamage = true;
        _chooseState = chooseState.stop;
    }

    public void ActiveAreaAtkV()
    {
        areaAtkV.SetActive(true);
        areaAtkV.transform.localScale = new Vector3(.1f, .02f, .1f);
        StartCoroutine(DisableAreaAtkV());
    }
    public IEnumerator DisableAreaAtkV()
    {
        yield return new WaitForSeconds(.4f);
        areaAtkV.SetActive(false);
    }
}
