using UnityEngine;
using Spine.Unity;
using System.Collections; // Thư viện của Spine

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private void Awake()
    {
        Instance = this;
    }
    enum EStatePlayer
    {
        normal, stair, endGame, rope
    }
    public PlayerBag playerBag;
    [SerializeField] private float speed = 5f;
    [SerializeField] private SkeletonAnimation skeletonAnimation; // Spine SkeletonAnimation

    private EStatePlayer statePlayer = EStatePlayer.normal;
    private Vector2 movement;
    private Vector2 directionAnim;

    // Các tên animation trong Spine
    private string ANIM_IDLE_DOWN = "idle";
    private string ANIM_IDLE_UP = "idle";
    private string ANIM_IDLE_LEFT = "idle";

    private string ANIM_WALK_DOWN = "Fmove";
    private string ANIM_WALK_UP = "Bmove";
    private string ANIM_WALK_LEFT = "Lmove";
    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////
    /// </summary>
    private string ANIM_MY_IDLE = "idleMy";
    
    private string ANIM_MY_WALK_DOWN = "FMymove";
    private string ANIM_MY_WALK_UP = "BMymove";
    private string ANIM_MY_WALK_LEFT = "LMymove";
    
    private string currentAnimation = "";

    public bool keyIsPressed = false;

    private bool isFinishGoal = false;
    
    private float lastFootstepTime = 0f; // Thời gian lần cuối phát âm thanh bước chân
    private const float footstepInterval = 4f; // Khoảng thời gian giữa mỗi lần phát âm thanh
    // Start is called before the first frame update
    void Start()
    {
        if (skeletonAnimation == null)
        {
            Debug.LogError("SkeletonAnimation chưa được gán!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.stateGamePlay == GameManager.GameState.Play)
        {
            if (statePlayer == EStatePlayer.normal)
            {
                MoveNormal();
            }
            else if (statePlayer == EStatePlayer.stair)
            {
                MoveStair();
            }
            else if (statePlayer == EStatePlayer.endGame)
            {
                // pause game
            }
            else if (statePlayer == EStatePlayer.rope)
            {
                StartCoroutine(CoFalling());
            }
        }
    }

    void MoveNormal()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        keyIsPressed = IsMovementKeyPressed();

        movement = new Vector2(moveX, 0).normalized;
        directionAnim = new Vector2(moveX, moveY).normalized;

        // Phát âm thanh bước chân mỗi 2 giây
        if (keyIsPressed && Time.time - lastFootstepTime >= footstepInterval)
        {
            SoundManager.Instance.OnStepFoot();
            lastFootstepTime = Time.time; // Cập nhật thời gian phát âm thanh
        }

        transform.Translate(movement * speed * Time.deltaTime);
        UpdateAnimation();
    }

    void MoveStair()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        keyIsPressed = IsMovementKeyPressed();

        if (transform.position.y >= -1.12f) 
        {
            directionAnim = new Vector2(moveX, -moveY).normalized;
        }
        else
        {
            directionAnim = new Vector2(moveX, moveY).normalized;
        }

        movement = new Vector2(moveX, moveY).normalized;

        // Phát âm thanh bước chân (crack) mỗi 2 giây
        if (keyIsPressed && Time.time - lastFootstepTime >= footstepInterval)
        {
            SoundManager.Instance.OnStepFootCrack();
            lastFootstepTime = Time.time; // Cập nhật thời gian phát âm thanh
        }

        transform.Translate(movement * speed * Time.deltaTime);
        UpdateAnimation();
    }


    bool IsMovementKeyPressed()
    {
        KeyCode[] movementKeys = new KeyCode[]
        {
        KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, // AWSD
        KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow // Mũi tên
        };

        foreach (KeyCode key in movementKeys)
        {
            if (Input.GetKey(key))  // Nếu một trong các phím được giữ
            {
                return true;
            }
        }

        return false;  // Không có phím di chuyển nào được giữ
    }
    void UpdateAnimation()
    {
        string newAnimation = "";

        if (directionAnim.magnitude > 0)
        {
            // Đang di chuyển
            if (Mathf.Abs(directionAnim.x) > Mathf.Abs(directionAnim.y))
            {
                // Di chuyển theo trục X (trái/phải)
                newAnimation = ANIM_WALK_LEFT;

                // Lật nhân vật nếu đi phải
                Vector3 localScale = transform.localScale;
                localScale.x = directionAnim.x < 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
                transform.localScale = localScale;
            }
            else
            {
                // Di chuyển theo trục Y (lên/xuống)
                newAnimation = directionAnim.y > 0 ? ANIM_WALK_UP : ANIM_WALK_DOWN;
            }
        }
        else
        {
            // Không di chuyển
            if (currentAnimation.Contains("walk"))
            {
                if (currentAnimation == ANIM_WALK_LEFT) newAnimation = ANIM_IDLE_LEFT;
                else if (currentAnimation == ANIM_WALK_UP) newAnimation = ANIM_IDLE_UP;
                else if (currentAnimation == ANIM_WALK_DOWN) newAnimation = ANIM_IDLE_DOWN;
            }
        }

        // Thay đổi animation nếu cần
        if (!string.IsNullOrEmpty(newAnimation) && newAnimation != currentAnimation)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, newAnimation, true);
            currentAnimation = newAnimation;
        }
    }


    IEnumerator CoFalling()
    {
        statePlayer = EStatePlayer.endGame;
        float duration = 2f; // Thời gian để di chuyển hết đường cung
        float elapsedTime = 0f;
        Vector3 startPoint = transform.position;
        Vector3 endPoint = new Vector3(startPoint.x + 5f, startPoint.y + 3f, startPoint.z); // Điểm kết thúc (cửa sổ)
        float height = 0.5f; // Chiều cao của cung

        // Chuyển sang chế độ vật lý động
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // Tỷ lệ thời gian (0 đến 1)

            // Công thức quỹ đạo đường cung (parabolic path)
            float x = Mathf.Lerp(startPoint.x, endPoint.x, t); // Di chuyển theo trục X
            float y = Mathf.Lerp(startPoint.y, endPoint.y, t) + height * Mathf.Sin(t * Mathf.PI); // Tạo đường cung theo trục Y

            transform.position = new Vector3(x, y, startPoint.z);

            yield return null; // Chờ đến frame tiếp theo
        }

        // Đảm bảo nhân vật dừng ở điểm cuối
        transform.position = endPoint;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "stair")
        {
            statePlayer = EStatePlayer.stair;
        }
        else if (collision.gameObject.tag == "destination")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, ANIM_IDLE_LEFT, true);

            statePlayer = EStatePlayer.endGame;
        }else if (collision.gameObject.tag == "goal" && isFinishGoal)
        {
            statePlayer = EStatePlayer.endGame;
            GameManager.Instance.SetWinGame();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "stair")
        {
            statePlayer = EStatePlayer.normal;
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -0.1f));
        }
    }

    public void FinishGoal()
    {
        if (!isFinishGoal)
        {
            isFinishGoal = true;
            ANIM_IDLE_DOWN = ANIM_MY_IDLE;
            ANIM_IDLE_LEFT = ANIM_MY_IDLE;
            ANIM_IDLE_UP = ANIM_MY_IDLE;
            ANIM_WALK_DOWN = ANIM_MY_WALK_DOWN;
            ANIM_WALK_UP = ANIM_MY_WALK_UP;
            ANIM_WALK_LEFT = ANIM_MY_WALK_LEFT;
        }
        
    }
}
