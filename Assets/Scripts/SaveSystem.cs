using UnityEngine;
using System.IO;

public class SaveSystem
{
    public static readonly string Save_Folder = Application.dataPath + "/Saves/";

    /// <summary>
    /// Creating Save folder to saving game data
    /// </summary>
    public static void Initialize()
    {
        if (!Directory.Exists(Save_Folder))
        {
            Directory.CreateDirectory(Save_Folder);
        }
    }

    public static void SaveInventory(string saveString)
    {
        File.WriteAllText(Save_Folder + "inventory.json", saveString);
    }

    public static string LoadInventory()
    {
        if (File.Exists(Save_Folder + "inventory.json"))
        {
            string saveString = File.ReadAllText(Save_Folder + "inventory.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }



    public static void DeleteAllData() 
    {
        if (File.Exists(Save_Folder + "save.json"))
        {
            File.Delete(Save_Folder + "save.json");
        }
    }

}
