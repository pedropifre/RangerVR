using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Echo : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer BaseMeshObj; // Original object to bake 

    [SerializeField]
    GameObject BakeMeshObj; // GameObject to store the baked mesh 

    public GameObject objpai;

    //SkinnedMeshRenderer list when BakeMeshObj is instantiated
    List<SkinnedMeshRenderer> BakeCloneMeshList;
    List<GameObject> objGameObjectList;

    const int CloneCount = 30; // Number of afterimages 
    const int FlameCountMax = 8; // Frequency to update afterimages 
    int FlameCount = 1;

    //coordenadas
    public translate_coordinates coordenadasScript;
    public Camera_coordinates posicaoPiao;
    public float coordenadaXSource;
    public float coordenadaYSource;
    public TextMesh x;
    public TextMesh y;
    public GameObject posicaoMestre;

    private float contagem = 0;
    public TextMesh hitT;

    void Start()
    {
        BakeCloneMeshList = new List<SkinnedMeshRenderer>();
        objGameObjectList = new List<GameObject>();


        // Duplicate afterimage
        for (int i = 0; i < CloneCount; i++)
        {
            var obj = Instantiate(BakeMeshObj);
            obj.transform.SetParent(objpai.transform);
            obj.name = "Objeto "+i;
            BakeCloneMeshList.Add(obj.GetComponent<SkinnedMeshRenderer>());
            objGameObjectList.Add(obj);
            obj.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public float RecalcularCardinal(float ponto)
    {
        if (ponto > 0.5f)
        {
            ponto = ponto * 100;
            ponto = ponto * 2;
            ponto = ponto - 100;
            ponto = ponto / 100;
            ponto = 7 * ponto;

        }
        else if (ponto < 0.5f)
        {
            ponto = ponto * 14;
            ponto = -7 + ponto;
        }
        else if (ponto == 0.5f)
        {
            ponto = 0;
        }

        return ponto;
    }

    void FixedUpdate()
    {
        // Update once to 4 frames 
        FlameCount++;
        if (FlameCount % FlameCountMax != 0)
        {
            return;
        }

        // Move the Bake Mesh to the previous one 
        for (int i = BakeCloneMeshList.Count - 1; i >= 1; i--)
        {
            BakeCloneMeshList[i].sharedMesh = BakeCloneMeshList[i - 1].sharedMesh;

            // Copy position and rotation
            BakeCloneMeshList[i].transform.position = BakeCloneMeshList[i - 1].transform.position;
            BakeCloneMeshList[i].transform.rotation = BakeCloneMeshList[i - 1].transform.rotation;
            BakeCloneMeshList[i].material.color = BakeCloneMeshList[i - 1].material.color;
            
            
            if (Math.Round(posicaoMestre.transform.localPosition.x,2) == 
                Math.Round(objGameObjectList[i].transform.localPosition.x,2) &&
                Math.Round(posicaoMestre.transform.localPosition.y, 2)
                == Math.Round(objGameObjectList[i].transform.localPosition.y, 2))
            {
                BakeCloneMeshList[i].material.color = Color.red;
            }





            Vector2 posicaoPiaoAtual = posicaoPiao.TranformCoordinate();

            float piaox = posicaoPiaoAtual.x;
            float piaoy = posicaoPiaoAtual.y;


            hitT.text = "Hit Nº " + contagem;
            

            x.text = objGameObjectList[2].name;
            y.text = "";
            Vector2 posicaoCaixa = objGameObjectList[i].transform.localPosition;
            Color corObj = objGameObjectList[i].GetComponent<Renderer>().material.color;
            if (piaox < 0.5 && piaoy > 0.5)
            {
                if (posicaoCaixa.x <= 0 && posicaoCaixa.y <= 0)
                {
                    BakeCloneMeshList[i].material.color = Color.yellow;
                }
                if (posicaoCaixa.x <= 0 && posicaoCaixa.y >= 0)
                {
                    if (BakeCloneMeshList[i].material.color == Color.red)
                    {
                        BakeCloneMeshList[i].material.color = Color.red;
                    }
                }
            }
            else if (piaox > 0.5 && piaoy < 0.5)
            {
                if (posicaoCaixa.x >= 0 && posicaoCaixa.y >= 0)
                {
                    BakeCloneMeshList[i].material.color = Color.yellow;
                }
                if (posicaoCaixa.x >= 0 && posicaoCaixa.y >= 0)
                {
                    if (BakeCloneMeshList[i].material.color == Color.red)
                    {
                        BakeCloneMeshList[i].material.color = Color.red;
                    }
                }
            }
            else if (piaox < 0.5 && piaoy < 0.5)
            {
                if (posicaoCaixa.x >= 0 && posicaoCaixa.y <= 0)
                {
                    BakeCloneMeshList[i].material.color = Color.yellow;
                }
                if (posicaoCaixa.x <= 0 && posicaoCaixa.y <= 0)
                {
                    if (BakeCloneMeshList[i].material.color == Color.red)
                    {
                        BakeCloneMeshList[i].material.color = Color.red;
                    }
                }
            }
            else if (piaox > 0.5 && piaoy > 0.5)
            {
                if (posicaoCaixa.x <= 0 && posicaoCaixa.y >= 0)
                {
                    BakeCloneMeshList[i].material.color = Color.yellow;
                }
                if (posicaoCaixa.x >= 0 && posicaoCaixa.y >= 0)
                {
                    if (BakeCloneMeshList[i].material.color == Color.red)
                    {
                        BakeCloneMeshList[i].material.color = Color.red;
                    }
                }
            }












        }

        // Bake the current skin mesh 
        // Be careful as it can easily become a bottleneck! 
        Mesh mesh = new Mesh();
        BaseMeshObj.BakeMesh(mesh);
        BakeCloneMeshList[0].sharedMesh = mesh;

        // Copy position and rotation 
        BakeCloneMeshList[0].transform.position = transform.position;
        BakeCloneMeshList[0].transform.rotation = transform.rotation;
        BakeCloneMeshList[0].material.color = Color.red;
   
    }

    
    void OnTriggerEnter(Collider other) {  
        Color corObj =  other.GetComponent<Renderer>().material.color;
        if(other.gameObject.tag == "ray_hit" && corObj == Color.yellow)
        {
            Debug.Log("Acertou um bloco");
            contagem++;
        }
    }
      

}