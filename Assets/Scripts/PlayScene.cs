using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class PlayScene : MonoBehaviour
{
    public VehicleAgent harvest;
    public VehicleAgent plow;
    public VehicleAgent plant;
    public VehicleAgent water;

   


    public void LaunchHarvest()
    {
        harvest.StartAI();
    }
    public void LaunchPlowing()
    {
        plow.StartAI();
    }
    public void LaunchPlanting()
    {
        plant.StartAI();
    }
    public void LaunchWatering()
    {
        water.StartAI();
    }
}
