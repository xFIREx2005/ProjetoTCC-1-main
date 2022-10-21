using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings:")]
    public float speedRun;
    public float speedSprint;
    public float rotateSpeed;

    [Header("Animation Settings:")]
    public Animator anim;

    float speed;
    Rigidbody rb;

    float xRaw;
    float zRaw;

    float x;
    float z;

    bool onTheGround;
    public static bool mov;
    public bool movi;
    public bool ifDamage;

    public static int maxHelth = 200;
    public static float currentHealth;

    public Image imgHealthBar;
    public Image imgStaminaBar;
    public GameObject canvasHealthBar;

    public float stamina;
    public int maxStamina = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = speedRun;
        onTheGround = true;
        mov = true;
        ifDamage = true;
        stamina = maxStamina;
        healthPercent = (float)currentHealth / maxHelth;
        imgHealthBar.fillAmount = healthPercent;
    }

    private void Update()
    {
        movi = mov;

            Camera.main.transform.gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.main.transform.gameObject.GetComponent<Camera>().fieldOfView, 50f, .1f);
        
        if (mov == true)
        {
            if (onTheGround == true)
            {
                Animation();
            }
            Jump();
            Movimentation();
            Rotation();
            Roll();
        }
    }
    
    private void FixedUpdate()
    {
        xRaw = Input.GetAxisRaw("Horizontal");
        zRaw = Input.GetAxisRaw("Vertical");

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        HealthBar();
        StaminaBar();
    }

    void Movimentation()
    {
        if (xRaw != 0 || zRaw != 0)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * transform.forward);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (stamina >= 0)
                {
                    speed = speedSprint;
                }
                else { speed = speedRun; }
                if(stamina > 0)
                { 
                    stamina -= Time.deltaTime;
                }
            }
            else{speed = speedRun;}
        }
        if (Input.GetKey(KeyCode.LeftShift)) { }
        else{if (stamina < 5){stamina += Time.deltaTime;}}
    }

    void Rotation()
    {
        float camY = Camera.main.transform.rotation.eulerAngles.y;

        if (zRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY, 0), Time.deltaTime * rotateSpeed);
        }
        if (zRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 180, 0), Time.deltaTime * rotateSpeed);
        }
        if (xRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + 90, 0), Time.deltaTime * rotateSpeed);
        }
        if (xRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 90, 0), Time.deltaTime * rotateSpeed);
        }
    }

    void Animation()
    {
        if (!xRaw.Equals(0) || !zRaw.Equals(0))
        {
            anim.SetBool("running", true);
            if(stamina > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool("sprint", true);
                }
                else
                {
                    anim.SetBool("sprint", false);

                }
            }
            else
            {
                anim.SetBool("sprint", false);

            }
        }
        else
        {
            anim.SetBool("running", false);
            anim.SetBool("sprint", false);
        }
    }

    void Jump()
    {
        if (onTheGround == true && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * 4, ForceMode.Impulse);
            onTheGround = false;
        }
        if(onTheGround == false)
        {
            mov = false;
           anim.SetBool("jump", true);
           anim.SetBool("running", false);
           anim.SetBool("sprint", false);
        }
    }
    void Roll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            anim.SetBool("sprint", false);
            anim.SetBool("running", false);
            anim.SetBool("1", false);
            anim.SetBool("2", false);
            anim.SetBool("3", false);
            mov = false;
            anim.SetBool("roll", true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onTheGround = true;
            mov = true;
            anim.SetBool("jump", false);
        }
    }
    public void TakeDamageRepeat(float damage)
    {
        currentHealth -= damage;
    }
        public void TakeDamage(int damage)
    {
        mov = false;
        anim.SetBool("sprint", false);
        anim.SetBool("running", false);
        anim.SetBool("1", false);
        anim.SetBool("2", false);
        anim.SetBool("3", false);
        anim.SetBool("jump", false);
        anim.SetBool("damage", true);
            if (ifDamage == true)
            {
                currentHealth -= damage;
                ifDamage = false;
                if (currentHealth <= 0)
                {
                    Die();
                }
                
            }
        
    }

    void Die()
    {
        SceneManager.LoadScene("BigBig");
    }

    public void death()
    {
        StartCoroutine(DeathT());
    }
    public IEnumerator DeathT()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void CancelAnim()
    {
        ifDamage = true;
        mov = true;
        anim.SetBool("roll", false);
        anim.SetBool("sprint", false);
        anim.SetBool("running", false);
        anim.SetBool("1", false);
        anim.SetBool("2", false);
        anim.SetBool("3", false);
        anim.SetBool("damage", false);
        PlayerAttack.noOfClicks = 0;
    }

    public float healthPercent;
    public float staminaPercent;
    public void HealthBar()
    {
        healthPercent = (float)currentHealth / maxHelth;
        imgHealthBar.fillAmount = Mathf.Lerp(imgHealthBar.fillAmount, healthPercent, Time.deltaTime * 2);
    }
    public void StaminaBar()
    {
        staminaPercent = (float)stamina / maxStamina;
        imgStaminaBar.fillAmount = Mathf.Lerp(imgStaminaBar.fillAmount, staminaPercent, Time.deltaTime * 2);
    }

}
