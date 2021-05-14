using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionEffect;
    public GameObject restartUI;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    IEnumerator AddDelay()
    {       
        yield return new WaitForSeconds(3);
        restartUI.SetActive(true);       
    }

    void StartCrashSequence()
    {
        explosionEffect.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<ShipController>().enabled = false;
        StartCoroutine(AddDelay());
    
    }
    public void RestartLevel()
    {
        int currentLevelindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelindex);
    }

}
