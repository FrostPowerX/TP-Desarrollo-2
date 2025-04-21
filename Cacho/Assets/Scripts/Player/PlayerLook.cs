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

        LoockProcess();
    }

    private void LoockProcess()
    {
        float Xmouse = look.ReadValue<Vector2>().x;
        float mouseY = look.ReadValue<Vector2>().y;

        if (invert) mouseY *= -1;

        xRotation -= (mouseY * Time.deltaTime) * ySensi;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        root.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (Xmouse * Time.deltaTime) * xSensi);
    }

    public void SensChange(float X, float Y)
    {
        xSensi = X;
        ySensi = Y;
    }
    public void InvertMode(bool value) => invert = value;
    public void OnMenu(bool value) => desactivated = value;
}