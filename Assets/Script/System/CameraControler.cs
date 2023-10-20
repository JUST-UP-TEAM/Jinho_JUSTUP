using UnityEngine;

public class CameraControler : MonoBehaviour
{
    Basic_Character player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Basic_Character>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);
    }
}
