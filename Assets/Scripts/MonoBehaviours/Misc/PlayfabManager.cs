//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace MonoBehaviours.Misc
{
    public class PlayfabManager : MonoBehaviour
    {
        public static PlayfabManager Instance { get; private set; }

        public static bool LoggedIn = false;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            Login();
        }

        private void Login()
        {
            Debug.Log("Trying to login...");
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, _ =>
            {
                Debug.Log("Login Successful! ");
                LoggedIn = true;
            }, OnError);
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError(error.Error + ": " + error.ErrorMessage + "\n" + 
                           error.GenerateErrorReport());
        }

        public void GetTitleData(string dataName, Action<string> callback)
        {
            Debug.Log("Trying to get title data \"" + dataName + "\"...");
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), result =>
            {
                if (result.Data == null || !result.Data.ContainsKey(dataName))
                {
                    throw new ArgumentOutOfRangeException(nameof(dataName), "\"" + dataName + "\" is not found in title data! ");
                }
                Debug.Log("Get title data \"" + dataName + "\" successful! ");
                callback(result.Data[dataName]);
            }, OnError);
        }
    }
}