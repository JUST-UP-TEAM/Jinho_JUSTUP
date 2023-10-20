using UnityEngine;

public class ParticleSetting : MonoBehaviour
{
    public int rotationValue = 120;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0.04f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationValue);
    }
}
