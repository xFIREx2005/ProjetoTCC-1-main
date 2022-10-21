using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatArea : MonoBehaviour
{
    public GameObject door;
    public GameObject spawn;
    public GameObject[] enemies;

    public bool ifEnemies;
    public bool ifCombat;

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length != 0)
        {
            ifEnemies = true;
        }
        else
        {
            ifEnemies = false;
        }

        if(ifCombat == true )
        {
            door.SetActive(true);
        }

        if(ifEnemies == false)
        {
            door.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spawn.SetActive(true);
            ifCombat = true;
            ifEnemies = true;
        }
    }
}
