using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLessons : MonoBehaviour
{
    public LayerMask whatIsGround;

    public float lengthToDetectGround = 0.5f;
    public float boxLength = 2f;

    Vector2 boxSize;
    public bool isGrounded;

    public Transform point1;
    public Transform point2;

    // Start is called before the first frame update
    void Start()
    {
        boxSize = new Vector2(boxLength, lengthToDetectGround);
    }

    // Update is called once per frame
    void Update()
    {
        //CheckGroundusingOverlapBox();
        CheckGroundusingOverlapPoint();
    }

    void CheckGroundusingSingleRaycast()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down) Only origin and direction

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f); Only origin, direction and length

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, whatIsGround);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }

    void CheckGroundusingOverlapBox()
    {
        Vector2 boxCenter = (Vector2)transform.position + (Vector2.down * boxSize.y * 0.5f);

        isGrounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, whatIsGround) != null);

        if (isGrounded) 
        {
            Debug.Log("I have fallen");
        }

    }

    void CheckGroundusingOverlapPoint()
    {
        isGrounded = Physics2D.OverlapPoint(point1.position, whatIsGround) != null || Physics2D.OverlapPoint(point2.position, whatIsGround) != null;

        if (isGrounded)
        {
            Debug.Log("I have fallen");
        }
    }


    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube((Vector2)transform.position + (Vector2.down * boxSize.y * 0.5f), boxSize);
       // Gizmos.DrawRay(transform.position, Vector2.down * 0.5f);
    }
}
