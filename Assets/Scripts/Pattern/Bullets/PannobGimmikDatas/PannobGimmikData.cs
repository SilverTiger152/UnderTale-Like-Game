using UnityEngine;

public class PannobGimmikData : MoveData
{
    public float darkTime;
    public float duration;
    public float targetVisionScale;

    public override void ApplyTo(BulletSettings bulletSettingInfo)
    {
        darkTime = bulletSettingInfo.darkTime;
        duration = bulletSettingInfo.mirrorDuration;
        targetVisionScale = bulletSettingInfo.targetVisionScale;
    }
}
