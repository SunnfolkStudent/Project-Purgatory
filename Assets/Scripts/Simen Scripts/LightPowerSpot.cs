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
    private int LightRange;
    public LayerMask interactLayer;

    [SerializeField] private Vector2 startPoint, Direction;
    private int maxReflections = 100;
    private int currentReflections = 0;
    private List<Vector3> Points;
    private const int Infinity = 999;
    private bool atLightPoint;
    private bool firstBounce = true;

    public string[] tags;
    [SerializeField] private Animator _animator;
    [SerializeField] private SCRUB canAnimateBool = null;
    [SerializeField] private EmpowermentPoint empower = null;
    
    
    private Vector2[] lightDirection = {Vector2.zero, Vector2.left, Vector2.up, Vector2.right};
    private int index;

    private void Start()
    {
        DisableLight();
        startPoint = lightPoint.transform.position;
        Points = new List<Vector3>();
        canAnimateBool.canAnimate = true;
        index = 0;
    }

    private void Update()
    {
        if (!atLightPoint) return;
        
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

        if (index >= 1)
        {
            isLightOn = true;
            RayCast();
        }
        else
        {
            LineRenderer.positionCount = 1;
            if (!empower) return;
            empower.CanEmpowerLight = false;
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
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(1, new Vector3(lightDirection[index].x * LightRange, lightDirection[index].y * LightRange, 0f));
        }
    
        private void DisableLight()
        {
            LineRenderer.enabled = false;
            LineRenderer.positionCount = 1;
            if (empower == null) return;
            empower.CanEmpowerLight = false;
        }
    
        private void LightController()
        {
            if (_input.Light)
            {
                index = (index + 1) % lightDirection.Length;
                DisableLight();
            }
        }
  #endregion

    private void RayCast()
    {
        var hitData = Physics2D.Raycast(lightPoint.position, lightDirection[index], LightRange, interactLayer);
        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);
        
        print(hitData.transform.name);

        
        IsTagTrue(hitData);
        
        /*
        if (hitData.transform.CompareTag("Mirror") || hitData.transform.CompareTag("Ice") || hitData.transform.CompareTag("LightPowerUp") || hitData.transform.CompareTag("Ground") || hitData.transform.CompareTag("LightTrigger"))
        {
            print("Running");
            ReflectFurther(startPoint, hitData);
        }*/
        if (IsTagTrue(hitData))
        {
            print("Running");
            ReflectFurther(startPoint, hitData);
        }
        else if (hitData.transform.CompareTag("LightTrigger"))
        {
            return;
        }
        else
        {
            Points.Add(startPoint + (Direction - startPoint).normalized * Infinity);
        }
        
        LineRenderer.positionCount = Points.Count;
        LineRenderer.SetPositions(Points.ToArray());
    }

    private bool IsTagTrue(RaycastHit2D hitData)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (hitData.transform.CompareTag(tags[i]))
            {
                return true;
            }
        }
        return false;
    }

    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        Points.Add(hitData.point);
        if (currentReflections > maxReflections) return;
        
        ray = new Ray2D(lightPoint.position, lightDirection[index]);
        currentReflections++;
        
        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);
        
        if (hitData.transform.CompareTag("LightTrigger") || hitData.transform.CompareTag("LightPowerUp") || hitData.transform.CompareTag("Ground")) return;

        

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, LightRange);
        if (newHitData)
        {
            if ((hitData.transform.CompareTag("LightTrigger") || hitData.transform.CompareTag("LightPowerUp") || hitData.transform.CompareTag("Ground")) && !firstBounce) return;

            if (newHitData.transform.CompareTag("LightTrigger") && canAnimateBool.canAnimate)
            {
                _animator.Play("OpenDoor");
                canAnimateBool.canAnimate = false;
            }
            else
            {
                ReflectFurther(hitData.point, newHitData);
                
                ray = new Ray2D(newHitData.point, Vector2.Reflect(newDirection, newHitData.normal));
                firstBounce = false;
            }
            
            if (newHitData.transform.CompareTag("LightPowerUp"))
            {
                empower.CanEmpowerLight = true;
            }
        }
        else
        {
            Points.Add(hitData.point + newDirection * LightRange);
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
            print("No more light");
            atLightPoint = false;
            index = 0;
            DisableLight();
            if (!empower) return;
            empower.CanEmpowerLight = false;
        }
    }
}