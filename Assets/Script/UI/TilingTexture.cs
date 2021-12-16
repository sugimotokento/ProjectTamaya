using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TilingTexture : MonoBehaviour {
    [SerializeField] private Vector2 baseScale=new Vector2(10,10);
    private Renderer renderer;

    // Start is called before the first frame update
    void Start() {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        Vector2 tiling;
        tiling.x = transform.lossyScale.x / baseScale.x;
        tiling.y = transform.lossyScale.y / baseScale.y;

        renderer.material.SetTextureScale("_MainTex", tiling);
    }
}
