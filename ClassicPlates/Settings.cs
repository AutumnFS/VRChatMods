﻿using MelonLoader;

namespace ClassicPlates;

internal static class Settings
{
    public static void Init()
    {
        var melonPreferencesCategory = MelonPreferences.CreateCategory("ClassicNameplates");
        Enabled = melonPreferencesCategory.CreateEntry("_enabled", true, "Enable Old Nameplates");
        ModernMovement = melonPreferencesCategory.CreateEntry("_modernMovement", true, "Enable Modern Nameplates Movement");

        ShowSelfOnMenu = melonPreferencesCategory.CreateEntry("_showSelfOnMenu", true, "Show Local on QM");
        ShowOthersOnMenu = melonPreferencesCategory.CreateEntry("_showOthersOnMenu", true, "Show Others on QM");

        Offset = melonPreferencesCategory.CreateEntry("_offset", .3f, "Height Offset");
        Scale = melonPreferencesCategory.CreateEntry("_scale", 1f, "Plate Scale");

        PlateColor = melonPreferencesCategory.CreateEntry("_plateColor", "#00FF00", "Plate Color");
        NameColor = melonPreferencesCategory.CreateEntry("_nameColor", "#FFFFFF", "Name Color");

        PlateColorByRank = melonPreferencesCategory.CreateEntry("_plateColorByRank", false, "Rank Colored Plate");
        NameColorByRank = melonPreferencesCategory.CreateEntry("_nameColorByRank", false, "Rank Colored Name");

        ShowRank = melonPreferencesCategory.CreateEntry("_showRank", true, "Show Rank");
        ShowVoiceBubble = melonPreferencesCategory.CreateEntry("_showVoiceBubble", true, "Show Voice Bubble");
        ShowIcon = melonPreferencesCategory.CreateEntry("_showIcon", true, "Show User Icon");

        ShowInteraction = melonPreferencesCategory.CreateEntry("_showinteract", false, "Interaction Icon");
        ShowInteractionOnMenu = melonPreferencesCategory.CreateEntry("_showinteractOnMenu", true, "Interaction Icon on QM");
        
        ShowPerformance = melonPreferencesCategory.CreateEntry("_showperf", true, "Performance Icon");
        ShowFallback = melonPreferencesCategory.CreateEntry("_showfallback", true, "Fallback Icon");
        ShowMaster = melonPreferencesCategory.CreateEntry("_showmaster", true, "Master Icon");
        ShowQuest = melonPreferencesCategory.CreateEntry("_showquest", true, "Quest Icon");
        
        
        //TODO: Rainbow Plates
        //RainbowNameplates = melonPreferences_Category.CreateEntry("RainbowNameplates", false, "All Nameplates Rainbow");

        //TODO: Rainbow Plates for Friends
        //RainbowNameplatesFriendsOnly = melonPreferences_Category.CreateEntry("RainbowNameplatesFriendsOnly", false, "Rainbow Friends Only\n(requires All Rainbow to be on)");

        StatusMode = VRC.NameplateManager.field_Public_Static_StatusMode_0;
    }


    public static MelonPreferences_Entry<bool>? Enabled;
    public static MelonPreferences_Entry<bool>? ModernMovement;

    public static MelonPreferences_Entry<bool>? ShowSelfOnMenu;
    public static MelonPreferences_Entry<bool>? ShowOthersOnMenu;

    public static MelonPreferences_Entry<float>? Offset;
    public static MelonPreferences_Entry<float>? Scale;

    public static MelonPreferences_Entry<string>? PlateColor;
    public static MelonPreferences_Entry<string>? NameColor;

    public static MelonPreferences_Entry<bool>? PlateColorByRank;
    public static MelonPreferences_Entry<bool>? NameColorByRank;

    public static MelonPreferences_Entry<bool>? ShowRank;
    public static MelonPreferences_Entry<bool>? ShowVoiceBubble;
    public static MelonPreferences_Entry<bool>? ShowIcon;

    public static MelonPreferences_Entry<bool>? ShowInteraction;
    public static MelonPreferences_Entry<bool>? ShowInteractionOnMenu;
    
    public static MelonPreferences_Entry<bool>? ShowPerformance;
    public static MelonPreferences_Entry<bool>? ShowFallback;
    public static MelonPreferences_Entry<bool>? ShowMaster;
    public static MelonPreferences_Entry<bool>? ShowQuest;

    private static VRC.NameplateManager.StatusMode _statusMode;
    private static VRC.NameplateManager.NameplateMode _nameplateMode;

    public static VRC.NameplateManager.StatusMode StatusMode
    {
        get => _statusMode;
        set
        {
            if (_statusMode == value) return;
            _statusMode = value;
            if (ClassicPlates.NameplateManager?.Nameplates.Values == null) return;
            foreach (var plate in ClassicPlates.NameplateManager.Nameplates.Values)
            {
                if (plate != null) plate.OnStatusModeChanged(_statusMode);
            }
        }
    }

    public static VRC.NameplateManager.NameplateMode NameplateMode
    {
        get => _nameplateMode;
        set
        {
            if (_nameplateMode == value) return;
            _nameplateMode = value;
            if (ClassicPlates.NameplateManager?.Nameplates.Values == null) return;
            foreach (var plate in ClassicPlates.NameplateManager.Nameplates.Values)
            {
                if (plate != null) plate.OnNameplateModeChanged(_nameplateMode);
            }
        }
    }
}