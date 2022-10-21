using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitsune_Bullet : MonoBehaviour
{
    public float speed;
    public int attackDamage;
    private void Start()
    {
        attackDamage = 10;
    }
    void Update()
    {
        transform.Translate(0,0, speed);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
  
}
