using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantWheat : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WheatPoint"))
        {
            PlantWheatMesh(other.gameObject);
        }
    }

    private void PlantWheatMesh(GameObject wheat)
    {
        WheatMesh cutWheatMesh = wheat.GetComponent<WheatMesh>();
        if (cutWheatMesh != null)
        {
            cutWheatMesh.Plant();
        }
    }
}

