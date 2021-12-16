using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SESlider;

    // BGM—p
    public void  SetBGMValue(float val) { BGMSlider.value = val; }
    public float GetBGMValue() { return BGMSlider.value; }
    // SE—p
    public void  SetSEValue(float val) { BGMSlider.value = val; }
    public float GetSEValue() { return BGMSlider.value; }

}
