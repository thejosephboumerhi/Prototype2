using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverVehicleController : MonoBehaviour
{
    //The link for this code is in the Prototype2 Wiki
    //Identifies the body for the racer
    Rigidbody HoverRacer;

   
    //Looks for its rigidbody with this script in it
    private void Start()
    {
        HoverRacer = this.GetComponent<Rigidbody>();
    }

    //Floats to tweak on how much turning you want and how fast you are going to go, affected too by the mass and drags of the rigidbody 
    public float multiplier;
    public float driveForce, turnTorque;

    //Creates raycasts going down to detect a surface to hover above, utilizes empty objects below the racer.
    //From what I was see from the video's comments section and other similar, the number of raycasting objects
    //could be increased to provide more accuracy, maybe 6 or 8 total.
    public Transform[] gravPoints = new Transform[4];
    public RaycastHit[] surfaceHits = new RaycastHit [4];

    //Sends force down where the empty objects point down, uses an input system using "axes",
    //which be found under "Input Manager" in "Project Settings".
    private void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
            ApplyForce(gravPoints[i], surfaceHits[i]);

        //The "Axes" Input
        HoverRacer.AddForce(Input.GetAxis("Vertical") * driveForce * transform.forward);
        HoverRacer.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up);
    }

    private void Update()
    {
        
    }

    //Applies force if when there is a surface "below", looks for the "Y" axes, so it could "float".
    void ApplyForce(Transform gravPoints, RaycastHit hit)
    {
        if (Physics.Raycast(gravPoints.position, -gravPoints.up, out hit)) {
            float force = 0;
            force = Mathf.Abs(1/(hit.point.y - gravPoints.position.y));
            HoverRacer.AddForceAtPosition(transform.up * force * multiplier, gravPoints.position, ForceMode.Acceleration);
        }
    }
}
