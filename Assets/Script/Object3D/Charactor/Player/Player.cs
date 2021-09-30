using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [HideInInspector]
    public List<PlayerAction> action = new List<PlayerAction>();
    public PlayerHP hP;

    private void Awake() {
        //ƒvƒŒƒCƒ„[‚Ì“®ì‚ğ‚¢‚ê‚é
        action.Add(new PlayerMoveAction());
        action.Add(new PlayerTacleAction());
        action.Add(new PlayerGuruguruAction());
    }

    // Start is called before the first frame update
    private void Start() {
        for (int i = 0; i < action.Count; ++i) {
            action[i].Init(this);
        }
    }

    private void FixedUpdate() {
        for(int i=0; i < action.Count; ++i) {
            action[i].Action();
        }
    }

    public PlayerAction GetAction<T>() {
        for (int i = 0; i < action.Count; ++i) {
            if (typeof(T) == action[i].GetType()) {
                return action[i];
            }
        }
        return null;
    }
}
