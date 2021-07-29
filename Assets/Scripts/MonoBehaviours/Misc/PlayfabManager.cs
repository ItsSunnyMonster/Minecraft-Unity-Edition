//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager Instance { get; private set; }

    public static bool LoggedIn = false;

    private LogFile _logFile;

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

        _logFile = new LogFile("PlayFab");
        
        Login();
    }

    private void Login()
    {
        _logFile.AddLog(LogMode.Info, "Trying to login...");
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, _ =>
        {
            _logFile.AddLog(LogMode.Success, "Login Successful! ", false);
            LoggedIn = true;
        }, OnError);
    }

    private void OnError(PlayFabError error)
    {
        _logFile.AddLog(LogMode.Error,
            error.Error + ": " + error.ErrorMessage + "\n" + error.GenerateErrorReport(),
            false);
    }

    public void GetTitleData(string dataName, Action<string> callback)
    {
        _logFile.AddLog(LogMode.Info, "Trying to get title data \"" + dataName + "\"...");
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), result =>
        {
            if (result.Data == null || !result.Data.ContainsKey(dataName))
            {
                _logFile.AddLog(LogMode.Error, "\"" + dataName + "\" is not found in title data! ", false);
                throw new ArgumentOutOfRangeException(nameof(dataName), "\"" + dataName + "\" is not found in title data! ");
            }
            _logFile.AddLog(LogMode.Success, "Get title data \"" + dataName + "\" successful! ", false);
            callback(result.Data[dataName]);
        }, OnError);
    }
}