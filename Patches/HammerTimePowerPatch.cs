using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

[HarmonyPatch(typeof(HammerTimePower), "get_StackType")]
public static class HammerTimePower_PowerStackType_Patch
{
    public static void Postfix(ref PowerStackType __result)
    {
        //Force the HammerTimePower to stack
        __result = PowerStackType.Counter;
    }
}

[HarmonyPatch(typeof(HammerTimePower), "AfterForge")]
public static class HammerTimePower_AfterForge_Patch
{
    public static async void Postfix(HammerTimePower __instance, decimal amount, Player forger, AbstractModel? source)
    {
        //Duplicated from original function, with a modification for our custom stacking of the power
        if (source is HammerTimePower || forger != __instance.Owner.Player || __instance.Amount <= 1)
        {
            return;
        }

        IEnumerable<Player> enumerable = __instance.CombatState.Players.Where((Player p) => p.Creature.IsAlive && p != forger);
        foreach (Player item in enumerable)
        {
            //Apply extra Forge to account for our stacking, but subtract 1 from our stack count because we can't stop the original function from running (Prefix doesn't work)
            await ForgeCmd.Forge(amount * (__instance.Amount - 1), item, __instance);
        }
    }
}
