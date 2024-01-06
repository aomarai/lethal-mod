using System.Reflection;
using UnityEngine;


namespace CreatureRadar;

internal class Assets
{
    internal static AssetBundle radarAssetBundle;

    private const string assetBundleName = "radarAssets";
    private static string[] allAssetNames = new string[0];

    public static Item radarItem;

    public static AudioClip radarBeep;

    internal static void LoadAssets()
    {
        if (radarAssetBundle == null)
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CreatureRadar." + assetBundleName))
            {
                radarAssetBundle = AssetBundle.LoadFromStream(assetStream);
            }
        }
        allAssetNames = radarAssetBundle.GetAllAssetNames();
    }

    internal static void InitAssets()
    {
        if (radarAssetBundle == null)
        {
            Debug.LogError("AssetBundle is null, cannot load assets");
            return;
        }
        radarItem = radarAssetBundle.LoadAsset<Item>("Rock_03"); // TODO: Change this to the actual item
        radarBeep = radarAssetBundle.LoadAsset<AudioClip>("radar beep edited");
    }


}
