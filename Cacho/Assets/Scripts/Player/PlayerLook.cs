using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerLook : MonoBehaviour
{
    [SerializeField] GameObject root;
    [SerializeField] float xRotation = 0f;

    [SerializeField] float xSensi = 30f;
    [SerializeField] float ySensi = 30f;

    [SerializeField] bool invert;
    [SerializeField] bool desactivated;

    [SerializeField] InputActionAsset inputActionAsset;

    InputAction look;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        look = inputActionAsset.FindAction("Look");

        inputActionAsset.Enable();
    }

    void Update()
    {
        if (desactivated) return;
        if (!desactivated && Cursor.lockState == CursorLockMode.None) Cursor.lockState = CursorLockMode.Locked;

        LookProcess();
    }

    private void LookProcess()
    {

    }

    public void SensChange(float X, float Y)
    {
        xSensi = X;
        ySensi = Y;
    }
    public void InvertMode(bool value) => invert = value;
    public void OnMenu(bool value) => desactivated = value;
}