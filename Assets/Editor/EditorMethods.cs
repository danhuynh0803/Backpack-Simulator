using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorMethods : Editor
{
    const string extension = ".cs";
    public MapTile MapTile;

    public static void WriteToEnum<T>(string path, string name, ICollection<T> data)
    {
        using (StreamWriter file = File.CreateText(path + name + extension))
        {
            file.WriteLine("public enum " + name + " \n{");
            int i = 0;
            string[] enums = new string[data.Count];

            foreach (var line in data)
            {
                string lineRep = line.ToString().Replace(" ", string.Empty);
                enums[i] = lineRep;
                i++;
            }
            Array.Sort(enums);
            int j = 0;
            foreach (var line in enums)
            {
                file.WriteLine(string.Format("\t{0} = {1},",
                    line, j));
                j++;
            }

            file.WriteLine("\n}");
        }
        AssetDatabase.ImportAsset(path + name + extension);
    }
}