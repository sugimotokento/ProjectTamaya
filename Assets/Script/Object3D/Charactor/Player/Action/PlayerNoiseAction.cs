using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoiseAction : PlayerAction {
    public float noiseLate = 1;


    public override void CollisionEnter(Collision collision) {
        //ÉmÉCÉYê∂ê¨
        GameObject obj = Instantiate(player.noise, player.transform.position, Quaternion.identity);
        Noise noiseScript = obj.GetComponent<Noise>();
        noiseScript.SetDestroy(0.5f);
        noiseScript.SetRadius(player.moveSpeed.magnitude*noiseLate);
        noiseLate = 1;
    }
}
