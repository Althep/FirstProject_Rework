using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;
/*
public class CSVReader
{
    public Dictionary<string,List<object>> CSVReade(string path)
    {
        string[] lines;

        Dictionary<string, List<object>> DataDictionary = new Dictionary<string, List<object>>();
        
        TextAsset csvData = Resources.Load<TextAsset>(path);

        if(csvData == null)
        {
            Debug.Log("CSVDataError");
            return null;
        }
        lines = csvData.ToString().Split('\n');
        
        if (lines.Length<=1)
        {
            return null;
        }

        string[] head = lines[0].Split(',');
        
        for (int i =0; i < head.Length; i++)
        {
            DataDictionary.Add(head[i],new List<object>());
            Debug.Log($"head : {head[i]}");
        }
        Debug.Log($"Last Head {head[head.Length-1]}");
        for(int i = 1;  i < lines.Length; i++)
        {
            var value = lines[i].Split(',').Select(v => v?.Trim() ?? string.Empty).ToArray();
            if (value.Length != head.Length)
            {
                Debug.LogWarning($"Line {i + 1} length does not match header length.");
            }
            for (int j =0; j < head.Length; j++)
            {
                
                DataDictionary[head[j]].Add(value[j]);

                Debug.Log($"Head :{head[j]}, Value : {value[j]}");
                
            }
        }
        /*
        foreach(string key in DataDictionary.Keys)
        {
            Debug.Log($"Key: {key}, Values: {string.Join(", ", DataDictionary[key])}");
        }
        
        return DataDictionary;
    }
}
*/

public class CSVReader
{
    public Dictionary<string, List<object>> CSVReade(string path)
    {
        string[] lines;

        Dictionary<string, List<object>> DataDictionary = new Dictionary<string, List<object>>();

        TextAsset csvData = Resources.Load<TextAsset>(path);

        if (csvData == null)
        {
            Debug.LogError("CSVDataError: Could not load CSV data.");
            return null;
        }

        // CSV �����͸� �о�� �ٹٲ��� �������� ������
        lines = csvData.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length <= 1)
        {
            Debug.LogWarning("No data found in CSV file.");
            return null;
        }

        // ù ���� ����� ó��
        string[] head = lines[0].Split(',').Select(h => h.Trim()).ToArray();
        foreach (string header in head)
        {
            DataDictionary[header] = new List<object>();
            Debug.Log($"Header: {header}");
        }

        // �����͸� ó��
        for (int i = 1; i < lines.Length; i++)
        {
            var value = lines[i].Split(',').Select(v => v?.Trim() ?? string.Empty).ToArray(); // null üũ �� ���� ����
            for (int j = 0; j < head.Length; j++)
            {
                if (j < value.Length)
                {
                    // ���� ���� �� null üũ �� ���� �߰�
                    DataDictionary[head[j]].Add(value[j]);
                }
                else
                {
                    // ���� ������ ��� �� �� �߰�
                    DataDictionary[head[j]].Add(string.Empty);
                }
            }
        }

        // ������� ���� Ű�� ���� ���
        foreach (var key in DataDictionary.Keys)
        {
            Debug.Log($"Key: {key}, Values: {string.Join(", ", DataDictionary[key])}");
        }

        return DataDictionary;
    }
}
