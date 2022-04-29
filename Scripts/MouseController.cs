using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    #region Exposed

    [SerializeField] private LayerMask _layerMask;

    [Header("Cursor Texture")]
    [SerializeField] private Texture2D resizeIcon;
    [SerializeField] private Texture2D moveIcon;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    [SerializeField] private float _circleRadiusMax = 2;
    [SerializeField] private float _circleRadiusMin = .85f;
    [SerializeField] private float _colliderMarginMax = .08f;
    [SerializeField] private float _colliderMarginMin = .22f;
    [SerializeField] private float _forceRatio = 10f;
    [SerializeField] private float _forceRatioMax ;
    private Transform _activeEffector;
    #endregion

    #region Lifecycle

    
    void Update()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Camera.main = tag "main Camera"
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 deltaPos = 

        // prevent object from going oob
        mousePos.x = Mathf.Clamp(mousePos.x, -7.7f, 7.7f);
        mousePos.y = Mathf.Clamp(mousePos.y, -4.8f, 4.8f);
        

        Debug.DrawRay(_ray.origin, _ray.direction * 150f, Color.cyan);//ray.direction = val entre 0 et 1

        RaycastHit2D hit = Physics2D.GetRayIntersection(_ray, Mathf.Infinity, _layerMask);
        
        if(hit.collider != null)
        {
            

            
            if (hit.collider.CompareTag("Move"))
            {
                Cursor.SetCursor(moveIcon, hotSpot, CursorMode.Auto);
                    
                if (Input.GetMouseButton(0))
                {
                    // Trouver le milieu entre deux nombres équivaut à trouver la moyenne entre eux. Additionnez les nombres et divisez par deux.
                    //hit.transform.parent.position =  (mousePos + (Vector2)hit.transform.parent.position) / 2;
                    hit.transform.parent.position = mousePos;
                }
                
            }
            else if (hit.collider.CompareTag("Resize"))
            {
                Cursor.SetCursor(resizeIcon, hotSpot, CursorMode.Auto);
                if (Input.GetMouseButton(0))
                {
                    float _distance = Vector2.Distance(hit.transform.position, mousePos); // distance entre le pts central de l'objet et la position de lasours
                    CircleShape circleScript =  hit.transform.GetComponent<CircleShape>() ;
                    AreaEffector2D forceOfRatio = hit.transform.GetComponent<AreaEffector2D>();
                    forceOfRatio.forceMagnitude = Mathf.Clamp(_distance * _forceRatio, 10,_forceRatioMax);
                    circleScript.Radius = Mathf.Clamp( (_distance) , _circleRadiusMin, _circleRadiusMax);
                    circleScript._colliderMargin = Mathf.Clamp(_distance * .1f, _colliderMarginMin, _colliderMarginMax);


                   // hit.CircleShape.Radius = _distance - _lastFrameDistance;// ne fonctionne pas. Comment acceder au componnet CircleShape...?
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, hotSpot, CursorMode.Auto);
        }
        
    }

   

    #endregion
    #region Main Methods
    #endregion
    #region Private & Protected
    private float _lastFrameDistance;
#endregion

}
