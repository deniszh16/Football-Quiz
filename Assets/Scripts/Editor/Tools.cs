using UnityEditor;
using UnityEngine;

namespace DZGames.Football.Editor
{
    public static class Tools
    {
        [MenuItem("Tools/Clear PlayerPrefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}