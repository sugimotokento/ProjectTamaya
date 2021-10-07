using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [HideInInspector]
    public List<PlayerAction> action = new List<PlayerAction>();
    public PlayerHP hP;
    

    public Vector3 positionBuffer;

    private void Awake() {
        //ÉvÉåÉCÉÑÅ[ÇÃìÆçÏÇÇ¢ÇÍÇÈ
        action.Add(new PlayerMoveAction());
        action.Add(new PlayerTacleAction());
        action.Add(new PlayerGuruguruAction());
        action.Add(new PlayerReflectionAction());
        action.Add(new PlayerIaiAction());
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

    public T GetAction<T>() {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                return (T)(object)action[i];
            }
        }
        return (T)(object)null;
    }
}
