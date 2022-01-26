using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particles : MonoBehaviour
{
    
    public Vector3 teste;
    private Vector3 posicaoOld;
    public GameObject prefabParticle;
   

    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collided");
        posicaoOld = gameObject.transform.position;
        Destroy(gameObject);
        Instantiate(prefabParticle, posicaoOld, Quaternion.identity);
    }
    private void Start()
    {
        ParticleSystem particulas = gameObject.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        //teste = particulas.transform.localPosition;
        //Debug.Log(teste);
    }
}
