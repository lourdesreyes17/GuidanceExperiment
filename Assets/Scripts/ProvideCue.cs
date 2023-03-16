using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using UnityEngine;

using UXF;
public class ProvideCue : MonoBehaviour
{
    public Session session;

    public GameObject mentor; //right device
    public GameObject trainee; //left device
    public GameObject mockTrainee; // copy of trainee
    private GameObject mockMentor;

    public string guidanceCue;
    private Vector3 offset = new Vector3(30, 0, 0);

    // Guidance Coroutine Variables
    private float deltaTime = 1f; // 
    private bool CR_running = false;
    private bool firstCR = true;
    private bool showMentor = false;

    private int[] trialList = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 }; //3,

    // Arduino Variables
    SerialPort sp = new SerialPort("COM10", 74880); // BAUD rate changed to 74880

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
        
        if (!firstCR)
        {
            // mentor and trainee positions
            Vector3 mPos = mentor.transform.position;
            Vector3 tPos = trainee.transform.position - offset;

            Vector3 posDiff = mPos - tPos;

            float[] posDiffArray = new float[] { Math.Abs(posDiff.x), Math.Abs(posDiff.y), Math.Abs(posDiff.z) };
            float posMax = posDiffArray.Max();
            int maxIdx = posDiffArray.ToList().IndexOf(posMax); // x = 0, y = 1, z = 2
            int cueNum = maxIdx;

            // Want to write {1, 2, 3, 4, 5, 6}
            if (posDiff[maxIdx] > 0) // mentor is further right, back, or up
            {
                cueNum += 1; // 1 2 3
            }
            else // mentor is further left, forward, or down
            {
                cueNum += 4; // 4 5 6
            }
 
            guidanceCue = cueNum.ToString();
            
            sp.Write(guidanceCue);
            //Debug.Log("cue: " + guidanceCue);

            // END COROUTINE
            yield return new WaitForSeconds(deltaTime);            
        }
        else
        {
            yield return new WaitForSeconds(3);
            firstCR = false;
        }
            
        /*
        // Optional Debug Logs 
        Debug.Log("mentor position: " + mPos);
        Debug.Log("trainee position: " + tPos);
        Debug.Log("position difference: " + posDiff);

        
        Debug.Log("index: " + maxIdx);
        Debug.Log("absolute max: " + posDiff[maxIdx]);
        */
        
        CR_running = false;
    }

    // Update is called once per frame
    void Update()
    {
        mockTrainee.transform.position = trainee.transform.position - offset;

        if (showMentor)
        {
            mockMentor.transform.position = mentor.transform.position + offset;
        }

        // If the session has started and a coroutine is not running, call for guidance cue.
        if (session.InTrial & !CR_running)
        {
            StartCoroutine(GuidanceCoroutine());
        }
    }

    public void WaitToGuide(Trial trial)
    {
        firstCR = true;
        showMentor = false;
        
        if (trialList.Contains(trial.number - 1))
        {
            Destroy(mockMentor);
        }
    
        if (trialList.Contains(trial.number))
        {
            showMentor = true;
            mockMentor = Instantiate(Resources.Load("mockMentor", typeof(GameObject))) as GameObject;
        }
        
    }
}
