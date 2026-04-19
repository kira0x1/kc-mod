using System.Collections.Generic;
using System.Reflection;
using Assets.Code;
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
            }

            private void Preload(KCModHelper helper)
            {
                GlorpMod.helper = helper;

                var harmony = HarmonyInstance.Create("harmony");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }

            [HarmonyPatch(typeof(Keep))]
            [HarmonyPatch("OnInit")]
            public static class KeepPatch
            {
                private static void Postfix(Keep __instance)
                {
                    __instance.woodStack.Add(100);
                    __instance.appleStack.Add(15);
                    __instance.stoneStack.Add(30);
                }
            }

            [HarmonyPatch(typeof(Villager))]
            [HarmonyPatch("Init")]
            public static class BigVillagerPatch
            {
                //A function to run after target function invocation
                private static void Postfix(Villager __instance)
                {
                    // Run arbitrary code

                    // Get Private Field Called 'fullScale' with Reflection
                    FieldInfo scaleField = typeof(Villager).GetField("fullScale", BindingFlags.Instance | BindingFlags.NonPublic);

                    // Assign Private Field New Scale, 4x Original Size
                    // __instance is a Villager from Postfix Arguments
                    scaleField.SetValue(__instance, new Vector3(2.5f, 4f, 2.5f));
                }
            }


            [HarmonyPatch(typeof(VillagerSystem))]
            [HarmonyPatch("Start")]
            public static class GlorpPatch
            {
                private static void Postfix(VillagerSystem __instance)
                {
                    Color[] glorpColors =
                    {
                        new Color(0.717f, 0.96f, 0.95f, 1.0f),
                        new Color(0.639f, 0.96f, 0.73f, 1.0f),
                        new Color(0.25f, 0.96f, 0.486f, 1.0f)
                    };

                    helper.Log("\n--VERSION: 1.5--");

                    __instance.bodyColors = glorpColors;
                }
            }
        }
    }
}