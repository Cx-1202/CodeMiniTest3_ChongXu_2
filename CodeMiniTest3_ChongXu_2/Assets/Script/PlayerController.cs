using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    int speed = 10;
    int conecount = 1;
    int boxcount = 1;
    int markcount = 1;
    bool direction;

    Vector3 pointA = new Vector3(0, 0, 33.5f);
    Vector3 pointB = new Vector3(0, 0, 27.5f);

    public Animator PlayerAnimator;
    public GameObject PlayerCollider;
    public GameObject RotatePlate;
    public GameObject MovePlate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKey(KeyCode.W))
        {
            PlayerAnimator.SetBool("Move", true);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            PlayerAnimator.SetBool("Move", true);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            PlayerAnimator.SetBool("Move", false);
        }


        if(conecount <= 0)
        {
            RotatePlate.transform.rotation = Quaternion.Euler(0, 90, 0);

        }

        if(boxcount <= 0)
        {
            MovePlate.transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 1));
        }

        if (markcount <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            conecount--;
            Destroy(other.gameObject);
        }else if (other.gameObject.CompareTag("Object"))
        {
            boxcount--;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Mark"))
        {
            markcount--;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("PlaneD"))
        {
            PlayerCollider.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneD"))
        {
            PlayerCollider.transform.parent = null;
        }
    }
}
