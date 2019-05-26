using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
       
    public float cameraZoomSpeed;
    public float cameraScrollXMin;
    public float cameraScrollYMin;
    public float cameraScrollXMax;
    public float cameraScrollYMax;
    public float cameraScrollSpeedX;
    public float cameraScrollSpeedY;
    public float cameraScrollBoundXMin;
    public float cameraScrollBoundXMax;
    public float cameraScrollBoundYMin;
    public float cameraScrollBoundYMax;
    public float[] zoomLazyerZ;
    public int zoomIndex;
    private Transform lockOnTargetTransform;
    private float scrollX;
    private float scrollY;
    private Vector3 mousePositionPixel;
    private Vector3 mousePositionToViewPort;

    private void Start()
    {
        zoomIndex = 5;
    }


    private void FixedUpdate()
    {
        float keyInputX = Input.GetAxisRaw("Horizontal");
        float keyInputY = Input.GetAxisRaw("Vertical");
        //Input.GetAxis("Mouse ScrollWheel") = -0.1, 0, or 0.1
        mousePositionToViewPort = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mousePositionCheck(mousePositionToViewPort.x, cameraScrollXMin, cameraScrollXMax))
        {
            if (IsInsideTheBoundX())
            {
                scrollX = Mathf.Sign(mousePositionToViewPort.x - 0.5f);
            }
            else
            {
                transform.position = new Vector3(0f, 0f, transform.position.z);
            }
        }
        else
        if (keyInputX != 0f)
        {
            if (IsInsideTheBoundX())
            {
                scrollX = Mathf.Sign(keyInputX);
            }
            else
            {
                transform.position = new Vector3(0f, 0f, transform.position.z);
            }
        }
        else
        {
            scrollX = 0f;
        }

        if (mousePositionCheck(mousePositionToViewPort.y, cameraScrollYMin, cameraScrollYMax))
        {
            if (IsInsideTheBoundY())
            {
                scrollY = Mathf.Sign(mousePositionToViewPort.y - 0.5f);
            }
            else
            {
                transform.position = new Vector3(0f, 0f, transform.position.z);
            }
        }
        else
        if (keyInputY != 0f)
        {
            if (IsInsideTheBoundY())
            {
                scrollY = Mathf.Sign(keyInputY);
            }
            else
            {
                transform.position = new Vector3(0f, 0f, transform.position.z);
            }
        }
        else
        {
            scrollY = 0f;
        }
        transform.position += new Vector3(scrollX * cameraScrollSpeedX * Time.deltaTime, scrollY * cameraScrollSpeedX * Time.deltaTime, 0f);
        zoomIndex = (int)Mathf.Clamp((zoomIndex + Input.GetAxis("Mouse ScrollWheel") * 10f), 0f, zoomLazyerZ.Length - 1);

        if (IsZooming())
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zoomLazyerZ[zoomIndex]);
        }
        else
        {
            float sign = Mathf.Sign(zoomLazyerZ[zoomIndex] - transform.position.z);
            float cameraZoom = sign * cameraZoomSpeed * Time.fixedDeltaTime;
            transform.position += new Vector3(0f, 0f, cameraZoom);
        }

        if(lockOnTargetTransform != null)
        {
            if(Input.GetKey(KeyCode.Mouse1))
            {
                lockOnTargetTransform = null;
                return;
            }
            transform.position = new Vector3(lockOnTargetTransform.position.x, lockOnTargetTransform.position.y, transform.position.z);
        }
        else
        {
            // Stop locking on when player moves the camera
            if (keyInputX != 0 && keyInputY != 0)
            {
                lockOnTargetTransform = null;
            }
        }
    }

    private bool mousePositionCheck(float input, float min, float max)
    {
        if ((input < min || input > max) && input >= 0f && input <=1f)
            return true;
        return false;
    }

    private bool IsZooming()
    {
        if (Mathf.Abs(zoomLazyerZ[zoomIndex] - transform.position.z) < 0.1f)
            return true;
        return false;
    }

    public void LockOnTarget(Transform target)
    {
        lockOnTargetTransform = target;
    }

    private bool IsInsideTheBoundX()
    {
        if (transform.position.x < cameraScrollBoundXMax && transform.position.x > cameraScrollBoundXMin)
        {
            return true;
        }
        return false;
    }

    private bool IsInsideTheBoundY()
    {
        if (transform.position.y < cameraScrollBoundYMax && transform.position.y > cameraScrollBoundYMin)
        {
            return true;
        }
        return false;
    }

}
