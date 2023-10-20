using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    BasicControler player;

    public Text playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<BasicControler>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.text = "Health : " + player.PlayerHealth;
    }
}
