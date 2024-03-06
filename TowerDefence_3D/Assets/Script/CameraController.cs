using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Transform thisTransform;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    [SerializeField] private Vector2 minBound;
    [SerializeField] private Vector2 maxBound;

    private float scrollValue;
    private float horizontal;
    private float vertical;

    private Vector3 movementVector;
  
    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        thisTransform = gameObject.transform;
        movementVector = thisTransform.position;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            Move();
        }

        scrollValue = Input.GetAxisRaw("Mouse ScrollWheel");
        if(scrollValue != 0)
        {
            Zoom(scrollValue);
            ClampCamera();
        }
    }

    private float GetHeight()
    {
        if(!mainCamera.orthographic)
        {
            return thisTransform.position.y * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        return mainCamera.orthographicSize;
    }

    private void Move()
    {
        if(horizontal != 0)
        {
            movementVector.x += horizontal * moveSpeed * Time.deltaTime;
        }
        else
        {
            movementVector.z += vertical * moveSpeed * Time.deltaTime;
        }

        ClampCamera();
    }

    private void ClampCamera()
    {
        //float height = GetHeight();
        //float width = height * mainCamera.aspect;

        //movementVector.x = Mathf.Clamp(movementVector.x, minBound.x + width, maxBound.x - width);
        //movementVector.z = Mathf.Clamp(movementVector.z, minBound.y + height, maxBound.y - height);

        movementVector.x = Mathf.Clamp(movementVector.x, minBound.x , maxBound.x );
        movementVector.z = Mathf.Clamp(movementVector.z, minBound.y , maxBound.y);

        thisTransform.position = movementVector;
    }

    private void Zoom(float zoomValue)
    {
        float newFieldOfView = mainCamera.fieldOfView - (zoomValue * zoomSpeed);
        newFieldOfView = Mathf.Clamp(newFieldOfView, minZoom, maxZoom);
        mainCamera.fieldOfView = newFieldOfView;
    }
}
