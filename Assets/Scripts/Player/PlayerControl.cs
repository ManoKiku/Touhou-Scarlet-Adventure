using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct Bullet
{
    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    public int cost;
}

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance { get; private set; }

    
    public Rigidbody2D rb { get; private set; }
    public bool isActive = true;
    public Vector2 axis;

    private Vector2 lastNoZeroAxis;

    [Header("Player stats")]
    [SerializeField]
    private float speed;

    [Header("Bullet stats")]
    [SerializeField]
    private Bullet[] bulletType;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float fireRate;

    private int currentType = 0;
    private float nextFireTime;
    private bool isFiring = false;


    private void Awake()
    {
        Time.timeScale = 1.0f;
        instance = this;
        nextFireTime = Time.time;
        
        rb = GetComponent<Rigidbody2D>();
        GameInput.instance.action.Player.Use.performed += HandleUse;
        GameInput.instance.action.Player.Attack.started += StartFiring;
        GameInput.instance.action.Player.Attack.canceled += StopFiring;
    }

    private void Update()
    {
        currentType = bulletType.Select((value, index) =>
        {
            return new { value.cost, index };
        }).Where(x => x.cost <= PlayerStatus.instance.powerAmount).OrderByDescending(x => x.cost).First().index;

        if (isFiring && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(isActive)
        {
            axis = GameInput.instance.GetMovementVector();
        }

        if(axis != Vector2.zero) {
            lastNoZeroAxis = axis;
        }

        if(DialogueManager.Instance.isDialogueActive)
        {
            axis = Vector2.zero;
        }

        rb.velocity = axis.normalized * speed;
    }


    private void StartFiring(InputAction.CallbackContext context)
    {
        isFiring = true;
    }

    private void StopFiring(InputAction.CallbackContext context)
    {
        isFiring = false;
    }

    private void HandleUse(InputAction.CallbackContext e) {
        if(!isActive || DialogueManager.Instance.isDialogueActive) 
            return;

        PlayerStatus.instance.UseBomb();
    }

    private void Shoot() 
    {
        if(!isActive || DialogueManager.Instance.isDialogueActive)
            return;

        GameObject buff = Instantiate(bulletType[currentType].bulletPrefab);

        float angle = Mathf.Atan2(lastNoZeroAxis.y, lastNoZeroAxis.x) * Mathf.Rad2Deg;
        float snappedAngle = Mathf.Round(angle / 45) * 45;

        buff.transform.rotation = Quaternion.Euler(0, 0, snappedAngle + 90);
        buff.transform.position = transform.position + new Vector3(lastNoZeroAxis.x, lastNoZeroAxis.y).normalized / 3;
        
        Rigidbody2D[] bullets = buff.GetComponentsInChildren<Rigidbody2D>();
        buff.GetComponent<Rigidbody2D>().velocity = lastNoZeroAxis.normalized * bulletSpeed;
        foreach(var i in bullets)
        {
            i.GetComponent<Rigidbody2D>().velocity = lastNoZeroAxis.normalized * bulletSpeed;
        }

        Destroy(buff, 10);
    }
}
