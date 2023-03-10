using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.UI;

public class TraineeInstructions : MonoBehaviour
{
    public Text blockLabel;
    public Text trialLabel; // indicates trial and condition number for each trial
    public Text startInstructions; // tells trainee to place stylus at start point

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
        startInstructions.text = "";
    }

    public void InstructToStart(Trial trial)
    {
        startInstructions.text = "Place stylus on Start Point \n Wait for guidance cues...";
    }

}
