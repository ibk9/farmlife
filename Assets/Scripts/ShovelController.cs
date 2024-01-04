using UnityEngine;

public class ShovelController : MonoBehaviour
{
    public int plantableHitsRequired = 5;
    private int plantableHitCount = 0;

    public int holeHitsRequired = 5;
    private int holeHitCount = 0;

    // References to the Hole objects
    public GameObject hole1;
    public GameObject hole2;
    public GameObject hole3;
    public GameObject hole4;
    public GameObject hole5;
    public GameObject hole6;


    // References to the Pile objects
    public GameObject pile1;
    public GameObject pile2;
    public GameObject pile3;
    public GameObject pile4;
    public GameObject pile5;
    public GameObject pile6;


    private void OnCollisionEnter(Collision collision)
    {
        int plantableNumber;
        if (collision.gameObject.tag.StartsWith("Plantable") && int.TryParse(collision.gameObject.tag.Substring("Plantable".Length), out plantableNumber))
        {
            Debug.Log($"Collided with {collision.gameObject.tag}");

            // Increase the hit count for plantables
            plantableHitCount++;

            // Check if the required hits for plantables are reached
            if (plantableHitCount >= plantableHitsRequired)
            {
                // Activate the corresponding Hole object based on the plantable number
                ActivateHole(plantableNumber);
                plantableHitCount = 0; // Reset hit count for plantables
            }
        }

        int holeNumber;
        if (collision.gameObject.tag.StartsWith("Hole") && int.TryParse(collision.gameObject.tag.Substring("Hole".Length), out holeNumber))
        {
            Debug.Log($"Collided with {collision.gameObject.tag}");

            // Increase the hit count for holes
            holeHitCount++;

            // Check if the required hits for holes are reached
            if (holeHitCount >= holeHitsRequired)
            {
                // Activate the corresponding Pile object based on the hole number
                ActivatePile(holeNumber);
                holeHitCount = 0; // Reset hit count for holes
            }
        }
    }

    private void ActivateHole(int plantableNumber)
    {
        // Activate the corresponding Hole object based on the plantable number
        GameObject holeObject = GetHoleObject(plantableNumber);
        ActivateObject(holeObject, $"Hole{plantableNumber}");
    }

    private void ActivatePile(int holeNumber)
    {
        // Activate the corresponding Pile object based on the hole number
        GameObject pileObject = GetPileObject(holeNumber);
        ActivateObject(pileObject, $"Pile{holeNumber}");
    }

    private void ActivateObject(GameObject obj, string objName)
    {
        if (obj != null)
        {
            // Activate the object
            obj.SetActive(true);
            Debug.Log($"Activated {objName}");
        }
        else
        {
            Debug.LogError($"{objName} reference is null");
        }
    }

    private GameObject GetHoleObject(int holeNumber)
    {
        // Get the corresponding Hole object based on the hole number
        switch (holeNumber)
        {
            case 1:
                return hole1;
            case 2:
                return hole2;
            case 3:
                return hole3;
            case 4:
                return hole4;
            case 5:
                return hole5;
            case 6:
                return hole6;
            default:
                Debug.LogError($"Invalid hole number: {holeNumber}");
                return null;
        }
    }

    private GameObject GetPileObject(int holeNumber)
    {
        // Get the corresponding Pile object based on the hole number
        switch (holeNumber)
        {
            case 1:
                return pile1;
            case 2:
                return pile2;
            case 3:
                return pile3;
            case 4:
                return pile4;
            case 5:
                return pile5;
            case 6:
                return pile6;
            default:
                Debug.LogError($"Invalid hole number: {holeNumber}");
                return null;
        }
    }
}
