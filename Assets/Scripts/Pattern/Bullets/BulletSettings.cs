using UnityEngine;

public class BulletSettings
{
    public Vector3 OriginalLocation;
    public Vector3 TargetLocation;
    public float speed;
    public float radious;

    public float interval;
    public float width;
    public float height;
    public float sustainmentTime;

    public float slope;

    public float a;                // �� (���)
    public float p;                // ���� ������ (������ x)
    public float q;
    public float xDir;

    public float reverseTime;
    public float mirrorDuration;

    public float detectionRadius;
    public string textContent;

    public void setCircleSettings(Vector3 ol, Vector3 tl, float s, float r)
    {
        OriginalLocation = ol;
        TargetLocation = tl;
        speed = s; 
        radious = r;
    }

    public void setSquareSettings(Vector3 ol, float i, float w, float h, float st)
    {
        OriginalLocation = ol;
        interval = i;
        width = w;
        height = h;
        sustainmentTime = st;
    }

    public void setLinearFunctionSettings(Vector3 ol, float s, float r, float sl, float st)
    {
        OriginalLocation = ol;
        speed = s;
        radious = r;
        slope = sl;
        sustainmentTime = st;

        interval = getInterval(radious, speed);
    }

    public void setQuadraticFunctionSettings(Vector3 ol, float s, float r, float st, float a, float p, float q, float xD)
    {
        OriginalLocation = ol;
        speed = s;
        radious = r;
        sustainmentTime = st;
        this.a = a;
        this.p = p;
        this.q = q;
        xDir = (xD >= 0f) ? 1f : -1f;

        interval = getInterval(radious, speed);
    }

    public void setMirrorSettings(float rt, float md)
    {
        this.reverseTime = rt;
        this.mirrorDuration = md;
    }

    public void setVanishSettings(Vector3 ol, Vector3 tl, float s, float r, float dr, string text)
    {
        OriginalLocation = ol;
        TargetLocation = tl;
        speed = s; 
        radious = r;
        detectionRadius = dr;
        textContent = text;
    }

    private float getInterval(float radious, float speed)
    {
        float diameter = radious * 2f;

        float density = 0.03f;
        float calculatedInterval = (diameter * density) / speed;

        // �ּ� ���� ���� (�ʹ� �۾����� �� �ɸ� �� ����)
        calculatedInterval = Mathf.Max(calculatedInterval, 0.001f);

        return calculatedInterval;
    }
}
