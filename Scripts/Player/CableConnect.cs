using UnityEngine;

public class CableConnect : MonoBehaviour
{
    public GameObject inObject;
    public GameObject outObject;
    public bool isBeingConnected;

    [SerializeField] private GameObject cablePrefab;   
    [SerializeField] private Transform handle;
    [SerializeField] private Transform ghostConnect;

    private bool rendererCreated;
    private LineRenderer line;
    private ConnectionScript connectionScript;
    private Interacted inInteracted;
    private Interacted outInteracted;

    void Update()
    {
        if (!isBeingConnected) return;


        if (!rendererCreated && inObject != null)
        {
            GameObject cableInstance = Instantiate(cablePrefab, inObject.transform);
            cableInstance.name = "CableLine";

            line = cableInstance.GetComponent<LineRenderer>();
            connectionScript = cableInstance.GetComponent<ConnectionScript>();

            if (line == null)
            {
                Debug.LogError("Cable prefab is missing a LineRenderer!");
                return;
            }

            line.positionCount = 2;
            line.SetPosition(0, inObject.transform.position);
            rendererCreated = true;
        }

      
        if (rendererCreated && outObject == null)
        {
            Ray ray = new Ray(handle.position, handle.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                ghostConnect.position = hit.point;
                line.SetPosition(1, ghostConnect.position);
            }
        }

 
        if (rendererCreated && inObject != null && outObject != null)
        {
            line.SetPosition(1, outObject.transform.position);
            if (inObject.transform.parent != null)
            inInteracted = inObject.transform.parent.GetComponent<Interacted>();

            if (outObject.transform.parent != null)
                outInteracted = outObject.transform.parent.GetComponent<Interacted>();

            if (inInteracted != null && outInteracted != null)
            {
                connectionScript.inSignal = inInteracted;
                connectionScript.outSignal = outInteracted;
            }


            inInteracted = null;
            outInteracted = null;
            connectionScript = null;
            inObject = null;
            outObject = null;
            isBeingConnected = false;
            rendererCreated = false;
            line = null;
        }
    }
}
