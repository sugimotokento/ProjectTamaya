using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemButton : MonoBehaviour {
    [SerializeField] private PlayerItem playerItem;
    public void OnClick() {
        playerItem.UseItem();
    }
}
