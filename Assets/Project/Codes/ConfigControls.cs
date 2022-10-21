using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigControls : MonoBehaviour
{
    public float mouseSensi;
    public Slider sliderSensi;

    private void Start()
    {
        mouseSensi = StaticControls.mouseSensi;

        sliderSensi.value = mouseSensi;
    }


    // Update is called once per frame
    void Update()
    {
        CameraController.mouseSensivity = mouseSensi;
        StaticControls.mouseSensi = mouseSensi;
    }

    public void MouseSensivity(float sensi)
    {
        mouseSensi = sensi;
    }
}
