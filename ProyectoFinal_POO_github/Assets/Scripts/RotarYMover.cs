using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RotarYMover : MonoBehaviour
{
    private enum RotationAxis { X,Y,Z};
    [SerializeField]
    private RotationAxis rotationAxis;

    [SerializeField]
    [Range(0f, 5f)]
    private float rotationSpeed;

    [SerializeField]
    [Range(0f, 5f)]
    private float upDownAmplitude;

    [SerializeField]
    [Range(0f, 5f)]
    private float upDownFrequency;

    float radIncrement;
    float rad = 0;
    private float initialX, initialY, initialZ;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radIncrement = 2 * Mathf.PI * upDownFrequency;
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;


    }

    void FixedUpdate()
    {
        Movement();
        Rotation();

    }
    // Update is called once per frame
    void Rotation()
    {
        switch (rotationAxis) { 
            case RotationAxis.X:
                transform.Rotate(rotationSpeed * Vector3.right);
                break;
            case RotationAxis.Y:
                transform.Rotate(rotationSpeed * Vector3.up);
                break;
            case RotationAxis.Z:
                transform.Rotate(rotationSpeed * Vector3.forward);
                break;

        }
        
    }

    void Movement()
    {
#if UNITY_EDITOR
        radIncrement = 2*Mathf.PI* upDownFrequency;
#endif
        transform.position = new Vector3(transform.position.x, initialY + upDownAmplitude * Mathf.Sin(rad), transform.position.z);

        rad += radIncrement * Time.fixedDeltaTime;
        if (rad > 2 * Mathf.PI) {
            rad = 0;
        
        }
    }
}
