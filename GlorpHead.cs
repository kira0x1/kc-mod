using Harmony;
using UnityEngine;
using Object = UnityEngine.Object;

namespace kc_mod
{
    public static class GlorpHead
    {
        public static AssetBundle assetBundle;
        public static KCModHelper helper;
        public static GameObject alienHeadPrefab;

        public static string GetBundlePath(string folderName)
        {
            string os = "win64";
            switch (Application.platform)
            {
                case RuntimePlatform.LinuxPlayer:
                    os = "linux";
                    break;
                case RuntimePlatform.OSXPlayer:
                    os = "osx";
                    break;
                case RuntimePlatform.WindowsPlayer:
                default:
                    os = !SystemInfo.operatingSystem.Contains("64") ? "win32" : "win64";
                    break;
            }

            string bundlePath = $"{helper.modPath}/{folderName}/{os}/";
            return bundlePath;
        }

        public static AssetBundle LoadBundle(string path, string bundleName)
        {
            helper.Log("\nsearching for AsseBundle manifest\n");
            var tempBundle = AssetBundle.LoadFromFile(path);

            AssetBundleManifest assetBundleManifest = tempBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            foreach (string allAssetBundle in assetBundleManifest.GetAllAssetBundles())
            {
                if (!allAssetBundle.Contains(bundleName))
                    continue;

                foreach (string allDependency in assetBundleManifest.GetAllDependencies(allAssetBundle))
                    AssetBundle.LoadFromFile($"{path}/{allDependency}");

                tempBundle.Unload(false);
                helper.Log($"Found bundle: {path}/{allAssetBundle}");
                return AssetBundle.LoadFromFile($"{path}/{allAssetBundle}");
            }

            tempBundle.Unload(false);
            return null;
        }

        public static void PreScriptLoad(KCModHelper _helper)
        {
            helper = _helper;
            helper.Log("-- loading glorp head --");

            var bundlePath = GetBundlePath("alienhead");
            // assetBundle = LoadBundle(bundlePath, "alienhead");

            assetBundle = AssetBundle.LoadFromFile(bundlePath);
            helper.Log($"bundle path: {bundlePath}");
            helper.Log($"bundle: {assetBundle.name}");

            // AssetBundle tempBundle = AssetBundle.LoadFromFile(bundlePath);
            // assetBundle = AssetBundle.LoadFromFile(bundlePath);
            // assetBundle = KCModHelper.LoadAssetBundle(_helper.modPath, "alienhead");

            if (assetBundle == null)
            {
                helper.Log("could not find assetbundle 'alienhead'");
                return;
            }

            // alienHeadPrefab = assetBundle.LoadAsset<GameObject>("assets/workspace/alienhead.prefab");
            // helper.Log("loaded alienhead prefab");
        }

        [HarmonyPatch(typeof(Villager))]
        [HarmonyPatch("Init")]
        public static class GlorpVillagerPatch
        {
            //A function to run after target function invocation
            private static void Postfix(Villager __instance)
            {
                helper.Log("found transform field");

                Vector3 vpos = __instance.Pos;
                // var head = transform.Find("head");

                // if (head.childCount > 0)
                // {
                //     helper.Log("head has children already");
                //     for (int i = 0; i < head.childCount; i++)
                //     {
                //         var c = head.GetChild(i);
                //         helper.Log($"child {i}: {c.name}");
                //     }
                // }

                GameObject alienHeadGo = Object.Instantiate(alienHeadPrefab, vpos, Quaternion.identity);
                helper.Log("spawned alienhead prefab");
            }
        }
    }
}