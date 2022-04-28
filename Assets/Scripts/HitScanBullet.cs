using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanBullet : Projectile
{
    // Start is called before the first frame update
    public override void Start()
    {
        projectileRB = this.GetComponent<Rigidbody2D>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, projectileRange, whatIsKillable);

        if (hit.collider != null)
        {
            Debug.Log("I hit something");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.right * projectileRange);
    }

}
