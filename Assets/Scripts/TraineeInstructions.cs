using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.UI;

public class TraineeInstructions : MonoBehaviour
{
    public Text traineeLabel; // labels "Trainee Side" at beginning of session
    public Text blockLabel;
    public Text trialLabel; // indicates trial and condition number for each trial
    public Text startInstructions; // tells trainee to place stylus at start point

    public Session session;
    void Awake()
    {
        // clear instructions until we start the session
        traineeLabel.text = "";
        blockLabel.text = "";
        trialLabel.text = "";
        startInstructions.text = "";
    }

    // assign to On Session Begin event
    public void Present(Session session)
    {
        traineeLabel.text = session.settings.GetString("trainee_label");
        startInstructions.text = "Place stylus on Start Point \n Wait for guidance cues";
    }

    public void LabelBlock(Block block)
    {
        int blockNum = session.currentBlockNum;
        string traintestLabel = session.CurrentBlock.settings.GetString("train_test_label");
        blockLabel.text = "Block " + blockNum.ToString() + " (" + traintestLabel + ")";
    }

    public void LabelTrial(Trial trial)
    {
        int trialsPerBlock = session.settings.GetInt("training_trials");
        int trialNum = session.currentTrialNum;
        trialNum = trialNum - (trialsPerBlock * (session.currentBlockNum - 1));
        trialLabel.text = "Trial " + trialNum.ToString();
        startInstructions.text = "";
    }

    public void InstructToStart(Trial trial)
    {
        startInstructions.text = "Place stylus on Start Point \n Wait for guidance cues";
    }

}
