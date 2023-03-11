using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Appodeal.Editor.AppodealManager.Data
{
    public enum DependencyType
    {
        Unknown,
        AdNetwork,
        Service,
        Core
    }
    
    [Serializable]
    public class ServerConfig
    {
        public List<AppodealDependency> ad_networks;
        public List<AppodealDependency> services;
        public AppodealDependency core;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    [Serializable]
 public class AppodealDependency
    {
        public string name;
        public DependencyType type;
        public IosDependency ios_info;
        public AndroidDependency android_info;

        [Serializable]
        public class AndroidDependency
        {
            public string name;
            public string version;
            public string unity_content;
            public List<Dependency> dependencies;

            public AndroidDependency(string name, string version, string unity_content)
            {
                this.name = name;
                this.version = version;
                this.unity_content = unity_content;
            }

            public AndroidDependency(string name, string version, string unity_content, List<Dependency> dependencies)
            {
                this.name = name;
                this.version = version;
                this.unity_content = unity_content;
                this.dependencies = dependencies;
            }
        }

        [Serializable]
        public class IosDependency
        {
            public string name;
            public string version;
            public string unity_content;
            public List<Dependency> dependencies;

            public IosDependency(string name, string version, string unity_content)
            {
                this.name = name;
                this.version = version;
                this.unity_content = unity_content;
            }

            public IosDependency(string name, string version, string unity_content, List<Dependency> dependencies)
            {
                this.name = name;
                this.version = version;
                this.unity_content = unity_content;
                this.dependencies = dependencies;
            }
        }

        [Serializable]
        public class Dependency
        {
            public string name;
            public string version;
        }
    }
}
