using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShard : MonoBehaviour
{
    Basic_Character player;

    private void Start()
    {
        player = FindObjectOfType<Basic_Character>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("º°Á¶°¢");
            Destroy(this.gameObject);
            player.GetStarShard();
        }
    }
}
