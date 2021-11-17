using UnityEngine;

public class Lessons : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private SpriteRenderer _back;
    public float speed = 1.0f;
    public float jumpheight;
    private bool isGrounded;
    //[SerializeField]
    //private SomeView _someView;
    //add links to test views <1>

    //private SomeManager _someManager;
    //add links to some logic managers <2>

    private void Start()
    {
        //SomeConfig config = Resources.Load("SomeConfig", typeof(SomeConfig));as   SomeConfig;
        //load some configs here <3>

        //_someManager = new SomeManager(config);;
        //create some logic managers here for tests <4>

    }

    private void Update()
    {
        CheckGround();
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(transform.up * jumpheight, ForceMode2D.Impulse);
        //_someManager.Update();;
        //update logic managers here <5>
    }

    private void FixedUpdate()
    {
        //_someManager.FixedUpdate();;
        //update logic managers here <6>
    }

    private void OnDestroy()
    {
        //_someManager.Dispose();;
        //dispose logic managers here <7>
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 1);
        isGrounded = colliders.Length > 1;
    }
}

