using System.Collections.Generic;
using Assets.Scripts.General_Scripts;
using UnityEngine;
using Input = Assets.Scripts.General_Scripts.Input;


public class LightPowerSpot : MonoBehaviour
{
    [SerializeField] private Input _input;
    [SerializeField] private PlayerMovement Movement;
    public LineRenderer LineRenderer;
    private Ray2D ray;
    public Transform lightPoint;
    private bool isLightOn;
    public int LightRange;
    public LayerMask interactLayer;
    public bool canBounceFurther;

    private Vector2 startPoint, Direction;
    int maxReflections = 100;
    int currentReflections = 0;
    private List<Vector3> Points;
    const int Infinity = 999;
    public bool atLightPoint;

    [SerializeField] private Animator _animator;
    private bool canAnimate;
    
    
    public Vector2[] lightDirection = {Vector2.zero, Vector2.left, Vector2.up, Vector2.right};

    private void Start()
    {
        DisableLight();
        startPoint = lightPoint.transform.position;
        Points = new List<Vector3>();
        canAnimate = true;
    }

    private void Update()
    {
        if (!atLightPoint) return;
        
        LightController();
        
        if (isLightOn)
        {
            EnableLight();
            Movement.canInput = false;
        }

        if (isLightOn)
        {
            UpdateLight();
        }

        if (!isLightOn)
        {
            DisableLight();
            Movement.canInput = true;
        }

        if ((_input.MoveVector.x != 0 || _input.MoveVector.y > 0) && !Movement.canInput) 
        {
            RayCast();
        }
        else if (_input.MoveVector == Vector2.zero)
        {
            LineRenderer.positionCount = 2;
        }

    }
    
    #region LightStuff
     private void EnableLight()
        {
            LineRenderer.enabled = true;
            LineRenderer.SetPosition(0, new Vector3(lightPoint.position.x, lightPoint.position.y, 0f));
        }
    
        private void UpdateLight()
        {
            LineRenderer.SetPosition(0, new Vector3(lightPoint.position.x, lightPoint.position.y, 0f));
    
            LineRenderer.SetPosition(1, new Vector3(_input.MoveVector.x * LightRange, lightPoint.position.y, 0f));
        }
    
        private void DisableLight()
        {
            LineRenderer.enabled = false;
            LineRenderer.positionCount = 2;
        }
    
        private void LightController()
        {
            if (_input.Light)
            {
                isLightOn = !isLightOn;
            }
        }
  #endregion

    private void RayCast()
    {
        var hitData = Physics2D.Raycast(lightPoint.position, _input.MoveVector, LightRange, interactLayer);
        Debug.DrawRay(lightPoint.position, _input.MoveVector * LightRange, Color.magenta);
        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hitData.transform.CompareTag("Mirror") || hitData.transform.CompareTag("Ice"))
        {
            ReflectFurther(startPoint, hitData);
        }
        else if (hitData.transform == null)
        {
            // Nohing
        }
        else
        {
            Points.Add(startPoint + (Direction - startPoint).normalized * Infinity);
        }
        

        LineRenderer.positionCount = Points.Count;
        LineRenderer.SetPositions(Points.ToArray());
    }
    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        Points.Add(hitData.point);
        if (currentReflections > maxReflections) return;
        
        ray = new Ray2D(lightPoint.position, _input.MoveVector);
        currentReflections++;

        if (hitData.transform.CompareTag("LightTrigger")) return;
        
        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);
        

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, LightRange);
        if (newHitData)
        {
            
            if (newHitData.transform.CompareTag("LightTrigger") && canAnimate)
            {
                print("This works");
                _animator.Play("OpenDoor");
                canAnimate = false;
                canBounceFurther = false;
            }
            else
            {
                print(hitData.transform.name);
                ReflectFurther(hitData.point, newHitData);
                
                ray = new Ray2D(newHitData.point, Vector2.Reflect(newDirection, newHitData.normal));
                Debug.DrawRay(newHitData.point, Vector2.Reflect(newDirection, newHitData.normal) * LightRange, Color.red);
            }
        }
        else
        {
            Points.Add(hitData.point + newDirection * LightRange);
            canBounceFurther = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            atLightPoint = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            atLightPoint = false;
        }
    }
}