using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
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

/*

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

        // CSV 데이터를 읽어와 줄바꿈을 기준으로 나눈다
        lines = csvData.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length <= 1)
        {
            Debug.LogWarning("No data found in CSV file.");
            return null;
        }

        // 첫 줄은 헤더로 처리
        string[] head = lines[0].Split(',').Select(h => h.Trim()).ToArray();
        foreach (string header in head)
        {
            DataDictionary[header] = new List<object>();
        }

        // 데이터를 처리
        for (int i = 1; i < lines.Length; i++)
        {
            var value = lines[i].Split(',').Select(v => v?.Trim() ?? string.Empty).ToArray(); // null 체크 후 공백 제거
            for (int j = 0; j < head.Length; j++)
            {
                if (j < value.Length)
                {
                    // 공백 제거 및 null 체크 후 값을 추가
                    DataDictionary[head[j]].Add(value[j]);
                }
                else
                {
                    // 값이 부족한 경우 빈 값 추가
                    DataDictionary[head[j]].Add(string.Empty);
                }
            }
        }

        // 디버깅을 위한 키와 값을 출력
        /*
        foreach (var key in DataDictionary.Keys)
        {
            Debug.Log($"Key: {key}, Values: {string.Join(", ", DataDictionary[key])}");
        }
        
        return DataDictionary;
    }
}

*/


public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;
        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}