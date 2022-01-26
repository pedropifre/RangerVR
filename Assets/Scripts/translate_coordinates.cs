using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translate_coordinates : Camera_coordinates
{
    public Camera thisCam;
    public GameObject obj;
    public GameObject tela;
    Vector2 ScreenXY;
    public Vector2 novasCoordenadas;
    public float coordenadaX;
    public float coordenadaY;



    
    
    public void Update()
    {
        ScreenXY = thisCam.WorldToScreenPoint(obj.transform.position);

        coordenadaX = RecalcularCardinal(Camera_coordinates.coordinateMultiplied.x);
        coordenadaY = RecalcularCardinal(Camera_coordinates.coordinateMultiplied.y);

        obj.transform.localPosition = new Vector3(coordenadaX,coordenadaY,12.99f);

        


    }

    public float CoordenadaX()
    {
        return coordenadaX;
    }
    
    public float CoordenadaY()
    {
        return coordenadaY;
    }

    public float RecalcularCardinal(float ponto)
    {
        if(ponto>0.5f)
        {
            ponto = ponto * 100;
            ponto = ponto * 2;
            ponto = ponto - 100;
            ponto = ponto / 100;
            ponto = 7 * ponto;
            
        }
        else if(ponto<0.5f)
        {
            ponto = ponto * 14;
            ponto = -7 + ponto;
        }
        else if(ponto==0.5f)
        {
            ponto = 0;
        }

        return ponto;
    }
    public float RecalcularCardinalY(float ponto)
    {
        if(ponto>0.5f)
        {
            ponto = ponto * -7f;
        }
        else if(ponto<0.5f)
        {
            ponto = ponto * 7f;
        }
        else if(ponto==0.5f)
        {
            ponto = 0;
        }

        return ponto;
    }
}
