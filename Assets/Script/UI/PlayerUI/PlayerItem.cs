using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour {

    public enum PlayerItemIndex {
        ONIGIRI,
        SIOYAKI,
        MESI2,
        MAX
    }


    [SerializeField] private Player player;
    [SerializeField] private Text keyText;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Image itemImage;

    private List<PlayerItemIndex> itemList = new List<PlayerItemIndex>();

    private int key = 100;

    // Start is called before the first frame update
    void Start() {
        itemList.Add(PlayerItemIndex.ONIGIRI);
    }

    // Update is called once per frame
    void Update() {
        keyText.text = "✖" + key.ToString();

        if (itemList.Count > 0) {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite[(int)itemList[0]];
        } else {
            itemImage.gameObject.SetActive(false);
        }
    }


    public void UseItem() {
        if (itemList.Count > 0) {
            itemList.RemoveAt(0);
            player.hP.Heal();
            player.sound.PlayShot(PlayerSound.SoundIndex.EAT_GOHAN);
        }
    }



    public int GetKey() {
        return key;
    }
    public void AddKey(int add = 1) {
        player.sound.PlayShot(PlayerSound.SoundIndex.GET_KEY);
        key += add;
    }
    public void AddItem(PlayerItemIndex item) {
        player.sound.PlayShot(PlayerSound.SoundIndex.GOHAN);
        itemList.Add(item);
    }
    public void UseKey(int num = 1) {
        player.sound.PlayShot(PlayerSound.SoundIndex.UNLOCK);
        key -= num;
    }
}
