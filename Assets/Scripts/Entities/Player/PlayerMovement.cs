using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement, ITeleport
{
    [SerializeField] float turnSpeed;
    [SerializeField] float thrustForce;

    int turnDirection = 0;
    bool thrusting = false;
    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, turnDirection * turnSpeed * Time.deltaTime));
        if(thrusting)
        {
            body.AddForce(GetFacingDirection() * thrustForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
    public void SetThrustOnOff(bool value)
    {
        thrusting = value;
    }

    public void SetTurnDirection(int value)
    {
        turnDirection = value;
    }

    public Vector2 GetFacingDirection()
    {
        Vector2 rotatedVector = transform.rotation * Vector2.up;
        return rotatedVector;
    }

    public void SetRandomPosition()
    {
        Vector2 viewPortPos = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        transform.position = Camera.main.ViewportToWorldPoint(viewPortPos);
    }
}
