namespace Alpha.ViewLogs.Constants
{
    using System.Collections;
    using System.Collections.Generic;
    using AppResources;
    using Sunplus.Agent.Model.Entities;

    public static class Permissions
    {
        private static readonly IDictionary<string, string> PemissionAbbs = new Dictionary<string, string>
        {
            [PageAccessType.CreateAccount + PageAccessType.UpdateAccount + PageAccessType.ViewMemberInfo] = PageAccessType.CreateAccount,
            [PageAccessType.CreateAccount + PageAccessType.UpdateAccount] = PageAccessType.CreateAccount,
            [PageAccessType.UpdateAccount + PageAccessType.ViewMemberInfo] = PageAccessType.UpdateAccount
        };

        public static IDictionary<string, string> PemissionAbbriations
        {
            get
            {
                return PemissionAbbs;
            }
        }

        public static IDictionary<string, string> PageAccessTypeName
        {
            get
            {
                // Multiple language => must create every time when user gets text
                var permissions = new Dictionary<string, string>();

                foreach (DictionaryEntry accessType in PageAccessType.AccessTypes)
                {
                    var key = accessType.Key.ToString();
                    permissions.Add(key, Resources.ResourceManager.GetString("PageAccess_" + GetShortPermissionKey(key)));
                }

                return permissions;
            }
        }

        private static string GetShortPermissionKey(string key)
        {
            return key.Substring(1, key.Length - 2);
        }
    }
}
