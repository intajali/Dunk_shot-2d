using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D :: collision :: "+ collision.gameObject.tag);
        if(collision.gameObject.tag == "ball")
        {
            GameManager.instance.IsGrounded = true;
        }
    }
}
