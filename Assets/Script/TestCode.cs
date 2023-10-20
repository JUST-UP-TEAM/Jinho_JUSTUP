using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    public float rayLength;

    // Update is called once per frame
    void Update()
    {
        FloorCheck();
    }

    private bool FloorCheck()
    {
        Vector2 origin = this.transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength);
        Debug.DrawRay(origin, direction, new Color(1, 0, 0));


        if (hit.collider.tag == "Floor")
        {
            return true;
        }
        return false;
    }
}
