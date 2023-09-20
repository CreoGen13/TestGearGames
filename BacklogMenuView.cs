using System;
using DG.Tweening;
using Infrastructure.Services;
using Scriptables;
using Scriptables.Settings;
using UI.BaseUI;
using UI.HUD.PauseMenu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.HUD.BacklogMenu
{
    public class BacklogMenuView : MenuView<BacklogMenuPresenter>
    {
        [Header("Buttons")]
        [SerializeField] private Button buttonExit;

        private ScriptableUiSettings _uiSettings;
        private SoundService _soundService;
        
        [Inject]
        private void Construct(BacklogMenuPresenter backlogMenuPresenter, ScriptableUiSettings uiSettings, SoundService soundService)
        {
            _soundService = soundService;
            _uiSettings = uiSettings;
            Presenter = backlogMenuPresenter;

            InitButtons();
        }

        protected sealed override void InitButtons()
        {
            buttonExit.onClick.AddListener(() =>
            {
                _soundService.PlayButtonSound();
                
                Presenter.OnClickButtonExit();
            });
        }
        public void SetButtonsInteractable(bool interactable)
        {
            buttonExit.interactable = interactable;
        }
        public override void OpenMenu(Action onComplete = null)
        {
            _soundService.PlayMenuSound(MenuSound.SmallTreeIntro);
            
            windowTransform.gameObject.SetActive(true);
            windowTransform.anchoredPosition = new Vector2(windowTransform.rect.width, 0);
            background.color = new Color(0, 0, 0, 0);
            
            Sequence = DOTween.Sequence();
            Sequence.Join(windowTransform.DOAnchorPos(
                Vector2.zero,
                _uiSettings.uiAnimationDuration));
            Sequence.Join(background.DOColor(
                _uiSettings.backgroundFadeColor, 
                _uiSettings.uiAnimationDuration));
            Sequence.OnComplete(() =>
            {
                OnMenuOpened();
                onComplete?.Invoke();
            });
            Sequence.Play();
        }

        public override void CloseMenu(Action onComplete = null)
        {
            _soundService.PlayMenuSound(MenuSound.SmallTreeOutro);

            windowTransform.anchoredPosition = Vector2.zero;
            background.color = _uiSettings.backgroundFadeColor;
            
            Sequence = DOTween.Sequence();
            Sequence.Join(windowTransform.DOAnchorPos(
                new Vector2(windowTransform.rect.width, 0),
                _uiSettings.uiAnimationDuration));
            Sequence.Join(background.DOColor(
                new Color(0, 0, 0, 0),
                _uiSettings.uiAnimationDuration));
            Sequence.OnComplete(() =>
            {
                windowTransform.gameObject.SetActive(false);
                OnMenuClosed();
                onComplete?.Invoke();
            });
            Sequence.Play();
        }

        public override void OpenMenuInstant()
        {
            windowTransform.anchoredPosition = Vector2.zero;
            windowTransform.gameObject.SetActive(true);
            background.color = _uiSettings.backgroundFadeColor;
        }

        public override void CloseMenuInstant()
        {
            windowTransform.anchoredPosition = new Vector2(windowTransform.rect.width, 0);
            windowTransform.gameObject.SetActive(false);
            background.color = new Color(0, 0, 0, 0);
        }
    }
}