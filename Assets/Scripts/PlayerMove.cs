using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PhotonView photonView;

    public Transform cam;

    [SerializeField] private float speed;
    private float temp_speed;
    [SerializeField] private float forceJump = 5f;
    [SerializeField] private float wJump = 0.15f;

    private Rigidbody rb;
    private Collision collis;
    private bool isGround;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        rb = GetComponent<Rigidbody>();
        temp_speed = speed;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        Move();
        Jump();
    }

    private void OnCollisionExit(Collision col)
    {
        StartCoroutine(waitJump());
        collis = col;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.enabled)
        {
            isGround = true;
            speed = temp_speed;
        }

        switch (col.collider.tag)
        {
            case "pl_speed":
                speed = temp_speed * 2f;
                break;

            case "pl_slow":
                speed = temp_speed / 1.7f;
                break;

            case "Finish":
                isGround = false;
                break;

            default:
                if (!col.collider.enabled)  isGround = false;
                break;
        }
    }

    IEnumerator waitJump()
    {
        if (isGround)
        {
            yield return new WaitForSeconds(wJump);
            if(!collis.collider.enabled)  isGround = false;
        }
    }

    void Move()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //rb.AddForce(new Vector3(moveHorizontal, 0, moveVertical));

        
        if (Input.GetKey(KeyCode.W) && isGround)
            rb.AddForce(cam.forward * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.W) && !isGround)
            rb.AddForce(cam.forward * speed / 2 * Time.deltaTime);

        if (Input.GetKey(KeyCode.D) && isGround)
            rb.AddForce(cam.right * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D) && !isGround)
            rb.AddForce(cam.right * speed / 2 * Time.deltaTime);
        if (Input.GetKey(KeyCode.A) && isGround)
            rb.AddForce(-cam.right * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.A) && !isGround)
            rb.AddForce(-cam.right * speed / 2 * Time.deltaTime);

        if (Input.GetKey(KeyCode.S) && isGround)
            rb.AddForce(-cam.forward * speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.S) && !isGround)
            rb.AddForce(-cam.forward * speed / 2 * Time.deltaTime);
        

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
            isGround = false;
        }
    }
}