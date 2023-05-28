using MelonLoader;
using HarmonyLib;
using Il2Cpp;

namespace WeaponCrosshair
{
    internal class Implementation : MelonMod
    {
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg($"[{Info.Name}] Version {Info.Version} loaded!");
            Settings.OnLoad();
        }
    }

    [HarmonyPatch(typeof(HUDManager), nameof(HUDManager.UpdateCrosshair))]
    internal class HUDManager_UpdateCrosshair
    {
        private static void Postfix(HUDManager __instance)
        {
            if (GameManager.GetPlayerManagerComponent().PlayerIsZooming())
            {
                GearItem itemInHands = GameManager.GetPlayerManagerComponent().m_ItemInHands;
                bool showForStoneItem = (Settings.options.stoneCrosshair && itemInHands.m_StoneItem);
                bool showForGunItem = (Settings.options.rifleCrosshair && itemInHands.m_GunItem);
                bool showForBowItem = (Settings.options.bowCrosshair && itemInHands.m_BowItem);
                if ( showForStoneItem || showForGunItem || showForBowItem )
                {
                    //MelonLoader.MelonLogger.Log("Attempting to show crosshair");
                    Utils.SetActive(InterfaceManager.GetPanel<Panel_HUD>().m_Sprite_Crosshair.gameObject, true);
                    InterfaceManager.GetPanel<Panel_HUD>().m_Sprite_Crosshair.alpha = 1f;
                }
            }
        }
    }
}
