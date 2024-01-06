using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;
using LethalLib.Modules;

namespace CreatureRadar;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("LethalLib", BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency("LC_API", BepInDependency.DependencyFlags.HardDependency)]
public class CreatureRadar : BaseUnityPlugin
{
    public const string ModGUID = "com.wibbleh.creatureradar";
    public const string ModName = "CreatureRadar";
    public const string ModVersion = "0.0.1";

    public static AssetBundle MainAssets;
    public static ManualLogSource logger;
    public static ConfigFile config;
    private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
    public static CreatureRadar Instance;
    internal ManualLogSource manualLogSource;

    public static ConfigEntry<int> itemShopPrice { get; private set; }
    public static ConfigEntry<float> batteryUsage { get; private set; }

    public AudioSource radarAudioSource;
    public AudioClip radarAudioClip;


    private void Awake()
    {
        // Plugin startup logic
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

        if (Instance == null)
        {
            Instance = this;
        }

        manualLogSource = BepInEx.Logging.Logger.CreateLogSource(ModGUID);

        harmony.PatchAll(typeof(CreatureRadar));
        harmony.PatchAll();

        itemShopPrice = Config.Bind<int>("ItemPrice", "Price", 50, "Price of the item in the shop");
        batteryUsage = Config.Bind<float>("BatteryUsage", "Usage", 0.02f, "How much battery is used per second");

        Assets.LoadAssets();
        Assets.InitAssets();

        Item radarItemAsset = Assets.radarItem;

        CreatureRadarItem creatureRadar = radarItemAsset.spawnPrefab.AddComponent<CreatureRadarItem>();
        creatureRadar.itemProperties = radarItemAsset;

        Items.RegisterShopItem(radarItemAsset, itemShopPrice.Value);
        NetworkPrefabs.RegisterNetworkPrefab(radarItemAsset.spawnPrefab);
    }
}