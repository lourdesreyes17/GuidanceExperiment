using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

using UXF;

public class RandomizePaths : MonoBehaviour
{
    // reference to the UXF Session - so we can start the trial.
    public Session session;

    
    public TextAsset protocolFile;          // Set this in the inspector at the beginning of the experiment, put text file in Assets.
    private List<string> pathStrings;       // Make a list oSf the strings in the text file

    int pnum = 0;                           // initialize iterator to get new path after each trial
    private GameObject pathGameObject;      // create gameobject for prefab to be set to
    private GameObject traineePathGameObject;

    private Vector3 offset = new Vector3(30f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        // Read text file and store contents in a list
        pathStrings = new List<string>(protocolFile.text.Split(","));
    }


    // assign this function to run On Trial Begin
    public void UpdatePath(Trial trial)
    {
        string pathNum = pathStrings[pnum];
        string currentPathString = "Path" + pathNum; // have name be the same as prefab ("Path#", no space)

        //mentor
        pathGameObject = Instantiate(Resources.Load(currentPathString, typeof(GameObject))) as GameObject;
        //trainee
        traineePathGameObject = Instantiate(Resources.Load(currentPathString, typeof(GameObject))) as GameObject;
        traineePathGameObject.transform.position = traineePathGameObject.transform.position + offset;

        // use settings.Get*() to access settings (independent variables)
        session.CurrentTrial.settings.SetValue("path_number", pathNum);
        Debug.Log("Current path: " + pathNum);
    }

    public void RemovePath(Trial trial)
    {
        Destroy(pathGameObject);
        Destroy(traineePathGameObject);
        pnum++;
    }
}