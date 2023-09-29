using UnityEngine.Events;

public static class StaticEvents
{
    public static UnityEvent<float> OnPowerChanged = new UnityEvent<float>();
    public static UnityEvent OnCannonShot = new UnityEvent();
}
