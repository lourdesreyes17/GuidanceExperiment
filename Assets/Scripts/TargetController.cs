using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// add the UXF namespace     
using UXF; // <-- new
using System.Threading;

public class TargetController : MonoBehaviour
{
    // reference to the UXF Session - so we can end the trial.
    public Session session;

    private int[] trialList = { 1, 2, 4, 13, 14, 15, 16, 17, 18, 19, 20, 21 }; //3, 5, 6

    // define 3 public variables - we can then assign their color values in the inspector.
    public Material blue;
    public Material transparent;
    public Material green;

    private Material mainMaterial;

    void Start() // <-- new
    {
        // disable the whole GameObject immediately
        gameObject.SetActive(false);

        if (transform.parent.name == "Mentor Side")
        {
            mainMaterial = blue;
        }
    }

    IEnumerator Countdown()
    {
        GetComponent<MeshRenderer>().material = green;
        yield return new WaitForSeconds(1f); // coroutine does not take affect until 0.5 seconds
        

        if (session.InTrial & transform.parent.name == "Trainee Side")
        {
            // disable the whole GameObject
            gameObject.SetActive(false);

            // end current trial
            session.EndCurrentTrial();
            Debug.Log("End of Trial");
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "NeedleTip") 
        {
            StartCoroutine(Countdown());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "NeedleTip")
        {
            StopAllCoroutines();
            GetComponent<MeshRenderer>().material = mainMaterial;
        }
    }
    

    public void ColorTarget(Trial trial)
    {
        if (!trialList.Contains(trial.number) & transform.parent.name == "Trainee Side")
        {
            mainMaterial = transparent;
        }
        else
        {
            mainMaterial = blue;
        }

        GetComponent<MeshRenderer>().material = mainMaterial;
    }
    
}
