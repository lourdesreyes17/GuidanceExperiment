using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using UnityEngine;

using UXF;
public class ProvideGuidance : MonoBehaviour
{
    public Session session;

    public GameObject mentor; //right device
    public GameObject trainee; //left device
    public GameObject mockTrainee; // copy of trainee
    private Vector3 offset = new Vector3(12,-1,0);

    // Arduino Variables
    SerialPort sp = new SerialPort("COM10", 74880); // BAUD rate changed to 74880

    // Guidance Coroutine Variables

    private float deltaTime = 0f; // 
    private float tolerance = 0.15f;  // min error from path
    private bool CR_running = false;

    // Start is called before the first frame update
    void Start()
    {
        // open serial port
        sp.Open();
        sp.ReadTimeout = 100;
    }

    IEnumerator GuidanceCoroutine()
    {
        // toggle: Coroutine is running
        CR_running = true;

        // mentor and trainee positions
        float mPosX = mentor.transform.position.x;
        float mPosZ = mentor.transform.position.z;
        float tPosX = trainee.transform.position.x - 12;
        float tPosZ = trainee.transform.position.z;

        // calculate difference in positions
        float xDiff = mPosX - tPosX; // left, right
        float zDiff = mPosZ - tPosZ; // forward, back


        // Optional Debug Logs 
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        
        Debug.Log("mentor x, z: " + mPosX + ", " + mPosZ);
        Debug.Log("trainee x, z: " + tPosX + ", " + tPosZ);
        /*
        Debug.Log("x difference: " + xDiff + "(" + Math.Abs(xDiff) + ")");
        Debug.Log("z difference: " + zDiff + "(" + Math.Abs(zDiff) + ")");
        */

        // Prioritize greatest error
        if (Math.Abs(xDiff) > Math.Abs(zDiff)) // if x difference is greater than z difference
        {
            if (xDiff > tolerance) // if mentor is right of trainee, command trainee to move "right", command.
            {
                sp.Write("1");

                // END COROUTINE and turn CR_running off
                yield return new WaitForSeconds(deltaTime);
                CR_running = false;

                // Optional Debug Logs
                Debug.Log("command trainee to move RIGHT");
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            }
            else if (xDiff < -tolerance) // else if mentor is left of trainee command trainee to move "left"
            {
                sp.Write("2");

                // END COROUTINE and turn CR_running off
                yield return new WaitForSeconds(deltaTime);
                CR_running = false;

                // Optional Debug Logs
                Debug.Log("command trainee to move LEFT");
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            }
            else // else try CR again
            {
                yield return null;
                CR_running = false;
                Debug.Log("X else. Coroutine restarted at timestamp : " + Time.time);
            }
        }
        else if (Math.Abs(zDiff) > Math.Abs(xDiff)) // if x difference is greater than z difference, command.
        {
            if (zDiff > tolerance) // if mentor is above trainee, command trainee to move "up"
            {
                sp.Write("3");

                // END COROUTINE and turn CR_running off
                yield return new WaitForSeconds(deltaTime);
                CR_running = false;

                // Optional Debug Logs
                Debug.Log("command trainee to move UP");
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            }
            else if (zDiff < -tolerance) // else if mentor is below trainee command trainee to move "down"
            {
                sp.Write("4");

                // END COROUTINE and turn CR_running off
                yield return new WaitForSeconds(deltaTime);
                CR_running = false;

                // Optional Debug Logs
                Debug.Log("command trainee to move DOWN");
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            }
            else // else if z is zero, try CR again --- most likely will not happen
            {
                yield return null;
                CR_running = false;
                Debug.Log("Z else. Coroutine restarted at timestamp : " + Time.time);
            }
        }
        else // else if both are zero, try CR again
        {
            yield return null;
            CR_running = false;
            Debug.Log("No difference. Coroutine restarted at timestamp : " + Time.time);
        }
    }

    // Update is called every frame
    void Update()
    {
        mockTrainee.transform.position = trainee.transform.position - offset;
        //Debug.Log(mockTrainee.transform.position);
        
            //gameObject.transform.position.x = trainee.transform.position.x;
        //gameObject.transform.position.z = trainee.transform.position.z;

        // If the session has started and a coroutine is not running, call for guidance cue.
        if (session.InTrial & !CR_running)
        {
            StartCoroutine(GuidanceCoroutine());
        }

    }

    // assign this function to run On Block Begin
    public void UpdateSettings(Block block)
    {
        // use settings.Get*() to access settings (independent variables)
        deltaTime = session.CurrentBlock.settings.GetFloat("delta_time");
        Debug.Log("dt set to: " + deltaTime + " for Block " + session.currentBlockNum);
    }
}
