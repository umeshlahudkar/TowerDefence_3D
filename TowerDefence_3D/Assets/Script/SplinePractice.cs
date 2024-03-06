using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;



public class SplinePractice : MonoBehaviour
{
    public SplineContainer splineContainer;

    public float speed = 1f;
    private float t = 0f; // Parameter to move along the spline

    private void Start()
    {
        transform.position = splineContainer.EvaluatePosition(0);
    }

    void Update()
    {
        t += Time.deltaTime * speed;

        if(t < 1)
        {
            float3 position = splineContainer.EvaluatePosition(t);
            Vector3 direction = position - (float3)transform.position;

            Quaternion rotation = quaternion.LookRotation(direction, Vector3.up);

            transform.position = new Vector3(position.x, position.y, position.z);
            transform.rotation = rotation;
        }
    }

}
