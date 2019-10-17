using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkPower = 150f;
    public float jumpPower = 3f;
    public AnimationClip walk, jump;
    public Animation Legs;
    public Transform ground;
    public bool isGrounded;

    private float distToGround;
    private Vector2 inputAxis;
    private Rigidbody2D rig;
    private float startScale;
    private bool isJump;
    private bool isWalk;
    private int walkAxis;
    private bool canJump;
    private bool canWalk;
    public int countJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        startScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded)
        {
            canJump = true;
            canWalk = true;
        }

        if (canJump && ((Input.GetButtonDown("Jump")) || Input.GetKeyDown(KeyCode.Space)))
        {
            countJump = countJump + 1;
            isJump = true;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            isWalk = true;
            if (Input.GetKey(KeyCode.D)) walkAxis = 1;
            else walkAxis = -1;
        }
    }

    private void FixedUpdate()
    {
        if (isWalk)
        {
            if (walkAxis == 1)
            {
                transform.localScale = new Vector3(startScale, startScale, 1);
            }
            else
            {
                transform.localScale = new Vector3(-startScale, startScale, 1);
            }
            rig.velocity = new Vector2(walkAxis * walkPower * Time.deltaTime, rig.velocity.y);

            if (canWalk)
            {
                Legs.clip = walk;
                Legs.Play();
            }
            isWalk = false;
        }
        if (isJump)
        {
            rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            if(countJump == 2)
            {
                canJump = false;
            }
            Legs.clip = jump;
            Legs.Play();
            canWalk = false;
            isJump = false;
        }
    }
}
