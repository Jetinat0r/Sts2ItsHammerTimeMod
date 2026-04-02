// Original method:
//   private static Dictionary<string, string> LoadTable(string path);
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Localization;

[HarmonyPatch(typeof(LocManager), "LoadTable")]
public static class Localization_Patch
{
    public static void Postfix(ref Dictionary<string, string> __result, string path)
    {
        if (__result == null)
            return;

        if (path.Contains("eng/cards.json"))
        {
            //We can only do the english translation right now
            //I'm assuming this function gets run per file per translation, so this is the time to hijack it for english
            //  And if we want to hijack another language, do a similar thing with e.g. "zhs/cards.json"
            __result["HAMMER_TIME.description"] = "Whenever you [gold]Forge[/gold], all allies [gold]Forge[/gold] as well.{IfUpgraded:show:\nAt the start of your turn [gold]Forge[/gold] {Forge:diff()}.|}";
        }
    }
}
