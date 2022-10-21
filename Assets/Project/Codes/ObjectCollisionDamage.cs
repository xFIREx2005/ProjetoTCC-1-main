using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisionDamage : MonoBehaviour
{
    public int attackDamage;
    private void Update()
    {
        transform.localScale += new Vector3(12f, 0, 12f) * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("objectdamege");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }
}
