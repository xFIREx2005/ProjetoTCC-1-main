using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicao : MonoBehaviour
{
    public GameObject gameControl; 
    public void Trocar()
    {
        SceneManager.LoadScene(TrocaCena.cenaStatic);
    }
    public void Iniciar()
    {
        gameControl.SetActive(true);
        gameObject.SetActive(false);
    }
}
