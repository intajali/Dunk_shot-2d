using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseScreenView : MonoBehaviour
{
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonSetting;

    private Vector2 animStartPointX = new Vector2(800f,0f);
    private Vector2 animEndPointX = Vector2.zero;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = transform.GetComponent<RectTransform>();
    }


    public void Render()
    {
        SetListeners();
        OnEntryAnim();
    }

    private void SetListeners()
    {
        buttonResume.onClick.RemoveAllListeners();
        buttonResume.onClick.AddListener(OnClickResume);

        buttonRestart.onClick.RemoveAllListeners();
        buttonRestart.onClick.AddListener(OnClickRestart);

        buttonSetting.onClick.RemoveAllListeners();
        buttonSetting.onClick.AddListener(OnClickSetting);
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


    private void OnClickResume()
    {
        OnExiteAnim();
    }

    private void OnClickRestart()
    {
        OnExiteAnim();
        ScoreHandler.instance.OnClickReplay();
    }

    private void OnClickSetting()
    {
        OnExiteAnim();
        ScoreHandler.instance.settingScreenView.Render();
    }
}
