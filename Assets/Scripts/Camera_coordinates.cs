using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Camera_coordinates : MonoBehaviour
{
    public Vector2 coordinate;
    public static Vector2 coordinateMultiplied;
    public int lenght = 200;
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;
    
    

    private void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, lenght))
        {
            coordinate = hit.textureCoord;
            collision = hit.point;
        }
        TranformCoordinate();
        //Debug.Log(coordinateMultiplied.x +"        "+ coordinateMultiplied.y);

      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }

    public Vector2 TranformCoordinate()
    {
        coordinateMultiplied = new Vector2(coordinate.x , coordinate.y );
        return coordinateMultiplied;
    }

    
}
