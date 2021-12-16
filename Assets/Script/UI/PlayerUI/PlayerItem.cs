using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour {
    [SerializeField] private Player player;
    [SerializeField] private Text keyText;
    [SerializeField] private Sprite[] sprite;

    private int key = 100;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        keyText.text = "✖" + key.ToString();
    }


    public void UseItem() {

    }



    public int GetKey() {
        return key;
    }
    public void AddKey(int add = 1) {
        player.sound.PlayShot(PlayerSound.SoundIndex.GET_KEY);
        key += add;
    }
    public void AddItem() {
        int rand = Random.Range(0, sprite.Length);
    }
    public void UseKey(int num = 1) {
        player.sound.PlayShot(PlayerSound.SoundIndex.UNLOCK);
        key -= num;
    }
}
