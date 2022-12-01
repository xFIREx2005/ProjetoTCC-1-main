using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Chuvis : EnemyController
{
    enum estadoDaAI
    {
        parado, seguindo, procurandoAlvoPerdido
    };
    estadoDaAI _estadoAI = estadoDaAI.parado;
    void Start()
    {
        float scale = Random.Range(0.2f, 0.3f);
        transform.localScale = new Vector3(scale, scale, scale);
        maxHelth = 70;
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

    // Update is called once per frame
    void Update()
    {
        VerSeguir1();
        VerfDis1();
    }

    public void VerSeguir1()
    {
        if (stop == false)
        {
            if (_cabeca)
            {
                switch (_estadoAI)
                {
                    case estadoDaAI.parado:
                        anim.SetBool("walk", true);
                        _navMesh.speed = 2;
                        _navMesh.SetDestination(posicInicialDaAI);
                        distI = Vector3.Distance(transform.position, posicInicialDaAI);
                        if (distI < 1)
                        {
                            anim.SetBool("walk", false);
                            _navMesh.speed = 0;
                        }
                        if (_cabeca.inimigosVisiveis.Count > 0)
                        {
                            alvo = _cabeca.inimigosVisiveis[0];
                            ultimaPosicConhecida = alvo.position;
                            _estadoAI = estadoDaAI.seguindo;
                        }
                        break;
                    case estadoDaAI.seguindo:
                        timerProc = 0;
                        anim.SetBool("walk", true);
                        _navMesh.speed = 3;
                        _navMesh.SetDestination(alvo.position);
                        if (!_cabeca.inimigosVisiveis.Contains(alvo))
                        {
                            ultimaPosicConhecida = alvo.position;
                            _estadoAI = estadoDaAI.procurandoAlvoPerdido;
                        }
                        break;
                    case estadoDaAI.procurandoAlvoPerdido:
                        anim.SetBool("walk", true);
                        _navMesh.SetDestination(ultimaPosicConhecida);
                        timerProcura += Time.deltaTime;
                        if (timerProcura > 5)
                        {
                            timerProcura = 0;
                            _estadoAI = estadoDaAI.parado;
                            break;
                        }
                        if (_cabeca.inimigosVisiveis.Count > 0)
                        {
                            alvo = _cabeca.inimigosVisiveis[0];
                            ultimaPosicConhecida = alvo.position;
                            _estadoAI = estadoDaAI.seguindo;
                        }
                        break;
                }
            }
        }
    }

    public void VerfDis1()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist <= 2f)
        {

            _navMesh.speed = 0;
            anim.SetBool("walk", false);
            Vector3 pos = player.transform.position - transform.position;
            pos.y = 0;
            Quaternion rot = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.time * 10);
            if (attack == true)
            {
                anim.SetBool("attack", true);
                AttackRecover();
                attack = false;
            }
        }
        else
        {
            anim.SetBool("attack", false);
        }

        if (ifAttack == true)
        {
            _navMesh.speed = 0;
            Attack();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}

