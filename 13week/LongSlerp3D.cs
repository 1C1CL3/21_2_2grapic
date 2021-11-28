using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSlerp3D : MonoBehaviour
{

    public Vector3 roll;
    public Quaternion quaternion0;
    public Quaternion quaternion1;
    public float Slerp = 0;

    float FloatSelect(float Comparand, float ValueGEZero, float ValueLTZero)
    {
        return Comparand >= 0f ? ValueGEZero : ValueLTZero;
    }
    public void Tick()
    {
        Slerp = Mathf.Clamp(Slerp, 0f, 1f);
        Time.timeScale = 0.1f;
        Slerp += Time.deltaTime; 
    }

    public void QuatSlerp()
    {
        Slerp = Mathf.Clamp(Slerp, 0f, 1f);
        
        Slerp += Time.deltaTime;
        

        float RawCosom =
        quaternion0.x * quaternion1.x +
        quaternion0.y * quaternion1.y +
        quaternion0.z * quaternion1.z +
        quaternion0.w * quaternion1.w;

        float Cosom = FloatSelect(RawCosom, RawCosom, -RawCosom);
        float Scale0, Scale1;
        if (Cosom < 0.9999f)
        {
            float Omega = Mathf.Acos(Cosom);
            float InvSin = 1f / Mathf.Sin(Omega);
            Scale0 = Mathf.Sin((1f - Slerp) * Omega) * InvSin;
            Scale1 = Mathf.Sin(Slerp * Omega) * InvSin;
        }
        else
        {
            // Use linear interpolation.
            Scale0 = 1.0f - Slerp;
            Scale1 = Slerp;
        }
        Scale1 = FloatSelect(RawCosom, Scale1, -Scale1);


        Quaternion Result;

        Result.x = Scale0 * quaternion0.x + Scale1 * quaternion1.x;
        Result.y = Scale0 * quaternion0.y + Scale1 * quaternion1.y;
        Result.z = Scale0 * quaternion0.z + Scale1 * quaternion1.z;
        Result.w = Scale0 * quaternion0.w + Scale1 * quaternion1.w;

        this.transform.rotation = Result;
    }

    public void ForedLongSlerp()
    {
        Slerp = Mathf.Clamp(Slerp, 0f, 1f);

        Slerp += Time.deltaTime;


        float RawCosom =
        quaternion0.x * quaternion1.x +
        quaternion0.y * quaternion1.y +
        quaternion0.z * quaternion1.z +
        quaternion0.w * quaternion1.w;

        float Cosom = FloatSelect(RawCosom, -RawCosom, RawCosom);
        float Scale0, Scale1;
        if (Cosom < 0.9999f)
        {
            float Omega = Mathf.Acos(Cosom);
            float InvSin = 1f / Mathf.Sin(Omega);
            Scale0 = Mathf.Sin((1f - Slerp) * Omega) * InvSin;
            Scale1 = Mathf.Sin(Slerp * Omega) * InvSin;
        }
        else
        {
            // Use linear interpolation.
            Scale0 = 1.0f - Slerp;
            Scale1 = Slerp;
        }
        Scale1 = FloatSelect(RawCosom, -Scale1, Scale1);


        Quaternion Result;

        Result.x = Scale0 * quaternion0.x + Scale1 * quaternion1.x;
        Result.y = Scale0 * quaternion0.y + Scale1 * quaternion1.y;
        Result.z = Scale0 * quaternion0.z + Scale1 * quaternion1.z;
        Result.w = Scale0 * quaternion0.w + Scale1 * quaternion1.w;

        this.transform.rotation = Result;
    }
    private void Start()
    {
        Time.timeScale = 0.4f;
        quaternion0 = this.transform.rotation;
        quaternion1.eulerAngles = roll;


    }

    // Update is called once per frame
    void Update()
    {
        //QuatSlerp();
        ForedLongSlerp();
    }
} 

