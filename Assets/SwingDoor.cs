using UnityEngine;

public class SwingDoor : MonoBehaviour
{
    public Transform door; // Assign the door model here
    public float openAngle = 90f; // How wide it swings open
    public float openSpeed = 2f;  // How fast it opens
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool playerNear = false;

    void Start()
    {
        closedRotation = door.rotation;
        openRotation = Quaternion.Euler(door.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    void Update()
    {

        if (playerNear)
        {
            door.rotation = Quaternion.Lerp(door.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            door.rotation = Quaternion.Lerp(door.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }
}
