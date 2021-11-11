using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;//�e�I�u�W�F�N�g
    [SerializeField] private GameObject player;//�v���C���[�I�u�W�F�N�g
    [SerializeField] private GameObject enemyUI;//�G�l�~�[��UI�I�u�W�F�N�g
    [SerializeField] private GameObject[] MovePos;//�G�l�~�[�̈ړ��p���W

    //private NavMeshAgent navAgent;

    private EnemyUI UIscript;//UI�I�u�W�F�N�g�ɂ��Ă�X�N���v�g

    private Vector3 vec3 = new Vector3(0, 90, 0);//�e���ˎ��̃x�N�g��

    private float   rad;//�G�l�~�[�ƃv���C���[�Ԃ̊p�x
    private Vector3 Epos, Ppos;//�G�l�~�[�A�v���C���[�̍��W

    private float   B_cnt;   //�e�̔��ˊԊu�i�b���j
    private Vector3 Bpos;    //�e�̔��ˍ��W
    private float   B_rad;   //���ˎ��̃G�l�~�[�A�v���C���[�Ԃ̊p�x
    private float   r = 2.0f;//���̔��ˈʒu�̋���

    [SerializeField] private float viewVigilant = 5.0f;//�x�� ����̋���
    [SerializeField] private float viewDanger = 1.0f;  //���퓬 ����̋���

    private float viewrad = 0;  //�G�l�~�[�̎����p�x
    private float viewrange = 15; //�G�l�~�[�̎���̍L��

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
        //���̐ݒ�
        {
            //���̔��˕���
            Epos.x = transform.position.x;
            Epos.y = transform.position.y;
            Ppos.x = player.transform.position.x;
            Ppos.y = player.transform.position.y;

            B_rad = Mathf.Atan2(Ppos.y - Epos.y, Ppos.x - Epos.x);
            vec3.x = -1 * B_rad * Mathf.Rad2Deg;

            //���ˊԊu�p�J�E���g
            B_cnt += Time.fixedDeltaTime;

            //���̔��ˈʒu
            Bpos = transform.position;
            Bpos.x = Bpos.x + r * Mathf.Cos(B_rad);
            Bpos.y = Bpos.y + r * Mathf.Sin(B_rad);
        }
        //�p�x��-180~180�܂�
        //Debug.Log(Mathf.Repeat(-vec3.x, 360));

        //���̔���
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

    //�G�l�~�[�̎����Ɛg�̂̌���
    private void ViewEnemy()
    {
        //�����̕����Ɍ�����ς���
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, viewrad);
    }

    //�v���C���[������ɓ���������UI�ω�
    private void SeeingPlayer()
    {
        float dis = Vector3.Distance(transform.position, player.transform.position);
        float range = transform.localEulerAngles.z;//�p�x��0~360�܂�
        float range_min = range - viewrange;//����̉���
        float range_max = range + viewrange;//����̏��
        bool is_player = false;//����Ƀv���C���[�������Ă��邩�̃t���O
        float playerrad = Mathf.Repeat(-vec3.x, 360);//�v���C���[�ꎞ�ۑ��p�x

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

        //�x�����ɐN��
        if (dis <= viewVigilant && dis > viewDanger && is_player == true)
        {
            UIscript.SetAlertness(Time.deltaTime * 5);
        }
        //���퓬���ɐN��
        if (dis <= viewDanger && is_player == true)
        {
            UIscript.SetDanger();
        }
    }
}
