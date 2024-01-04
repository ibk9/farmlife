using UnityEngine;

public class HaybaleController : MonoBehaviour
{
    private int haybaleCount = 0;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Container"))
        {
            haybaleCount++;
            Debug.Log("Haybale count: " + haybaleCount);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Container"))
        {
            haybaleCount--;
            Debug.Log("Haybale count: " + haybaleCount);
        }
    }
}