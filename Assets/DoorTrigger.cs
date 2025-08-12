using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform door;      // The door to open
    public Vector3 openRotation = new Vector3(0, 90, 0); // How far to rotate
    public float openSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion targetRotation;
    private bool isOpening = false;

    void Start()
    {
        closedRotation = door.rotation;
        targetRotation = closedRotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetRotation = Quaternion.Euler(door.eulerAngles + openRotation);
            isOpening = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetRotation = closedRotation;
        }
    }

    void Update()
    {
        if (isOpening)
        {
            door.rotation = Quaternion.Lerp(door.rotation, targetRotation, Time.deltaTime * openSpeed);
        }
    }
}
