using UnityEngine;
public class HarvesterController : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;
    public Transform[] wheels;
    public Transform reel;

    private int currentWaypointIndex = 0;
    private bool isReached = false;
    private bool isRotating = false;

    void Update()
    {
        RotateReel();
    }

    void MoveToWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {

            if (isReached)
            {
                RotateHarvester();
                return;
            }
            if(isRotating)
            {
                TranslateHarvester();
                return;
            }

            // Move towards the current waypoint
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

            // Rotate the harvester's wheels
            RotateWheels();
            RotateReel();

            // Check if the harvester has reached the current waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                isReached = true;
            }
        }
        else
        {
            // Harvester has reached the last waypoint, you may want to stop or restart the movement here.
        }
    }


    void RotateWheels()
    {
        float movement = moveSpeed * Time.deltaTime;
        float rotationAmount = movement * rotationSpeed;

        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right, rotationAmount);
        }
    }
    void RotateReel()
    {
        float movement = moveSpeed * Time.deltaTime;
        float rotationAmount = movement * rotationSpeed;
        reel.Rotate(Vector3.right, rotationAmount);
        
    }
    void RotateHarvester()
    {
        Quaternion targetRotation;
        if (currentWaypointIndex % 2 == 0)
        {           
           targetRotation = Quaternion.Euler(0f, -180f, 0f);
           transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
        else
        {
            targetRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
       
       
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f)
        {
            isRotating = true;
            isReached = false;
            currentWaypointIndex++;           
        }     
    }
    void TranslateHarvester()
    {
        Vector3 rightMovement = new Vector3(1f, 0f, 0f) * 5f * Time.deltaTime;
        transform.position += rightMovement;
        if (Vector3.Distance(transform.position, new Vector3(waypoints[currentWaypointIndex].position.x, transform.position.y, transform.position.z)) < 0.1f)
        {
            isRotating = false;
        }
    }

}
