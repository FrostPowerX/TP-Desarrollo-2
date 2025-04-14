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

    public void ConstantForceRequest(ForceRequest forceRequest)
    {
        constantForceRequest = forceRequest;
    }

    public void InstantForceRequest(ForceRequest forceRequest)
    {
        instantForceRequest = forceRequest;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ConstantForce();
        InstantForce();
    }

    void CancelForce()
    {
        constantForceRequest = null;
    }

    void ConstantForce()
    {
        if (constantForceRequest == null)
            return;

        float percentForce = rb.linearVelocity.magnitude / constantForceRequest.speed;
        float percentActualForce = Mathf.Clamp01(1f - percentForce);

        rb.AddForce(constantForceRequest.direction * constantForceRequest.force * percentActualForce, ForceMode.Force);
    }

    void InstantForce()
    {
        if (instantForceRequest == null)
            return;

        rb.AddForce(instantForceRequest.direction * instantForceRequest.force, ForceMode.Impulse);
        instantForceRequest = null;
    }
}
