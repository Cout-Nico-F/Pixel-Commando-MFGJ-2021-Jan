using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    private Slider masterSlider;

    private void Awake()
    {
        masterSlider = this.gameObject.GetComponent<Slider>();
    }
    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master_Volume_Value", 0.5f);
    }
    public void Master_Volume(float sliderValue)
    {
        mixer.SetFloat("Master_Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Master_Volume_Value", sliderValue);
    }

}

