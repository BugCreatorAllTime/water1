using UnityEngine;
using System.Collections;

namespace FH.Core
{
    public static class ApplicationConstances
    {
        private const string applicationDataResourcePath = "ApplicationData";
        private const string applicationDataAssetResourcePath = "Assets/FH/Core/Resources/ApplicationData";

        public static string ApplicationDataResourcePath
        {
            get
            {
                return applicationDataResourcePath;
            }
        }

        public static string ApplicationDataAssetResourcePath
        {
            get
            {
                return applicationDataAssetResourcePath;
            }
        }

        public static class ResourcesPath
        {

        }
    }

}