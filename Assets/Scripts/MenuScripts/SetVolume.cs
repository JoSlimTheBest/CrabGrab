using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    private bool silenceActive = false;
    private float current;

    public Image imageSilnce;
    public Sprite silence;
    public Sprite noS;

    public bool music;
    

    public void Awake()
    {
      //  float sliderV;
      //  sliderV = GetMasterLevel();
      //  mixer.SetFloat("MusicVol", sliderV);

      //  GetComponent<Slider>().value = Mathf.Log10(sliderV) * 20;

    }
  

    public void Start()
    {
        if(music == true)
        {
            if (PlayerPrefs.HasKey("MusicAudio"))
            {
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicAudio");
            }
            else
            {
                GetComponent<Slider>().value = 0.25f;
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("SFXAudio"))
            {
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXAudio");
            }
            else
            {
                GetComponent<Slider>().value = 0.5f;
            }
        }
       

        SetLevel(GetComponent<Slider>().value);
    }

    public float GetMasterLevel()
    {
        float value;
        bool result = mixer.GetFloat("MusicVol", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue)*20);
        if(music == true)
        {
            PlayerPrefs.SetFloat("MusicAudio", sliderValue);
        }
        else
        {
            PlayerPrefs.SetFloat("SFXAudio", sliderValue);
        }
        if(sliderValue > 0.0001f)
        {
            silenceActive = false;
        }
        else
        {
            silenceActive = true;
        }
        CheckImage();
    }


    public void SetSilence()
    {
        if(silenceActive == false)
        {
            current = GetComponent<Slider>().value;
            GetComponent<Slider>().value = 0.0001f;
            silenceActive = true;
        }
        else
        {
            GetComponent<Slider>().value = current;
            silenceActive = false;
        }

        CheckImage();
    }

    public void CheckImage()
    {
        if(silenceActive == true)
        {
            imageSilnce.sprite = silence;

        }
        else
        {
            imageSilnce.sprite = noS;
        }
    }
}
