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
    [SerializeField] private float Espeed = 3.5f;//�G�l�~�[�̑���
    public bool isSumaki = false;�@�@�@�@�@�@�@�@//�Ŋ����ɂ���Ă��邩
    private bool isDie = false;
    [SerializeField] private int ItemNum;�@�@�@�@//�����Ă���A�C�e���i���o�[
    private float VigTime;                       //�x�����p������
    private float DanTime;                       //�퓬���p������
    [SerializeField] private bool isHelp;                         //���Ă΂ꂳ�ꂽ��


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

    private bool SeeRay;                //�Ԃɏ�Q�������邩
    private bool SeePlayer;             //�v���C���[�������Ă��邩
    private bool VigPlayer;             //�x�����@�����Ă��邩
    private bool DanPlayer;             //�퓬���@�����Ă��邩

    private float viewrad = 0;    //�G�l�~�[�̎����p�x
    [SerializeField] private float viewrange = 15; //�G�l�~�[�̎���̍L��
    private Vector3 oldepos;       //�G�l�~�[�̑O�ʒu
    private Vector3 oldppos;       //�v���C���[�̍ŏI�ڌ��ʒu enemy�̈ړ��ꏊ


    //�T���֘A
    [SerializeField] private GameObject LocalPoints; //�ʏ펞�ړ��|�C���g
    [SerializeField] private GameObject GlobalPoints;//���Ă΂ꎞ�ړ��|�C���g
    private int Lindex = 0;
    private int Gindex = 0;
    private bool isStart;
    private Vector3 Goal;
    private int IndexStart;
    private int IndexGoal;
    private float KeepLenS;
    private float KeepLenG;

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
        oldepos = transform.position;
        VigPlayer = false;
        DanPlayer = false;
        SeeRay = false;
        SeePlayer = false;
        isHelp = false;
        VigTime = 0.0f;
        DanTime = 0.0f;
        isStart = true;
        IndexStart = 0;
        IndexGoal = 0;
        KeepLenS = 100.0f;
        KeepLenG = 100.0f;
    }


    void Update()
    {
        float alert = UIscript.GetAlertness();


        if (isSumaki == false)
        {
            if (alert < 1 && isHelp == true)
            {
                HelpEnemy();
            }

            if (alert <= 0 && isHelp == false)
                SearchStatus();
            else if (alert > 0 && alert < 100)
                WarningStatus();
            else if (alert >= 100)
                FightStatus();
        }
        else
        {
            //�Ŋ���������
            DieEnemy();
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
        VigTime += Time.deltaTime;

        if (VigTime < 8.0f)
        {
            //�ړ�
            Vector3 dist = oldppos - transform.position;
            this.transform.position += dist.normalized * Time.deltaTime * Espeed * 0.3f;


            //����
            if (dist.magnitude < 0.5f)
            {
                LostPlayerViewEnemy();
            }
            else
            {
                ViewEnemy();
            }
        }
        else
        {
            VigPlayer = false;
        }
    }


    //============================================================================
    //�퓬���
    //============================================================================
    private void FightStatus()
    {
        DanTime += Time.deltaTime;
        
        if (DanTime < 5.0f)
        {
            if (SeePlayer == true)
            {
                //�ړ�
                float dis = Vector3.Distance(transform.position, player.gameObject.transform.position);
                Vector3 dist = player.gameObject.transform.position - transform.position;
                if (dis > 1.5f)
                    this.transform.position += dist.normalized * Time.deltaTime * Espeed * 1.2f;

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
                viewrad = VecRad(player.gameObject.transform.position, transform.position);
                viewrad = -1 * viewrad * Mathf.Rad2Deg;

                //�����̕����Ɍ�����ς���
                transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1 * viewrad);
                oldepos = transform.position;
            }
            else
            {
                //�ړ�
                Vector3 dist = oldppos - transform.position;
                this.transform.position += dist.normalized * Time.deltaTime * Espeed * 1.2f;

                //����
                if (dist.magnitude < 0.5f)
                {
                    ViewEnemy();
                }
                else
                {
                    viewrad = VecRad(player.gameObject.transform.position, transform.position);
                    viewrad = -1 * viewrad * Mathf.Rad2Deg;

                    //�����̕����Ɍ�����ς���
                    transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1 * viewrad);
                    oldepos = transform.position;
                }
            }
        }
        else
        {
            VigPlayer = false;
            DanPlayer = false;
        }
    }


    //============================================================================
    //���Ă΂���
    //============================================================================
    private void HelpEnemy()
    {
        if (isStart)
        {
            float length = 0.0f;
            Vector3 Rvec, dist;
            RaycastHit hit;
            KeepLenS = 100;
            KeepLenG = 100;
            for (int i = 0; i< GlobalPoints.transform.childCount; i++)
            {
                dist = GlobalPoints.transform.GetChild(i).gameObject.transform.position;

                //��Q���̗L��
                Goal = player.gameObject.transform.position;
                Rvec = Goal - dist;
                length = Rvec.magnitude;
                if (Physics.Raycast(dist, Rvec, out hit, length + 1.0f))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        if(KeepLenS > length)
                        {
                            KeepLenS = length;
                            IndexGoal = i;
                        }
                    }
                }

                Goal = transform.position;
                Rvec = Goal - dist;
                length = Rvec.magnitude;
                if (Physics.Raycast(dist, Rvec, out hit, length + 1.0f))
                {
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        if (KeepLenG > length)
                        {
                            KeepLenG = length;
                            IndexStart = i;
                        }
                    }
                }
            }
            Gindex = IndexStart;

            isStart = false;
        }
        else
        {
            Vector3 dist = GlobalPoints.transform.GetChild(Gindex).gameObject.transform.position
                - this.transform.position;
            this.transform.position += dist.normalized * Time.deltaTime * Espeed * 1.2f;

            if (dist.magnitude < 0.5f && (IndexStart > IndexGoal))
            {

                if (Gindex == IndexGoal) Gindex = IndexGoal;
                else Gindex--;
            }
            else if(dist.magnitude < 0.5f && (IndexStart < IndexGoal))
            {
                if (Gindex == IndexGoal) Gindex = IndexGoal;
                else Gindex++;
            }

            ViewEnemy();
        }
    }


    //============================================================================
    //�퓬�s�\���
    //============================================================================
    private void DieEnemy()
    {
        if (!isDie && ItemNum == 0)
            player.item.AddKey(1);

        isDie = true;
    }


    //
    //
    //���̑��֐�
    //============================================================================
    //�ړ��i�T�����j
    private void SearchMove(float sp)
    {
        Vector3 dist = LocalPoints.transform.GetChild(Lindex).gameObject.transform.position - this.transform.position;
        this.transform.position += dist.normalized * Time.deltaTime * sp;

        if (dist.magnitude < 0.5f)
        {
            Lindex++;
            if (Lindex >= LocalPoints.transform.childCount) Lindex = 0;
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
        viewrad = VecRad(transform.position, oldepos);
        viewrad = -1 * viewrad * Mathf.Rad2Deg;

        //�����̕����Ɍ�����ς���
        transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1 * viewrad);
        oldepos = transform.position;
    }

    //�v���C���[�����������Ƃ��̎����Ɛg�̂̌���
    private void LostPlayerViewEnemy()
    {
        viewrad += -1 * Time.deltaTime;

        //�����̕����Ɍ�����ς���
        transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1 * viewrad);
        oldepos = transform.position;
    }


    //�v���C���[������ɓ����Ă��邩
    private void SeeingPlayer()
    {
        Vector3 ppos = player.gameObject.transform.position;
        Vector3 epos = transform.position;
        float dis = Vector3.Distance(epos, ppos);
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

        //��Q���̗L��
        RaycastHit hit;
        Vector3 Rvec = ppos - epos;
        if (Physics.Raycast(epos, Rvec, out hit, RangeVigilant))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                SeeRay = true;
            }
            else
            {
                SeeRay = false;
            }
        }
        else
        {
            SeeRay = false;
        }

        if (is_player == true && SeeRay == true)
        {
            SeePlayer = true;
            //�x�����ɐN��
            if (dis <= RangeVigilant && dis > RangeDanger)
            {
                VigTime = 0;
                DanTime = 0;
                VigPlayer = true;
                oldppos = player.gameObject.transform.position;
            }
            //���퓬���ɐN��
            else if (dis <= RangeDanger)
            {
                VigTime = 0;
                DanTime = 0;
                DanPlayer = true;
                oldppos = player.gameObject.transform.position;
            }
        }
        else
        {
            SeePlayer = false;
        }
    }


    //UI�ω�
    private void ChangeUI()
    {
        //�x�����ɐN��
        if (VigPlayer && SeePlayer == true)
        {
            UIscript.AddAlertness(Time.deltaTime * AddAlert);
        }
        //���퓬���ɐN��
        if (DanPlayer)
        {
            UIscript.SetDanger();
        }

        //�Q�[�W����
        if (!VigPlayer && !DanPlayer)
        {
            UIscript.SetAlertness(0);
        }
    }

}
