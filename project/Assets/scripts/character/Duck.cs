using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    [Header("Ignore")]
    [SerializeField] float dirInputX;
    [SerializeField] Rigidbody2D _rb2D;
    [SerializeField] int lastDirMov = 1;

    [Header("Recommend no modify")]
    [SerializeField] Vector2 distanceFootsDuck;
    [SerializeField] float amplitudeFoots;
    [SerializeField] LayerMask layerBackground;

    [Header("Modifiables")]
    [SerializeField] float impulse = 2;
    [SerializeField] float speed = 5;
    [SerializeField] float offsetMovement = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dirInputX = Input.GetAxisRaw("Horizontal") + offsetMovement;
    }

    private void FixedUpdate()
    {
        if (CanJump()) Jump(impulse);
        Move(speed);
    }

    bool CanJump()
    {
        RaycastHit2D hit2D;

        hit2D = Physics2D.Raycast(transform.position + new Vector3(distanceFootsDuck.x, distanceFootsDuck.y), transform.right * amplitudeFoots, 1, layerBackground);

        if (hit2D)
        {
            if (Input.GetButton("Jump"))
            {
                return true;
            }

            Debug.DrawRay(transform.position + new Vector3(distanceFootsDuck.x, distanceFootsDuck.y), transform.right * amplitudeFoots, Color.green);

            return false;
        }

        else
        {
            Debug.DrawRay(transform.position + new Vector3(distanceFootsDuck.x, distanceFootsDuck.y), transform.right * amplitudeFoots, Color.green);

            return false;
        }
    }

    void Jump(float impulse)
    {
        _rb2D.AddForce(new Vector2(0, impulse), ForceMode2D.Impulse);
    }

    void Move(float speed)
    {
        if (dirInputX > 0) lastDirMov = 1;
        else if (dirInputX < 0) lastDirMov = -1;

        transform.localScale = new Vector3(lastDirMov, 1, 1);

        Vector2 movement = new Vector2(dirInputX * speed, _rb2D.velocity.y);

        _rb2D.velocity = movement;
    }
}
