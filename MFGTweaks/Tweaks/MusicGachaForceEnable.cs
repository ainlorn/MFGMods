using HarmonyLib;
using MFG.Scenes.GachaSelect;
using MFG.Types;
using System;
using UnityEngine.SceneManagement;

namespace MFGTweaks.Tweaks;

public class MusicGachaForceEnable : BaseTweak
{

    public override string Description => "Forces music gacha to be enabled despite PPEX problems";
    public override bool EnabledByDefault => true;

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(MusicGachaForceEnable));
    }


    [HarmonyPrefix, HarmonyPatch(typeof(GameUtility), nameof(GameUtility.GetPaseliPrice))]
    private static bool Hook_GameUtility_GetPaseliPrice(ref GameContents.GAME_MODE mode, ref int __result)
    {
        if (mode == GameContents.GAME_MODE.MUSICGACHA)
        {
            __result = 100;
            return false;
        }
        else
        {
            return true;
        }
    }

    [HarmonyPostfix, HarmonyPatch(typeof(PaseliUtility.PPEX), nameof(PaseliUtility.PPEX.IsEnablePPEX), MethodType.Getter)]
    private static void Hook_PaseliUtility_PPEX_get_IsEnablePPEX(ref bool __result)
    {
        __result = true;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(PaseliUtility), nameof(PaseliUtility.ComsumePaseli))]
    private static bool Hook_PaseliUtility_ComsumePaseli(ref GameContents.GAME_MODE mode)
    {
        if (mode == GameContents.GAME_MODE.MUSICGACHA)
        {
            mode = GameContents.GAME_MODE.GACHA;
        }
        return true;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(PaseliUtility), nameof(PaseliUtility.ComsumePaseliWithPrice))]
    private static void Hook_PaseliUtility_ComsumePaseliWithPrice(ref GameContents.GAME_MODE mode, ref int price)
    {
        if (mode == GameContents.GAME_MODE.MUSICGACHA || price == -25)
        {
            price = 100;
            mode = GameContents.GAME_MODE.GACHA;
        }
    }

    [HarmonyPrefix, HarmonyPatch(typeof(GachaSelectFlow), nameof(GachaSelectFlow.AllButton_Enable))]
    private static void Hook_GachaSelectFlow_AllButton_Enable(GachaSelectFlow __instance, ref bool isEnable)
    {
        isEnable = true;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(GachaSelectFlow), nameof(GachaSelectFlow.PPEXPriceRefresh))]
    private static bool Hook_GachaSelectFlow_PPEXPriceRefresh(GachaSelectFlow __instance)
    {
        __instance.isMusicGachaEnable = true;
        return false;
    }

}
