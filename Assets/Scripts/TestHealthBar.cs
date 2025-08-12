using UnityEngine;
using UnityEngine.UI;

public class TestHealthBar : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.maxValue = 100;
        slider.value = 100;
        InvokeRepeating(nameof(Damage), 1f, 0.5f); // Repeats every 0.5s
    }

    void Damage()
    {
        slider.value -= 5;
        Debug.Log("Slider now: " + slider.value);
    }
}
