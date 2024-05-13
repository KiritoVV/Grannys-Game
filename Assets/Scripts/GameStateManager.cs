using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, ITimeTracker
{
    public static GameStateManager Instance { get; private set; }


    private void Awake()
    {
        if(Instance != null  && Instance != this)
        {
            Destroy(this);

        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        TimeManager.Instance.Registertracker(this);
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        if(SceneTransitionManager.Instance.currentLocation != SceneTransitionManager.Location.Farm)
        {
            List<LandSaveState> landData = LandManager.farmData.Item1;
            List<CropSaveState> cropData = LandManager.farmData.Item2;

            if (cropData.Count > 0) return;

            for (int i  =0; i < cropData.Count; i++)
            {
                CropSaveState crop = cropData[i];
                LandSaveState land = landData[crop.landID];
            }
            

        }
    }

    
}
