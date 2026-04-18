namespace kc_mod
{
    using Harmony;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using UnityEngine;

    namespace KCMod
    {
        public class BigPeopleMod : MonoBehaviour
        {
            public static KCModHelper helper;

            // After scene loads
            private void SceneLoaded(KCModHelper helper)
            {
            }

            private void Preload(KCModHelper helper)
            {
                BigPeopleMod.helper = helper;
                helper.Log(helper.modPath);

                var harmony = HarmonyInstance.Create("harmony");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
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
                    scaleField.SetValue(__instance, new Vector3(4f, 4f, 4f));
                }
            }
        }
    }
}