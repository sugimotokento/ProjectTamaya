using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject player;

    private Vector3 vec3 = new Vector3(50, 90, 0);

    private float rad;
    private float Epos_x, Epos_y, Ppos_x, Ppos_y;

    private bool B_flag;
    private float B_cnt;
    private Vector3 Bpos;

    // Start is called before the first frame update
    void Start()
    {
        B_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //‹…‚ÌÝ’è
        {
            //‹…‚Ì”­ŽË•ûŒü
            Epos_x = transform.position.x;
            Epos_y = transform.position.y;
            Ppos_x = player.transform.position.x;
            Ppos_y = player.transform.position.y;

            float rad = Mathf.Atan2(Ppos_y - Epos_y, Ppos_x - Epos_x);
            vec3.x = -1 * rad * Mathf.Rad2Deg;

            B_cnt += Time.deltaTime;

            //‹…‚Ì”­ŽËˆÊ’u
            float r = 2.0f;
            Bpos = transform.position;
            Bpos.x = Bpos.x + r * Mathf.Cos(rad);
            Bpos.y = Bpos.y + r * Mathf.Sin(rad);
        }

        //‹…‚Ì”­ŽË
        if (Input.GetKeyDown("space"))
        {
            B_flag = !B_flag;
        }

        if (B_flag == true && B_cnt > 0.5f)
        {
            Instantiate(bullet, Bpos, Quaternion.Euler(vec3));
            B_cnt = 0;
        }

    }
}
