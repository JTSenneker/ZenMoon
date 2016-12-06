using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls the day and night cycle
/// </summary>
public class DayNightController : MonoBehaviour
{
    /// <summary>
    /// The time the day lasts in minutes
    /// </summary>
    public float dayTime = 5;
    /// <summary>
    /// The time the night lasts in minutes
    /// </summary>
    public float nightTime = 3;
    /// <summary>
    /// How many seconds are in a minute
    /// </summary>
    float secsInMin = 60;
    /// <summary>
    /// The Timer that counts down the minutes
    /// </summary>
    public float Timer;
    /// <summary>
    /// Whether or not it is day time
    /// </summary>
    bool isDay = true;

    /// <summary>
    /// Initializes the timer for day time
    /// </summary>
    void Start()
    {
        Timer = dayTime * secsInMin;
    }

    /// <summary>
    /// Counts the timerdown and resets the timer to the night or day time cycle depending on whether or not it was just day time.
    /// </summary>
    void Update()
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            if (isDay)
            {
                print("saving...");
                SaveLoadController.Save();
                Timer = nightTime * secsInMin;
                isDay = false;
            }
            else if (!isDay)
            {
                JDStaticVariables.DayChange();
                Timer = dayTime * secsInMin;
                isDay = true;
            }
        }
    }
}
