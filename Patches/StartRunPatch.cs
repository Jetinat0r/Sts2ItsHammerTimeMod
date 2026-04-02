using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Unlocks;

[HarmonyPatch(typeof(Player), nameof(Player.CreateForNewRun), new Type[] { typeof(CharacterModel), typeof(UnlockState), typeof(ulong) })]
public class StartRun_Patch
{
    static void Postfix(Player __result, UnlockState unlockState, ulong netId)
    {
        //If we're Regent and in MultiplayerAdd 1 HammerTime card to our deck at the start of the run
        //I would love a smarter way to check multiplayer than just netId != 1, but I don't know how!
        if (__result.Character is Regent && netId != 1)
        {
            __result.Deck.AddInternal(ModelDb.Card<HammerTime>().ToMutable());
        }
    }
}
