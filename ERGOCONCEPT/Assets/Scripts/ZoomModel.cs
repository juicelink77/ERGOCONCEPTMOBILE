using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomModel : MonoBehaviour
{
    public Camera Camera3D;
    private float cameraDistanceMax = 18f;
    private float cameraDistanceMin = 6f;
    public float MouseZoomSpeed = 2.5f;
    public float TouchZoomSpeed = 0.1f;
    private float distanceOrigin;
    private RotateModel rotateModel;
    private void Start()
    {
        rotateModel = gameObject.GetComponent<RotateModel>();
    }
    private void OnEnable()
    {
        distanceOrigin = Camera3D.fieldOfView;
    }

    void Update()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 2)
            {
                rotateModel.locked = true;
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);
            }
            else
            {
                rotateModel.locked = false;
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);
        }


        if (Camera3D.fieldOfView < cameraDistanceMin)
        {
            Camera3D.fieldOfView = 0.1f;
        }
        else
        if (Camera3D.fieldOfView > cameraDistanceMax)
        {
            Camera3D.fieldOfView = 179.9f;
        }
    }

    private void Zoom(float deltaMagnitudeDiff, float speed)
    {
        Camera3D.fieldOfView += deltaMagnitudeDiff * speed;
        Camera3D.fieldOfView = Mathf.Clamp(Camera3D.fieldOfView, cameraDistanceMin, cameraDistanceMax);
    }

    private void OnDisable()
    {
        Camera3D.fieldOfView = distanceOrigin;
    }
}