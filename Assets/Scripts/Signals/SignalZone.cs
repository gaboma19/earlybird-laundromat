using UnityEngine;

public class SignalZone : MonoBehaviour
{
    [SerializeField] Signal signal;
    public ZONE zone;
    [SerializeField] GameObject[] arrows;

    public enum ZONE
    {
        FRONT_DESK, WASHING_MACHINES, DRYERS, FOLDING_TABLES
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            signal.SetSignals(zone, arrows);
        }
    }
}
