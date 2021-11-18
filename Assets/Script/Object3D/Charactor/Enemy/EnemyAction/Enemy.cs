using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //�I�u�W�F�N�g�E�X�N���v�g�֘A
    [SerializeField] private GameObject bullet;//�e�I�u�W�F�N�g
    [SerializeField] private Player player;//�v���C���[�N���X
    [SerializeField] private GameObject enemyUI;//�G�l�~�[��UI�I�u�W�F�N�g

    private EnemyUI UIscript;//UI�I�u�W�F�N�g�ɂ��Ă�X�N���v�g


    //�G�l�~�[�̃p�����[�^�֘A
    private float Espeed = 3.5f;//�G�l�~�[�̑���
    public bool isSumaki = false;//�Ŋ����ɂ���Ă��邩


    //�e�֘A
    private Vector3 Bvec = new Vector3(0, 90, 0);//���ˎ��̃x�N�g��
    private Vector3 Bpos;             //���ˍ��W

    private float     B_cnt;            //���ˊԊu�i�b���ۑ��p�j
    private float     B_interval = 2.0f;//���ˊԊu�i�萔�j
    private float     B_rad;            //���ˎ��̃G�l�~�[�A�v���C���[�Ԃ̊p�x
    private float     r = 2.0f;         //���ˈʒu�̋���


    //����֘A
    [SerializeField] private float RangeVigilant = 5.0f;//�x�� ����̋���
    [SerializeField] private float RangeDanger = 1.0f;  //���퓬 ����̋���

    private bool VigPlayer;             //�x�����@�����Ă��邩
    private bool DanPlayer;             //�퓬���@�����Ă��邩
    private Vector3 vigpos;             //�x����enemy�̈ړ��ꏊ
    private float dantime;              //�퓬�������������ԕۑ��p

    private float viewrad = 0;    //�G�l�~�[�̎����p�x
    private float viewrange = 15; //�G�l�~�[�̎���̍L��
    private Vector3 oldpos;       //�G�l�~�[�̑O�ʒu


    //�T���֘A
    [SerializeField] private GameObject points;//�ړ��|�C���g
    private int index = 0;

    //UI�֘A
    private float AddAlert = 5.0f;//UI���Z�l


    //
    //
    //���C���֐�
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
            //�Ŋ���������
            //DieEnemy();
        }
    }


    void FixedUpdate()
    {
        AllStatus();
    }



    //
    //
    //�X�e�[�^�X�֐�
    //============================================================================
    //�S���
    //============================================================================
    private void AllStatus()
    {
        //�e�̔��˕����E�����̃v���O�����ɕK�{
        B_rad = VecRad(player.gameObject.transform.position, transform.position);
        Bvec.x = -1 * B_rad * Mathf.Rad2Deg;

        SeeingPlayer();
        ChangeUI();
    }


    //============================================================================
    //���G���
    //============================================================================
    private void SearchStatus()
    {
        //�ړ�
        SearchMove(Espeed);

        //����
        ViewEnemy();
    }


    //============================================================================
    //�x�����
    //============================================================================
    private void WarningStatus()
    {
        //�ړ�
        transform.position = Vector3.MoveTowards(transform.position, vigpos, Time.deltaTime * Espeed * 0.3f);

        //����
        ViewEnemy();
    }


    //============================================================================
    //�퓬���
    //============================================================================
    private void FightStatus()
    {
        //�ړ�
        float dis = Vector3.Distance(transform.position, player.gameObject.transform.position);
        if (dis > 1.5f)
            transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, Time.deltaTime * Espeed * 1.2f);

        //�e�̐ݒ�
        {
            //���ˊԊu�p�J�E���g
            B_cnt += Time.fixedDeltaTime;

            //�e�̔��ˈʒu
            Bpos = transform.position;
            Bpos.x = Bpos.x + r * Mathf.Cos(B_rad);
            Bpos.y = Bpos.y + r * Mathf.Sin(B_rad);
        }

        //�e�̔���
        if (B_cnt > B_interval)
        {
            Instantiate(bullet, Bpos, Quaternion.Euler(Bvec));
            B_cnt = 0;
        }

        //����
        ViewEnemy();
    }


    //============================================================================
    //�퓬�s�\���
    //============================================================================
    private void DieEnemy()
    {
        player.item.AddKey(1);
    }


    //
    //
    //���̑��֐�
    //============================================================================
    //�ړ��i�T�����j
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


    //�p�x�Z�o
    private float VecRad(Vector3 a1, Vector3 a2)
    {
        float vecrad;

        vecrad = Mathf.Atan2(a1.y - a2.y, a1.x - a2.x);

        return vecrad;
    }


    //�G�l�~�[�̎����Ɛg�̂̌���
    private void ViewEnemy()
    {
        viewrad = VecRad(transform.position, oldpos);
        viewrad = -1 * viewrad * Mathf.Rad2Deg;

        //�����̕����Ɍ�����ς���
        transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1 * viewrad);
        oldpos = transform.position;
    }


    //�v���C���[������ɓ����Ă��邩
    private void SeeingPlayer()
    {
        float dis = Vector3.Distance(transform.position, player.gameObject.transform.position);
        float range = Mathf.Repeat(-viewrad, 360);//�p�x��0~360�܂�
        float range_min = range - viewrange;//����̉���
        float range_max = range + viewrange;//����̏��
        bool is_player = false;//����Ƀv���C���[�������Ă��邩�̃t���O
        float playerrad = Mathf.Repeat(-Bvec.x, 360);//�v���C���[�ꎞ�ۑ��p�x

        //����̏���A�����𒴉߂����Ƃ��̏���
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

        //�퓬��Ԏ�������������
        if (DanPlayer == true)
        {
            dantime += Time.fixedDeltaTime;
        }

        //�x�����ɐN��
        if (dis <= RangeVigilant && dis > RangeDanger && is_player == true)
        {
            dantime = 0;
            VigPlayer = true;
            vigpos = player.gameObject.transform.position;
        }
        //���퓬���ɐN��
        else if (dis <= RangeDanger && is_player == true)
        {
            dantime = 0;
            DanPlayer = true;
            vigpos = player.gameObject.transform.position;
        }
    }


    //UI�ω�
    private void ChangeUI()
    {
        Debug.Log(dantime);
        Debug.Log(DanPlayer);

        //�퓬����������
        if (dantime > 5.0f)
        {
            dantime = 0.0f;
            DanPlayer = false;
        }
        //�x�����ɐN��
        if (VigPlayer)
        {
            UIscript.AddAlertness(Time.deltaTime * AddAlert);
        }
        //���퓬���ɐN��
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
