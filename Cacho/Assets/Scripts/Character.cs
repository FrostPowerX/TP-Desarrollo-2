using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move the character with a controller
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] ForceRequest constantForceRequest;
    [SerializeField] ForceRequest instantForceRequest;
    [SerializeField] Rigidbody rb;

    bool isForceActive;

    public void ConstantForceRequest(ForceRequest forceRequest)
    {
        constantForceRequest = forceRequest;
        isForceActive = true;
    }

    public void InstantForceRequest(ForceRequest forceRequest)
    {
        instantForceRequest = forceRequest;
        InstantForce();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        constantForceRequest = new ForceRequest();
        instantForceRequest = new ForceRequest();
    }

    private void FixedUpdate()
    {
        ConstantForce();
    }


    void ConstantForce()
    {
        if (!isForceActive)
            return;

        float percentForce = rb.linearVelocity.magnitude / constantForceRequest.speed;
        float percentActualForce = Mathf.Clamp01(1f - percentForce);

        rb.AddForce(constantForceRequest.direction * constantForceRequest.force * percentActualForce, ForceMode.Force);
    }

    void InstantForce()
    {
        rb.AddForce(instantForceRequest.direction * instantForceRequest.force, ForceMode.Impulse);
        instantForceRequest = null;
    }

    public void CancelForce()
    {
        isForceActive = false;
    }
}
