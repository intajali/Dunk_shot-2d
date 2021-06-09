using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingScreenView : MonoBehaviour
{
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle vibrationToggle;
    [SerializeField] private Button closeButton;

    private Vector2 animStartPointX = new Vector2(800f, 0f);
    private Vector2 animEndPointX = Vector2.zero;

    private RectTransform rectTransform;


    [SerializeField] GameObject[] soundSpriteObjects;
    [SerializeField] GameObject[] vibrationSpriteObjects;

    private void Awake()
    {
        rectTransform = transform.GetComponent<RectTransform>();
    }


    public void Render()
    {
        SetListeners();
        OnEntryAnim();

        string soundData = DataUpdates.instance.OnRetrive(DataUpdates.SOUND_KEY);
        string vibrationData = DataUpdates.instance.OnRetrive(DataUpdates.VIBRATION_KEY);
        if (soundData.ToLower().Equals("true"))
        {
            soundSpriteObjects[0].SetActive(true);
            soundSpriteObjects[1].SetActive(false);
        }
        else
        {
            soundSpriteObjects[1].SetActive(true);
            soundSpriteObjects[0].SetActive(false);
        }

        if (vibrationData.ToLower().Equals("true"))
        {
            vibrationSpriteObjects[0].SetActive(true);
            vibrationSpriteObjects[1].SetActive(false);
        }
        else
        {
            vibrationSpriteObjects[1].SetActive(true);
            vibrationSpriteObjects[0].SetActive(false);
        }

    }

    private void SetListeners()
    {
        soundToggle.onValueChanged.RemoveAllListeners();
        soundToggle.onValueChanged.AddListener((value) => OnSoundUpdate(value));


        vibrationToggle.onValueChanged.RemoveAllListeners();
        vibrationToggle.onValueChanged.AddListener((value)=> { OnVibrationUpdate(value); });

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(OnPreviosButtonClick);
    }

    private void OnEntryAnim()
    {
        rectTransform.DOAnchorPos(animEndPointX, 0.25f).SetEase(Ease.InOutSine).OnComplete(() => {
            gameObject.SetActive(true);
        });
    }

    private void OnExiteAnim()
    {
         rectTransform.DOAnchorPos(animStartPointX, 0.01f).SetEase(Ease.InOutSine).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }




    private void OnSoundUpdate(bool flag)
    {
        if(flag)
        {
            soundSpriteObjects[0].SetActive(true);
            soundSpriteObjects[1].SetActive(false);
        }
        else
        {
            soundSpriteObjects[1].SetActive(true);
            soundSpriteObjects[0].SetActive(false);
        }
        DataUpdates.instance.OnSave(DataUpdates.SOUND_KEY, flag.ToString());
    }

    private void OnVibrationUpdate(bool flag)
    {
        if (flag)
        {
            vibrationSpriteObjects[0].SetActive(true);
            vibrationSpriteObjects[1].SetActive(false);
        }
        else
        {
            vibrationSpriteObjects[1].SetActive(true);
            vibrationSpriteObjects[0].SetActive(false);
        }

        DataUpdates.instance.OnSave(DataUpdates.VIBRATION_KEY, flag.ToString());
    }

    private void OnPreviosButtonClick()
    {
        OnExiteAnim();
    }

}
