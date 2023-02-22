using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// add the UXF namespace
using UXF;

public class TelementoringExperimentGenerator : MonoBehaviour
{
    // initiate text file with block information
    public TextAsset conditionSettingsFile;                  // Set this in the inspector at the beginning of the experiment, put text file in Assets.
    private List<string> conditionSettingsFloats;       // Make a list of the floats in the text file

    // Start is called before the first frame update
    void Start()
    {
        var content = conditionSettingsFile.text;
        var allConditionSettings = content.Split(", ");
        conditionSettingsFloats = new List<string>(allConditionSettings);
    }

    // generate the blocks and trials for the session.
    // the session is passed as an argument by the event call.
    public void Generate(Session session)
    {
        // generate six blocks
        // get training and testing trial amounts
        int nTrainingTrials = session.settings.GetInt("training_trials");
        int nTestingTrials = session.settings.GetInt("testing_trials");

        // 1. training block for first condition
        // get condition setting
        float firstDeltaTime = float.Parse(conditionSettingsFloats[0], CultureInfo.InvariantCulture.NumberFormat);
        Debug.Log("First Condition Setting: dt = " + firstDeltaTime);
        // create block and set delta time according to file number
        Block firstConditionTraining = session.CreateBlock(nTrainingTrials);
        firstConditionTraining.settings.SetValue("delta_time", firstDeltaTime);
        firstConditionTraining.settings.SetValue("train_test_label", "Training");
        Block firstConditionTesting = session.CreateBlock(nTestingTrials);
        firstConditionTesting.settings.SetValue("delta_time", firstDeltaTime);
        firstConditionTesting.settings.SetValue("train_test_label", "Testing");

        // IMPORTANT: Commented out blocks two and three for training purposes
        /*
        // 2. training block for second condition
        // get condition setting
        float secondDeltaTime = float.Parse(conditionSettingsFloats[1], CultureInfo.InvariantCulture.NumberFormat); ;
        Debug.Log("Second Condition Setting: dt = " + secondDeltaTime);
        // create block and set delta time according to file number
        Block secondConditionTraining = session.CreateBlock(nTrainingTrials);
        secondConditionTraining.settings.SetValue("delta_time", secondDeltaTime);
        secondConditionTraining.settings.SetValue("train_test_label", "Training");
        Block secondConditionTesting = session.CreateBlock(nTestingTrials);
        secondConditionTesting.settings.SetValue("delta_time", secondDeltaTime);
        secondConditionTesting.settings.SetValue("train_test_label", "Testing");

        // 3. training block for third condition
        // get condition setting
        float thirdDeltaTime = float.Parse(conditionSettingsFloats[2], CultureInfo.InvariantCulture.NumberFormat); ;
        Debug.Log("First Condition Setting: dt = " + thirdDeltaTime);
        // create block and set delta time according to file number
        Block thirdConditionTraining = session.CreateBlock(nTrainingTrials);
        thirdConditionTraining.settings.SetValue("delta_time", thirdDeltaTime);
        thirdConditionTraining.settings.SetValue("train_test_label", "Training");
        Block thirdConditionTesting = session.CreateBlock(nTestingTrials);
        thirdConditionTesting.settings.SetValue("delta_time", thirdDeltaTime);
        thirdConditionTesting.settings.SetValue("train_test_label", "Testing");
        */
    }
}
