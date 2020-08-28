#if UNITY_2018_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Appodeal.Editor.AppodealManager.Data;
using UnityEditor;
using UnityEngine;

namespace Appodeal.Editor.AppodealManager.AppodealDependencies
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class AppodealDependencyUtils
    {
        #region Constants

        public const string PluginRequest = "https://mw-backend.appodeal.com/v1/unity";
        public const string AdaptersRequest = "https://mw-backend.appodeal.com/v1/unity/config/ver/";
        public const string Network_configs_path = "Assets/Appodeal/Editor/NetworkConfigs/";
        public const string Replace_dependency_value = "com.appodeal.ads.sdk.networks:";
        public const string Replace_dependency_core = "com.appodeal.ads.sdk:core:";
        public const string PackageName = "Name";
        public const string CurrentVersionHeader = "Current Version";
        public const string LatestVersionHeader = "Latest Version";
        public const string ActionHeader = "Action";
        public const string BoxStyle = "box";
        public const string ActionUpdate = "Update";
        public const string ActionImport = "Import";
        public const string EmptyCurrentVersion = "    -  ";
        public const string AppodealUnityPlugin = "Appodeal Unity Plugin";
        public const string AppodealSdkManager = "Appodeal SDK Manager";
        public const string Appodeal = "Appodeal";
        public const string Loading = "Loading...";
        public const string ProgressBar_cancelled = "Progress bar canceled by the user";
        public const string AppodealCoreDependencies = "Appodeal Core Dependencies";
        public const string iOS = "iOS";
        public const string Android = "Android";
        public const string AppodealNetworkDependencies = "Appodeal Network Dependencies";
        public const string SpecOpenDependencies = "<dependencies>\n";
        public const string SpecCloseDependencies = "</dependencies>";
        public const string XmlFileExtension = ".xml";
        public const string TwitterMoPub = "TwitterMoPub";
        public const string APDAppodealAdExchangeAdapter = "APDAppodealAdExchangeAdapter";
        public const string Dependencies = "Dependencies";

        #endregion
        
        public static FileInfo[] GetInternalDependencyPath()
        {
            var info = new DirectoryInfo(Network_configs_path);
            var fileInfo = info.GetFiles();

            return fileInfo.Length <= 0 ? null : fileInfo.Where(val => !val.Name.Contains("meta")).ToArray();
        }

        public static void ShowInternalErrorDialog(EditorWindow editorWindow, string message, string debugLog)
        {
            EditorUtility.ClearProgressBar();
            Debug.LogError(debugLog);
            var option = EditorUtility.DisplayDialog("Internal error",
                $"{message}. Please contact to Appodeal support.",
                "Ok");
            if (option)
            {
                editorWindow.Close();
            }
        }

        public static string GetConfigName(string value)
        {
            var configName = value.Replace(Network_configs_path, string.Empty);
            return configName.Replace("Dependencies.xml", string.Empty);
        }

        public static string GetiOSContent(string path)
        {
            var iOSContent = string.Empty;
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                if (line.Contains("<iosPods>"))
                {
                    iOSContent += line + "\n";
                }

                if (line.Contains("<iosPod name="))
                {
                    iOSContent += line + "\n";
                }

                if (line.Contains("</iosPods>"))
                {
                    iOSContent += line;
                }
            }

            return iOSContent;
        }

        public static string GetAndroidContent(string path)
        {
            var iOSContent = string.Empty;
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                if (line.Contains("<androidPackages>"))
                {
                    iOSContent += line + "\n";
                }

                if (line.Contains("<androidPackage spec="))
                {
                    iOSContent += line + "\n";
                }

                if (line.Contains("<repositories>"))
                {
                    iOSContent += line + "\n";
                }

                if (line.Contains("<repository>"))
                {
                    iOSContent += line + "\n";
                }

                if (line.Contains("</repositories>"))
                {
                    iOSContent += line + "\n";
                }

                if (line.Contains("</androidPackages>"))
                {
                    iOSContent += line;
                }
            }

            return iOSContent;
        }

        public static string GetAndroidDependencyName(string value)
        {
            var dependencyName = value.Replace(Replace_dependency_value, string.Empty);
            var sub = dependencyName.Substring(0,
                dependencyName.LastIndexOf(":", StringComparison.Ordinal));
            return sub.Contains("@aar") ? sub.Substring(0, sub.LastIndexOf("@", StringComparison.Ordinal)) : sub;
        }

        public static string GetAndroidDependencyVersion(string value)
        {
            var androidDependencyVersion =
                value.Replace(Replace_dependency_value + GetAndroidDependencyName(value) + ":", string.Empty);
            if (androidDependencyVersion.Contains("@aar"))
            {
                androidDependencyVersion = androidDependencyVersion.Substring(0,
                    androidDependencyVersion.LastIndexOf("@", StringComparison.Ordinal));
            }

            return androidDependencyVersion;
        }
        
        public static string GetAndroidDependencyCoreVersion(string value)
        {
            var androidDependencyVersion =
                value.Replace(Replace_dependency_core, string.Empty);
            if (androidDependencyVersion.Contains("@aar"))
            {
                androidDependencyVersion = androidDependencyVersion.Substring(0,
                    androidDependencyVersion.LastIndexOf("@", StringComparison.Ordinal));
            }

            return androidDependencyVersion;
        }

        public static string ReplaceBetaVersion(string value)
        {
            return Regex.Replace(value, "-Beta", string.Empty);
        }

        public static int CompareVersion(string interal, string latest)
        {
            var xParts = interal.Split('.');
            var yParts = latest.Split('.');
            var partsLength = Math.Max(xParts.Length, yParts.Length);
            if (partsLength <= 0) return string.Compare(interal, latest, StringComparison.Ordinal);
            for (var i = 0; i < partsLength; i++)
            {
                if (xParts.Length <= i) return -1;
                if (yParts.Length <= i) return 1;
                var xPart = xParts[i];
                var yPart = yParts[i];
                if (string.IsNullOrEmpty(xPart)) xPart = "0";
                if (string.IsNullOrEmpty(yPart)) yPart = "0";
                if (!int.TryParse(xPart, out var xInt) || !int.TryParse(yPart, out var yInt))
                {
                    var abcCompare = String.Compare(xPart, yPart, StringComparison.Ordinal);
                    if (abcCompare != 0)
                        return abcCompare;
                    continue;
                }

                if (xInt != yInt) return xInt < yInt ? -1 : 1;
            }

            return 0;
        }

        public static void ShowInfoDependency(NetworkDependency networkDependency)
        {
            Debug.Log($"Name - {networkDependency.name}");
            if (networkDependency.android_info != null)
            {
                Debug.Log($"AndroidDependency name - {networkDependency.android_info.name}");
                Debug.Log($"AndroidDependency version - {networkDependency.android_info.version}");
                Debug.Log($"AndroidDependency content - {networkDependency.android_info.unity_content}");
            }

            if (networkDependency.ios_info != null)
            {
                Debug.Log($"iOSDependency name - {networkDependency.ios_info.name}");
                Debug.Log($"iOSDependency version - {networkDependency.ios_info.version}");
                Debug.Log($"iOSDependency content - {networkDependency.ios_info.unity_content}");
            }
        }

        public static void ShowPluginInfo(AppodealUnityPlugin appodealUnityPluginAppodealUnity)
        {
            if (appodealUnityPluginAppodealUnity != null)
            {
                if (!string.IsNullOrEmpty(appodealUnityPluginAppodealUnity.name))
                {
                    Debug.Log($"UnityPluginInfo name: - {appodealUnityPluginAppodealUnity.name}");
                }
                else
                {
                    Debug.Log("UnityPluginInfo name: - IsNullOrEmpty");
                }

                if (!string.IsNullOrEmpty(appodealUnityPluginAppodealUnity.build_type))
                {
                    Debug.Log($"UnityPluginInfo build_type: - {appodealUnityPluginAppodealUnity.build_type}");
                }
                else
                {
                    Debug.Log("UnityPluginInfo build_type: - IsNullOrEmpty");
                }

                Debug.Log($"UnityPluginInfo id: - {appodealUnityPluginAppodealUnity.id}");

                if (!string.IsNullOrEmpty(appodealUnityPluginAppodealUnity.version))
                {
                    Debug.Log($"UnityPluginInfo version: - {appodealUnityPluginAppodealUnity.version}");
                }
                else
                {
                    Debug.Log("UnityPluginInfo version: - IsNullOrEmpty");
                }

                if (!string.IsNullOrEmpty(appodealUnityPluginAppodealUnity.updated_at))
                {
                    Debug.Log($"UnityPluginInfo updated_at: - {appodealUnityPluginAppodealUnity.updated_at}");
                }
                else
                {
                    Debug.Log("UnityPluginInfo updated_at: - IsNullOrEmpty");
                }

                if (!string.IsNullOrEmpty(appodealUnityPluginAppodealUnity.created_at))
                {
                    Debug.Log($"UnityPluginInfo created_at: - {appodealUnityPluginAppodealUnity.created_at}");
                }
                else
                {
                    Debug.Log("UnityPluginInfo created_at: - IsNullOrEmpty");
                }

                if (!string.IsNullOrEmpty(appodealUnityPluginAppodealUnity.source))
                {
                    Debug.Log($"UnityPluginInfo source: - {appodealUnityPluginAppodealUnity.source}");
                }
                else
                {
                    Debug.Log("UnityPluginInfo source: - IsNullOrEmpty");
                }

                foreach (var SupportedSdk in appodealUnityPluginAppodealUnity.sdks)
                {
                    Debug.Log($"UnityPluginInfo SupportedSdk Id - {SupportedSdk.id}");

                    if (!string.IsNullOrEmpty(SupportedSdk.platform))
                    {
                        Debug.Log($"UnityPluginInfo SupportedSdk.Platform: - {SupportedSdk.platform}");
                    }
                    else
                    {
                        Debug.Log("UnityPluginInfo SupportedSdk.Platform: - IsNullOrEmpty");
                    }

                    if (!string.IsNullOrEmpty(SupportedSdk.build_type))
                    {
                        Debug.Log($"UnityPluginInfo SupportedSdk.BuildType: - {SupportedSdk.build_type}");
                    }
                    else
                    {
                        Debug.Log("UnityPluginInfo SupportedSdk.BuildType: - IsNullOrEmpty");
                    }

                    if (!string.IsNullOrEmpty(SupportedSdk.version))
                    {
                        Debug.Log($"UnityPluginInfo SupportedSdk.Version: - {SupportedSdk.version}");
                    }
                    else
                    {
                        Debug.Log("UnityPluginInfo SupportedSdk.Version: - IsNullOrEmpty");
                    }
                }
            }
            else
            {
                Debug.Log("UnityPluginInfo unityPluginInfo - null");
            }
        }

        public static void GuiHeaders(GUIStyle headerInfoStyle, GUILayoutOption btnFieldWidth)
        {
            using (new EditorGUILayout.HorizontalScope(GUILayout.ExpandWidth(false)))
            {
                GUILayout.Button(PackageName, headerInfoStyle, GUILayout.Width(150));
                GUILayout.Space(25);
                GUILayout.Button(CurrentVersionHeader, headerInfoStyle, GUILayout.Width(110));
                GUILayout.Space(90);
                GUILayout.Button(LatestVersionHeader, headerInfoStyle);
                GUILayout.Button(ActionHeader, headerInfoStyle, btnFieldWidth);
                GUILayout.Button(string.Empty, headerInfoStyle, GUILayout.Width(5));
            }
        }
        
        public static NetworkDependency GetAppodealDependency(
            Dictionary<string, NetworkDependency> networkDependencies)
        {
            NetworkDependency networkDependency = null;
            foreach (var dependency
                in networkDependencies.Where(dependency
                        => dependency.Key.Contains(AppodealDependencyUtils.Appodeal))
                    .Where(dependency => dependency.Value != null))
            {
                networkDependency = dependency.Value;
            }

            return networkDependency;
        }
        
        
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            var wrapper = new Wrapper<T> {Items = array};
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            var wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        public static string fixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
#endif