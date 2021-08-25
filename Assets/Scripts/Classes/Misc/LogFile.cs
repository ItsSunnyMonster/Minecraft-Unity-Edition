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

    /// <summary>
    /// Creates a new LogFile and an instance of the representation class. 
    /// </summary>
    /// <param name="logSubject">The subject of the log eg. PlayFab</param>
    /// <exception cref="InvalidDataException">Initialization of _path failed! </exception>
    public LogFile(string logSubject)
    {
        // /Logs/PlayFab/2021-06-28_20.37.46.2938.log
        _path = Application.persistentDataPath + "/Logs/" + logSubject + "/" + DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss.ffff") + ".log";

        // If log file exists then do nothing
        if (File.Exists(_path)) return;
        
        // Create the intermediate directories
        Directory.CreateDirectory(Path.GetDirectoryName(_path) ?? throw new InvalidDataException("path is null! "));
        
        // Write a start message to the start file
        using var streamWriter = File.CreateText(_path);
        // ================ PlayFab Log START ================
        streamWriter.WriteLine("================ " + logSubject + " Log START ================");
    }

    /// <summary>
    /// Logs a new message. 
    /// </summary>
    /// <param name="mode">The Severity of the log eg. LogMode.Error</param>
    /// <param name="content">The message of the log</param>
    /// <param name="silent">If false then the log is going to be displayed in game as a pop up</param>
    /// <exception cref="ArgumentOutOfRangeException">The passed LogMode is not handled by the function! </exception>
    public void AddLog(LogMode mode, string content, bool silent = true)
    {
        // Read the log file and append the log to it
        using var streamWriter = File.AppendText(_path);
        // (2021-06-29) [ERROR] Failed to log in
        streamWriter.WriteLine("(" + DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss.ffff") + ") [" + mode.ToString().ToUpper() + "] " + content);

        // Log the respective types to the Unity Debug console
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

        // If silent then do nothing
        if (silent) return;
        
        // Get the instance of the Pop up manager
        var pm = PopUpManager.Instance;
        // Switch the respective types and display the log in the form of a pop up
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