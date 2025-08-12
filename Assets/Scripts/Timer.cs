using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Slider slider;
    public float sliderValue = 5;

    void Start()
    {
        StartCoroutine(Counting());
    }

    
    public IEnumerator Counting()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Time out");
        while (true)
        {
            yield return new WaitForSeconds(1);
            slider.value--;

        }
    }

    public void Update()
    {
        slider.value = sliderValue; 

    }

}
