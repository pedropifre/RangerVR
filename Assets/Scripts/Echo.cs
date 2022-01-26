using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    const int CloneCount = 50; // Number of afterimages 
    const int FlameCountMax = 4; // Frequency to update afterimages 
    int FlameCount = 1;

    //coordenadas
    public translate_coordinates coordenadasScript;
    public Camera_coordinates posicaoPiao;
    public float coordenadaXSource;
    public float coordenadaYSource;
    public TextMesh x;
    public TextMesh y;

    void Start()
    {
        BakeCloneMeshList = new List<SkinnedMeshRenderer>();
        objGameObjectList = new List<GameObject>();

        // Duplicate afterimage
        for (int i = 0; i < CloneCount; i++)
        {
            var obj = Instantiate(BakeMeshObj);
            obj.transform.SetParent(objpai.transform);
            BakeCloneMeshList.Add(obj.GetComponent<SkinnedMeshRenderer>());
            objGameObjectList.Add(obj);
            obj.GetComponent<Renderer>().material.color = Color.blue;
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

            //coordenadaXSource = coordenadasScript.CoordenadaX();
            //coordenadaYSource = coordenadasScript.CoordenadaY();

            //float xi = coordenadaXSource;
            //float yi = coordenadaYSource;
            //if(xi > 0 && yi > 0)
            //{
            //    objGameObjectList[i].GetComponent<Renderer>().material.color = Color.green;
            //}
            //else if(xi < 0 && yi < 0)
            //{
            //    objGameObjectList[i].GetComponent<Renderer>().material.color = Color.blue;
            //}
            //else if (xi > 0 && yi < 0)
            //{
            //    objGameObjectList[i].GetComponent<Renderer>().material.color = Color.red;
            //}
            //else if (xi < 0 && yi > 0)
            //{
            //    objGameObjectList[i].GetComponent<Renderer>().material.color = Color.yellow;
            //}
        }

        // Bake the current skin mesh 
        // Be careful as it can easily become a bottleneck! 
        Mesh mesh = new Mesh();
        BaseMeshObj.BakeMesh(mesh);
        BakeCloneMeshList[0].sharedMesh = mesh;

        // Copy position and rotation 
        BakeCloneMeshList[0].transform.position = transform.position;
        BakeCloneMeshList[0].transform.rotation = transform.rotation;
   
    }

    private void Update()
    {
        Vector2 posicaoPiaoAtual = posicaoPiao.TranformCoordinate();

        float piaox = posicaoPiaoAtual.x;
        float piaoy = posicaoPiaoAtual.y;

        x.text = "x = " + posicaoPiaoAtual.x;
        y.text = "y = " + posicaoPiaoAtual.y;
        

        for (int v = objGameObjectList.Count - 1; v >= 1; v--)
        {
            Vector2 posicaoCaixa = objGameObjectList[v].transform.localPosition;
            if (piaox < 0.5 && piaoy > 0.5)
            {
                if(posicaoCaixa.x <=0 && posicaoCaixa.y >= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.red;
                }
                else if (posicaoCaixa.x > 0 && posicaoCaixa.y < 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (posicaoCaixa.x >= 0 && posicaoCaixa.y >= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (posicaoCaixa.x <= 0 && posicaoCaixa.y <= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
            }
            else if (piaox > 0.5 && piaoy < 0.5)
            {
                if (posicaoCaixa.x >= 0 && posicaoCaixa.y >= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (posicaoCaixa.x >= 0 && posicaoCaixa.y <= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.red;
                }
                else if (posicaoCaixa.x <= 0 && posicaoCaixa.y >= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (posicaoCaixa.x <= 0 && posicaoCaixa.y <= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
            }
            else if (piaox < 0.5 && piaoy < 0.5)
            {
                if (posicaoCaixa.x >= 0 && posicaoCaixa.y >= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (posicaoCaixa.x > 0 && posicaoCaixa.y < 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (posicaoCaixa.x <= 0 && posicaoCaixa.y >= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (posicaoCaixa.x <= 0 && posicaoCaixa.y <= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.red;
                }
            }
            else if (piaox > 0.5 && piaoy > 0.5)
            {
                if (posicaoCaixa.x <= 0 && posicaoCaixa.y >= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (posicaoCaixa.x < 0 && posicaoCaixa.y < 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (posicaoCaixa.x >= 0 && posicaoCaixa.y >= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.red;
                }
                else if (posicaoCaixa.x >= 0 && posicaoCaixa.y <= 0)
                {
                    objGameObjectList[v].GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }

    }
}