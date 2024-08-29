using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CSVReader
{
    public Dictionary<string,List<object>> CSVReade(string path)
    {
        string[] lines;

        Dictionary<string, List<object>> DataDictionary = new Dictionary<string, List<object>>();
        
        TextAsset csvData = Resources.Load<TextAsset>(path);

        lines = csvData.ToString().Split('\n');
        
        if (lines.Length<=1)
        {
            return null;
        }

        string[] head = lines[0].Split(',');
        for(int i =0; i < head.Length; i++)
        {
            DataDictionary.Add(head[i],new List<object>());
        }
        for(int i = 1;  i < lines.Length; i++)
        {
            var value = lines[i].Split(',');
            for(int j =0; j < head.Length; j++)
            {
                DataDictionary[head[j]].Add(value[j]);
            }
        }

        foreach(string key in DataDictionary.Keys)
        {
            Debug.Log($"Key: {key}, Values: {string.Join(", ", DataDictionary[key])}");
        }

        return DataDictionary;
    }
}
