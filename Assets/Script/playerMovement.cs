using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float speed = 20;
    float horizontal;
    float vertical;
    bool shoot;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        shoot = Input.GetMouseButtonDown(0);
        if (shoot) {
            Debug.Log("atirou");
        }
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        
        transform.up = direction;
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
