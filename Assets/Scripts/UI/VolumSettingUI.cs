using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumSettingUI : MonoBehaviour
{  
    [SerializeField] TextMeshProUGUI BGMVolumText;
    [SerializeField] TextMeshProUGUI effectVolumText;
    [SerializeField] Scrollbar BGMScrollbar;
    [SerializeField] Scrollbar effectScrollbar;
    [SerializeField] Button closeButton;

    [SerializeField] TextMeshProUGUI moveUpButtonText;
    [SerializeField] TextMeshProUGUI moveDownButtonText;
    [SerializeField] TextMeshProUGUI moveLeftButtonText;
    [SerializeField] TextMeshProUGUI moveRightButtonText;
    [SerializeField] TextMeshProUGUI interactButtonText;
    [SerializeField] TextMeshProUGUI alterInteractButtonText;

    [SerializeField] Button moveUpButton;
    [SerializeField] Button moveDownButton;
    [SerializeField] Button moveLeftButton;
    [SerializeField] Button moveRightButton;
    [SerializeField] Button interactButton;
    [SerializeField] Button alterInteractButton;

    [SerializeField] Transform pressToReBindKeyTransfrom;

    public static VolumSettingUI Instance;

    private string BGM_VOLUM = "BGMVolum";
    private string EFFECT_VOLUM = "EffectVolum";

    private void Awake() {
        if (Instance != null) {
            throw new Exception("VolumSettingUI存在多个Instance!");
        }     
        Instance = this;

        BGMScrollbar.value = PlayerPrefs.GetFloat(BGM_VOLUM, 0.5f);
        effectScrollbar.value = PlayerPrefs.GetFloat(EFFECT_VOLUM, 0.5f);

        BGMVolumText.text = "BGM Volum: " + Math.Round(BGMScrollbar.value * 10);
        effectVolumText.text = "BGM Volum: " + Math.Round(effectScrollbar.value * 10);

        moveUpButton.onClick.AddListener(() => {
            Rebind(EnhancedInput.Binding.Move_Up);
        });

        moveDownButton.onClick.AddListener(() => {
            Rebind(EnhancedInput.Binding.Move_Down);
        });

        moveLeftButton.onClick.AddListener(() => {
            Rebind(EnhancedInput.Binding.Move_Left);
        });

        moveRightButton.onClick.AddListener(() => {
            Rebind(EnhancedInput.Binding.Move_Right);
        });

        interactButton.onClick.AddListener(() => {
            Rebind(EnhancedInput.Binding.Interact);
        });

        alterInteractButton.onClick.AddListener(() => {
            Rebind(EnhancedInput.Binding.AlterInteract);
        });


        
    }
    
    private void Start() {
        BGMScrollbar.onValueChanged.AddListener(BGMScrollbarEvent);
        effectScrollbar.onValueChanged.AddListener(EffectScrollbarEvent);
        closeButton.onClick.AddListener(Hide);
        EnhancedInput.Instance.OnPause += OnPauseEvent;

        UpdateVisual();
        Hide();
        HidePressToReBindKeyTransfrom();

    }

    private void BGMScrollbarEvent(float vol) {
        SoundManager.Instance.SetBGMVolume(vol);
        BGMVolumText.text = "BGM Volum: " + Math.Round(vol * 10);

        PlayerPrefs.SetFloat(BGM_VOLUM, vol);
    }

    private void EffectScrollbarEvent(float vol) {
        VoiceManager.Instance.SetVolume(vol);
        effectVolumText.text = "Effect Volum: " + Math.Round(vol * 10);

        PlayerPrefs.SetFloat(EFFECT_VOLUM, vol);
    }

    private void OnPauseEvent(object sender, EventArgs e) {
        Hide();
    }
    
    private void UpdateVisual() {
        moveUpButtonText.text = EnhancedInput.Instance.ShowBinding(EnhancedInput.Binding.Move_Up);
        moveDownButtonText.text = EnhancedInput.Instance.ShowBinding(EnhancedInput.Binding.Move_Down);
        moveLeftButtonText.text = EnhancedInput.Instance.ShowBinding(EnhancedInput.Binding.Move_Left);
        moveRightButtonText.text = EnhancedInput.Instance.ShowBinding(EnhancedInput.Binding.Move_Right);
        interactButtonText.text = EnhancedInput.Instance.ShowBinding(EnhancedInput.Binding.Interact);
        alterInteractButtonText.text = EnhancedInput.Instance.ShowBinding(EnhancedInput.Binding.AlterInteract);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowPressToReBindKeyTransfrom() {
        pressToReBindKeyTransfrom.gameObject.SetActive(true);
    }

    private void HidePressToReBindKeyTransfrom() {
        pressToReBindKeyTransfrom.gameObject.SetActive(false);
    }

    private void Rebind(EnhancedInput.Binding binding) {
        ShowPressToReBindKeyTransfrom();
        EnhancedInput.Instance.RebindKey(binding, () => {
            HidePressToReBindKeyTransfrom();
            UpdateVisual();
        });
    }
}
