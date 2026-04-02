using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

[HarmonyPatch(typeof(HammerTime), MethodType.Constructor)]
public static class HammerTime_Constructor_Patch
{
    public static void Postfix(HammerTime __instance)
    {
        //Set the card's cost to 0
        AccessTools.FieldRefAccess<CardModel, int>("<CanonicalEnergyCost>k__BackingField")(__instance) = 0;

        //Give the card the Innate keyword
        AccessTools.FieldRefAccess<CardModel, HashSet<CardKeyword>>("_keywords")(__instance) = new HashSet<CardKeyword>() { CardKeyword.Innate };

        //Provide a dynamic var for the upgraded LocString to use to display the Furnace power the new upgrade gives
        DynamicVarSet _dynamicVars = new DynamicVarSet(new List<DynamicVar> { new ForgeVar(2) });
        _dynamicVars.InitializeWithOwner(__instance);
        AccessTools.FieldRefAccess<CardModel, DynamicVarSet>("_dynamicVars")(__instance) = _dynamicVars;
    }
}

[HarmonyPatch(typeof(HammerTime), "OnPlay")]
public static class HammerTime_OnPlay_Patch
{
    public static async void Postfix(HammerTime __instance, PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        //Since we can't lower our card's cost (of 0) anymore, so we apply Furnace as an upgrade effect
        if (__instance.IsUpgraded)
        {
            await PowerCmd.Apply<FurnacePower>(__instance.Owner.Creature, 2m, __instance.Owner.Creature, __instance);
        }
    }
}
