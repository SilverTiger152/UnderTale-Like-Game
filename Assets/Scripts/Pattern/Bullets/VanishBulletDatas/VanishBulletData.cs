using UnityEngine;

public class VanishBulletData : MoveData
{
    public Vector3 originalLocation;
    public Vector3 direction;
    public float speed;
    public float detectionRadius;
    public string textContent;

    public override void ApplyTo(BulletSettings bulletSettingInfo)
    {
        originalLocation = bulletSettingInfo.OriginalLocation;
        direction = bulletSettingInfo.TargetLocation;
        speed = bulletSettingInfo.speed;
        detectionRadius = bulletSettingInfo.radious;
        textContent = bulletSettingInfo.textContent;
    }
}