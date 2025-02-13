using UnityEngine;

public class RopeGenerator : MonoBehaviour
{
    public GameObject ropeSegmentPrefab; 
    public Transform startPoint;         
    public int segmentCount = 10;       
    public float segmentSpacing = 0.5f;  

    void Start()
    {
        GameObject previousSegment = null; 

        for (int i = 0; i < segmentCount; i++)
        {
            
            Vector3 position = startPoint.position + Vector3.down * i * segmentSpacing;

          
            GameObject newSegment = Instantiate(ropeSegmentPrefab, position, Quaternion.identity, transform);

            
            if (previousSegment != null)
            {
                HingeJoint joint = newSegment.GetComponent<HingeJoint>();
                joint.connectedBody = previousSegment.GetComponent<Rigidbody>();
            }

            
            previousSegment = newSegment;
        }

        
        Rigidbody startRigidbody = startPoint.GetComponent<Rigidbody>();
        if (startRigidbody == null)
        {
            startRigidbody = startPoint.gameObject.AddComponent<Rigidbody>();
            startRigidbody.isKinematic = true; 
        }

        if (previousSegment != null)
        {
            HingeJoint lastJoint = previousSegment.GetComponent<HingeJoint>();
            lastJoint.connectedBody = startRigidbody;
        }
    }
}
