using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VehicleAgent : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform parkPoint;
    public int currentWaypointIndex = 0;

    public GameObject reel;
    public GameObject spray;

    private NewCarUserControl newCarUserControl;
    private NavMeshAgent agent;

    public bool isSpecialVehicle = false;
    private int lapsCompleted = 0;

    private bool startAI = false;
    private bool goingToPark = false;
    private bool reachedLastWaypoint = false;

    enum State
    {
        MovingToWaypoint,
        MovingToPark,
        Stopped
    }

    private State currentState = State.MovingToWaypoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        newCarUserControl = GetComponent<NewCarUserControl>();
        if (startAI)
        {
            SetNextWaypoint();
        }
    }

    void Update()
    {
        if (startAI)
        {
            switch (currentState)
            {
                case State.MovingToWaypoint:
                    MoveToWaypoint();
                    break;

                case State.MovingToPark:
                    MoveToPark();
                    break;

                case State.Stopped:
                    break;
            }
        }
    }

    void MoveToWaypoint()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
           

            if (currentWaypointIndex == waypoints.Length-1)
            {
                reachedLastWaypoint = true;
                if (isSpecialVehicle)
                {
                    if (reachedLastWaypoint)
                    {
                        lapsCompleted++;

                        if (lapsCompleted >= 3)
                        {
                            currentState = State.MovingToPark;
                        }
                        else
                        {
                            reachedLastWaypoint = false;
                        }
                    }
                }
                else
                {
                    if (reachedLastWaypoint)
                    {
                        currentState = State.MovingToPark;
                    }
                }
            }
            SetNextWaypoint();
        }
    }

    void MoveToPark()
    {
        if (reachedLastWaypoint && !goingToPark)
        {
            agent.SetDestination(parkPoint.position);
            goingToPark = true;
        }

        if (goingToPark && !agent.pathPending && agent.remainingDistance < 0.1f)
        {
            StopAI();
        }
    }

    void SetNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    public void StartAI()
    {
        startAI = true;
        newCarUserControl.joystick = 0.1f;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentState = State.MovingToWaypoint;
        if (isSpecialVehicle)
        {
            SetNextWaypoint();
        }
        if (reel != null) { reel.SetActive(true); }
        if (spray != null) { spray.SetActive(true); }

    }

    public void StopAI()
    {
        startAI = false;
        newCarUserControl.joystick = 0f;
        agent.ResetPath();
        goingToPark = false;
        reachedLastWaypoint = false;
        currentState = State.Stopped;
        if (reel != null) { reel.SetActive(false); }
        if (spray != null) { spray.SetActive(false); }
    }
}
