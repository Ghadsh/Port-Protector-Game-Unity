using UnityEngine;

public class soundPlay : MonoBehaviour
{

    public AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
         audio.Play();
    }

    void OnTriggerExit(Collider other)
    {
        audio.Pause();
    }
}
