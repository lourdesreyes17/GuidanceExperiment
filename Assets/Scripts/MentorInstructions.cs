using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.UI;

public class MentorInstructions : MonoBehaviour
{
    public Text mentorLabel;
    public Text blockLabel;
    public Text conditionLabel;
    //public Text traintestLabel;
    public Text trialLabel;
    //public Text startInstructions;

    public Session session;

    void Awake()
    {
        // clear instructions until we start the session
        mentorLabel.text = "";
        conditionLabel.text = "";
        blockLabel.text = "";
        
        //traintestLabel = "";
        trialLabel.text = "";
        //startInstructions.text = "";

    }

    // assign to On Session Begin event
    public void Present(Session session)
    {
        mentorLabel.text =  session.settings.GetString("mentor_label");
    }

    public void LabelBlock(Block block)
    {
        float currentCondition = session.CurrentBlock.settings.GetFloat("delta_time");
        conditionLabel.text = "Condition: " + currentCondition.ToString() + "s cue interval";
        int blockNum = session.currentBlockNum;
        string traintestLabel = session.CurrentBlock.settings.GetString("train_test_label");
        blockLabel.text = "Block " + blockNum.ToString() + " (" + traintestLabel + ")";
        //traintestLabel.text = session.CurrentTrial.settings.GetString("train/test label");
    }

    public void LabelTrial(Trial trial)
    {
        int trialsPerBlock = session.settings.GetInt("training_trials");
        int trialNum = session.currentTrialNum;
        trialNum = trialNum-(trialsPerBlock*(session.currentBlockNum-1));
        trialLabel.text = "Trial " + trialNum.ToString();
        //traintestLabel.text = session.CurrentTrial.settings.GetString("train/test label");
    }

}
