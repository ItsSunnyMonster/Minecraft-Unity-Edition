//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using System.IO;
using UnityEngine;

public class LogFile
{
    //Application.persistentDataPath + "/Logs/(" + logSubject + ") " + DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss.ffff") + ".log"

    private readonly string _path;

    public LogFile(string logSubject)
    {
        // /Logs/PlayFab/2021-06-28_20.37.46.2938.log
        _path = Application.persistentDataPath + "/Logs/" + logSubject + "/" + DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss.ffff") + ".log";

        if (!File.Exists(_path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path) ?? throw new InvalidDataException("path is null! "));
            using var streamWriter = File.CreateText(_path);    
            // ================ PlayFab Log START ================
            streamWriter.WriteLine("================ " + logSubject + " Log START ================");
        }
    }

    public void AddLog(LogMode mode, string content, bool silent = true)
    {
        using var streamWriter = File.AppendText(_path);
        // (2021-06-29) [ERROR] Failed to log in
        streamWriter.WriteLine("(" + DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss.ffff") + ") [" + mode.ToString().ToUpper() + "] " + content);

        switch (mode)
        {
            case LogMode.Success:
                Debug.Log("[SUCCESS] " + content);
                break;
            case LogMode.Info:
                Debug.Log(content);
                break;
            case LogMode.Error:
                Debug.LogError(content);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "LogMode not handled in switch statement! ");
        }

        if (silent) return;
        var pm = PopUpManager.Instance;
        switch (mode)
        {
            case LogMode.Success:
                pm.PopUp("Check Mark", "Success", content);
                break;
            case LogMode.Info:
                pm.PopUp(iconID: "Info", content: content);
                break;
            case LogMode.Error:
                pm.PopUp("Red Cross", "Error", content);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }
}

public enum LogMode
{
    Success,
    Info,
    Error
}