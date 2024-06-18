using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private GameObject progressObject;
    [SerializeField] private Image progressImage;
    
    private IProgressBar progressBar;

    private void Start() {
        progressBar = progressObject.GetComponent<IProgressBar>();
        if (progressBar == null) {
            Debug.LogError(progressBar + "没有IProgressBar组件!");
        }

        progressBar.OnProgressChanged += CuttingProgress;
        progressImage.fillAmount = 0f;
        Hide();
    }

    private void CuttingProgress(object sender, IProgressBar.OnProgressChangedEventArgs e){
        progressImage.fillAmount = e.progressNormalized;

        if (progressImage.fillAmount <= 0 || progressImage.fillAmount >= 1) {
            Hide();
            progressImage.fillAmount = 0f;
        }
        else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
