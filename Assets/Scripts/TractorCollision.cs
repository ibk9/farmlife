using System.Collections;
using UnityEngine;

public class TractorCollision : MonoBehaviour
{
    public ParticleSystem dirtPileParticles;
    public float changeDelay = 0.5f;
    private bool isTouchingDirtPile = false;
    private WaitForSeconds delay = new WaitForSeconds(2f);
    private Coroutine stopParticlesCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CuttedWheat"))
        {
            ChangeWheatMesh(other.gameObject);
        }
        if (other.CompareTag("DirtPile"))
        {
            StartCoroutine(ChangeToDirt(other.gameObject));
            if (!isTouchingDirtPile)
            {
                isTouchingDirtPile = true;
                PlayParticales();
            }
  
            if (stopParticlesCoroutine != null)
            {
                StopCoroutine(stopParticlesCoroutine);
            }
            stopParticlesCoroutine = StartCoroutine(StopParticlesAfterDelay());
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DirtPile"))
        {
            isTouchingDirtPile = false;
        }
    }

    private void ChangeWheatMesh(GameObject wheat)
    {
        WheatMesh cutWheatMesh = wheat.GetComponent<WheatMesh>();
        if (cutWheatMesh != null)
        {
            cutWheatMesh.Remove();
        }
    }

    private void PlayParticales()
    {
        dirtPileParticles.gameObject.SetActive(true);
    }

    private void StopParticales()
    {
        dirtPileParticles.gameObject.SetActive(false);
    }
    private IEnumerator ChangeToDirt(GameObject dirtPile)
    {
        yield return new WaitForSeconds(changeDelay);

        MeshRenderer dirtMeshRenderer = dirtPile.GetComponent<MeshRenderer>();
        if (dirtMeshRenderer != null)
        {
            dirtMeshRenderer.enabled = true;
        }
    }

    private IEnumerator StopParticlesAfterDelay()
    {
        yield return delay;

        if (!isTouchingDirtPile)
        {
            StopParticales();
        }
    }
}
