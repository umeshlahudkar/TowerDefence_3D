using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePractice : MonoBehaviour
{
    public GameObject selectedObject;
    public float z;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                selectedObject = raycastHit.collider.gameObject;
            }
        }

        if(selectedObject != null)
        {
            Vector3 mousePos = Input.mousePosition;

            // Set the z coordinate to match the distance from the camera to the object
            mousePos.z = Camera.main.WorldToScreenPoint(selectedObject.transform.position).z;

            // Convert the mouse position from screen coordinates to world coordinates
            Vector3 position = Camera.main.ScreenToWorldPoint(mousePos);

            // Maintain the original Y position of the object
            position.y = selectedObject.transform.position.y;

            // Update the object's position
            selectedObject.transform.position = position;
        }
    }
}
