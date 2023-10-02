using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpotlight : Singleton<GameSpotlight>
{
    Light spotlight;
    [SerializeField] [Range(0, 100)] private float spotPerc = 5.5f;
    private float spotAngle => PercentToAngle(spotPerc);

    float[] increments =
    {
        5.5f, 7, 8.5f, 10, 12.5f, 16, 20, 25, 30, 35, 45, 55, 70, 90, 100
    };

    private int curIdx=0;

    // Start is called before the first frame update
    void Start()
    {
        spotlight = GetComponent<Light>();
        spotlight.spotAngle = spotAngle;
    }

    float PercentToAngle(float percent)
    {
        return percent * 1.8f;
    }


    // Update is called once per frame
    public void IncreaseSpotDistance()
    {
        curIdx = Mathf.Clamp(curIdx + 1, 0, increments.Length - 1);
        spotPerc = increments[curIdx];
        // Clamp the range to 0-180
        spotPerc = Mathf.Clamp(spotPerc, 0, 100);
        spotlight.spotAngle = Mathf.Clamp(spotAngle, 0, 180);
    }
}