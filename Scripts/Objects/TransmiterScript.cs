using UnityEngine;

public class TransmiterScript : MonoBehaviour
{
    [SerializeField] private Interacted interacted;   // transmitter's interact state
    [SerializeField] private float sendFreq = 0.1f;
    [SerializeField] private float radius;
    private float timer;

    void Update()
    {
        if (interacted.beingInteracted && timer >= sendFreq)
        {
            FindReceivers(true);  // transmitter ON
            timer = 0;
        }
        else if (interacted.beingInteracted)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // transmitter OFF → turn off receivers too
            FindReceivers(false);
            timer = 0;
        }
    }

    void FindReceivers(bool state)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.name == "Reciver")
            {
                float receiverDistance = Vector3.Distance(hit.transform.position, transform.position);

                Interacted receiverInteracted = hit.GetComponent<Interacted>();
                if (receiverInteracted != null)
                {
                    receiverInteracted.beingInteracted = state; // mirror transmitter state
                }

                ReciverScript receiverScript = hit.GetComponent<ReciverScript>();
                if (receiverScript != null)
                {
                    receiverScript.reciverDelay = receiverDistance;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
