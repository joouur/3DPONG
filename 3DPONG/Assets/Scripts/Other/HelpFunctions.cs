using UnityEngine;
using System.Collections;

public class HelpFunctions : MonoBehaviour
{
    public Vector3 compVelocit(Vector3 normal, Vector3 velo)
    {
        Vector3 cTangnt = new Vector3((normal.x * Mathf.Cos(1.5707f) - normal.y * (float)Mathf.Sin(1.5707f)),
                                    (normal.x * (float)Mathf.Sin(1.5707f) + normal.y * (float)Mathf.Cos(1.5707f)),
                                    0);
        float a = -Vector3.Dot(velo, normal);
        float b = Vector3.Dot(velo, cTangnt);
        Vector3 resultantVel = a * normal + b * cTangnt;
        return resultantVel;
    }
}