using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

//Good Resources:
//  Example Mod: https://github.com/lamali292/sts2_example_mod/tree/main
//  Localization Strings: https://github.com/lamali292/WatcherMod/blob/main/Watcher/localization/eng/cards.json
//  Localization Patching of Existing Cards: https://github.com/AdUhTkJm/sts2-rebalance-mod/blob/main/src/cards/LocalizationPatch.cs

[ModInitializer("Initialize")]
public class ItsHammerTimeMain
{
    public static void Initialize()
    {
        var harmony = new Harmony("itsHammerTime.patch");
        harmony.PatchAll();
    }
}
