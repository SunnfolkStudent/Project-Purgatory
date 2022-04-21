using System.Collections.Generic;
using UnityEngine;

public class LightPowerSpot : MonoBehaviour
{
    [SerializeField] private Input _input;
    public LineRenderer LineRenderer;
    public Transform lightPoint;
    [HideInInspector] public Vector2 currentDirection;
    private bool isLightOn;
    public int LightRange;
    public LayerMask interactLayer;

    private Vector2 startPoint, Direction;
    int maxReflections = 100;
    int currentReflections = 0;
    private List<Vector3> Points;
    const int Infinity = 999;


    private void Start()
    {
        DisableLight();
        startPoint = lightPoint.transform.position;
        Points = new List<Vector3>();
    }

    private void Update()
    {
        LightController();
        
        if (isLightOn)
        {
            EnableLight();
        }

        if (isLightOn)
        {
            UpdateLight();
        }

        if (!isLightOn)
        {
            DisableLight();
        }

        RayCast();
    }

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
    }

    private void LightController()
    {
        if (_input.Light)
        {
            isLightOn = !isLightOn;
        }
    }

    private void RayCast()
    {
        var hitData = Physics2D.Raycast(lightPoint.position, (_input.MoveVector).normalized, LightRange, interactLayer);
        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);
        
        if (hitData.transform.CompareTag("Mirror"))
        {
            ReflectFurther(startPoint, hitData);
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
        if (currentReflections > maxReflections) return;

        Points.Add(hitData.point);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, LightRange);
        if (newHitData)
        {
            ReflectFurther(hitData.point, newHitData);
        }
        else
        {
            Points.Add(hitData.point + newDirection * LightRange);
        }
    }
}