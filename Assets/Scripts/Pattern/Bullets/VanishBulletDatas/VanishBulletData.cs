using UnityEngine;

public class VanishBulletData : MoveData
{
    public Vector3 originalLocation;
    public Vector3 direction;
    public float speed;
    public float radious;
    public float detectionRadius;
    public string textContent;
    public float acceleration;

    public override void ApplyTo(BulletSettings bulletSettingInfo)
    {
        originalLocation = bulletSettingInfo.OriginalLocation;
        direction = bulletSettingInfo.TargetLocation;
        speed = bulletSettingInfo.speed;
        radious = bulletSettingInfo.radious;
        detectionRadius = bulletSettingInfo.detectionRadius;
        textContent = bulletSettingInfo.textContent;
        acceleration = bulletSettingInfo.acceleration;
    }
}