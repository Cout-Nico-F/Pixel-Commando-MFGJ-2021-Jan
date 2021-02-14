using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SFB;
using System.Runtime.InteropServices;

public static class FileManager
{
    public static string newPath;
    public static string[] loadPath;
    public static string loadPathPro;

    public static void CreateNewFile(string a_FileName)
    {
        newPath = StandaloneFileBrowser.SaveFilePanel("Save File", "", a_FileName, "dat");
        newPath.ToString();
    }
    public static void DownloadFile(string a_FileName)
    {
        loadPath = StandaloneFileBrowser.OpenFilePanel("Open File", "", "dat", false);
        
    }

    //Write
    public static bool WriteToFile(string a_FileContents)
    {
        //Local Storage
        File.WriteAllText(newPath, a_FileContents);

        #region Encryption
        StringBuilder outSb = new StringBuilder(a_FileContents.Length);
        int key = 2;
        for (int i = 0; i < a_FileContents.Length; i++)
        {
            char ch = (char)(a_FileContents[i] * key);
            outSb.Append(ch);
        }
        a_FileContents = outSb.ToString();
        #endregion

        try
        {
            //Local Storage
            File.WriteAllText(newPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to write {newPath} with exception {e}");
        }
        return false;
    }

    //Read
    public static bool LoadFromFile(out string json)
    {
        Debug.Log(loadPathPro);
        json = File.ReadAllText(loadPathPro);

        #region DesEncryption
        StringBuilder outSb = new StringBuilder(json.Length);
        int key = 2;
        for (int i = 0; i < json.Length; i++)
        {
            char ch = (char)(json[i] / key);
            outSb.Append(ch);
        }
        json = outSb.ToString();
        File.WriteAllText(loadPathPro, json);
        JsonUtility.FromJsonOverwrite(json, loadPathPro);
        #endregion

        try
        {
            json = File.ReadAllText(loadPathPro);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to write {loadPathPro} with exception {e}");
            json = "";
            return false;
        }
    }

}
