using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.UI;

public class TraineeInstructions : MonoBehaviour
{
    public Text blockLabel;
    public Text trialLabel; // indicates trial and condition number for each trial
    public Text instructions; // tells trainee to place stylus at start point

    public Session session;

    public void LabelBlock(Block block)
    {
        int blockNum = session.currentBlockNum;
        string traintestLabel = session.CurrentBlock.settings.GetString("train_test_label");
        blockLabel.text = "Block " + blockNum.ToString() + "  - " + traintestLabel;
    }

    public void LabelTrial(Trial trial)
    {
        int trialNum = session.currentTrialNum;
        trialLabel.text = "Trial " + trialNum.ToString();
        instructions.text = "";

        if (trialNum == 1 | trialNum == 2)
        {
            instructions.text = "Reach for Target (red)";
        }
    }

    public void InstructToStart(Trial trial)
    {
        instructions.text = "Place stylus on Start Point (blue) \n Wait for guidance cues...";

    }

}
