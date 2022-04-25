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

    private Vector2 startPoint, Direction;
    int maxReflections = 100;
    int currentReflections = 0;
    private List<Vector3> Points;
    const int Infinity = 999;
    public bool atLightPoint;

    public readonly Vector2[] lightDirection = {Vector2.zero, Vector2.left, Vector2.up, Vector2.right};

    private void Start()
    {
        DisableLight();
        startPoint = lightPoint.transform.position;
        Points = new List<Vector3>();
    }

    private void Update()
    {
        LightController();
        if (!atLightPoint) return;
    
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

        if (_input.MoveVector.x != 0 || _input.MoveVector.y > 0) 
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
        Debug.DrawRay(ray.direction, _input.MoveVector * LightRange, Color.magenta);
        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hitData.transform.CompareTag("Mirror") || hitData.transform.CompareTag("Ice"))
        {
            ReflectFurther(startPoint, hitData);
        }
        else
        {
            Points.Add(startPoint + (Direction - startPoint).normalized * Infinity);
        }

        LineRenderer.positionCount = Points.Count;
        LineRenderer.SetPositions(Points.ToArray());

        /*
        Mesh mesh = new Mesh();
        mesh.name = gameObject.name + " mesh";
        LineRenderer.BakeMesh(mesh);
        mesh.Optimize();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents(); 

        gameObject.GetComponentInChildren<MeshCollider>().sharedMesh = mesh; 
        */
    }
    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        if (currentReflections > maxReflections) return;
        ray = new Ray2D(lightPoint.position, _input.MoveVector);
        Points.Add(hitData.point);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, LightRange);
        if (newHitData)
        {
            ReflectFurther(hitData.point, newHitData);
            ray = new Ray2D(newHitData.point, Vector2.Reflect(ray.direction, newHitData.normal));
        }
        else
        {
            Points.Add(hitData.point + newDirection * LightRange);
        }
    }
    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Can use light now");
            atLightPoint = true;
        }
        else
        {
            print("car go broo");
            atLightPoint = false;
        }
    }
}