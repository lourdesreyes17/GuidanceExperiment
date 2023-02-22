using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Runtime.InteropServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UXF;

public class CompareVelocities : MonoBehaviour
{
    public Session session;

    public Vector3 mentorVelocity;
    public Vector3 traineeVelocity;
    public float mentorVelMag;

    #region DLL_Imports
    [DllImport("OHToUnityBridge")] public static extern void getVelocity(string configName, double[] velocity3);
    #endregion

    // Update is called once per frame
    void Update()
    {
        double[] mentorVellInput = new double[3];
        getVelocity("Left Device", mentorVellInput);
        mentorVelocity.x = (float)mentorVellInput[0];
        mentorVelocity.y = (float)mentorVellInput[1];
        mentorVelocity.z = (float)mentorVellInput[2];

        double[] traineeVellInput = new double[3];
        getVelocity("Right Device", traineeVellInput);
        traineeVelocity.x = (float)traineeVellInput[0];
        traineeVelocity.y = (float)traineeVellInput[1];
        traineeVelocity.z = (float)traineeVellInput[2];

        
        mentorVelMag = mentorVelocity.magnitude;
        float traineeVelMag = traineeVelocity.magnitude;
        float magDiff = traineeVelMag - mentorVelMag;

        float minMentorVel = 20;
        float slowDownVel = 5;
        float stopVel = 10;
        
        
        if (mentorVelMag > minMentorVel & magDiff > stopVel)
        {
            Debug.Log("Stop" + mentorVelMag + ", " + traineeVelMag);
        }
        else if (mentorVelMag > minMentorVel & magDiff > slowDownVel)
        {
            Debug.Log("Slow Down" + mentorVelMag + ", " + traineeVelMag);
        }
        
        

        // if (abs magnitude mentor speed is above 10) and if 
        // (abs magnitude of trainee speed - abs magnitude of mentor speed)
        // is greater than 20
        // give slow down command 

        // if (abs magnitude mentor speed is above 10) and if 
        // (abs magnitude of trainee speed - abs magnitude of mentor speed)
        // is greater than 40
        // give stop command  
    }
}
