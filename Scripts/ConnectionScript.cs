using UnityEngine;

public class ConnectionScript : MonoBehaviour
{
    public Interacted inSignal;   // device on the IN side
    public Interacted outSignal;  // device on the OUT side
    private bool signalChanged;
    void Update()
    {
        if (inSignal != null && outSignal != null)
        {
            if (signalChanged != inSignal.beingInteracted)
            {
                signalChanged = inSignal.beingInteracted;
                Debug.Log(inSignal.beingInteracted);
                Debug.Log(signalChanged);
                outSignal.beingInteracted = inSignal.beingInteracted;
            }
           
        }
    }
}
