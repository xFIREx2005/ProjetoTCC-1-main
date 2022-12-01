using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform cameraTarget;
    public int maxHelth;
    public int currentHealth;
    public Image imgHealthBar;
    public GameObject canvasHealthBar;

    public Animator anim;
    public bool ifDamage;
    public GameObject player;

    public FOVInimigos _cabeca;
    public NavMeshAgent _navMesh;
    public Transform alvo;
    public Vector3 posicInicialDaAI;
    public Vector3 ultimaPosicConhecida;
    public float timerProcura;
    public static float timerProc;
    public float dist;
    public float distI;
    public static bool attack;
    enum estadoDaAI
    {
        parado, seguindo, procurandoAlvoPerdido
    };
    estadoDaAI _estadoAI = estadoDaAI.parado;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerlayers;
    public int attackDamage = 20;
    public bool stop;
    public bool animDamage;

    public bool ifAttack;


    public void TakeDamage(int damage)
    {
        Debug.Log("timerEsp");
        if (animDamage == true)
        {
            if (ifDamage == true)
            {
                this.transform.LookAt(player.transform);
                currentHealth -= damage;
                if (currentHealth <= 0)
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("run", false);
                    anim.SetBool("attack", false);
                    anim.SetBool("damage", false);
                    anim.SetBool("die", true);
                    healthPercent = 0;
                    Destroy(canvasHealthBar);
                    
                }
                else
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("run", false);
                    anim.SetBool("attack", false);
                    anim.SetBool("damage", true);
                }
                ifDamage = false;
            }
        }
        else
        {
            if (ifDamage == true)
            {
                currentHealth -= damage;
                StartCoroutine(Damage());
                ifDamage = false;
            }
            if (currentHealth <= 0)
            {
                Destroy(canvasHealthBar);
                Destroy(gameObject);
            }
        }

    }
    public IEnumerator Damage()
    {
        yield return new WaitForSeconds(0.5f);
        ifDamage = true;
    }

    public void DamageRecover()
    {
        ifDamage = true;
    }

    public void death()
    {
        this.GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine(DeathT());
    }
    public IEnumerator DeathT()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public IEnumerator AttackRecover()
    {
        yield return new WaitForSeconds(2.5f);
        attack = true;
        stop = false;
    }

    public void VerSeguir()
    {
        if (stop == false)
        {
            if (_cabeca)
            {
                switch (_estadoAI)
                {
                    case estadoDaAI.parado:
                        anim.SetBool("walk", true);
                        anim.SetBool("run", false);
                        _navMesh.speed = 2;
                        _navMesh.SetDestination(posicInicialDaAI);
                        distI = Vector3.Distance(transform.position, posicInicialDaAI);
                        if (distI < 1)
                        {
                            anim.SetBool("walk", false);
                            anim.SetBool("run", false);
                            timerProc += 1 * Time.deltaTime;
                            if (timerProc >= 3)
                            {
                                anim.SetBool("proc", true);
                            }
                            anim.SetBool("walk", false);
                            _navMesh.speed = 0;
                        }
                        if (_cabeca.inimigosVisiveis.Count > 0)
                        {
                            alvo = _cabeca.inimigosVisiveis[0];
                            ultimaPosicConhecida = alvo.position;
                            anim.SetBool("proc", false);
                            _estadoAI = estadoDaAI.seguindo;
                        }
                        break;
                    case estadoDaAI.seguindo:
                        timerProc = 0;
                        anim.SetBool("proc", false);
                        anim.SetBool("walk", false);
                        anim.SetBool("run", true);
                        _navMesh.speed = 3;
                        _navMesh.SetDestination(alvo.position);
                        if (!_cabeca.inimigosVisiveis.Contains(alvo))
                        {
                            ultimaPosicConhecida = alvo.position;
                            _estadoAI = estadoDaAI.procurandoAlvoPerdido;
                        }
                        break;
                    case estadoDaAI.procurandoAlvoPerdido:
                        anim.SetBool("run", false);
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

    public void VerfDis()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist <= 2f)
        {

            _navMesh.speed = 0;
            anim.SetBool("proc", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
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

    public void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerlayers); 
        foreach (Collider player in hitEnemies)
        {
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }
    public void AttackTrue()
    {
        ifAttack = true;
    }
    public void AttackFalse()
    {
        ifAttack = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
    public float healthPercent;
    public void HealthBar()
    {
        healthPercent = (float)currentHealth / maxHelth;
        imgHealthBar.fillAmount = Mathf.Lerp(imgHealthBar.fillAmount, healthPercent, Time.deltaTime * 2);
    }
}
