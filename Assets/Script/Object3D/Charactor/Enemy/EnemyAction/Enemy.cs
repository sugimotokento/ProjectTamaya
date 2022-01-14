using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //�I�u�W�F�N�g�E�X�N���v�g�֘A
    [SerializeField] private GameObject bullet; //�e�I�u�W�F�N�g
    [SerializeField] private Player player;     //�v���C���[�N���X
    [SerializeField] private GameObject enemyUI;//�G�l�~�[��UI�I�u�W�F�N�g

    private EnemyUI UIscript;                   //UI�I�u�W�F�N�g�ɂ��Ă�X�N���v�g

    public EnemySound sound;


    //�G�l�~�[�̃p�����[�^�֘A
    [SerializeField] private float Espeed = 3.5f;//�G�l�~�[�̑���
    public bool isSumaki = false;�@�@�@�@�@�@�@�@//�Ŋ����ɂ���Ă��邩
    public int ItemNum;�@�@�@�@//�����Ă���A�C�e���i���o�[
    private float VigTime;                       //�x�����p������
    private float DanTime;                       //�퓬���p������
    public bool isHelp;                          //���Ă΂ꂳ�ꂽ��
    private bool isNoise;

    private Vector3 NoisePos;

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
    private float AfterRangeDanger;
    [SerializeField] private float AddDangerArea = 1.0f;

    private bool SeeRay;                //�Ԃɏ�Q�������邩
    private bool SeePlayer;             //�v���C���[�������Ă��邩
    private bool VigPlayer;             //�x�����@�����Ă��邩
    private bool DanPlayer;             //�퓬���@�����Ă��邩

    private float viewrad = 0;                     //�G�l�~�[�̎����p�x
    [SerializeField] private float viewrange = 15; //�G�l�~�[�̎���̍L��
    private Vector3 oldepos;                       //�G�l�~�[�̑O�ʒu
    private Vector3 oldppos;                       //�v���C���[�̍ŏI�ڌ��ʒu enemy�̈ړ��ꏊ
    private float KeepAlert;
    private int Sumaki;


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

    public int SCENE_NUM;

    //�A�j���[�V����
    public Animator EnemyAnime;

    //
    //
    //���C���֐�
    //============================================================================
    void Start()
    {
        UIscript = enemyUI.transform.GetChild(0).GetComponent<EnemyUI>();

        viewrad = 0;
        oldepos = transform.position;
        VigPlayer = false;
        DanPlayer = false;
        SeeRay = false;
        SeePlayer = false;
        isHelp = false;
        isNoise = false;
        VigTime = 0.0f;
        DanTime = 0.0f;
        isStart = true;
        IndexStart = 0;
        IndexGoal = 0;
        KeepLenS = 100.0f;
        KeepLenG = 100.0f;
        Lindex = 0;
        KeepAlert = 0;
        Sumaki = 0;

        AfterRangeDanger = RangeDanger;
        SCENE_NUM = 1;

        EnemyAnime.SetBool("isSumaki", false);
        EnemyAnime.SetBool("isMove", true);
        EnemyAnime.SetBool("isBattle", false);
        EnemyAnime.SetBool("isWarning", false);
        EnemyAnime.SetBool("isNormal", true);
        EnemyAnime.SetBool("isCall", false);
        EnemyAnime.SetFloat("BattleBlend", 0.0f);
    }


    void Update()
    {
        if (isSumaki == false)
        {
            if (Sumaki == 0)
            {
                UIscript.SetAlertness(KeepAlert);
            }
            KeepAlert = UIscript.GetAlertness();

            EnemyAnime.SetBool("isSumaki", false);

            if (KeepAlert < 1 && isHelp == true)
            {
                EnemyAnime.SetBool("isCall", true);
                HelpEnemy();
                SCENE_NUM = 0;
            }
            else if (SCENE_NUM == 0)
            {
                SCENE_NUM = 100;
                isStart = true;
            }

            if (KeepAlert <= 0 && SCENE_NUM != 0 && isNoise == false)
            {
                EnemyAnime.SetBool("isBattle", false);
                EnemyAnime.SetBool("isWarning", false);
                EnemyAnime.SetBool("isCall", false);
                EnemyAnime.SetBool("isNormal", true);
                SearchStatus();
            }
            else if (KeepAlert > 0 && KeepAlert < 100)
            {
                EnemyAnime.SetBool("isWarning", true);
                WarningStatus();
                SCENE_NUM = 2;
            }
            else if (KeepAlert >= 100)
            {
                if (SCENE_NUM != 3)
                {
                    ScoreManager.findNum++;
                    sound.EnemyPlayShot(EnemySound.EnemySoundIndex.Contact);
                }

                EnemyAnime.SetBool("isBattle", true);
                FightStatus();
                SCENE_NUM = 3;
            }
            else if (isNoise == true)
            {
                WarningNoiseStatus();
                SCENE_NUM = 5;
            }

            Sumaki += 1;
        }
        else
        {
            Sumaki = 0;
            UIscript.SetAlertness(0);
            isHelp = false;

            EnemyAnime.SetBool("isSumaki", true);
            EnemyAnime.SetBool("isBattle", false);
            EnemyAnime.SetBool("isWarning", false);
            EnemyAnime.SetBool("isCall", false);
            EnemyAnime.SetBool("isNormal", false);
        }
    }


    void FixedUpdate()
    {
        if (isSumaki == false)
        {
            AllStatus();
        }
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
        if (SCENE_NUM != 1 && SCENE_NUM != 4)
        {
            float length = 0.0f;
            Vector3 Rvec, dist;
            RaycastHit hit;
            KeepLenS = 100;
            KeepLenG = 100;

            for (int i = 0; i < LocalPoints.transform.childCount; i++)
            {
                dist = LocalPoints.transform.GetChild(i).gameObject.transform.position;

                //��Q���̗L��
                Rvec = transform.position - dist;
                length = Rvec.magnitude;
                if (Physics.Raycast(dist, Rvec, out hit, length + 1.0f))
                {
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        if (KeepLenS > length)
                        {
                            KeepLenS = length;
                            Lindex = i;
                            SCENE_NUM = 1;
                        }
                    }
                }
            }

            if (SCENE_NUM != 1)
            {
                for (int i = 0; i < GlobalPoints.transform.childCount; i++)
                {
                    dist = GlobalPoints.transform.GetChild(i).gameObject.transform.position;

                    IndexGoal = IndexStart;

                    //��Q���̗L��
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

                SCENE_NUM = 4;
            }
        }

        if (SCENE_NUM == 4)
        {
            Vector3 dist = GlobalPoints.transform.GetChild(Gindex).gameObject.transform.position
                - this.transform.position;
            this.transform.position += dist.normalized * Time.deltaTime * Espeed;

            if (dist.magnitude < 0.5f && (IndexStart > IndexGoal))
            {
                if (Gindex == IndexGoal)
                {
                    SCENE_NUM = 1;
                }
                else Gindex--;
            }
            else if (dist.magnitude < 0.5f && (IndexStart < IndexGoal))
            {
                if (Gindex == IndexGoal)
                {
                    SCENE_NUM = 1;
                }
                else Gindex++;
            }
        }

        if (SCENE_NUM == 1)
        {
            //�ړ�
            SearchMove(Espeed);

            SCENE_NUM = 1;
        }

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

            if(dist.magnitude < 0.5f)
            {
                transform.position = oldepos;
            }

            //����
            ViewEnemy();
        }
        else
        {
            isHelp = false;
            isStart = true;
            VigPlayer = false;
        }
    }


    //============================================================================
    //���R���W�����x�����
    //============================================================================
    private void WarningNoiseStatus()
    {
        //�ړ�
        Vector3 dist = NoisePos - transform.position;
        this.transform.position += dist.normalized * Time.deltaTime * Espeed;

        //����
        ViewEnemy();

        if (dist.magnitude < 2.5f)
        {
            transform.position = oldepos;
            isNoise = false;
        }
        else
        {
            isHelp = false;
            isStart = true;
            VigPlayer = false;
        }
    }


    //============================================================================
    //�퓬���
    //============================================================================
    private void FightStatus()
    {
        isHelp = true;

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
                    B_cnt += Time.deltaTime;

                    //�e�̔��ˈʒu
                    Bpos = transform.position;
                    Bpos.x = Bpos.x + r * Mathf.Cos(B_rad);
                    Bpos.y = Bpos.y + r * Mathf.Sin(B_rad);
                }

                EnemyAnime.SetFloat("BattleBlend", B_cnt);

                //�e�̔���
                if (B_cnt > B_interval)
                {
                    sound.EnemyPlayShot(EnemySound.EnemySoundIndex.PopBallet);
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
                viewrad = VecRad(player.gameObject.transform.position, transform.position);
                viewrad = -1 * viewrad * Mathf.Rad2Deg;

                //�����̕����Ɍ�����ς���
                transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1 * viewrad);
                oldepos = transform.position;

            }
        }
        else
        {
            isHelp = false;
            isStart = true;
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

            for (int i = 0; i < GlobalPoints.transform.childCount; i++)
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
                if (Gindex == IndexGoal)
                {
                    transform.position = oldepos;
                    isHelp = false;
                }
                else Gindex--;
            }
            else if(dist.magnitude < 0.5f && (IndexStart < IndexGoal))
            {
                if (Gindex == IndexGoal)
                {
                    transform.position = oldepos;
                    isHelp = false;
                }
                else Gindex++;
            }

            

            ViewEnemy();
        }
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
        if (transform.position != oldepos)
        {
            viewrad = VecRad(transform.position, oldepos);
            viewrad = -1 * viewrad * Mathf.Rad2Deg;

            //�����̕����Ɍ�����ς���
            transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1 * viewrad);
            oldepos = transform.position;
        }
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
            if (dis <= RangeVigilant && dis > AfterRangeDanger)
            {
                AfterRangeDanger += AddDangerArea * Time.fixedDeltaTime;
                VigTime = 0;
                DanTime = 0;
                VigPlayer = true;
                oldppos = player.gameObject.transform.position;
            }
            //���퓬���ɐN��
            else if (dis <= AfterRangeDanger)
            {
                VigTime = 0;
                DanTime = 0;
                DanPlayer = true;
                oldppos = player.gameObject.transform.position;
            }
        }
        else
        {
            AfterRangeDanger = RangeDanger;
            SeePlayer = false;
        }
    }


    //UI�ω�
    private void ChangeUI()
    {
        //�x�����ɐN��
        if (VigPlayer && SeePlayer == true)
        {
            UIscript.AddAlertness(Time.fixedDeltaTime * AddAlert);
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


    //���R���W�����Ƃ̓����蔻��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Noise")
        {
            isNoise = true;
            NoisePos =other.gameObject.transform.position;
        }
    }
}
