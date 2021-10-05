//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class VersionChecker : MonoBehaviour
{
    public GameObject versionNoticePanel;
    public TextMeshProUGUI noticeBody;

    private static List<string> _ignoredVersions = new List<string>();
    private string _latestVersion;

    private void Awake()
    {
        // Check if file exists
        if (!File.Exists(Application.persistentDataPath + "/Data/ignored versions.mcd")) return;
        
        // Create binary formatter
        var formatter = new BinaryFormatter();
            
        // Open file stream
        var stream = new FileStream(Application.persistentDataPath + "/Data/ignored versions.mcd", FileMode.Open);

        // Get the list from file
        var ignoredVersionsFromFile = (List<string>) formatter.Deserialize(stream);

        // Set the list
        _ignoredVersions = ignoredVersionsFromFile;
            
        // Close file stream
        stream.Close();
    }

    private IEnumerator Start()
    {
        if (Application.version.Contains("_internal") || Application.version.Contains("_dev"))
        {
            yield break;
        }
        
        yield return new WaitUntil(() => PlayfabManager.LoggedIn);

        PlayfabManager.Instance.GetTitleData("Latest Version", data =>
        {
            // Checks
            if (data == null) return;
            if (data == Application.version) return;
            
            //Set variable
            _latestVersion = data;
            
            // Open Notice Panel
            versionNoticePanel.SetActive(true);

            // Set Text
            var noticeText = "New Update Available! \n" +
                             "Current version: " + Application.version + "\n" +
                             "New version: " + _latestVersion;
            noticeBody.text = noticeText;
        });
    }

    /// <summary>
    /// Adds the latest version to the ignored update list
    /// </summary>
    public void IgnoreUpdate()
    {
        _ignoredVersions.Add(_latestVersion);
        ClosePanel();

        // Create formatter
        var formatter = new BinaryFormatter();
        
        // Create data directory
        Directory.CreateDirectory(Application.persistentDataPath + "/Data/");
        
        // Open file stream
        var stream = new FileStream(Application.persistentDataPath + "/Data/ignored versions.mcd", FileMode.OpenOrCreate);
        
        // Serialize the list
        formatter.Serialize(stream, _ignoredVersions);
        
        // Close file stream
        stream.Close();
    }

    public void DownloadUpdate()
    {
        Application.OpenURL("https://github.com/SunnyMonster123/Minecraft-clone/releases/tag/" + _latestVersion);
        ClosePanel();
    }

    public void ClosePanel()
    {
        versionNoticePanel.SetActive(false);
    }
}
