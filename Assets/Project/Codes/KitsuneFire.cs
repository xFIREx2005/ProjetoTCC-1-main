using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitsuneFire : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TimerDestroy());
    }
    private IEnumerator TimerDestroy()
    {
        yield return new WaitForSeconds(Random.Range(7,11));
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.GetComponent<PlayerController>().TakeDamageRepeat(.2f);
    }
    private void OnTriggerStay(Collider collision)
    {
        collision.gameObject.GetComponent<PlayerController>().TakeDamageRepeat(.2f);
    }
}
