using System.Collections;
using UnityEngine;

public class Watering : MonoBehaviour
{
    public GameObject water;
    public Material wetSoilMaterial;
    public Material originalSoilMaterial;
    public GameObject[] smallTrees; // Assign small tree objects in the Unity Editor
    public GameObject[] bigTrees;   // Assign big tree objects in the Unity Editor
    private bool isPouring = false;
    private bool isSmallTreeActive = true;

    void Update()
    {
        float currentRotation = transform.rotation.eulerAngles.x;

        if (currentRotation >= 335f && !isPouring)
        {
            StartCoroutine(PourWater());
        }
    }

    IEnumerator PourWater()
    {
        isPouring = true;

        water.SetActive(true);

        GameObject closestPile = GetClosestPile();

        if (closestPile != null)
        {
            yield return new WaitForSeconds(3f);
            ChangeMaterialToWetSoil(closestPile);

            // Deactivate water after changing material
            water.SetActive(false);

            yield return new WaitForSeconds(5f);

            // Revert material to the original soil material
            RevertMaterialToOriginal(closestPile);

            int pileNumber;
            if (int.TryParse(closestPile.name.Replace("Pile", ""), out pileNumber))
            {
                if (isSmallTreeActive)
                {
                    ActivateSmallTree(pileNumber);
                }
                else
                {
                    ActivateBigTree(pileNumber);
                }

                // Switch the state for the next watering
                isSmallTreeActive = !isSmallTreeActive;
            }
        }

        isPouring = false;
    }

    void ActivateSmallTree(int pileNumber)
    {
        if (pileNumber <= smallTrees.Length)
        {
            // Activate the corresponding small tree
            smallTrees[pileNumber - 1].SetActive(true);
            Debug.Log($"Activated Small Tree {pileNumber}.");
        }
        else
        {
            Debug.LogError($"Invalid pile number: {pileNumber}");
        }
    }

    void ActivateBigTree(int pileNumber)
    {
        if (pileNumber <= bigTrees.Length)
        {
            // Activate the corresponding big tree
            bigTrees[pileNumber - 1].SetActive(true);
            Debug.Log($"Activated Big Tree {pileNumber}.");
        }
        else
        {
            Debug.LogError($"Invalid pile number: {pileNumber}");
        }
    }

    GameObject GetClosestPile()
    {
        GameObject[] piles = GameObject.FindGameObjectsWithTag("Pile");
        GameObject closestPile = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject pile in piles)
        {
            float distance = Vector3.Distance(transform.position, pile.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPile = pile;
            }
        }

        return closestPile;
    }

    void ChangeMaterialToWetSoil(GameObject pile)
    {
        Renderer pileRenderer = pile.GetComponent<Renderer>();

        if (pileRenderer != null)
        {
            pileRenderer.material = wetSoilMaterial;
            Debug.Log($"Pile {pile.name} has been watered and changed to wet soil material.");
        }
    }

    void RevertMaterialToOriginal(GameObject pile)
    {
        Renderer pileRenderer = pile.GetComponent<Renderer>();

        if (pileRenderer != null)
        {
            pileRenderer.material = originalSoilMaterial;
            Debug.Log($"Wet soil material for Pile {pile.name} has reverted to the original material.");
        }
    }
//void HandleSmallTree()
//    {
//        // Deactivate the current small tree
//        if (currentSmallTreeIndex >= 0 && currentSmallTreeIndex < smallTrees.Length)
//        {
//            smallTrees[currentSmallTreeIndex].SetActive(false);
//            Debug.Log($"Deactivated Small Tree {currentSmallTreeIndex + 1}.");
//        }

//        // Increment the index to move to the next small tree
//        currentSmallTreeIndex++;

//        // Activate the next small tree
//        if (currentSmallTreeIndex < smallTrees.Length)
//        {
//            smallTrees[currentSmallTreeIndex].SetActive(true);
//            Debug.Log($"Activated Small Tree {currentSmallTreeIndex + 1}.");
//        }
//        else
//        {
//            Debug.Log("All small trees activated. No more small trees available.");
//        }
//    }

//    void DeactivateClosestPlant()
//    {
//        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");

//        if (currentWetPile != null)
//        {
//            GameObject closestPlant = null;
//            float closestPlantDistance = Mathf.Infinity;

//            foreach (GameObject plant in plants)
//            {
//                float distance = Vector3.Distance(currentWetPile.transform.position, plant.transform.position);

//                if (distance < closestPlantDistance)
//                {
//                    closestPlantDistance = distance;
//                    closestPlant = plant;
//                }
//            }

//            // Deactivate the closest plant object
//            if (closestPlant != null)
//            {
//                closestPlant.SetActive(false);
//                Debug.Log($"Deactivated the closest plant to the wet pile: {closestPlant.name}");
//            }
//            else
//            {
//                Debug.Log("No plant found to deactivate.");
//            }
//        }
//        else
//        {
//            Debug.Log("No wet pile available to identify the closest plant.");
//        }
//    }

//    void HandleBigTree()
//    {
//        // Deactivate the current big tree
//        if (currentBigTreeIndex >= 0 && currentBigTreeIndex < bigTrees.Length)
//        {
//            bigTrees[currentBigTreeIndex].SetActive(false);
//            Debug.Log($"Deactivated Big Tree {currentBigTreeIndex + 1}.");
//        }

//        // Increment the index to move to the next big tree
//        currentBigTreeIndex++;

//        // Activate the next big tree
//        if (currentBigTreeIndex < bigTrees.Length)
//        {
//            bigTrees[currentBigTreeIndex].SetActive(true);
//            Debug.Log($"Activated Big Tree {currentBigTreeIndex + 1}.");
//        }
//        else
//        {
//            Debug.Log("All big trees activated. No more big trees available.");
//        }
//    }
}
