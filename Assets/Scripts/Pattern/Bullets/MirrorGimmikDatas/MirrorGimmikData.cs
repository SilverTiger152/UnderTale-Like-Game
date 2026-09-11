using UnityEngine;

public class MirrorGimmikData : MoveData
{
    public float reverseTime;
    public float duration;

    public override void ApplyTo(BulletSettings bulletSettingInfo)
    {
        reverseTime = bulletSettingInfo.reverseTime;
        duration = bulletSettingInfo.mirrorDuration;
    }
}
