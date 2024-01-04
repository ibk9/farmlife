using System.Collections;
using UnityEngine;

public class WheatMesh : MonoBehaviour
{
    public Mesh cutMesh;
    public Mesh plantMesh;
    public Mesh[] stageMeshs;
    public int growthStage = 0;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private bool isSprayed = false;


    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Cut()
    {
        if (cutMesh != null && meshFilter != null)
        {
            meshFilter.mesh = cutMesh;
            gameObject.tag = "CuttedWheat";
        }
    }
    public void Remove()
    {
        meshFilter.mesh = null;
        gameObject.tag = "WheatPoint";
    }
    public void Plant()
    {
        meshFilter.mesh = plantMesh;
        gameObject.tag = "PlantWheat";
    }
    public void Spray(int _stage)
    {
        if (!isSprayed) { StartCoroutine(SprayTimer(_stage)); }             
    }
    private IEnumerator SprayTimer(int _stage)
    {
        isSprayed = true;
        yield return new WaitForSeconds(10f);
        if(growthStage < 3)
        {
            meshFilter.mesh = stageMeshs[_stage];
            growthStage++;
        } 
        if(growthStage == 3)
        {
            gameObject.tag = "Wheat";
            growthStage = 0;
        }
        isSprayed = false;
    }
}

