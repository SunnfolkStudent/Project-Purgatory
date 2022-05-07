using System.Collections.Generic;
using UnityEngine;
using Input = Assets.Scripts.General_Scripts.Input;

[RequireComponent(typeof(AudioSource))]
public class LightPowerSpot : MonoBehaviour
{
    [SerializeField] private Input _input;
    public LineRenderer LineRenderer;
    private Ray2D ray;
    public Transform lightPoint;
    private bool isLightOn;
    private int LightRange = 1000;
    public LayerMask interactLayer;

    [SerializeField] private Vector2 startPoint, Direction;
    int maxReflections = 100;
    int currentReflections = 0;
    private List<Vector3> Points;
    const int Infinity = 999;
    private bool atLightPoint;
    public bool firstBounce = false;
    public bool FirstLightTriggerHit = false;
    public bool LightPowerUpTrue = false;
    public bool HitGround = false;

    public string[] tags;
    [SerializeField] private Animator _animator;
    [SerializeField] private string animations;
    [SerializeField] private SCRUB canAnimateBool = null;
    [SerializeField] private EmpowermentPoint empower = null;
    private SpriteRenderer spriteRend = null;
    [SerializeField] private Sprite sprite = null;

    #region Sounds
    public float LightSoundEffectVolume;
    private AudioSource aSouce;
    [SerializeField] private AudioClip lightTrigger;
    [SerializeField] private AudioClip LightOnSound;
    #endregion
    
    [SerializeField] private Vector2[] lightDirection = {Vector2.zero, Vector2.left, Vector2.up, Vector2.right};
    public int index;

    private void Start()
    {
        aSouce = GetComponent<AudioSource>();
        DisableLight();
        startPoint = lightPoint.transform.position;
        Points = new List<Vector3>();
        canAnimateBool.canAnimate = true;
        index = 0;
        aSouce.volume = LightSoundEffectVolume;
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
            if (!GameStatus.HasPickedUpLightPower) return; 

            LineRenderer.enabled = true;
            LineRenderer.SetPosition(0, new Vector3(lightPoint.position.x, lightPoint.position.y, 0f));
        }
    
        private void UpdateLight()
        {
            if (!GameStatus.HasPickedUpLightPower) return; 

            LineRenderer.SetPosition(0, new Vector3(lightPoint.position.x, lightPoint.position.y, 0f));
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(1, new Vector3(lightDirection[index].x * LightRange, lightDirection[index].y * LightRange, 0f));
        }
    
        private void DisableLight()
        {
            LineRenderer.enabled = false;
            LineRenderer.positionCount = 1;
            aSouce.Stop();
            FirstLightTriggerHit = false;
            LightPowerUpTrue = false;
            HitGround = false;
            firstBounce = false;
            if (empower == null) return;
            empower.CanEmpowerLight = false;
        }
    
        private void LightController()
        {
            if (!GameStatus.HasPickedUpLightPower) return; 

            if (_input.Interact)
            {
                index = (index + 1) % lightDirection.Length;
                DisableLight();
            }
        }
  #endregion

    private void RayCast()
    {
        if (!GameStatus.HasPickedUpLightPower) return; 
        var hitData = Physics2D.Raycast(lightPoint.position, lightDirection[index], LightRange, interactLayer);
        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);
        
        IsTagTrue(hitData);

        if (IsTagTrue(hitData))
        {
            ReflectFurther(startPoint, hitData);
        }
        else
        {
            Points.Add(startPoint + (lightDirection[index] - startPoint).normalized * Infinity);
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
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        aSouce.PlayOneShot(LightOnSound);
        Points.Add(hitData.point);
        if (currentReflections > maxReflections) return;
        
        ray = new Ray2D(lightPoint.position, lightDirection[index]);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);
        
        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, LightRange, interactLayer);
        if (newHitData || hitData)
        {
            if ((newHitData.transform.CompareTag("Ground") || hitData.transform.CompareTag("Ground")) && HitGround && !firstBounce) return;
            print("Runnning now");
            if ((newHitData.transform.CompareTag("LightPowerUp") || hitData.transform.CompareTag("LightPowerUp")) && LightPowerUpTrue && !firstBounce) return;
            if ((newHitData.transform.CompareTag("LightTrigger") || hitData.transform.CompareTag("LightTrigger")) && FirstLightTriggerHit && !firstBounce) return;
            
            if (newHitData.transform.CompareTag("LightTrigger") && canAnimateBool.canAnimate)
            {
                _animator.Play(animations);
                spriteRend = newHitData.transform.gameObject.GetComponent<SpriteRenderer>();
                spriteRend.sprite = sprite;
                aSouce.PlayOneShot(lightTrigger);
                canAnimateBool.canAnimate = false;
                FirstLightTriggerHit = true;
            }
            else if (hitData.transform.CompareTag("LightTrigger") && canAnimateBool)
            {
                _animator.Play(animations);
                spriteRend = hitData.transform.gameObject.GetComponent<SpriteRenderer>();
                spriteRend.sprite = sprite;
                aSouce.PlayOneShot(lightTrigger);
                canAnimateBool.canAnimate = false;
                FirstLightTriggerHit = true;
            }
            else if (newHitData.transform.CompareTag("LightPowerUp") || hitData.transform.CompareTag("LightPowerUp"))
            {
                empower.CanEmpowerLight = true;
                LightPowerUpTrue = true;
            }
            else if ((newHitData.transform.CompareTag("Ground") || hitData.transform.CompareTag("Ground")) && !HitGround)
            {
                HitGround = true;
            }
            else
            {
                ReflectFurther(hitData.point, newHitData);
                
                ray = new Ray2D(newHitData.point, Vector2.Reflect(newDirection, newHitData.normal));
                firstBounce = true;
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
            atLightPoint = false;
            index = 0;
            DisableLight();
            if (!empower) return;
            empower.CanEmpowerLight = false;
        }
    }
}