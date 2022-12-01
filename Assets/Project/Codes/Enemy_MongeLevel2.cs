using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_MongeLevel2 : EnemyController
{
    public enum chooseState
    {
        atkEstocada, atkBey, stop, atkRicochete
    };
    chooseState _chooseState = chooseState.stop;

    public bool ifStop;
    public bool ifFollow;
    public int chooseAtk;
    public float timerEsp;
    public bool ifAtk;
    private float speed;
    private bool atkRic;
    public int numRic;

    private void Start()
    {
        numRic = 3;
        maxHelth = 400;
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
        if (atkRic == true)
        {
            AtkRicochete();
        }
        States();
        HealthBar();
    }

    public void Follow()
    {
        _navMesh.speed = speed;
        _navMesh.SetDestination(player.transform.position);
    }

    public void States()
    {
        switch (_chooseState)
        {
            case chooseState.atkEstocada:
                if (ifAtk == true)
                {
                    ifFollow = true;
                    anim.SetBool("walk", true);
                    speed = 5;
                    if (dist <= 3f)
                    {                        
                        this.transform.LookAt(player.transform);
                        ifFollow = false;
                        anim.SetBool("walk", false);
                        anim.SetBool("estoc", true);
                        ifAtk = false;
                    }
                }
                break;

            case chooseState.atkBey:
                if (ifAtk == true)
                {
                    ifFollow = true;
                    ifAttack = true;
                    speed = 6;
                    anim.SetBool("bey", true);
                    StartCoroutine(TimerBey());
                    ifAtk = false;
                }
               
                break;

            case chooseState.atkRicochete:
                if(ifAtk == true)
                {
                    anim.SetBool("fly", true);
                    /*Vector3 pos = player.transform.position - transform.position;
                    pos.y = 0;
                    Quaternion rot = Quaternion.LookRotation(pos);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 10);*/
                    atkRic = true;
                    ifAtk = false;
                }
                break;

            case chooseState.stop:
                _navMesh.speed = 0;
                anim.SetBool("fly", false);
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
        if (chooseAtk == 3) { _chooseState = chooseState.atkRicochete; ifAtk = true; numRic = Random.Range(3, 5); }
    }
    public IEnumerator TimerBey()
    {
        yield return new WaitForSeconds(Random.Range(3,6));
        ifAttack = false;
        CancelAtk();
    }
    public IEnumerator TimerRic()
    {
        anim.SetBool("fly", false);
        yield return new WaitForSeconds(Random.Range(1, 3));
        ifAtk = true;
    }

    public void AtkRicochete()
    {
        speed = .4f;
        transform.Translate(0, 0, speed);
    }
    public void CancelAtk()
    {
        ifStop = true;
        ifDamage = true;
        _chooseState = chooseState.stop;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_chooseState == chooseState.atkRicochete)
        {
            if (collision.gameObject.tag == "Wall")
            {
                this.transform.LookAt(player.transform);
                atkRic = false;
                if (numRic <= 0)
                {
                    CancelAtk();
                }
                else
                {
                    numRic--;
                    StartCoroutine(TimerRic());
                }
            }
            if (collision.gameObject.tag == "Player")
            {
                this.transform.LookAt(player.transform);
                atkRic = false;
                anim.SetBool("fly", false);
                CancelAtk();
                player.GetComponent<PlayerController>().TakeDamage(30);
            }
        }
    }
}
