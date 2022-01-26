using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particles1 : MonoBehaviour
{
    private ParticleSystem particulas;
    
    private Vector3 posicaoOld;
    public GameObject prefab;


    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    private void Update()
    {

        //Collider[] colList = transform.GetComponentsInChildren<Collider>();
        //Debug.Log(colList.Length);
        
        //Debug.Log(teste);
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject ChildGameObject = gameObject.transform.GetChild(0).gameObject;
        particulas = ChildGameObject.GetComponent<ParticleSystem>();

        Debug.Log("Collided");
        posicaoOld = particulas.transform.position;
        Instantiate(prefab, posicaoOld, Quaternion.identity,gameObject.transform);
        Destroy(ChildGameObject);
        Debug.Log("destruiu");
        Debug.Log("Criou nova");
    }
}
