using UnityEngine;

public class BombTimer : MonoBehaviour
{
    private float timeToActivate = 1f;
    void Update()
    {
        if (timeToActivate > 0) timeToActivate -= Time.deltaTime;
    }

    public bool GetTimer()
    {
        if (timeToActivate > 0)
            return false;
        else return true;
    }
}
