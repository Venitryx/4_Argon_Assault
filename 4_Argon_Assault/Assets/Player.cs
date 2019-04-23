using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("in meters/second")] [SerializeField] float xSpeed = 4f;
    [SerializeField] float xMinPos = -5f;
    [SerializeField] float xMaxPos = 5f;
    [SerializeField] float yMinPos = -3f;
    [SerializeField] float yMaxPos = 5f;

    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float controlPitchFactor = -5f;

    [SerializeField] float positionYawFactor = 0.75f;

    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        ProcessLocation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        //x = pitch, y = yaw, z = roll
        float pitch, yaw, roll;
        pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        yaw = transform.localPosition.x * positionYawFactor;
        roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessLocation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * xSpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float rawNewYPos = transform.localPosition.y + yOffset;

        float xPos = Mathf.Clamp(rawNewXPos, xMinPos, xMaxPos);
        float yPos = Mathf.Clamp(rawNewYPos, yMinPos, yMaxPos);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }
}
