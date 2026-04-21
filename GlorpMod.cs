using System.Reflection;
using Harmony;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace kc_mod
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

        // [HarmonyPatch(typeof(Keep))]
        // [HarmonyPatch("OnInit")]
        // public static class KeepPatch
        // {
        //     private static void Postfix(Keep __instance)
        //     {
        //         __instance.woodStack.Add(100);
        //         __instance.appleStack.Add(15);
        //         __instance.stoneStack.Add(30);
        //     }
        // }

        [HarmonyPatch(typeof(Villager))]
        [HarmonyPatch("Init")]
        public static class BigVillagerPatch
        {
            //A function to run after target function invocation
            private static void Postfix(Villager __instance)
            {
                // Run arbitrary code

                // Get Private Field Called 'fullScale' with Reflection
                FieldInfo scaleField =
                    typeof(Villager).GetField("fullScale", BindingFlags.Instance | BindingFlags.NonPublic);

                // Assign Private Field New Scale, 4x Original Size
                // __instance is a Villager from Postfix Arguments
                scaleField.SetValue(__instance, new Vector3(2.5f, 4f, 2.5f));


                // GameObject[] root = SceneManager.GetActiveScene().GetRootGameObjects();
                // helper.Log("\n\nroot objects");
                // foreach (var o in root)
                // {
                //     helper.Log(o.name);
                // }
            }
        }

        [HarmonyPatch(typeof(GameUI))]
        [HarmonyPatch("SelectPerson")]
        public static class GameUIPatch
        {
            private static void Prefix(GameUI __instance, ref Villager villager)
            {
                helper.Log($"selected villager: {villager.name}");
                Villager v = villager;
                v.body.localScale = new Vector3(5f, 5f, 5f);
                villager = v;
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

                helper.Log("\n--VERSION: 1.6--");

                __instance.bodyColors = glorpColors;
            }
        }
    }
}