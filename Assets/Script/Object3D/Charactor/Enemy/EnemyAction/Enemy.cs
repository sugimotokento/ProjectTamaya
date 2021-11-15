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

    private EnemyUI UIscript;//UIオブジェクトについてるスクリプト

    //private NavMeshAgent navAgent;

    private float Espeed = 3.5f;//エネミーの速さ
    
    private Vector3 Bvec = new Vector3(0, 90, 0);//弾発射時のベクトル

    private float   B_cnt;   //弾の発射間隔（秒数）
    private Vector3 Bpos;    //弾の発射座標
    private float   B_rad;   //発射時のエネミー、プレイヤー間の角度
    private float   r = 2.0f;//球の発射位置の距離

    [SerializeField] private float viewVigilant = 5.0f;//警戒 視野の距離
    [SerializeField] private float viewDanger = 1.0f;  //即戦闘 視野の距離

    private float viewrad = 0;  //エネミーの視線角度
    private float viewrange = 15; //エネミーの視野の広さ
    private Vector3 oldpos;//エネミーの前位置

    [SerializeField] private GameObject points;//移動ポイント
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        UIscript = enemyUI.GetComponent<EnemyUI>();
        viewrad = 0;
        oldpos = transform.position;
        //navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (UIscript.GetAlertness() <= 0)
            SearchStatus();
        else if (UIscript.GetAlertness() > 0 && UIscript.GetAlertness() < 100)
            WarningStatus();
        else if (UIscript.GetAlertness() >= 100)
            FightStatus();


        AllStatus();
    }

    void FixedUpdate()
    {
    }

    //void Update()
    //{
    //    EnemyMove();
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    //球の設定
    //    {
    //        //球の発射方向
    //        B_rad = VecRad(player.transform.position, transform.position);
    //        Bvec.x = -1 * B_rad * Mathf.Rad2Deg;

    //        //発射間隔用カウント
    //        B_cnt += Time.fixedDeltaTime;

    //        //球の発射位置
    //        Bpos = transform.position;
    //        Bpos.x = Bpos.x + r * Mathf.Cos(B_rad);
    //        Bpos.y = Bpos.y + r * Mathf.Sin(B_rad);
    //    }
    //    //角度は-180~180まで
    //    //Debug.Log(Mathf.Repeat(-Bvec.x, 360));

    //    //球の発射
    //    if (UIscript.GetAlertness() >= 100 == true && B_cnt > 0.5f)
    //    {
    //        Instantiate(bullet, Bpos, Quaternion.Euler(Bvec));
    //        B_cnt = 0;
    //    }


    //    ViewEnemy();
    //    SeeingPlayer();
    //}

    //ステータス関数
    //============================================================================
    //全状態
    //============================================================================
    private void AllStatus()
    {
        //弾の設定
        {
            //弾の発射方向
            B_rad = VecRad(player.transform.position, transform.position);
            Bvec.x = -1 * B_rad * Mathf.Rad2Deg;

            //発射間隔用カウント
            B_cnt += Time.fixedDeltaTime;

            //弾の発射位置
            Bpos = transform.position;
            Bpos.x = Bpos.x + r * Mathf.Cos(B_rad);
            Bpos.y = Bpos.y + r * Mathf.Sin(B_rad);
        }
        SeeingPlayer();
    }

    //============================================================================
    //索敵状態
    //============================================================================
    private void SearchStatus()
    {
        //移動
        SearchMove(Espeed);

        //視線
        ViewEnemy();
    }

    //============================================================================
    //警戒状態
    //============================================================================
    private void WarningStatus()
    {
        //移動
        SearchMove(Espeed * 0.3f);

        //視線
        ViewEnemy();
    }

    //============================================================================
    //戦闘状態
    //============================================================================
    private void FightStatus()
    {
        //移動
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * Espeed * 1.2f);

        
        

        //弾の発射
        if (B_cnt > 2.0f)
        {
            Instantiate(bullet, Bpos, Quaternion.Euler(Bvec));
            B_cnt = 0;
        }
    }


    //その他関数
    //============================================================================
    private void EnemyMove()
    {
        //navAgent.SetDestination(player.transform.position);

        if (UIscript.GetAlertness() >= 100)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * Espeed);
        }
        else
        {
            Vector3 dist = points.transform.GetChild(index).gameObject.transform.position - this.transform.position;
            this.transform.position += dist.normalized * Time.deltaTime * Espeed;

            if (dist.magnitude < 0.5f)
            {
                index++;
                if (index >= points.transform.childCount) index = 0;
            }
        }
    }

    private void SearchMove(float sp)
    {
        Vector3 dist = points.transform.GetChild(index).gameObject.transform.position - this.transform.position;
        this.transform.position += dist.normalized * Time.deltaTime * sp;

        if (dist.magnitude < 0.5f)
        {
            index++;
            if (index >= points.transform.childCount) index = 0;
        }
    }

    //角度算出
    private float VecRad(Vector3 a1, Vector3 a2)
    {
        float vecrad;

        vecrad = Mathf.Atan2(a1.y - a2.y, a1.x - a2.x);

        return vecrad;
    }

    //エネミーの視線と身体の向き
    private void ViewEnemy()
    {
        viewrad = VecRad(transform.position, oldpos);
        viewrad = -1 * viewrad * Mathf.Rad2Deg;

        //視線の方向に向きを変える
        transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, viewrad);
        oldpos = transform.position;
    }

    //プレイヤーが視野に入った時のUI変化
    private void SeeingPlayer()
    {
        float dis = Vector3.Distance(transform.position, player.transform.position);
        float range = Mathf.Repeat(-viewrad, 360);//角度は0~360まで
        float range_min = range - viewrange;//視野の下限
        float range_max = range + viewrange;//視野の上限
        bool is_player = false;//視野にプレイヤーが入っているかのフラグ
        float playerrad = Mathf.Repeat(-Bvec.x, 360);//プレイヤー一時保存角度

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
        //Debug.Log(range);
        //Debug.Log(range_max);
        //Debug.Log(range_min);

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
