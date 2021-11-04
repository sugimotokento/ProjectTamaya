using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private List<PlayerAction> action = new List<PlayerAction>();
    public PlayerHP hP;
    public PlayerFuel fuel;
    public PlayerItem item;
    public PlayerSound sound;

    public GameObject visual;
    public GameObject rope;
    public GameObject[] line = new GameObject[2];
    public GameObject iaiEffect;
    public GameObject noise;

    [HideInInspector] public Vector3 positionBuffer;
    [HideInInspector] public Vector3 moveSpeed;



    private void Awake() {
        AddAction<PlayerReflectionAction>();
        AddAction<PlayerMoveAction>();  
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
}
