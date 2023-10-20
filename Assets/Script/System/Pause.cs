using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPause = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(isPause == false)
            {
                isPause = true;
                Time.timeScale = 0;
            }
            else if(isPause == true)
            {
                isPause = false;
                Time.timeScale = 1;
            }
        }
    }
}
