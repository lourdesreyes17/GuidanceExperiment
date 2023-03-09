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
        int nPretrainingTrials = session.settings.GetInt("pretraining_trials");
        int nTrainingTrials = session.settings.GetInt("training_trials");
        int nTestingTrials = session.settings.GetInt("testing_trials");

        // create blocks 1: pretraining, 2: training, 3: testing
        Block pretrainingBlock = session.CreateBlock(nPretrainingTrials);
        pretrainingBlock.settings.SetValue("train_test_label", "Pretraining");
        pretrainingBlock.settings.SetValue("start_z", 0); // change start point to have 0 z position
        //pretrainingBlock.settings.SetValue("table_set_active", false); // setting .json: "table_set_active": true,


        Block trainingBlock = session.CreateBlock(nTrainingTrials);
        trainingBlock.settings.SetValue("train_test_label", "Training");
        
        Block testingBlock = session.CreateBlock(nTestingTrials);
        testingBlock.settings.SetValue("train_test_label", "Testing");

    }
}
