using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UXF;
public class PositionTarget : MonoBehaviour
{
    // reference to the UXF Session - so we can start the trial.
    public Session session;

    //public float offset;

    public GameObject mentorTarget;
    public GameObject traineeTarget;

    public TextAsset positionFile;
    private List<string> xyzLists;
    private List<string> xVals;
    private List<string> yVals;
    private List<string> zVals;

    int pnum = 0;                           // initialize iterator to get new path after each trial

    // Start is called before the first frame update
    void Start()
    {
        xyzLists = new List<string>(positionFile.text.Split("\n"));
        xVals = new List<string>(xyzLists[0].Split(",")); 
        yVals = new List<string>(xyzLists[1].Split(",")); 
        zVals = new List<string>(xyzLists[2].Split(","));
    }

    // assign this function to run On Trial Begin
    public void UpdatePosition(Trial trial)
    {
        float currentXVal = float.Parse(xVals[pnum]);
        float currentYVal = float.Parse(yVals[pnum]);
        float currentZVal = float.Parse(zVals[pnum]);

        //transform.position = new Vector3(currentXVal, currentYVal, zVal);
        mentorTarget.transform.localPosition = new Vector3(currentXVal, currentYVal, currentZVal);
        traineeTarget.transform.localPosition = new Vector3(currentXVal, currentYVal, currentZVal);

        // use settings.Get*() to access settings (independent variables)
        session.CurrentTrial.settings.SetValue("target_x", currentXVal);
        session.CurrentTrial.settings.SetValue("target_y", currentYVal);
        session.CurrentTrial.settings.SetValue("target_z", currentZVal);
        

        Debug.Log("Target position:" + traineeTarget.transform.localPosition);

        Debug.Log("Target local position: " + traineeTarget.transform.position);

        pnum++;
    }

}
