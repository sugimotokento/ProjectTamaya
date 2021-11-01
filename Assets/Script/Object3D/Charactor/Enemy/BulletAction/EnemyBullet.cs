using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;

    private bool isdes;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Destroy(gameObject, 1.5f);
        isdes = false;
    }

    void FixedUpdate()
    {
        //ˆÚ“®
        rb.velocity = transform.forward * speed * Time.fixedDeltaTime;

        if (isdes)
            Destroy(gameObject);
    }


    //“–‚½‚è”»’è
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isdes = true;
        }
        else
            Destroy(gameObject);
    }
}
