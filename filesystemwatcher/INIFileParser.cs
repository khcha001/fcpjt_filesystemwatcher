using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class INIFileParser
{
    private Dictionary<string, Dictionary<string, string>> _data = new Dictionary<string, Dictionary<string, string>>();

    public INIFileParser(string iniFilePath)
    {
        Load(iniFilePath);
    }

    private void Load(string iniFilePath)
    {
        if (!File.Exists(iniFilePath))
            throw new FileNotFoundException("Unable to locate the INI file: " + iniFilePath);

        string currentSection = "";
        var lines = File.ReadAllLines(iniFilePath);

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line.Trim()) || line.StartsWith(";"))
                continue;

            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                currentSection = line.Substring(1, line.Length - 2);
                if (!_data.ContainsKey(currentSection))
                {
                    _data[currentSection] = new Dictionary<string, string>();
                }
            }
            else
            {
                var keyValue = line.Split(new[] { '=' }, 2);
                if (keyValue.Length != 2)
                    continue;

                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();
                _data[currentSection][key] = value;
            }
        }
    }

    public string GetValue(string section, string key, string defaultValue = "")
    {
        if (_data.ContainsKey(section) && _data[section].ContainsKey(key))
            return _data[section][key];

        return defaultValue;
    }

    public List<string> GetValueList(string section, string key, string defaultValue = "")
    {
        List<string> values = new List<string>();

        if (_data.ContainsKey(section) && _data[section].ContainsKey(key))
        {
            string value = _data[section][key];
            string[] valueArray = value.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in valueArray)
            {
                values.Add(item.Trim());
            }
        }
        else
        {
            values.Add(defaultValue);
        }

        return values;
    }
}