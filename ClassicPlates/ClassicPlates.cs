﻿using System.Collections;
using ClassicPlates.Compatibility;
using ClassicPlates.Patching;
using MelonLoader;
using VRC;

[assembly: MelonInfo(typeof(ClassicPlates.ClassicPlates), "ClassicPlates", "1.0.5", ".FS.#8519")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonOptionalDependencies("UIExpansionKit")]
namespace ClassicPlates;

public class ClassicPlates : MelonMod {
    private static readonly MelonLogger.Instance Logger = new("ClassicNameplates");

    public static NameplateManager? NameplateManager;

    public override void OnApplicationStart()
    {
        AssetManager.Init();
        Settings.Init();
        Compat.Init();

        MelonCoroutines.Start(UIManagerInit());
    }

    private static IEnumerator UIManagerInit()
    {
        while (VRCUiManager.prop_VRCUiManager_0 == null) { yield return null; }
        try
        {
            NameplateManager = new NameplateManager();
            NetworkManagerHooks.Init();
            Patching.Patching.Init();
        }
        catch (Exception obj)
        {
            MelonLogger.Error("Unable to Apply Patches: " + obj);
        }
    }

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (NameplateManager?.Nameplates == null) return;
        if (NameplateManager.Nameplates.Count <= 0) return;
        foreach (var plate in NameplateManager.Nameplates)
        {
            NameplateManager.Nameplates.Remove(plate.Key);
        }
    }

    public override void OnPreferencesSaved()
    {
        if (NameplateManager?.Nameplates == null) return;

        if (RoomManager.field_Internal_Static_ApiWorld_0 != null &&
            RoomManager.field_Internal_Static_ApiWorldInstance_0 != null)
        {
            foreach (var player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
            {
                if (player._vrcplayer != null)
                    NameplateManager.CreateNameplate(player._vrcplayer);
            }

            foreach (var plate in NameplateManager.Nameplates)
            {
                MelonDebug.Msg("Applying Settings for user: " + plate.Key);

                if (plate.Value != null && plate.Value.Nameplate != null)
                {
                    plate.Value.ApplySettings();
                }
            }
        }
        else
        {
            Debug("Not in Room, clearing any straggler Nameplates");
            NameplateManager.ClearNameplates();
        }
    }

    internal static void Log(object msg) => Logger.Msg(msg);

    internal static void Debug(object msg) {
        if (MelonDebug.IsEnabled())
            Logger.Msg(ConsoleColor.Cyan, msg);
    }

    internal static void Error(object obj) => Logger.Error(obj);

    internal static void DebugError(object obj) {
        if (MelonDebug.IsEnabled())
            Logger.Error($"[DEBUG] {obj}");
    }

    public static void Warning(string s)
    {
        Logger.Warning(s);
    }
}