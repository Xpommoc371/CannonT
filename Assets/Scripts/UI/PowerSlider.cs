using UnityEngine;

public class PowerSlider : MonoBehaviour
{

    private void Start()
    {
        OnSliderChanged(1);
    }

    public void OnSliderChanged(float value)
    {
        StaticEvents.OnPowerChanged.Invoke(value);
    }
}
