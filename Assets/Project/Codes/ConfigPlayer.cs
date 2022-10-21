using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigPlayer : MonoBehaviour
{
    public float currentHealth;

    private void Start()
    {
        currentHealth = StaticPlayer.currentHealth;
        PlayerController.currentHealth = currentHealth;
    }
    void Update()
    {
        currentHealth = PlayerController.currentHealth;
    }
}
