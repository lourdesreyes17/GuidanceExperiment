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

    int pnum = 0;                           // initialize iterator to get new path after each trial
    float zVal = -2.5f;
    // Start is called before the first frame update
    void Start()
    {
        // Read text file and store contents in a list
        xVals = new List<string>(xFile.text.Split(","));
        yVals = new List<string>(yFile.text.Split(","));
    }

    // assign this function to run On Trial Begin
    public void UpdatePosition(Trial trial)
    {
        float currentXVal = float.Parse(xVals[pnum]) + offset;
        float currentYVal = float.Parse(yVals[pnum]);

        gameObject.transform.position = new Vector3(currentXVal, currentYVal, zVal);

        // use settings.Get*() to access settings (independent variables)
        if (offset == 0)
        {
            session.CurrentTrial.settings.SetValue("target_x", currentXVal);
            session.CurrentTrial.settings.SetValue("target_y", currentYVal);
            Debug.Log("Target x,y: " + currentXVal + "," + currentYVal);
        }
        
        pnum++;
    }

}
