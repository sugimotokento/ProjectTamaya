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
    public GameObject rope;
    public GameObject[] line = new GameObject[2];
    public GameObject iaiEffect;
    public GameObject noise;
    public GameObject afterimage;

    [HideInInspector] public Vector3 positionBuffer;
    [HideInInspector] public Vector3 moveSpeed;


    public void SetDefaultAction() {
        
        AddAction<PlayerReflectionAction>();
        AddAction<PlayerMoveAction>();
        AddAction<PlayerDamageAnimationAction>();
        AddAction<PlayerNoiseAction>();
    }
    public void SetActiveUI(bool flag) {
        hP.gameObject.SetActive(flag);
        fuel.gameObject.SetActive(flag);
        item.gameObject.SetActive(flag);
        air.gameObject.SetActive(flag);
    }
    private void Die() {
        if (hP != null && hP.GetHp() <= 0) {
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
    private void OnTriggerEnter(Collider other) {
        for (int i = 0; i < action.Count; ++i) {
            action[i].TriggerEnter(other);
        }
    }
    public void AddAction<T>() where T : PlayerAction, new() {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                //既に存在する場合追加しない
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
