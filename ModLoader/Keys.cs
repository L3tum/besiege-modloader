using System;
using System.Collections.Generic;
using UnityEngine;

namespace spaar
{
    internal class Keys : MonoBehaviour
    {
        private static Dictionary<string, Key> Keymap;

        public static void LoadKeys()
        {
            Configuration configuration = ModLoader.Configuration;
            Keymap = new Dictionary<string, Key>();

            Keymap["Console"] = new Key(configuration.ConsoleK1, configuration.ConsoleK2);
            Keymap["ObjectExplorer"] = new Key(configuration.OEK1, configuration.OEK2);
            Keymap["Settings"] = new Key(configuration.SettingsK1, configuration.SettingsK2);
        }

        public static Key getKey(string keyName)
        {
            if (Keymap.ContainsKey(keyName))
            {
                return Keymap[keyName];
            }
            else
            {
                throw new ArgumentException("No such key: " + keyName);
            }
        }
    }
}
