using UnityEngine;
using System.Collections.Generic;

public class InteractableScript : MonoBehaviour
{
    [SerializeField] private int InteractDistance = 10;
    [SerializeField] private Transform handle;
    [SerializeField] private List<string> items = new List<string>() { "Interact", "Cable" };

    [SerializeField] private CableConnect cableConnect;
    public int selectedDevice = 0;

    private Interacted objectBeingInteractedWith;
    private bool isConnecting;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { selectedDevice = 0; Debug.Log("Interact"); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { selectedDevice = 1; Debug.Log("Cable connect"); }

        // Use GetMouseButtonDown for single click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(handle.position, handle.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, InteractDistance))
            {
                if (selectedDevice == 0) // Interact
                {
                    Interacted interacted = hit.collider.GetComponent<Interacted>();
                    if (interacted != null && hit.collider.gameObject.name != "Switch")
                    {
                        objectBeingInteractedWith = interacted;
                        interacted.beingInteracted = true;
                    }
                    else if (interacted != null)
                    {
                        objectBeingInteractedWith = interacted;
                        interacted.beingInteracted = !interacted.beingInteracted;
                    }
                }
                else if (selectedDevice == 1) // Cable connect
                {
                    // Clicked IN while not already connecting
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("IN") && !isConnecting)
                    {
                        isConnecting = true;
                        cableConnect.isBeingConnected = true;
                        cableConnect.inObject = hit.collider.gameObject;
                        
                       
                        
                    }
                    // Clicked OUT while connecting
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("OUT") && isConnecting)
                    {
                        cableConnect.outObject = hit.collider.gameObject;
                        isConnecting = false;
                       
                        Debug.Log("FINISHIED");
                    }
                }
            }
        }
        else if (objectBeingInteractedWith != null && !Input.GetMouseButton(0))
        {
            if (objectBeingInteractedWith.gameObject.name != "Switch")
            {
                objectBeingInteractedWith.beingInteracted = false;
                objectBeingInteractedWith = null; // reset
            }
            
        }
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine(handle.position, handle.position + handle.forward * InteractDistance, Color.blue);
    }
}
