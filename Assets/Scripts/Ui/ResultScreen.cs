﻿using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.SceneManagment;
using System;

namespace Assets.Scripts.Ui
{
    public class ResultScreen : UiScreenBase
    {
        const string NEXT_REWARD_TIME_KEY = "NextRewardIndex";
        const string FIRST_TIME_OPENED_KEY = "FirstTime";

        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button claimRewardButton;
        [Space]
        [SerializeField] private Text playerScoreText;
        [SerializeField] private Text highScore;
        [SerializeField] private Text coins;
        [Space]
        [SerializeField] private GameObject dailyRewardDisplay;
        [SerializeField] private GameObject rewardScreen;

        private ScoreManager scoreManager;

        // every "nextRewardDelay" player gets a reward
        private double nextRewardDelay = 24f;

        private void Awake()
        {
            scoreManager = GameManager.Instance.ScoreManager;
        }


        private void OnEnable()
        {
            

            DisplayHighScore();
            DisplayPlayerScore(scoreManager.Score);

            playAgainButton.onClick.AddListener(OnPlayAgainButtonClick);


            CheckForDailyReward();
        }

        private void OnDisable()
        {
            playAgainButton.onClick.RemoveListener(OnPlayAgainButtonClick);
        }


        void OnPlayAgainButtonClick()
        {
            // reload first scene/main manu scene
            SceneManager.LoadScene((int)SceneIndexes.LOADING_SCREEN);
        }

        void DisplayPlayerScore(int score)
        {
            playerScoreText.text = $"Your Score is {score}";
        }

        void DisplayHighScore()
        {
            highScore.text = $"HighScore {PlayerPrefs.GetInt(ScoreManager.HIGH_SCORE_KEY)}";
        }

        void CheckForDailyReward()
        {
            //this method will give the player a reward every 24 hours

            //caching "FirstTime" the player plays the game so he recieves the first reward at the start
            PlayerPrefs.GetInt(FIRST_TIME_OPENED_KEY, 0);

            if (string.IsNullOrEmpty(PlayerPrefs.GetString(NEXT_REWARD_TIME_KEY)))
                PlayerPrefs.SetString(NEXT_REWARD_TIME_KEY, DateTime.Now.ToString());

            DateTime currentDateTime = DateTime.Now;
            DateTime rewardDateTime = DateTime.Parse(PlayerPrefs.GetString(NEXT_REWARD_TIME_KEY, currentDateTime.ToString()));

            double elapsedTime = (currentDateTime - rewardDateTime).TotalHours;
            Debug.Log(elapsedTime);

            if (elapsedTime >= nextRewardDelay || PlayerPrefs.GetInt(FIRST_TIME_OPENED_KEY).Equals(0))
            {
                ActivateDailyReward();
            }
        }

        void ActivateDailyReward()
        {
            PlayerPrefs.DeleteKey(NEXT_REWARD_TIME_KEY);
            
            dailyRewardDisplay.SetActive(true);

            void DeactivateRewardScreen()
            {
                rewardScreen.SetActive(false);
            }

            void ActivateRewardScreen()
            {
                dailyRewardDisplay.SetActive(false);
                rewardScreen.SetActive(true);
                GameEvents.Instance.CollectReward(coins);
                this.Wait(2f, DeactivateRewardScreen);
            }

            claimRewardButton.onClick.AddListener(ActivateRewardScreen);

            PlayerPrefs.SetInt(FIRST_TIME_OPENED_KEY, 1);
        }
    }
}