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
    private const string ANIM_IDLE_DOWN = "idle_down";
    private const string ANIM_IDLE_UP = "idle_up";
    private const string ANIM_IDLE_LEFT = "idle_left";
    private const string ANIM_IDLE_RIGHT = "idle_right";

    private const string ANIM_WALK_DOWN = "walk_down";
    private const string ANIM_WALK_UP = "walk_up";
    private const string ANIM_WALK_LEFT = "walk_left";
    private const string ANIM_WALK_RIGHT = "walk_right";

    private string currentAnimation = "";

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
        else if (statePlayer == EStatePlayer.rope) {
            StartCoroutine(CoFalling());    
        }
    }

    void MoveNormal()
    {
        // Lấy input di chuyển từ bàn phím
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, 0).normalized;
        directionAnim = new Vector2(moveX, moveY).normalized;
        // Cập nhật vị trí nhân vật
        transform.Translate(movement * speed * Time.deltaTime);

        // Cập nhật animation dựa trên trạng thái di chuyển
        UpdateAnimation();
    }

    void MoveStair()
    {
        // Lấy input di chuyển từ bàn phím
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (transform.position.y >= 0.5f) 
        {
            directionAnim = new Vector2(moveX, -moveY).normalized;
            
        }
        else
        {
            directionAnim = new Vector2(moveX, moveY).normalized;
        }

        movement = new Vector2(moveX, moveY).normalized;
        // Cập nhật vị trí nhân vật
        transform.Translate(movement * speed * Time.deltaTime);

        // Cập nhật animation dựa trên trạng thái di chuyển
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        return;
        string newAnimation = "";

        if (directionAnim.magnitude > 0)
        {
            // Đang di chuyển
            if (Mathf.Abs(directionAnim.x) > Mathf.Abs(directionAnim.y))
            {
                // Di chuyển theo trục X (trái/phải)
                newAnimation = directionAnim.x > 0 ? ANIM_WALK_RIGHT : ANIM_WALK_LEFT;
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
                if (currentAnimation == ANIM_WALK_RIGHT) newAnimation = ANIM_IDLE_RIGHT;
                else if (currentAnimation == ANIM_WALK_LEFT) newAnimation = ANIM_IDLE_LEFT;
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
    //IEnumerator CoFalling()
    //{
    //    transform.Translate(new Vector3(0.5f, 0, 0) * speed * Time.deltaTime);
    //    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    //    yield return null; 
    //}

    IEnumerator CoFalling()
    {
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
            skeletonAnimation.AnimationState.SetAnimation(0, ANIM_IDLE_RIGHT, true);

            statePlayer = EStatePlayer.endGame;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "stair")
        {
            statePlayer = EStatePlayer.normal;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -0.1f));
        }
    }
}
