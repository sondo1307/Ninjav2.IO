using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class VibrateManager : MonoBehaviour
{
    public static VibrateManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GameDataManager.Instance.gameDataScrObj.vibrateOn)
        {
            MMVibrationManager.SetHapticsActive(true);
        }
        else
        {
            MMVibrationManager.SetHapticsActive(false);
        }
    }

    public void SmallVibrate()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
    }

    public void RigidBibrate()
    {
        MMVibrationManager.Haptic(HapticTypes.RigidImpact);
    }

    public void LongVibrate()
    {
        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
    }

    public void HeavyVibrate()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }

    public void SuccesVibrate()
    {
        MMVibrationManager.Haptic(HapticTypes.Success);
    }
}
