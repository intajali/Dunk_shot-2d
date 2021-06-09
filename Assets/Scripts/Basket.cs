using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{

    [SerializeField] private Transform basketTransform;
    // private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionEnter2D :: collision :: "+ collision.gameObject.tag);
    //    Debug.Log("OnCollisionEnter2D :: collision :: "+ this.gameObject.tag);
    //    if (collision.gameObject.tag == "ball")
    //    {
    //        GameManager.instance.IsGrounded = true;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered :: " + collision.tag);
        ScoreHandler.instance.UpdateScore();
         GameManager.instance.UpdateBasket();
       // Invoke("UpdateBasketPos",0.1f);
    }

    void UpdateBasketPos()
    {
        GameManager.instance.UpdateBasket();
    }

    public void UpdateBasket(Vector2 pos)
    {
        basketTransform.gameObject.SetActive(true);
        basketTransform.position = pos;
    }

    public void DisableBasket() {

        basketTransform.gameObject.SetActive(false);
    }

    

}
