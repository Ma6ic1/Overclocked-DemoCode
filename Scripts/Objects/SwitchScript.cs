using UnityEngine;

public class SwitChScript : MonoBehaviour
{
    [SerializeField] private Interacted interactedwith;
    [SerializeField] private GameObject lever;

    private bool previousState = false;

    void Update()
    {
        if (interactedwith == null) return;

        bool currentState = interactedwith.beingInteracted;

        // Only act when the state actually changes
        if (currentState != previousState)
        {
            previousState = currentState;

            if (currentState)
            {
                Debug.Log("Is ON");
                lever.transform.localRotation = Quaternion.Euler(45f, 0f, 0f);
            }
            else
            {
                Debug.Log("Is now OFF");
                lever.transform.localRotation = Quaternion.Euler(-45f, 0f, 0f);
            }
        }
    }
}
