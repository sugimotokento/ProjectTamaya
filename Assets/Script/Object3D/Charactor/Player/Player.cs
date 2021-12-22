using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private List<PlayerAction> action = new List<PlayerAction>();
    public PlayerHP hP;
    public PlayerFuel fuel;
    public PlayerItem item;
    public PlayerAir air;
    public PlayerSound sound;

    public GameObject visual;
   [SerializeField] public GameObject rope;
    public GameObject[] line = new GameObject[2];
    public GameObject iaiEffect;
    public GameObject noise;
    public GameObject afterimage;

    public Renderer[] rendere;

    public Animator animator;

    [HideInInspector] public Vector3 positionBuffer;
    [HideInInspector] public Vector3 moveSpeed;

    private bool canDieSound = true;

    public void SetDefaultAction() {        
        AddAction<PlayerReflectionAction>();
        AddAction<PlayerMoveAction>();
        AddAction<PlayerDamageAnimationAction>();
        AddAction<PlayerNoiseAction>();
        AddAction<PlayerGoalAction>();
    }
    public void SetActiveUI(bool flag) {
        hP.gameObject.SetActive(flag);
        fuel.gameObject.SetActive(flag);
        item.gameObject.SetActive(flag);
        air.gameObject.SetActive(flag);
    }
    private void Die() {
        if (hP != null && hP.GetHp() <= 0) {
            if (canDieSound == true) {
                sound.Play(PlayerSound.SoundIndex.MISS);
                canDieSound = false;
            }
            ReMoveActionAll();
            visual.SetActive(true);
            AddAction<PlayerDieAction>();
        }
    }
    private void Awake() {

    }

    // Start is called before the first frame update
    private void Start() {

    }
    private void Update() {
        for (int i = 0; i < action.Count; ++i) {
            action[i].UpdateAction();
        }
        if (StageManager.instance.isClear == true) {
            visual.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void FixedUpdate() {
        positionBuffer = transform.position;

        Die();
        if (CheckAction<PlayerMoveAction>() == false && CheckAction<PlayerTackleAction>() == false && CheckAction<PlayerIaiAction>() == false) moveSpeed = Vector3.zero;
        for (int i = 0; i < action.Count; ++i) {
            action[i].Action();
        }


    }


    private void OnCollisionEnter(Collision collision) {
        for (int i = 0; i < action.Count; ++i) {
            action[i].CollisionEnter(collision);
        }

    }
    private void OnCollisionStay(Collision collision) {
        for (int i = 0; i < action.Count; ++i) {
            action[i].CollisionStay(collision);
        }
    }
    private void OnTriggerEnter(Collider other) {
        for (int i = 0; i < action.Count; ++i) {
            action[i].TriggerEnter(other);
        }
    }
    public void AddAction<T>() where T : PlayerAction, new() {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                //Šù‚É‘¶Ý‚·‚éê‡’Ç‰Á‚µ‚È‚¢
                return;
            }
        }
        action.Add(new T());
        action[action.Count - 1].Init(this);
    }
    public T GetAction<T>() where T : PlayerAction {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                return (T)(object)action[i];
            }
        }
        return (T)(object)null;
    }
    public bool CheckAction<T>() where T : PlayerAction {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                return true;
            }
        }
        return false;
    }
    public void RemoveAction<T>() where T : PlayerAction {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                action.RemoveAt(i);
            }
        }
    }
    public void ChangeAction<T, C>() where C : PlayerAction, new() {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                action[i] = new C();
                action[i].Init(this);
            }
        }
    }
    public void ReMoveActionAll() {
        for (int i = 0; i < action.Count; ++i) {
            action.RemoveAt(i);
        }
    }
}
