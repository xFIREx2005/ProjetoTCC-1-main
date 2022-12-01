using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaCena : MonoBehaviour
{
    public static string cenaStatic;
    public string cena;
    public GameObject transi;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cenaStatic = cena;
            transi.SetActive(true);
            
        }
    }
}
