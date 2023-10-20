using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasicControler : MonoBehaviour
{

    SlidingPartical partical;

    private bool direction = false; //true = 오른쪽으로 이동, false = 왼쪽으로 이동
    private bool firstJumpAble = true; //플레이어의 점프 가능 여부 체크
    private bool doubleJumpAble = true; //플레이어의 더블 점프 가능 여부 체크
    private bool isSlidingOnWall = false; //플레이어가 벽에 닿아있는지 여부 체크

    public float rayLength;
    public float rayLengthFloor;
    public float moveSpeed;
    public float jumpPower;
    public float slidingSpeed; //슬라이딩으로 떨어지는 속도

    private int playerHealth;
    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    Vector3 wallPos; //충돌한 벽의 위치 저장


    void Start()
    {
        playerHealth = 3;
        partical = GetComponent<SlidingPartical>();
    }


    void Update()
    {

        WallCheck();  
        FloorCheck();
        JumpPlayer();


        if (isSlidingOnWall == true && FloorCheck() == false)
        {
            Debug.Log("!");
            return;
        }

        MovePlayer();
    }

    

    private void MovePlayer()
    {
        if (direction == true)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (direction == false)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    private void JumpPlayer()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && doubleJumpAble == true)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            isSlidingOnWall = false;
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpPower, 0);

            if (firstJumpAble == false)
            {
                doubleJumpAble = false;
            }
            firstJumpAble = false;

        }
    }

    private void WallSliding()
    {
        Debug.Log("벽타기");
        isSlidingOnWall = true;
        GetComponent<Rigidbody2D>().gravityScale = slidingSpeed;
        if (partical.isParticleCycle == true)
            partical.SpwanParticle();

    }

    private void InitJump()
    {
        firstJumpAble = true;
        doubleJumpAble = true;
    }


    private bool FloorCheck()
    {


        Vector2 origin = this.transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, 0.1f, direction, rayLengthFloor);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Floor"))
            {
                InitJump();
                return true;
            }
        }

        return false;
    }

    private int WallCheck()
    {
        Vector2 origin = this.transform.position;

        Vector2 direction = Vector2.right;
        RaycastHit2D hits = Physics2D.Raycast(origin, direction, rayLength);

        if(hits.collider != null)
        {
            if (hits.collider.CompareTag("Wall"))
            {
                Debug.Log("rightWall");
                InitJump();
                
                wallPos = hits.collider.transform.position;
                Trun(wallPos);
                WallSliding();

                return 1;

            }

        }
        direction = Vector2.left;

        hits = Physics2D.Raycast(origin, direction, rayLength);

        if(hits.collider != null)
        {
            if (hits.collider.CompareTag("Wall"))
            {
                Debug.Log("leftWall");
                InitJump();
                
                wallPos = hits.collider.transform.position;
                Trun(wallPos);
                WallSliding();
            }
        }

        return 0;
    }

    private void Trun(Vector3 wallPos)
    {
        if (wallPos.x < transform.position.x)
        {
            this.direction = true;
        }
        else if (wallPos.x > transform.position.x)
        {
            this.direction = false;
        }
    }

    public void PlayerHit()
    {
        playerHealth -= 1;
        PlayerDie();
    }

    private void PlayerDie()
    {
        if(playerHealth == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
