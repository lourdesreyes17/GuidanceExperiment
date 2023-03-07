using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.UI;

public class MentorInstructions : MonoBehaviour
{
    public Text blockLabel;
    public Text conditionLabel;
    public Text trialLabel;

    public Session session;

    public void LabelBlock(Block block)
    {
        conditionLabel.text = "Condition: Haptic Cues";
        int blockNum = session.currentBlockNum;
        string traintestLabel = session.CurrentBlock.settings.GetString("train_test_label");
        blockLabel.text = "Block " + blockNum.ToString() + " - " + traintestLabel;
    }

    public void LabelTrial(Trial trial)
    {
        int trialNum = session.currentTrialNum;
        trialLabel.text = "Trial " + trialNum.ToString();
    }

}
