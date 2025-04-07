using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    [SerializeField] bool onFloor;

    public bool OnFloor { get { return onFloor; } }

    private void OnTriggerStay(Collider other)
    {
        onFloor = other.CompareTag("Floor");
    }

    private void OnTriggerExit(Collider other)
    {
        onFloor = false;
    }
}
