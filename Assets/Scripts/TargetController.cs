using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add the UXF namespace     
using UXF; // <-- new

public class TargetController : MonoBehaviour
{
    // reference to the UXF Session - so we can end the trial.
    public Session session; // <-- new

    void Start() // <-- new
    {
        // disable the whole GameObject immediately
        gameObject.SetActive(false); // <-- new
    }

    /// OnTriggerEnter is called when the Collider 'other' enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "NeedleTip")
        {
            // disable the whole GameObject
            gameObject.SetActive(false);

            // end current trial
            session.EndCurrentTrial(); // <-- new
            Debug.Log("End of Trial");
        }
    }
}
