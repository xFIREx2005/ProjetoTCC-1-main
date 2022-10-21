using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISimples : EnemyController
{

    void Start()
    {
        maxHelth = 50;
        ifDamage = true;
        currentHealth = maxHelth;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        alvo = null;
        ultimaPosicConhecida = Vector3.zero;
        posicInicialDaAI = transform.position;
        attack = true;
        stop = false;
    }

    void Update()
    { 
        VerSeguir();
        VerfDis();
        HealthBar();
    }

    public void cancelAni()
    {
        GetComponent<AISimples>().enabled = true;
        ifDamage = true;
        StartCoroutine(AttackRecover());
        AISimples.timerProc = 0;
        anim.SetBool("damage", false);
        anim.SetBool("attack", false);
        anim.SetBool("proc", false);
    }
}
