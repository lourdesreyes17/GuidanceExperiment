using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UXF;
public class PositionTarget : MonoBehaviour
{
    // reference to the UXF Session - so we can start the trial.
    public Session session;

    public float offset;

    public TextAsset xFile;          // Set this in the inspector at the beginning of the experiment, put text file in Assets.
    private List<string> xVals;       // Make a list of the floats in the text file
    public TextAsset yFile;          // Set this in the inspector at the beginning of the experiment, put text file in Assets.
    private List<string> yVals;       // Make a list of the floats in the text file

    //public TextAsset positionFile; *************vvv*************
    //private List<string> xVals;
    //private List<string> yVals;
    //private List<string> yVals; *************^^^************
    int pnum = 0;                           // initialize iterator to get new path after each trial
    float zVal = -2.5f;
    // Start is called before the first frame update
    void Start()
    {
        // Read text file and store contents in a list
        xVals = new List<string>(xFile.text.Split(","));
        yVals = new List<string>(yFile.text.Split(","));

        //xyzLists = new List<string>(xFile.text.Split("\n")); ***************vvv**********
        //xVals = new List<string>(xyzLists[0].text.Split(",")); 
        //yVals = new List<string>(xyzLists[1].text.Split(",")); 
        //zVals = new List<string>(xyzLists[2].text.Split(",")); *************^^^************
    }

    // assign this function to run On Trial Begin
    public void UpdatePosition(Trial trial)
    {
        float currentXVal = float.Parse(xVals[pnum]) + offset;
        float currentYVal = float.Parse(yVals[pnum]);
        // float currentZVal = float.Parse(zVals[pnum]); ****************************

        transform.position = new Vector3(currentXVal, currentYVal, zVal);
        //gameObject.transform.position = new Vector3(currentXVal, currentYVal, currentZVal); ****************************

        // use settings.Get*() to access settings (independent variables)
        if (offset == 0)
        {
            session.CurrentTrial.settings.SetValue("target_x", currentXVal);
            session.CurrentTrial.settings.SetValue("target_y", currentYVal);
            //session.CurrentTrial.settings.SetValue("target_z", currentZVal); ****************************
            Debug.Log("Target x,y: " + currentXVal + "," + currentYVal);
            //Debug.Log("Target x,y: " + currentXVal + "," + currentYVal + "," + curentZVal); ****************************
        }

        pnum++;
    }

}
