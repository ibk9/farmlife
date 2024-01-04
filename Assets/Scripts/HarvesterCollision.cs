using System.Collections;
using UnityEngine;

public class HarvesterCollision : MonoBehaviour
{
    public int wheatGathered;
    public float wheatGatherDelay = 0.2f;
    public GameObject hayBalePrefab;
    public Transform shootPoint;
    public ParticleSystem wheatGather1Particles;
    public ParticleSystem wheatGather2Particles;
    public ParticleSystem wheatOutPutParticles;

    public GameObject harvesterController;

    private bool isTouchingWheat = false;
    private WaitForSeconds delay = new WaitForSeconds(2f); 
    private Coroutine stopParticlesCoroutine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wheat") && harvesterController.activeInHierarchy)
        {
            wheatGathered++;
            StartCoroutine(CutWheat(other.gameObject));  

            if (!isTouchingWheat) {
                isTouchingWheat = true;
                PlayParticales();
            }
            if (stopParticlesCoroutine != null)
            {
                StopCoroutine(stopParticlesCoroutine);
            }
            stopParticlesCoroutine = StartCoroutine(StopParticlesAfterDelay());

            if (wheatGathered % 1000 == 0)
            {
                SpawnHayBale();
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CuttedWheat"))
        {
            isTouchingWheat = false;
        }
    }

    private IEnumerator CutWheat(GameObject wheat)
    {
        yield return new WaitForSeconds(wheatGatherDelay);

        WheatMesh cutWheatMesh = wheat.GetComponent<WheatMesh>();
        if (cutWheatMesh != null)
        {
            cutWheatMesh.Cut();
        }
    }

    private void SpawnHayBale()
    {
        if (hayBalePrefab != null)
        {
            GameObject hayBale = Instantiate(hayBalePrefab, shootPoint.position, Quaternion.identity);
        }
    }

    private void PlayParticales()
    {
        wheatGather1Particles.gameObject.SetActive(true);
        wheatGather2Particles.gameObject.SetActive(true);
        wheatOutPutParticles.gameObject.SetActive(true);

    }

    private void StopParticales()
    {
        wheatGather1Particles.gameObject.SetActive(false);
        wheatGather2Particles.gameObject.SetActive(false);
        wheatOutPutParticles.gameObject.SetActive(false);
    }
    private IEnumerator StopParticlesAfterDelay()
    {
        yield return delay;

        if (!isTouchingWheat)
        {
            StopParticales();
        }
    }
}
