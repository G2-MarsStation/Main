using UnityEngine;
using UnityEngine.UI;

public class ActionSliderUI : MonoBehaviour
{
    public static ActionSliderUI instance;

    public GameObject sliderObject;
    public Slider slider;

    private void Awake()
    {
        instance = this;
        HideSlider();
    }

    public void ShowSlider()
    {
        sliderObject.SetActive(true);
        slider.value = 0;
    }

    public void HideSlider()
    {
        sliderObject.SetActive(false);
    }

    public void UpdateSlider(float progress)
    {
        slider.value = Mathf.Clamp01(progress);
    }
}
