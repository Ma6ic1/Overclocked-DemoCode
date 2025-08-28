using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private Interacted interactedwith;
    [SerializeField] private GameObject clicker;

    private bool onFlag;

    void Update()
    {
        if (interactedwith != null && interactedwith.beingInteracted && !onFlag)
        {
            Debug.Log("Is ON");
            onFlag = true;
            //visuals
            clicker.transform.position = clicker.transform.position + new Vector3(0, -0.2f, 0);
        }
        else if (interactedwith != null && !interactedwith.beingInteracted && onFlag)
        {

            Debug.Log("Is now OFF");
            onFlag = false;
            //Visuals
            clicker.transform.position = clicker.transform.position + new Vector3(0, 0.2f, 0);
        }
    }
}
