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
        // Create singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create a new log file to log to
        _logFile = new LogFile("PlayFab");
        
        Login();
    }

    /// <summary>
    /// Makes a new PlayFabClientAPI call to login with custom ID
    /// </summary>
    private void Login()
    {
        _logFile.AddLog(LogMode.Info, "Trying to login...");
        // Request to login with custom id
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, _ =>
        {
            _logFile.AddLog(LogMode.Success, "Login Successful! ");
            LoggedIn = true;
        }, OnError);
    }

    /// <summary>
    /// Logs a new error based on <paramref name="error"/>
    /// </summary>
    /// <param name="error">The error to log</param>
    private void OnError(PlayFabError error)
    {
        _logFile.AddLog(LogMode.Error,
            error.Error + ": " + error.ErrorMessage + "\n" + error.GenerateErrorReport(),
            false);
    }

    /// <summary>
    /// Gets the value of a title data
    /// </summary>
    /// <param name="dataName">The name of the title data to get</param>
    /// <param name="callback">The function called when received data. Receives a string as the data. </param>
    /// <exception cref="ArgumentOutOfRangeException">The requested data name is not found in title data. </exception>
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
            _logFile.AddLog(LogMode.Success, "Get title data \"" + dataName + "\" successful! ");
            callback(result.Data[dataName]);
        }, OnError);
    }
}