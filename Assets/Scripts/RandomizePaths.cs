using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

using UXF;

public class RandomizePaths : MonoBehaviour
{
    // reference to the UXF Session - so we can start the trial.
    public Session session;

    private bool PathExists = false;        // toggle PathExists so functions only run once

    public TextAsset protocolFile;          // Set this in the inspector at the beginning of the experiment, put text file in Assets.
    private List<string> pathStrings;       // Make a list oSf the strings in the text file
    int pnum = 0;                           // initialize iterator to get new path after each trial
    private GameObject pathGameObject;      // create gameobject for prefab to be set to
    private GameObject traineePathGameObject;

    private Vector3 traineePathPosition = new Vector3(12f, 2.1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        // Read text file and store contents in a list
        var content = protocolFile.text;
        var AllWords = content.Split(",");
        pathStrings = new List<string>(AllWords);
    }

    IEnumerator Countdown()
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(5f); // coroutine does not take affect until 0.5 seconds
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        Destroy(traineePathGameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (session.InTrial & !PathExists)
        {
            //mentor
            string currentPathString = "Path" + pathStrings[pnum]; // have name be the same as prefab ("Path#", no space)
            pathGameObject = Instantiate(Resources.Load(currentPathString, typeof(GameObject))) as GameObject;
            PathExists = true;

            //trainee
            traineePathGameObject = Instantiate(Resources.Load(currentPathString, typeof(GameObject))) as GameObject;
            traineePathGameObject.transform.position = traineePathPosition;

            StartCoroutine(Countdown());

        }

        if (!session.InTrial & PathExists)
        {
            Destroy(pathGameObject);
            PathExists = false;
            pnum++;
        }
    }

    // assign this function to run On Trial Begin
    public void UpdateSettings(Trial trial)
    {
        // use settings.Get*() to access settings (independent variables)
        session.CurrentTrial.settings.SetValue("path_number", pathStrings[pnum]);
        Debug.Log("Current path: " + pathStrings[pnum]);
    }
}