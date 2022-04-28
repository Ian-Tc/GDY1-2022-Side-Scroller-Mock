using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   public  Rigidbody2D projectileRB;

    public float projectileSpeed = 5f;
    public float projectileRange = 10f;
    public LayerMask whatIsKillable;

    // Start is called before the first frame update
    public virtual void Start()
    {
       
        DestroyBullet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveHorizontal()
    {
        projectileRB.velocity = projectileSpeed * transform.right;
    }

    //void HitScanBullet()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, projectileRange, whatIsKillable);

    //    if (hit.collider != null)
    //    {
    //        Debug.Log("I hit something");
    //    }
    //}

    public void DestroyBullet()
    {
        Destroy(this.gameObject, 0.2f);
    }

    
}
