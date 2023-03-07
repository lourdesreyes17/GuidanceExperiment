using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// add the UXF namespace
using UXF;

public class TelementoringExperimentGenerator : MonoBehaviour
{

    // generate the blocks and trials for the session.
    // the session is passed as an argument by the event call.
    public void Generate(Session session)
    {
        // generate six blocks
        // get training and testing trial amounts
        int nTrainingTrials = session.settings.GetInt("training_trials");
        int nTestingTrials = session.settings.GetInt("testing_trials");

        // 1. training block for first condition
        // create block and set delta time according to file number
        Block firstConditionTraining = session.CreateBlock(nTrainingTrials);
        firstConditionTraining.settings.SetValue("train_test_label", "Training");
        
        Block firstConditionTesting = session.CreateBlock(nTestingTrials);
        firstConditionTesting.settings.SetValue("train_test_label", "Testing");

    }
}
