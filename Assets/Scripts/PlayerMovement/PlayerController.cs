using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("PlayerCharacter")]
    public float characterForwardSpeed;
    [Header("Ricochet")]
    public float ricochetShotSpeed;

    void Update()
    {
        CheckMovementInput();
        RotatePlayerCharacter();
    }

    void CheckMovementInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput >= 0 + Mathf.Epsilon || verticalInput <= 0 - Mathf.Epsilon)
        {
            Transform player = GetComponent<Transform>();
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            float facingAngleToRad = player.eulerAngles.z * Mathf.Deg2Rad;
            rigidBody.velocity = verticalInput * characterForwardSpeed
                * new Vector2(Mathf.Cos(facingAngleToRad),
                              Mathf.Sin(facingAngleToRad));
        }
        else
        {
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = Vector3.zero;
        }
    }
    void RotatePlayerCharacter()
    {
        Vector3 mousePosition = Input.mousePosition;
        //the character is 10 unit away from the camera
        mousePosition.z = 7f;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float facingAngle = Mathf.Atan2(
            mouseWorldPosition.y - transform.position.y,
            mouseWorldPosition.x - transform.position.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, facingAngle);
    }
}
