using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    [SerializeField] GameObject destroyEnemy;
    [SerializeField] GameObject hitEnemy;
    
    [SerializeField] int hitPoints = 3;
    int scorePerHit = 10;

    ScoreBoard sb;
    GameObject parentGameObj;
    
    void Start()
    {
        sb = FindObjectOfType<ScoreBoard>();
        parentGameObj = GameObject.FindWithTag("SpawnAtRuntime");      
        addRigidity();
    }
    

    void addRigidity()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    void OnParticleCollision(GameObject o)
    {   
        Hit();
        if(hitPoints <1){
            Kill();  
        }            
    }

    void Hit()
    {
        GameObject fx = Instantiate(hitEnemy, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObj.transform;
        hitPoints--;
        sb.UpdateScore(scorePerHit);
    }
    void Kill()
    {
        GameObject fx = Instantiate(destroyEnemy, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObj.transform;
        Destroy(gameObject);
    }
}
