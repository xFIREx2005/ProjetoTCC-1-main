using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_SpawnBigBig : EnemyController
{
    public float randomSpeed;

    private void Start()
    {
        randomSpeed = Random.Range(2, 6);
        attackDamage = 20;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        _navMesh.speed = randomSpeed;
        StartCoroutine(Death());
    }

    private void Update()
    {
        _navMesh.SetDestination(player.transform.position);
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("objectdamege");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage);
            Destroy(gameObject);
        }
    }
}
