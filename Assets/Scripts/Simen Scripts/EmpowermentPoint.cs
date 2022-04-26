using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

public class EmpowermentPoint : MonoBehaviour
{
    private Ray2D ray;
    private List<Vector3> Points;
    private int currentReflections = 0;
    private int maxReflections = 100;
    private Vector2 startPoint, Direction;
    const int Infinity = 999;

    [SerializeField] private LineRenderer LightBeam;
    
    public Transform spawnPointForLight;
    public LayerMask interactLayer;
    public Vector2 vector;
    public int lightRange;
    
    private Animator animator;
    [SerializeField] private SCRUB canAnimateBool;

    public bool CanEmpowerLight;
    
    private void Start()
    {
        startPoint = spawnPointForLight.transform.position;
        Points = new List<Vector3>();
        CanEmpowerLight = false;
    }

    private void Update()
    {
        if (CanEmpowerLight)
        {
            EnableLight();
            UpdateLight();
            RayCast();
        }
        else
        {
            DisableLight();
        }
    }
    
    private void EnableLight()
    {
        LightBeam.enabled = true;
        LightBeam.SetPosition(0, new Vector3(spawnPointForLight.position.x, spawnPointForLight.position.y, 0f));
    }
    
    private void UpdateLight()
    {
        LightBeam.SetPosition(0, new Vector3(spawnPointForLight.position.x, spawnPointForLight.position.y, 0f));
        
        LightBeam.positionCount = 2;

        LightBeam.SetPosition(1, new Vector3(vector.x * lightRange, spawnPointForLight.position.y, 0f));
    }
    
    private void DisableLight()
    {
        LightBeam.enabled = false;
        LightBeam.positionCount = 1;
    }
    
    private void RayCast()
    {
        var hitData = Physics2D.Raycast(spawnPointForLight.position, vector, lightRange, interactLayer);
        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hitData.transform.CompareTag("Mirror") || hitData.transform.CompareTag("Ice") || hitData.transform.CompareTag("LightPowerUp"))
        {
            ReflectFurther(startPoint, hitData);
        }
        else if (hitData.transform == null)
        {
            // Nothing
        }
        else
        {
            Points.Add(startPoint + (Direction - startPoint).normalized * Infinity);
        }
        
        LightBeam.positionCount = Points.Count;
        LightBeam.SetPositions(Points.ToArray());
    }
    
    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        Points.Add(hitData.point);
        if (currentReflections > maxReflections) return;
        
        ray = new Ray2D(spawnPointForLight.position, vector);
        currentReflections++;

        if (hitData.transform.CompareTag("LightTrigger")) return;
        
        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);
        

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, lightRange);
        if (newHitData)
        {
            if (newHitData.transform.CompareTag("LightTrigger") && canAnimateBool.canAnimate)
            {
                animator.Play("OpenDoor");
                canAnimateBool.canAnimate = false;
            }
            else
            {
                print(hitData.transform.name);
                ReflectFurther(hitData.point, newHitData);
                
                ray = new Ray2D(newHitData.point, Vector2.Reflect(newDirection, newHitData.normal));
            }
        }
        else
        {
            Points.Add(hitData.point + newDirection * lightRange);
        }
    }
}
