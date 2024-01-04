using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class UseVehicle : MonoBehaviour
{
    public GameObject playerController;
    public GameObject seatPos;
    public GameObject vehicleSpecial;

    public void EnterVehicle()
    {
        playerController.SetActive(false);
        seatPos.SetActive(true);
    }
    public void ExitVehicle()
    {
        playerController.SetActive(true);
        seatPos.SetActive(false);
    }
    public void VehicleSpecial()
    {
        if(vehicleSpecial.activeInHierarchy)
        {
            vehicleSpecial.SetActive(false);
        }
        else
        {
            vehicleSpecial.SetActive(true);
        }

    }
}
