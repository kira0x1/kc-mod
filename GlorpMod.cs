using System.Reflection;
using Harmony;
using UnityEngine;

namespace kc_mod
{
    namespace KCMod
    {
        public class GlorpMod : MonoBehaviour
        {
            public static KCModHelper helper;

            // After scene loads
            private void SceneLoaded(KCModHelper helper)
            {
                helper.Log("test...");
            }

            private void Preload(KCModHelper helper)
            {
                GlorpMod.helper = helper;

                var harmony = HarmonyInstance.Create("harmony");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }

            // [HarmonyPatch(typeof(Villager))]
            // [HarmonyPatch("Init")]
            // public static class BigVillagerPatch
            // {
            //     //A function to run after target function invocation
            //     private static void Postfix(Villager __instance)
            //     {
            //         // Run arbitrary code

            //         // Get Private Field Called 'fullScale' with Reflection
            //         FieldInfo scaleField = typeof(Villager).GetField("fullScale", BindingFlags.Instance | BindingFlags.NonPublic);

            //         // Assign Private Field New Scale, 4x Original Size
            //         // __instance is a Villager from Postfix Arguments
            //         scaleField.SetValue(__instance, new Vector3(4f, 4f, 4f));
            //     }
            // }

            [HarmonyPatch(typeof(VillagerSystem))]
            [HarmonyPatch("Start")]
            public static class GlorpPath
            {
                private static void Postfix(VillagerSystem __instance)
                {
                    Color[] glorpColors =
                    {
                        new Color(0.717f, 0.96f, 0.95f, 1.0f),
                        new Color(0.639f, 0.96f, 0.73f, 1.0f),
                        new Color(0.25f, 0.96f, 0.486f, 1.0f)
                    };

                    helper.Log("\n--VERSION: 1.1--");

                    __instance.bodyColors = glorpColors;

                    helper.Log($"Body Colors: {__instance.bodyColors.Length}");
                    foreach (var instanceBodyColor in __instance.bodyColors)
                    {
                        helper.Log($"Body Color: {instanceBodyColor}");
                    }
                }
            }
        }
    }
}