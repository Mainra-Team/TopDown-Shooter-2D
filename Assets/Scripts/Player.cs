using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 controlMovement;
    [SerializeField, Header("Character stats")] float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalMovement = Input.GetAxisRaw("Vertical");
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        controlMovement = new Vector2(horizontalMovement, verticalMovement);

    }
    void FixedUpdate()
    {
        rb.velocity = controlMovement.normalized * Time.fixedDeltaTime * movementSpeed;
    }
}
