using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private List<PlayerAction> action = new List<PlayerAction>();
    public PlayerHP hP;
    public PlayerFuel fuel;
    public PlayerSound sound;

    public GameObject visual;
    public GameObject rope;
    public GameObject[] line=new GameObject[2];
    public GameObject iaiEffect;

    public Vector3 positionBuffer;
    public Vector3 moveSpeed;

    
    private void Awake() {
        action.Add(new PlayerReflectionAction());
        action.Add(new PlayerMoveAction());
     
        
    }

    // Start is called before the first frame update
    private void Start() {
        for (int i = 0; i < action.Count; ++i) {
            action[i].Init(this);
        }
    }

    private void Update() {
        for (int i = 0; i < action.Count; ++i) {
            action[i].UpdateAction();
        }
    }

    private void FixedUpdate() {
        positionBuffer = transform.position;

        for (int i=0; i < action.Count; ++i) {
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


    public T GetAction<T>() where T : PlayerAction {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                return (T)(object)action[i];
            }
        }
        return (T)(object)null;
    }

    public void ChangeAction<T, C>() where C : PlayerAction, new() {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                action[i]=new C();
                action[i].Init(this);
            }
        }
    }
}
