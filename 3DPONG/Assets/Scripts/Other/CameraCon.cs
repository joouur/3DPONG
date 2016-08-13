using UnityEngine;
using System.Collections;

public class CameraCon : MonoBehaviour {

    private Transform target;
    private Vector3 CameraInitPosition;
    private Quaternion CameraInitRotation;

    public float distance = 10.0f;
    public float heightDamp = 2.0f;
    public float rotationDamp = 1.5f;

    public void Awake()
    {
        CameraInitPosition = transform.position;
        CameraInitRotation = transform.rotation;


    }
    
    public void LateUpdate()
    {
        if (!target)
            return;
        
            
    }
}
