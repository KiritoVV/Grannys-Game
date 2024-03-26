using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TimeManager Instance { get; private set; }

    [SerializeField]
    GameTimeStamp timestamp;

    public float timeScale = 1.0f;

    //The transform of the direction of the sun
    public Transform sunTransform;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Set the static instance to this instance
            Instance = this;
        }
    }

    private void Start()
    {
        timestamp = new GameTimeStamp(0, GameTimeStamp.Season.Spring, 1, 6, 0);
        StartCoroutine(TimeUpdate());

    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/timeScale);
            Tick();
        }
    }

    // A tick of the in game time
    public void Tick()
    {
        timestamp.UpdateClock();
    }


}
