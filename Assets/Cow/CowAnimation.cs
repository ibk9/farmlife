using UnityEngine;

public class CowAI : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float turnSpeed = 120f;
    public float turnInterval = 5f;
    public float eatDuration = 10f;
    public float eatInterval = 40f;
    public AudioSource CowSound;

    private Animator animator;
    private Rigidbody rb;
    private float timeSinceLastTurn;
    private float eatTimer;
    private float timeSinceLastEat;
    private bool isWalking = false;
    private bool isEating = false;
   // private bool isHaybaleGrabbed = false;

    private void Start()
    {
        CowSound.Play();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        eatTimer = 0f;
        timeSinceLastEat = eatInterval;
    }

    private void Update()
    {
        timeSinceLastTurn += Time.deltaTime;
        timeSinceLastEat += Time.deltaTime;

       /* if (isHaybaleGrabbed)
        {
            WalkToPosition(new Vector3(10f, 0.7f, -4f));
            RotateCow(new Vector3(0f, -90f, 0f));
            isHaybaleGrabbed = false;
        }*/
        if (timeSinceLastEat >= eatInterval)
        {
            WalkToPosition(new Vector3(6f, 0.07f, -20f));
            RotateCow(new Vector3(0f, 90f, 0f));
            StartEating();
        }
        else if (!isWalking && !isEating)
        {
            StartWalking();
        }

        if (isWalking)
        {
            MoveCow();
            CheckWallCollision();
        }

        if (timeSinceLastTurn >= turnInterval && isWalking)
        {
            TurnRandomDirection();
            timeSinceLastTurn = 0f;
        }

        if (isEating)
        {
            UpdateEatTimer();
        }
    }

    private void WalkToPosition(Vector3 targetPosition)
    {
        StopWalking();
        transform.position = targetPosition;
        transform.eulerAngles = new Vector3(0f, -90f, 0f); // Set the rotation explicitly
        timeSinceLastEat = 0f;
    }

    private void RotateCow(Vector3 targetRotation)
    {
        StopWalking();
        transform.eulerAngles = targetRotation;
    }

    private void StartWalking()
    {
        isWalking = true;
        animator.SetBool("isWalking", true);
    }

    private void StopWalking()
    {
        isWalking = false;
        animator.SetBool("isWalking", false);
    }

    private void MoveCow()
    {
        Vector3 movement = walkSpeed * transform.right * Time.deltaTime;
        transform.position += movement;
    }

    private void TurnRandomDirection()
    {
        int randomDirection = Random.Range(0, 4);
        Quaternion targetRotation = Quaternion.Euler(0f, randomDirection * 90f, 0f);
        StartCoroutine(TurnCoroutine(targetRotation));
    }

    private System.Collections.IEnumerator TurnCoroutine(Quaternion targetRotation)
    {
        StopWalking();

        Quaternion startRotation = transform.rotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * turnSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        StartWalking();
    }

    private void CheckWallCollision()
    {
        Collider cowCollider = GetComponent<Collider>();
        Collider[] colliders = Physics.OverlapBox(transform.position, cowCollider.bounds.extents, transform.rotation, LayerMask.GetMask("WallLayer"));

        if (colliders.Length > 0)
        {
            StopWalking();
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Container"))
                {
                    Vector3 newPosition = new Vector3(13f * transform.localScale.x, 0.15f * transform.localScale.y, -2f * transform.localScale.z);
                    transform.position = newPosition;
                    break;
                }
            }
        }
    }

    private void StartEating()
    {
        isEating = true;
        animator.SetBool("isEating", true);
        timeSinceLastEat = 0f;
    }

    private void UpdateEatTimer()
    {
        eatTimer += Time.deltaTime;

        if (eatTimer >= eatDuration)
        {
            isEating = false;
            eatTimer = 0f;
            StartWalking();
        }
    }

    // New method to set the haybale grabbed flag
    /*public void HaybaleGrabbed()
    {
        isHaybaleGrabbed = true;
    }*/
}