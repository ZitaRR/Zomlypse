using System;
using System.IO;
using UnityEngine;

namespace Zomlypse.IO
{
    public static class FileManager
    {
        public const string MALE_NAMES = "mNames.json";
        public const string FEMALE_NAMES = "fNames.json";
        public const string COLOR_PRESETS = "colors.json";

        public static readonly string Directory;

        static FileManager()
        {
            Directory = $"{Application.persistentDataPath}/";
        }

        public static void Initialize()
        {
            if (!Exists($"{Directory}{MALE_NAMES}"))
            {
                TextAsset males = Resources.Load<TextAsset>(MALE_NAMES.Replace(".json", ""));
                File.WriteAllBytes($"{Directory}{MALE_NAMES}", males.bytes);
            }
            if (!Exists($"{Directory}{FEMALE_NAMES}"))
            {
                TextAsset females = Resources.Load<TextAsset>(FEMALE_NAMES.Replace(".json", ""));
                File.WriteAllBytes($"{Directory}{FEMALE_NAMES}", females.bytes);
            }
            if (!Exists($"{Directory}{COLOR_PRESETS}"))
            {
                TextAsset colors = Resources.Load<TextAsset>(COLOR_PRESETS.Replace(".json", ""));
                File.WriteAllBytes($"{Directory}{COLOR_PRESETS}", colors.bytes);
            }
        }

        public static bool Exists(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"{nameof(path)} cannot be empty or null");
            }

            return File.Exists($"{Directory}{path}");
        }

        public static T Read<T>(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"{nameof(path)} cannot be empty or null");
            }
            else if (!Exists(path))
            {
                throw new ArgumentException($"{Directory}{path} does not exist");
            }

            return JsonUtility.FromJson<T>(File.ReadAllText($"{Directory}{path}"));
        }
    }
}
