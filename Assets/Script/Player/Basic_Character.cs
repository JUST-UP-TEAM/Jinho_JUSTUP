using UnityEngine;
using UnityEngine.UI;

public class Basic_Character : MonoBehaviour
{

    SlidingPartical partical;
    public Slider slider;

    private bool direction = true; //true = 오른쪽으로 이동, false = 왼쪽으로 이동
    private bool isGrounded = false; //플레이어가 땅에 닿아있는지 체크
    private bool firstJumpAble = true; //플레이어의 점프 가능 여부 체크
    private bool doubleJumpAble = true; //플레이어의 더블 점프 가능 여부 체크
    private bool isSlidingOnWall = false; //플레이어가 벽에 닿아있는지 여부 체크
    private bool action = true;


    public float rayLength;
    public float moveSpeed;
    public float jumpPower;
    public float slidingSpeed; //슬라이딩으로 떨어지는 속도
    public float feverJump; //피버 점프 높이

    Vector3 wallPos; //충돌한 벽의 위치 저장


    void Start()
    {
        partical = GetComponent<SlidingPartical>();
        slider.value = 0.0f;
    }


    void Update()
    {
        if (action == false)
            return;

        JumpPlayer();

        if (isSlidingOnWall == true && (FloorCheck() == false && isGrounded == false))
            return;

        MovePlayer();
    }

    public void GetStarShard()
    {
        Debug.Log("피퍼게이지");
        slider.value += 0.25f;
        if (slider.value >= 1.0f)
        {
            FeverJump();
            slider.value = 0.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Floor")
        {
            InitJump();
            isGrounded = true;
            return;
        }

        else if (collision.collider.tag == "Wall")
        {
            wallPos = collision.collider.transform.position;
            if (wallPos.x < transform.position.x)
            {
                direction = true;
            }
            else if (wallPos.x > transform.position.x)
            {
                direction = false;
            }
            InitJump();
            WallSliding();
        }

        else if (collision.collider.tag == "Floor" && collision.collider.tag == "Wall")
        {

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
            WallSliding();
    }


    private void MovePlayer()
    {
        if (direction == true)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (direction == false)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    private void JumpPlayer()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && doubleJumpAble == true)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            isSlidingOnWall = false;
            isGrounded = false;
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

    private void FeverJump()
    {
        action = false;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, feverJump, 0);
    }

    private bool FloorCheck()
    {
        Vector2 origin = this.transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, 0.1f, direction, rayLength);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Floor"))
            {
                return true;
            }
        }

        return false;
    }
}
