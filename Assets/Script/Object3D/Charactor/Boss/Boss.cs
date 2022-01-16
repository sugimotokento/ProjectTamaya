using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private BossAction action;

    public GameObject bullet;
    public GameObject mazzle;
    public GameObject line;

    public Animator animator;
    public Renderer[] renderer = new Renderer[4];
    public BossSound sound;

    [HideInInspector] public int hp = 5;
    [HideInInspector] public bool isDamage = false;

    private bool isSetDieAction = false;
    // Start is called before the first frame update
    void Start() {
     //   SetDefaultAction();
    }

    // Update is called once per frame
    void Update() {

    }
    
    private void FixedUpdate() {
        action.Action();

        if (hp <= 0&&isSetDieAction==false) {
            SetAction<BossDieAction>();
            isSetDieAction = true;
        }
    }

    public void Damage() {
        if (CheckAction<BossDamageAction>() == false) {
            hp -= 1;
            isDamage = true;
        }
    }
    public void SetDefaultAction() {
        SetAction<BossStartIdleAction>();
    }
    public void SetActionNone() {
        SetAction<BossAction>();
    }
    public T GetAction<T>() where T : BossAction {
        return (T)(object)action;
    }
    public bool CheckAction<T>() where T : BossAction {
        if (typeof(T) == action.GetType()) {
            return true;
        } else {
            return false;
        }
    }

    public void SetAction<T>() where T : BossAction, new() {
        action = new T();
        action.Init(this);
    }
}

