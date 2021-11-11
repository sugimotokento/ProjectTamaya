using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;//弾オブジェクト
    [SerializeField] private GameObject player;//プレイヤーオブジェクト
    [SerializeField] private GameObject enemyUI;//エネミーのUIオブジェクト
    [SerializeField] private GameObject[] MovePos;//エネミーの移動用座標

    //private NavMeshAgent navAgent;

    private EnemyUI UIscript;//UIオブジェクトについてるスクリプト

    private Vector3 vec3 = new Vector3(0, 90, 0);//弾発射時のベクトル

    private float   rad;//エネミーとプレイヤー間の角度
    private Vector3 Epos, Ppos;//エネミー、プレイヤーの座標

    private float   B_cnt;   //弾の発射間隔（秒数）
    private Vector3 Bpos;    //弾の発射座標
    private float   B_rad;   //発射時のエネミー、プレイヤー間の角度
    private float   r = 2.0f;//球の発射位置の距離

    [SerializeField] private float viewVigilant = 5.0f;//警戒 視野の距離
    [SerializeField] private float viewDanger = 1.0f;  //即戦闘 視野の距離

    private float viewrad = 0;  //エネミーの視線角度
    private float viewrange = 15; //エネミーの視野の広さ

    [SerializeField] private GameObject points;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        UIscript = enemyUI.GetComponent<EnemyUI>();
        viewrad = 180;

        //navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 dist = points.transform.GetChild(index).gameObject.transform.position - this.transform.position;
        this.transform.position += dist.normalized * Time.deltaTime * 15;

        if (dist.magnitude < 0.5f)
        {
            index++;
            if (index >= points.transform.childCount) index = 0;
        }

        viewrad = -vec3.x;
        //navAgent.SetDestination(player.transform.position);
        EnemyMove();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //球の設定
        {
            //球の発射方向
            Epos.x = transform.position.x;
            Epos.y = transform.position.y;
            Ppos.x = player.transform.position.x;
            Ppos.y = player.transform.position.y;

            B_rad = Mathf.Atan2(Ppos.y - Epos.y, Ppos.x - Epos.x);
            vec3.x = -1 * B_rad * Mathf.Rad2Deg;

            //発射間隔用カウント
            B_cnt += Time.fixedDeltaTime;

            //球の発射位置
            Bpos = transform.position;
            Bpos.x = Bpos.x + r * Mathf.Cos(B_rad);
            Bpos.y = Bpos.y + r * Mathf.Sin(B_rad);
        }
        //角度は-180~180まで
        //Debug.Log(Mathf.Repeat(-vec3.x, 360));

        //球の発射
        if (UIscript.GetAlertness() >= 100 == true && B_cnt > 0.5f)
        {
            Instantiate(bullet, Bpos, Quaternion.Euler(vec3));
            B_cnt = 0;
        }


        ViewEnemy();
        SeeingPlayer();
    }

    private void EnemyMove()
    {
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime);

        if (UIscript.GetAlertness() >= 100 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
        }
    }

    //エネミーの視線と身体の向き
    private void ViewEnemy()
    {
        //視線の方向に向きを変える
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, viewrad);
    }

    //プレイヤーが視野に入った時のUI変化
    private void SeeingPlayer()
    {
        float dis = Vector3.Distance(transform.position, player.transform.position);
        float range = transform.localEulerAngles.z;//角度は0~360まで
        float range_min = range - viewrange;//視野の下限
        float range_max = range + viewrange;//視野の上限
        bool is_player = false;//視野にプレイヤーが入っているかのフラグ
        float playerrad = Mathf.Repeat(-vec3.x, 360);//プレイヤー一時保存角度

        //視野の上限、下限を超過したときの処理
        if (range_min < 0)
            range_min += 360;
        if (range_max > 360)
            range_max -= 360;
        if (range_max < range_min)
        {
            if (range_max >= playerrad || range_min <= playerrad)
            {
                is_player = true;
            }
            else
            {
                is_player = false;
            }
        }
        else if (range_max > range_min)
        {
            if (range_min <= playerrad && range_max >= playerrad)
            {
                is_player = true;
            }
            else
            {
                is_player = false;
            }
        }

        //警戒区域に侵入
        if (dis <= viewVigilant && dis > viewDanger && is_player == true)
        {
            UIscript.SetAlertness(Time.deltaTime * 5);
        }
        //即戦闘区域に侵入
        if (dis <= viewDanger && is_player == true)
        {
            UIscript.SetDanger();
        }
    }
}
