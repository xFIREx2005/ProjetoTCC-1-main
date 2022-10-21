 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitsune_Spawns : MonoBehaviour
{
    public GameObject[] objKitsuneFalse;
    public GameObject[] objKitsuneTrue;
    int randomKitsune;
    
    public void Spawn()
    {
        for (int i = 0; i < objKitsuneFalse.Length; i++)
        {
            objKitsuneFalse[i].GetComponent<EnemyController>().DamageRecover();
            objKitsuneFalse[i].GetComponent<Enemy_Kitsune_Clone>().CanShot();
        }
        for (int i = 0; i < objKitsuneTrue.Length; i++)
        {
            objKitsuneTrue[i].GetComponent<EnemyController>().DamageRecover();
            objKitsuneTrue[i].GetComponent<Enemy_Kitsune_Clone>().CanShot();
        }
        for (int i = 0; i < 4; i++)
        {
            randomKitsune = Random.Range(0, 5);
            objKitsuneFalse[randomKitsune].SetActive(true);
        }
        randomKitsune = Random.Range(0, 3);
        objKitsuneTrue[randomKitsune].SetActive(true);
    }
}
  