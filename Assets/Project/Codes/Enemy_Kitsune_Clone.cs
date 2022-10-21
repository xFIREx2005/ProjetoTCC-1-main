using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Kitsune_Clone : Enemy_Kitsune
{
    public GameObject[] cloneKitsunes;
    public GameObject kitsune;
    public GameObject kitsuneProjetil;
    public Transform posSpawnProjetil;
    public int timeShot;
    public bool canShot;
    public Transform posPlayer;

    public bool isReal;

    private void Update()
    {
        if (canShot == true)
        {
            StartCoroutine(TimeShot());
            canShot = false;
        }

        Vector3 pos = posPlayer.position - transform.position;
        pos.y = 0;

        Quaternion rot = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 4 * Time.deltaTime);

        if (isReal == true)
        {
            if(ifDamage == false)
            {
                for(int i = 0; i < cloneKitsunes.Length; i++)
                {
                    cloneKitsunes[i].SetActive(false);
                }
                kitsune.SetActive(true);
                kitsune.GetComponent<Enemy_Kitsune>().CancelAtk();
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (ifDamage == false)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator TimeShot()
    {
        timeShot = Random.Range(4, 7);
        yield return new WaitForSeconds(timeShot);
        anim.SetBool("shot", true);
    }

    public void Shot()
    {
        Instantiate(kitsuneProjetil, posSpawnProjetil.position, transform.rotation);
        anim.SetBool("shot", false);
        canShot = true;
    }

    public void CanShot()
    {
        canShot = true;
    }

    /*public void TakeDamage(int damage)
    {
        if(isReal == true)
        {
            currentHealth -= damage;
            kitsune.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }*/
}
