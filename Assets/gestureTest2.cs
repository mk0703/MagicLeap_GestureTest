using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class gestureTest2 : MonoBehaviour
{
    private bool OKHandPose = false;
    private float speed = 30.0f;  // Speed
    private float distance = 2.0f; // Distance between Main Camera to Cube
    private GameObject cube; 
    private MLHandKeyPose[] gestures;

    void Start()
    {
        //start HandTracking
        MLHands.Start();

        gestures = new MLHandKeyPose[4];
        gestures[0] = MLHandKeyPose.Ok; 
        gestures[1] = MLHandKeyPose.Fist;
        gestures[2] = MLHandKeyPose.OpenHand;
        gestures[3] = MLHandKeyPose.Finger;
        MLHands.KeyPoseManager.EnableKeyPoses(gestures, true, false); //judge geature

        cube = GameObject.Find("Cube");
        cube.SetActive(false);
    }

    void OnDestroy()
    {
        //end HandTracking
        MLHands.Stop();
    }

    void Update()
    {
        if (OKHandPose)
        {
            if (GetGesture(MLHands.Left, MLHandKeyPose.OpenHand)
            || GetGesture(MLHands.Right, MLHandKeyPose.OpenHand))
                cube.transform.Rotate(Vector3.up, +speed * Time.deltaTime);

            if (GetGesture(MLHands.Left, MLHandKeyPose.Fist)
            || GetGesture(MLHands.Right, MLHandKeyPose.Fist))
                cube.transform.Rotate(Vector3.up, -speed * Time.deltaTime);

            if (GetGesture(MLHands.Left, MLHandKeyPose.Finger))
                cube.transform.Rotate(Vector3.right, +speed * Time.deltaTime);

            if (GetGesture(MLHands.Right, MLHandKeyPose.Finger))
                cube.transform.Rotate(Vector3.right, -speed * Time.deltaTime);
        }
        else
        {
            if (GetGesture(MLHands.Left, MLHandKeyPose.Ok)
            || GetGesture(MLHands.Right, MLHandKeyPose.Ok))
            {
                OKHandPose = true;
                cube.SetActive(true);
                cube.transform.position = transform.position + transform.forward * distance;
                cube.transform.rotation = transform.rotation;
            }
        }
    }

    bool GetGesture(MLHand hand, MLHandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.KeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
