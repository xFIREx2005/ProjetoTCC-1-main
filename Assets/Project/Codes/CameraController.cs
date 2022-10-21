using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool lockCursor;
    public static float mouseSensivity;
    public Transform target;
    public float distanceFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    RaycastHit hit = new RaycastHit();

    float yaw;
    float pitch;

    public Transform enemyTarget;
    public bool lookEnemy;
    public FovPlayer _cabeca;
    public Transform alvo;
    public float numLook;
    public EnemyController enemy;
    public int enemyLook;

    void Start()
    {
        lookEnemy = false;
        if (lockCursor)
        { 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        numLook = _cabeca.inimigosVisiveis.Count;
        if (lookEnemy == false)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;

            transform.position = target.position - transform.forward * distanceFromTarget;
        }
        else
        {
            if (_cabeca.inimigosVisiveis.Count > 0)
            {
                enemyTarget = _cabeca.inimigosVisiveis[enemyLook];
                if (!enemyTarget)
                {
                    _cabeca.inimigosVisiveis.Remove(_cabeca.inimigosVisiveis[enemyLook]);
                }
                enemy = enemyTarget.GetComponent<EnemyController>();
                alvo = enemy.cameraTarget;
                transform.LookAt(alvo);
                transform.position = target.position - transform.forward * distanceFromTarget;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

            }
            else
            {
                lookEnemy = false;
            }
        }

        if (Physics.Linecast(target.position, transform.position, out hit))
        {
            transform.position = hit.point + transform.forward * 0.2f;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (lookEnemy == false)
            {
                lookEnemy = true;
            }
            else
            {
                lookEnemy = false;
            }
        }

        if (Input.GetMouseButtonDown(1) && lookEnemy == true)
        { 
            if (_cabeca.inimigosVisiveis.Count > (enemyLook + 1))
            {
                enemyLook++;
            }
            else
            {
                enemyLook = 0;
            }
        }        
    }
}
