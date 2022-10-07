using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public Image fade;

    public GameObject rulesPanel;

    private void Start()
    {
        titleText.rectTransform.DOLocalMoveY(titleText.rectTransform.localPosition.y + 15, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        fade.enabled = true;
        fade.DOFade(0f, 1f).OnComplete(() => fade.enabled = false);
    }

    public void OnClickPlay()
    {
        fade.enabled = true;
        fade.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene(1));
    }

    public void OnClickRules()
    {
        fade.enabled = true;
        fade.DOFade(1f, .5f).OnComplete(() => FadeInRules());
    }

    void FadeInRules()
    {
        rulesPanel.SetActive(true);
        fade.DOFade(0f, .5f).OnComplete(() => fade.enabled = false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickBack()
    {
        fade.enabled = true;
        fade.DOFade(1f, .5f).OnComplete(() => FadeOutRules());
    }

    public void FadeOutRules()
    {
        rulesPanel.SetActive(false);
        fade.DOFade(0f, .5f).OnComplete(() => fade.enabled = false);
    }
}
