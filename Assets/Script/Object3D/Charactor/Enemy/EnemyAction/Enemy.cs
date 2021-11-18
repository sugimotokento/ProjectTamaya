using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //オブジェクト・スクリプト関連
    [SerializeField] private GameObject bullet;//弾オブジェクト
    [SerializeField] private Player player;//プレイヤークラス
    [SerializeField] private GameObject enemyUI;//エネミーのUIオブジェクト

    private EnemyUI UIscript;//UIオブジェクトについてるスクリプト


    //エネミーのパラメータ関連
    private float Espeed = 3.5f;//エネミーの速さ
    public bool isSumaki = false;//簀巻きにされているか


    //弾関連
    private Vector3 Bvec = new Vector3(0, 90, 0);//発射時のベクトル
    private Vector3 Bpos;             //発射座標

    private float     B_cnt;            //発射間隔（秒数保存用）
    private float     B_interval = 2.0f;//発射間隔（定数）
    private float     B_rad;            //発射時のエネミー、プレイヤー間の角度
    private float     r = 2.0f;         //発射位置の距離


    //視野関連
    [SerializeField] private float RangeVigilant = 5.0f;//警戒 視野の距離
    [SerializeField] private float RangeDanger = 1.0f;  //即戦闘 視野の距離

    private bool VigPlayer;             //警戒区域　入っているか
    private bool DanPlayer;             //戦闘区域　入っているか
    private Vector3 vigpos;             //警戒時enemyの移動場所
    private float dantime;              //戦闘時見失った時間保存用

    private float viewrad = 0;    //エネミーの視線角度
    private float viewrange = 15; //エネミーの視野の広さ
    private Vector3 oldpos;       //エネミーの前位置


    //探索関連
    [SerializeField] private GameObject points;//移動ポイント
    private int index = 0;

    //UI関連
    private float AddAlert = 5.0f;//UI加算値


    //
    //
    //メイン関数
    //============================================================================
    void Start()
    {
        UIscript = enemyUI.GetComponent<EnemyUI>();

        viewrad = 0;
        dantime = 0;
        oldpos = transform.position;
        VigPlayer = false;
        DanPlayer = false;
    }


    void Update()
    {
        float alert = UIscript.GetAlertness();


        if (isSumaki == false)
        {
            if (alert <= 0)
                SearchStatus();
            else if (alert > 0 && alert < 100)
                WarningStatus();
            else if (alert >= 100)
                FightStatus();
        }
        else
        {
            //簀巻き成功時
            //DieEnemy();
        }
    }


    void FixedUpdate()
    {
        AllStatus();
    }



    //
    //
    //ステータス関数
    //============================================================================
    //全状態
    //============================================================================
    private void AllStatus()
    {
        //弾の発射方向・視線のプログラムに必須
        B_rad = VecRad(player.gameObject.transform.position, transform.position);
        Bvec.x = -1 * B_rad * Mathf.Rad2Deg;

        SeeingPlayer();
        ChangeUI();
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
        transform.position = Vector3.MoveTowards(transform.position, vigpos, Time.deltaTime * Espeed * 0.3f);

        //視線
        ViewEnemy();
    }


    //============================================================================
    //戦闘状態
    //============================================================================
    private void FightStatus()
    {
        //移動
        float dis = Vector3.Distance(transform.position, player.gameObject.transform.position);
        if (dis > 1.5f)
            transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, Time.deltaTime * Espeed * 1.2f);

        //弾の設定
        {
            //発射間隔用カウント
            B_cnt += Time.fixedDeltaTime;

            //弾の発射位置
            Bpos = transform.position;
            Bpos.x = Bpos.x + r * Mathf.Cos(B_rad);
            Bpos.y = Bpos.y + r * Mathf.Sin(B_rad);
        }

        //弾の発射
        if (B_cnt > B_interval)
        {
            Instantiate(bullet, Bpos, Quaternion.Euler(Bvec));
            B_cnt = 0;
        }

        //視線
        ViewEnemy();
    }


    //============================================================================
    //戦闘不能状態
    //============================================================================
    private void DieEnemy()
    {
        player.item.AddKey(1);
    }


    //
    //
    //その他関数
    //============================================================================
    //移動（探索時）
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
        transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1 * viewrad);
        oldpos = transform.position;
    }


    //プレイヤーが視野に入っているか
    private void SeeingPlayer()
    {
        float dis = Vector3.Distance(transform.position, player.gameObject.transform.position);
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

        //戦闘状態時見失った時間
        if (DanPlayer == true)
        {
            dantime += Time.fixedDeltaTime;
        }

        //警戒区域に侵入
        if (dis <= RangeVigilant && dis > RangeDanger && is_player == true)
        {
            dantime = 0;
            VigPlayer = true;
            vigpos = player.gameObject.transform.position;
        }
        //即戦闘区域に侵入
        else if (dis <= RangeDanger && is_player == true)
        {
            dantime = 0;
            DanPlayer = true;
            vigpos = player.gameObject.transform.position;
        }
    }


    //UI変化
    private void ChangeUI()
    {
        Debug.Log(dantime);
        Debug.Log(DanPlayer);

        //戦闘時見失った
        if (dantime > 5.0f)
        {
            dantime = 0.0f;
            DanPlayer = false;
        }
        //警戒区域に侵入
        if (VigPlayer)
        {
            UIscript.AddAlertness(Time.deltaTime * AddAlert);
        }
        //即戦闘区域に侵入
        if (DanPlayer)
        {
            UIscript.SetDanger();
        }
        if (!VigPlayer && !DanPlayer)
        {
            UIscript.AddAlertness(-1 * Time.deltaTime * AddAlert);

            if (UIscript.GetAlertness() <= 0.0f)
                UIscript.SetAlertness(0);
        }

        VigPlayer = false;
    }

}
