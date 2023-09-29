using TMPro;
using UnityEngine;

public class PowerDisplay : MonoBehaviour
{
    public TextMeshProUGUI powerText;

    private void OnEnable()
    {
        StaticEvents.OnPowerChanged.AddListener(UpdatePowerText);
    }

    private void UpdatePowerText(float newVal)
    {
        powerText.text = Mathf.FloorToInt(newVal).ToString();
    }
}
