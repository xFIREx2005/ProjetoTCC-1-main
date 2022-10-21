using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;

    private void Start()
    {
        Instantiate(enemy ,transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
