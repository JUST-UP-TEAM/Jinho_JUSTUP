using UnityEngine;
using UnityEngine.UI;

public class Basic_Item : MonoBehaviour
{
    private float itemPosCos = 0;
    private float itemPosX;
    private float itemPosY;
    public float itemXSpeed;
    public int dropSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ItemMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Debug.Log("아이템 트리거");
        }
    }

    protected void ItemMove()
    {
        itemPosCos += Time.deltaTime * itemXSpeed;
        itemPosX = Mathf.Cos(itemPosCos);
        itemPosY = Time.deltaTime * dropSpeed;
        transform.position += new Vector3(0, -1 * itemPosY, 0);
        transform.position = new Vector3(itemPosX, transform.position.y, 0);
    }
}
