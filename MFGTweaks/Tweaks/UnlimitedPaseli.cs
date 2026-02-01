using HarmonyLib;

namespace MFGTweaks.Tweaks;

public class UnlimitedPaseli : BaseTweak
{
    
    public override string Description => "Disables PIN code prompts and lockout after spending PASELI";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(UnlimitedPaseli));
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(PaseliUtility), nameof(PaseliUtility.IsReauthEnable))]
    private static bool Hook_PaseliUtility_IsReauthEnable(ref bool __result)
    {
        __result = true;
        return false;
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(PaseliUtility), nameof(PaseliUtility.IsNeedReauth))]
    private static bool Hook_PaseliUtility_IsNeedReauth(ref bool __result)
    {
        __result = false;
        return false;
    }

}
