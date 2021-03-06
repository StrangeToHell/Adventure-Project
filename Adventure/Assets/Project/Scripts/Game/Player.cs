﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Visuals")]
    public GameObject model;
    public float rotatingSpeed = 2;

    [Header("Equipment")]
    public int health = 3;
    public Sword sword;
    public Bow bow;
    public int arrowAmount = 15;
    public GameObject bombPrefab;
    public int bombAmount = 5;
    public float throwingSpeed;

    [Header("Movement")]
    public float movingVelocity;
    public float jumpingVelocity;
    public float knockbackForce;
    private bool justTeleported;

    public bool JustTeleported
    {
        get
        {
            bool returnValue = justTeleported;
            justTeleported = false;
            return returnValue;
        }
    }

    private Rigidbody playerRigidbody;
    private bool canJump;
    private Quaternion targetModelRotation;
    private float knockbackTimer;
    

	// Use this for initialization
	void Start () {
        bow.gameObject.SetActive(false);

        playerRigidbody = GetComponent<Rigidbody>();
        targetModelRotation = Quaternion.Euler(0, 90, 0);
	}
	
	// Update is called once per frame
	void Update () {
        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f)) {
            canJump = true;
        }

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, targetModelRotation, Time.deltaTime * rotatingSpeed);

        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
        }
        else
        {
            ProcessInput();
        }

    }

    void ProcessInput ()
    {

    playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);

        if (Input.GetKey("right"))
        {
        playerRigidbody.velocity = new Vector3(movingVelocity, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
            targetModelRotation = Quaternion.Euler(0, 90, 0);
        }
        

        if (Input.GetKey("left"))
        {
        playerRigidbody.velocity = new Vector3(-movingVelocity, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
            targetModelRotation = Quaternion.Euler(0, 270, 0);
        }

        if (Input.GetKey("up"))
        {
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, movingVelocity);
            targetModelRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey("down"))
        {
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, -movingVelocity);
            targetModelRotation = Quaternion.Euler(0, 180, 0);
        }

        if (canJump && Input.GetKeyDown("space"))
        {
            canJump = false;
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpingVelocity, playerRigidbody.velocity.z);
        }

        // Check Equipment Interaction
        if (Input.GetKeyDown("z"))
        {
            sword.gameObject.SetActive(true);
            bow.gameObject.SetActive(false);
            sword.Attack();
        }
        if (Input.GetKeyDown("x"))
        {
            if (arrowAmount > 0)
            {
                sword.gameObject.SetActive(false);
                bow.gameObject.SetActive(true);
                bow.Attack();
                arrowAmount--;
            }
        }

        if (Input.GetKeyDown ("c"))
        {
            ThrowBomb();
        }
    }

    private void ThrowBomb ()
    {
        if (bombAmount <= 0)
        {
            return;
        }

        GameObject bombObject = Instantiate(bombPrefab);
        bombObject.transform.position = transform.position + model.transform.forward;

        Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;
            
        bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection * throwingSpeed);

        bombAmount--;
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.GetComponent<EnemyBullet>() != null)
        {
            Hit((transform.position - otherCollider.transform.position).normalized);
            Destroy(otherCollider.gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
        {
            Hit((transform.position - collision.transform.position).normalized);
        }
    }

    private void Hit (Vector3 direction)
    {
        Vector3 knockbackDirection = (direction + Vector3.up).normalized;
        playerRigidbody.AddForce(knockbackDirection * knockbackForce);
        knockbackTimer = 1f;
        

        health--;
        if (health <= 0)
        {
           Destroy(gameObject);
        }
    }

    public void Teleport (Vector3 target)
    {
        transform.position = target;
        justTeleported = true;

    }
}
