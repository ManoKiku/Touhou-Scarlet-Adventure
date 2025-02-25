using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float fireRate;

    private float nextFireTime;
    private bool isFiring = false;


    private void Awake()
    {
        Time.timeScale = 1.0f;
        instance = this;
        
        rb = GetComponent<Rigidbody2D>();
        GameInput.instance.action.Player.Use.performed += HandleUse;
        GameInput.instance.action.Player.Attack.started += StartFiring;
        GameInput.instance.action.Player.Attack.canceled += StopFiring;
    }

    private void Update()
    {
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
        if(DialogueManager.Instance.isDialogueActive)
        {
            axis = Vector2.zero;
        }

        if(axis != Vector2.zero) {
            lastNoZeroAxis = axis;
        }

        rb.velocity = axis.normalized * speed;
    }


    private void StartFiring(InputAction.CallbackContext context)
    {
        isFiring = true;
        nextFireTime = Time.time;
    }

    private void StopFiring(InputAction.CallbackContext context)
    {
        isFiring = false;
    }

    private void HandleUse(InputAction.CallbackContext e) {
        if(isActive) {
            PlayerStatus.instance.UseBomb();
        }
    }

    private void Shoot() 
    {
        if(!isActive)
            return;

        GameObject buff = Instantiate(bulletPrefab);

        float angle = Mathf.Atan2(lastNoZeroAxis.y, lastNoZeroAxis.x) * Mathf.Rad2Deg;
        float snappedAngle = Mathf.Round(angle / 45) * 45;

        buff.transform.rotation = Quaternion.Euler(0, 0, snappedAngle + 90);
        buff.transform.position = transform.position;
        buff.GetComponent<Rigidbody2D>().velocity = lastNoZeroAxis.normalized * bulletSpeed;

        Destroy(buff, 10);
    }
}
