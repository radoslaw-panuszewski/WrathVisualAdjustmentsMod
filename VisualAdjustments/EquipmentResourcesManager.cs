﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Items.Shields;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.BundlesLoading;
using Kingmaker.Visual.CharacterSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace VisualAdjustments
{
    // Token: 0x02000013 RID: 19
    internal static class LibraryThing
    {
        public static Dictionary<string, string> resourceguidmap = new Dictionary<string, string> { };

        // Token: 0x06000054 RID: 84 RVA: 0x000039D8 File Offset: 0x00001BD8
        public static Dictionary<string, string> GetResourceGuidMap()
        {
            LocationList locationList = BundlesLoadService.Instance.m_LocationList;
            //LocationList locationList = (LocationList)HarmonyLib.AccessTools.Field(typeof(BundlesLoadService), "m_LocationList").GetValue(BundlesLoadService.Instance);
            if (resourceguidmap.Count == 0)
            {
                resourceguidmap = locationList.GuidToBundle;
            }
            return resourceguidmap;
        }
    }

    internal static class BluePrintThing
    {
        // Token: 0x06000053 RID: 83 RVA: 0x000039A0 File Offset: 0x00001BA0
        /* public static TBlueprint[] GetBlueprints<TBlueprint>() where TBlueprint : BlueprintScriptableObject
         {
          // return  Main.blueprints.OfType<TBlueprint>().ToArray();
         }*/
    }

    public class EquipmentResourcesManager
    {
        public static Dictionary<string, string> AllEEL
        {
            get
            {
                try
                {

                    if (m_AllEEL.Count == 0 /* && 1 == 2*/)
                    {
                        bool EEName(string name)
                        {
                            return (name[0] == 'e' && name[1] == 'e' && name[2] == '_') ;
                        }
                        if (LibraryThing.GetResourceGuidMap() != null)
                        {
                            //var task = new Task(async () =>
                            {
                                Main.logger.Log("AllEEGetter");
                                foreach (var kv in LibraryThing.GetResourceGuidMap().Where(a => EEName(a.Value)))
                                {
                                    
                                    var obj = ResourcesLibrary.TryGetResource<EquipmentEntity>(kv.Key);
                                    if (obj != null && obj.name != null) //&& obj.name.Contains("Wing"))
                                    {
                                        //Main.logger.Log("-----[\"" + obj.name + "\"] = ResourcesLibrary.TryGetResource<GameObject>(\"" + kv.Key + "\");");
                                        // m_WingsEE[go.name] = go;
                                        //Main.logger.Log($"{kv.Key} + {kv.Value} + {obj.name}");
                                        m_AllEEL[obj.name] = kv.Key;
                                    }
                                    //ResourcesLibrary.CleanupLoadedCache();
                                }
                                ResourcesLibrary.CleanupLoadedCache();
                            }//);
                            //task.Start();
                        }
                    }
                    if (m_AllEEL.Count == 0 && 1 == 2)
                    {
                        m_AllEEL["EE_AlchemistAccessories_F_Any"] = "c17371db120a64649a34675d049a495c";
                        m_AllEEL["EE_AlchemistAccessories_M_Any"] = "94a61b3f71fc8504ca2c7a0f8ca03bcb";
                        m_AllEEL["EE_Alchemist_F_Any"] = "a542253d14243514981ca6c6491f9f87";
                        m_AllEEL["EE_Alchemist_M_Any"] = "4a883e90c04fd2c408e36c836c37cde1";
                        m_AllEEL["EE_AngelTest_M_HM"] = "64b71c9d266e817408a282c1f97bab1e";
                        m_AllEEL["EE_ApronAlchemist_F_Any"] = "c7c5df037ad45064da365c44f5cc1871";
                        m_AllEEL["EE_ApronAlchemist_M_Any"] = "6ae8a1bb1cb6345438242c4e2fb892d2";
                        m_AllEEL["EE_ArcanistClassHat_F_DW"] = "80f7c29f00f0c3f43a85f857a6153b8d";
                        m_AllEEL["EE_ArcanistClassHat_F_EL"] = "3703f5b1eae21a7499b0b9baabaafdae";
                        m_AllEEL["EE_ArcanistClassHat_F_GN"] = "2cfa63882db59464abc367ae819dad12";
                        m_AllEEL["EE_ArcanistClassHat_F_HE"] = "0f1f0b7fdfc385d49aef57dc16c7bd4c";
                        m_AllEEL["EE_ArcanistClassHat_F_HL"] = "712701c7e8ac25c4baff83f1690d9801";
                        m_AllEEL["EE_ArcanistClassHat_F_HM"] = "8abf90540270eec4bbcaa8da6c81a7fa";
                        m_AllEEL["EE_ArcanistClassHat_F_HO"] = "682529245ed768e4aa84538687001648";
                        m_AllEEL["EE_ArcanistClassHat_F_OD"] = "a8e1ae6221a1af743a5fe7d55e1afa2b";
                        m_AllEEL["EE_ArcanistClassHat_F_TL"] = "e1ac1fcc71a41594d8828ee6fc95b603";
                        m_AllEEL["EE_ArcanistClassHat_M_DW"] = "7758845b02d9a184ba3613ca9112dde4";
                        m_AllEEL["EE_ArcanistClassHat_M_EL"] = "d581faf9408e8ac4c9f8bc6f6201977a";
                        m_AllEEL["EE_ArcanistClassHat_M_GN"] = "f0f78972cbc625644ba3fad60d1242de";
                        m_AllEEL["EE_ArcanistClassHat_M_HE"] = "9342ecaca7b50b84a9e18307b5967153";
                        m_AllEEL["EE_ArcanistClassHat_M_HL"] = "36b3246c49f7d7943ad58809e52de93a";
                        m_AllEEL["EE_ArcanistClassHat_M_HM"] = "5803f58736a4da2419e857ba9d6048c6";
                        m_AllEEL["EE_ArcanistClassHat_M_HO"] = "6c4fabbbb1234104fb7c57f4ec8aa78e";
                        m_AllEEL["EE_ArcanistClassHat_M_OD"] = "432bfe1f60a3c9e46b43eb81f302d8a1";
                        m_AllEEL["EE_ArcanistClassHat_M_TL"] = "bae25b74dfc692b4fa7a82bbda959c34";
                        m_AllEEL["EE_ArcanistHatColored_F_HE"] = "9c970ee92731b034c955fee647b6f38b";
                        m_AllEEL["EE_Arcanist_F_Any"] = "11266d19b35cb714d96f4c9de08df48e";
                        m_AllEEL["EE_Arcanist_M_Any"] = "65e7ae8b40be4d64ba07d50871719259";
                        m_AllEEL["EE_Backpack_Alchemist_U_Any"] = "98e0a4ed12819094bae10de6faa64c04";
                        m_AllEEL["EE_Backpack_Barbarian_U_Any"] = "9ecf363c24b356d4b81148eda5655fee";
                        m_AllEEL["EE_Backpack_Bard_U_Any"] = "d94181b08236abd4fbfd1e9680bdc0a3";
                        m_AllEEL["EE_Backpack_Kineticist_U_Any"] = "aa33fb01b1db1444ca27ba2f537e5837";
                        m_AllEEL["EE_Backpack_Warrior_U_Any"] = "88500c9b40dd6ca479829028f24460d5";
                        m_AllEEL["EE_BandedMailArmy_F_Any"] = "0fb35a8d8f604524c9a1a0b413477ebc";
                        m_AllEEL["EE_BandedMailArmy_M_Any"] = "5d31f8377c12a80459033330cd1bd34c";
                        m_AllEEL["EE_BandedMailDwarven_F_Any"] = "14052095ab0da0b4fa45689f4a61186a";
                        m_AllEEL["EE_BandedMailDwarven_M_Any"] = "9f56df10663cf63498e33b5850f26a70";
                        m_AllEEL["EE_BarbarianAccessories_F_Any"] = "905829bab72a95b47b47257fb6505040";
                        m_AllEEL["EE_BarbarianAccessories_M_Any"] = "6ef6daf07c2eae24aac858753b75ce4d";
                        m_AllEEL["EE_Barbarian_F_Any"] = "6faedc81998bbf24084dd5a94ccd91f2";
                        m_AllEEL["EE_Barbarian_M_Any"] = "41bbce033bbd0e6458db7ce760429998";
                        m_AllEEL["EE_BardAccessories_F_Any"] = "7a1655ab75173f74a93b6b369618f013";
                        m_AllEEL["EE_BardAccessories_M_Any"] = "cddd5bd766c85af40ab49d38eaf5bdea";
                        m_AllEEL["EE_Bard_F_Any"] = "436762cf39551fb48a0c540430666c18";
                        m_AllEEL["EE_Bard_M_Any"] = "eb4318c808dcb2e45a7184db61ab1c29";
                        m_AllEEL["EE_Beard00BatuKhan_M_HO"] = "104a8dd332a5c2e4b9176475ddb7273b";
                        m_AllEEL["EE_Beard00Bristle_M_HM"] = "e2216e3822f431a4d832ad28884db6dc";
                        m_AllEEL["EE_Beard00Short_M_AA"] = "7c49a83fda13e6948addad6092cca94f";
                        m_AllEEL["EE_Beard00Short_M_HM"] = "540a424d8a2c3a849bd7fbe0504e0f36";
                        m_AllEEL["EE_Beard01ChinCurtain_M_HO"] = "9ddc4370b22405f43860ca11c58c1be2";
                        m_AllEEL["EE_Beard01Goatee_M_EL"] = "6ef0ea044cc62aa4f85a9bb18b377438";
                        m_AllEEL["EE_Beard01Goatee_M_GN"] = "b4d25ae190a73ca40b42c7fb841f4dac";
                        m_AllEEL["EE_Beard01Long_M_OD"] = "7c42a312fe5c5ad4b9220a57f492f723";
                        m_AllEEL["EE_Beard01MediumGreybor_M_DW"] = "ca78137c923bdca4e8415803971c2f19";
                        m_AllEEL["EE_Beard01Medium_M_AA"] = "19474969a87b86a40a44b7b34abaabbc";
                        m_AllEEL["EE_Beard01Medium_M_HM"] = "c2d4658f29508d64cadca4248b2e1863";
                        m_AllEEL["EE_Beard01MuttonChopsSimple_M_HL"] = "e43b1be9d90068740bb611e28ce4f51b";
                        m_AllEEL["EE_Beard02Braid_M_HO"] = "b3e2b9e1f0d028c4bbb09b6c12f85f06";
                        m_AllEEL["EE_Beard02Long_M_DW"] = "4cdc6d5d05ff4c343b75a478cc01d7a8";
                        m_AllEEL["EE_Beard02MoustacheGoatee_M_AA"] = "fa7f51d3d9d90634888ef10a863b61b3";
                        m_AllEEL["EE_Beard02MoustacheGoatee_M_HM"] = "5c33644cfc212154e850936bd88018ea";
                        m_AllEEL["EE_Beard02MuttonChopsCurly_M_HL"] = "1f3e87a33ecd9334ab184aa242b5b0d0";
                        m_AllEEL["EE_Beard02SpikyChinCurtain_M_OD"] = "0a4238364914b1b4ebc1f3b086dc723a";
                        m_AllEEL["EE_Beard02WithMoustache_M_GN"] = "f12d10253be658948bd5bad05afd0790";
                        m_AllEEL["EE_Beard03ChinCurtain_M_AA"] = "b0fc93f220de54a41beb8a808fd08aa1";
                        m_AllEEL["EE_Beard03ChinCurtain_M_HM"] = "a6db66ee24fcbed45ba327acac99a7b7";
                        m_AllEEL["EE_Beard03FriendlyMuttonChops_M_DW"] = "09cf4efd119bdb545b168bbf5cb280c9";
                        m_AllEEL["EE_Beard03Medium_M_OD"] = "a1612f212b1b0984c8bdde713a734f67";
                        m_AllEEL["EE_Beard03SideburnsBushy_M_HO"] = "ed24c2cc9c5303148af482a0346f31ea";
                        m_AllEEL["EE_Beard03WithSharpMoustache_M_GN"] = "b77f8a53840cad4489f53639508bf547";
                        m_AllEEL["EE_Beard04LargeFourBraids_M_DW"] = "42558038708da864c8a6fb8a9490ea12";
                        m_AllEEL["EE_Beard04MoustacheGoatee_M_OD"] = "94f8943f7ab301a46910115bc5d4b696";
                        m_AllEEL["EE_Beard04OldmanLong_M_GN"] = "4dfd38118a6be1e469130bedc1917295";
                        m_AllEEL["EE_Beard04OldmanLong_M_HM"] = "37335873fb751fd4d9bc55005afe010c";
                        m_AllEEL["EE_Beard05FriendlyMuttonChops_M_HM"] = "372ace9cdb7b74a49aa143a28a5a3dd8";
                        m_AllEEL["EE_Beard05MoustacheMongolian_M_HO"] = "0514529c1387d8c4a803413ed49f7f09";
                        m_AllEEL["EE_Beard05MuttonChopsBraids_M_DW"] = "413cd80e68e056147b7751235efbb21e";
                        m_AllEEL["EE_Beard06MediumTwoBraids_M_DW"] = "9fedba9806741854ca18369218b2abe8";
                        m_AllEEL["EE_Beard06WiseMan_M_AA"] = "3735b6e46a1889549a737e12d8bcf200";
                        m_AllEEL["EE_Beard06WiseMan_M_HM"] = "a7b45e988928aef4c880f07fa5de24d9";
                        m_AllEEL["EE_BeltDruid_F_Any"] = "845c09e4033ffc74084d133e909f4b9c";
                        m_AllEEL["EE_BeltDruid_M_Any"] = "fd4260c59d8ec9042a86344404739fb3";
                        m_AllEEL["EE_BeltFighter_F_Any"] = "5a559e023eddcc743b95ce8d5b705301";
                        m_AllEEL["EE_BeltFighter_M_Any"] = "bb9b9861f465151478fd011ab01234d7";
                        m_AllEEL["EE_BeltHunter_F_Any"] = "967ca14373d118e4db19f788806acf56";
                        m_AllEEL["EE_BeltHunter_M_Any"] = "9acfe3870df3d2d4f9d13f80baa8820f";
                        m_AllEEL["EE_BeltMonk_F_Any"] = "4daa1b2ec08b72a4c8ee31578135e21e";
                        m_AllEEL["EE_BeltMonk_M_Any"] = "a0a16c10dbf319c43a43651a2d091fba";
                        m_AllEEL["EE_BeltOracle_F_Any"] = "681732028cbdbc242a6c7644364515cb";
                        m_AllEEL["EE_BeltOracle_M_Any"] = "0c94eada60c86a148944987fc78a31cf";
                        m_AllEEL["EE_BeltPaladin_F_Any"] = "a1be30dcd40bc0f4a9b3d87a3aa34540";
                        m_AllEEL["EE_BeltPaladin_M_Any"] = "4370a3e8240e3d1418cbeef5aad3197b";
                        m_AllEEL["EE_BeltRanger_F_Any"] = "3113ce430c9d88f4a88bd399503af582";
                        m_AllEEL["EE_BeltRanger_M_Any"] = "aafd8bea1dbdbcf4fbec08394539b5f1";
                        m_AllEEL["EE_BeltRogue_F_Any"] = "0117472f50f76444ebe08200ed70f350";
                        m_AllEEL["EE_BeltRogue_M_Any"] = "7335302de64a0584283186653d1cab5c";
                        m_AllEEL["EE_BeltShaman_F_Any"] = "52ec697b69b0c7a46b4c03fc219d6a26";
                        m_AllEEL["EE_BeltShaman_M_Any"] = "7f7186a001650c34ea609266f458a41d";
                        m_AllEEL["EE_BeltSkald_M_Any"] = "bdbb9af326463094aa60445378f54533";
                        m_AllEEL["EE_BeltSlayer_F_Any"] = "282638707649f1443921afd8d1ea19fe";
                        m_AllEEL["EE_BeltSlayer_M_Any"] = "8c3594832d3ddcc439ebad98a3978a34";
                        m_AllEEL["EE_BeltSorcerer_F_Any"] = "66243b0ebea97974db01299d920213e7";
                        m_AllEEL["EE_BeltSorcerer_M_Any"] = "81d5b9f8855d16d43ad231c6f1b028af";
                        m_AllEEL["EE_BeltWarpriest_F_Any"] = "5eb155aeb9f73f640b34a8e0a3f69cb5";
                        m_AllEEL["EE_BeltWarpriest_M_Any"] = "0d5a4ae6e6eabd946b7b53e213e072d9";
                        m_AllEEL["EE_BloodragerAccessories_F_Any"] = "cec1fa08b14c22647834f2168336e16f";
                        m_AllEEL["EE_BloodragerAccessories_M_Any"] = "69b184d9e882f204f99c2cff2d471a13";
                        m_AllEEL["EE_Bloodrager_F_Any"] = "2072db411b232024daf6fbfac1001065";
                        m_AllEEL["EE_Bloodrager_M_Any"] = "f26d20fbaedf1374388c75d7ab1d9ecc";
                        m_AllEEL["EE_Body01Lann_M_MM"] = "0a6c67d147709254cae8b4419fd9e241";
                        m_AllEEL["EE_Body01Wenduag_F_MM"] = "e0f5bf31457bbe946b84747bb9135112";
                        m_AllEEL["EE_Body01_F_AA"] = "ac46134d29a016e48b7655a56082dfe4";
                        m_AllEEL["EE_Body01_F_CM"] = "c6cb39ed3c0025f44b7b180fd01f3de3";
                        m_AllEEL["EE_Body01_F_DE"] = "87e529e61ef33f543848e01101fbd15c";
                        m_AllEEL["EE_Body01_F_DH"] = "ff3b4d7716516604aa46adb2443de3cf";
                        m_AllEEL["EE_Body01_F_DW"] = "082755264a7d4ab43888645cfeef2e43";
                        m_AllEEL["EE_Body01_F_EL"] = "86acde9747d561649919dd1117cd858c";
                        m_AllEEL["EE_Body01_F_GN"] = "e969fe61ab898284ebeb387accb994d9";
                        m_AllEEL["EE_Body01_F_HE"] = "861171cdd3930a84faab08ab85ba924a";
                        m_AllEEL["EE_Body01_F_HL"] = "50a2873551cf0db4cb603811a78b6804";
                        m_AllEEL["EE_Body01_F_HM"] = "bb6988a21733fad4296ad22537248fea";
                        m_AllEEL["EE_Body01_F_HO"] = "015525c32d965a2469ed9e39407d7d4c";
                        m_AllEEL["EE_Body01_F_KT"] = "d6a8c155406a4a145bd5a7830546bf95";
                        m_AllEEL["EE_Body01_F_OD"] = "72a2b0012493ac7428268482e0622964";
                        m_AllEEL["EE_Body01_F_SN"] = "01d291ceab015134da06307f7a6bdc14";
                        m_AllEEL["EE_Body01_F_SU"] = "8b143bbe34949f847b6bb076a9950856";
                        m_AllEEL["EE_Body01_F_TL"] = "6a5ae89de41b6b149856718b6058168f";
                        m_AllEEL["EE_Body01_F_ZB"] = "2d41501a76df73749aa0daa034519cd2";
                        m_AllEEL["EE_Body01_M_AA"] = "7b29a65cd4e83cb47b3c6394c51c7e4f";
                        m_AllEEL["EE_Body01_M_CM"] = "a2231bab98681e04b90d5b4a88a9a2fc";
                        m_AllEEL["EE_Body01_M_DE"] = "eeda8e7eac9ae494da2111f7bb968eab";
                        m_AllEEL["EE_Body01_M_DH"] = "d697db672cb123d4fa5cf47baafc8d41";
                        m_AllEEL["EE_Body01_M_DW"] = "f097c18b5fcd269468e19243a62786c0";
                        m_AllEEL["EE_Body01_M_EL"] = "14f3db044cb98ad4ab0ed4edfba5b6d6";
                        m_AllEEL["EE_Body01_M_GL"] = "8ddec2f3c8c0a11488145d838aeeab8a";
                        m_AllEEL["EE_Body01_M_GN"] = "ac15d06e7975ca74b970783daaef9b60";
                        m_AllEEL["EE_Body01_M_HE"] = "39763b45a3c0ff94ea6acbba28024168";
                        m_AllEEL["EE_Body01_M_HL"] = "d509ad2a15110a34cb793fec7c26214c";
                        m_AllEEL["EE_Body01_M_HM"] = "e7c86166041c1e04a92276abdab68afa";
                        m_AllEEL["EE_Body01_M_HO"] = "a72a342c0edebb646b5a62b98c06a796";
                        m_AllEEL["EE_Body01_M_KT"] = "5dc43dd33c0718c48a3e77481e2b292f";
                        m_AllEEL["EE_Body01_M_OD"] = "94079bf268731a046acbf917243e985d";
                        m_AllEEL["EE_Body01_M_SN"] = "680b808a2061e1f43a4d1a1e5a2f3e0e";
                        m_AllEEL["EE_Body01_M_SU"] = "204fe6af48eafa046a08bb30c0037b85";
                        m_AllEEL["EE_Body01_M_TL"] = "d1dcf6b4e326a9d459ee5b2e5b7b7cbc";
                        m_AllEEL["EE_Body01_M_ZB"] = "86b7f3a0ca19f4d41aa356b85f68ce02";
                        m_AllEEL["EE_Body02Alderpash_M_HM"] = "0db942005eda27846b946811cf49511e";
                        m_AllEEL["EE_Body02Ember_F_EL"] = "6b85ff84e749ead4ab03727b187cb703";
                        m_AllEEL["EE_Body02_F_SU"] = "c50b6405a02dc4d42a9434453c08bfee";
                        m_AllEEL["EE_Body03_F_SU"] = "26b482d7d98fac34a8edbbf8d3d1f654";
                        m_AllEEL["EE_Body12Coloxus_M_HM"] = "543b78c9d5363934e88d8478567ec054";
                        m_AllEEL["EE_Body12Eritrice_F_KT"] = "69734cf7079748a4d9b51f2e3962638a";
                        m_AllEEL["EE_Body13Izyagna_F_HM"] = "e89fbade867df104b9d21e24d1c83ff0";
                        m_AllEEL["EE_BootsArcanist_F_Any"] = "6476478062b12c64092636f113f87738";
                        m_AllEEL["EE_BootsArcanist_M_Any"] = "5c12c46f19237c8419735b6c85e7dc12";
                        m_AllEEL["EE_BootsBard_F_Any"] = "b1f5d18904329b6469e451265fb52adf";
                        m_AllEEL["EE_BootsBard_M_Any"] = "82784524280cfb34486d3c55d2520849";
                        m_AllEEL["EE_BootsBloodrager_F_Any"] = "37f6d6b810e384143917c0f80a270097";
                        m_AllEEL["EE_BootsBloodrager_M_Any"] = "f58a0cdfd40fd3d4394d9a3eb7f51b74";
                        m_AllEEL["EE_BootsCavalier_F_Any"] = "ca8d7de0b1aa04a4ba1a17ab0888e13c";
                        m_AllEEL["EE_BootsCavalier_M_Any"] = "a013b42d3e25d5a4c9f05f5b31c97c7d";
                        m_AllEEL["EE_BootsEvil_F_Any"] = "ab8e8ce3c14783446a85bef3411e1e17";
                        m_AllEEL["EE_BootsEvil_M_Any"] = "29ea6e3f22dd82c48857ea147cdc59c9";
                        m_AllEEL["EE_BootsFarmer1_F_Any"] = "3e48a78eeb463714190f13690cc324b3";
                        m_AllEEL["EE_BootsFarmer1_M_Any"] = "39eb05440ffc1f54da3c96e4a97fb791";
                        m_AllEEL["EE_BootsFarmer2_F_Any"] = "619277e51ab035d40ad7657f7073e87e";
                        m_AllEEL["EE_BootsFarmer2_M_Any"] = "4bbe922ea497a114fab6270692b374a0";
                        m_AllEEL["EE_BootsHunter_F_Any"] = "7dc0c86ee5d0f00419ac0f648e0e9e1e";
                        m_AllEEL["EE_BootsHunter_M_Any"] = "8b169aa9498b48c4b83aa8d8a73bd07c";
                        m_AllEEL["EE_BootsInquisitor_F_Any"] = "0dcee358458bb8648818291b0a3ede87";
                        m_AllEEL["EE_BootsInquisitor_M_Any"] = "84ff34b1ce1f4c74091ee4c7201183da";
                        m_AllEEL["EE_BootsManticore_F_Any"] = "5fd09c522486b804babd7977d0591c1f";
                        m_AllEEL["EE_BootsManticore_M_Any"] = "ceee47404e17612489b792a839f5dedb";
                        m_AllEEL["EE_BootsNecroVillain_F_Any"] = "b2eb00625c2b2244794bb3799ef92254";
                        m_AllEEL["EE_BootsNecroVillain_M_Any"] = "fde7ca57fddb76b49a4b98d4b7edf574";
                        m_AllEEL["EE_BootsNoble1_F_Any"] = "00ec47b850058234780755aa3ad0cfea";
                        m_AllEEL["EE_BootsNoble1_M_Any"] = "69825ce8033c43e449ee45943f428507";
                        m_AllEEL["EE_BootsNoble2_F_Any"] = "5bf7c2ec0a43599469d57858374af3a5";
                        m_AllEEL["EE_BootsNoble2_M_Any"] = "ddd37df6f8fe5fe4c980fc199bd421b8";
                        m_AllEEL["EE_BootsPaladin_F_Any"] = "836541140f9c715439b7ee32de25e7f8";
                        m_AllEEL["EE_BootsPaladin_M_Any"] = "6ad8ab625871ecd46bf7363633f72eca";
                        m_AllEEL["EE_BootsRanger_F_Any"] = "f2d063de637898e4ebea6d932ba63648";
                        m_AllEEL["EE_BootsRanger_M_Any"] = "be097ddf5dec7d24792f4d3be7739150";
                        m_AllEEL["EE_BootsRogue_F_Any"] = "f14e62330602c3b4da0073fa369ea2d1";
                        m_AllEEL["EE_BootsRogue_M_Any"] = "2b98f602a6b41ba45b20b8b34b045a89";
                        m_AllEEL["EE_BootsSandals_F_Any"] = "b7802e0710b09264eaf6db3d08c124ee";
                        m_AllEEL["EE_BootsSandals_M_Any"] = "a1d0fa7cbc16b004fbbe0f85adf8aa61";
                        m_AllEEL["EE_BootsShaman_F_Any"] = "94851eb712f56234db731aa4275e1a6a";
                        m_AllEEL["EE_BootsShaman_M_Any"] = "138fcb0672996c14aaffe7fb252b7830";
                        m_AllEEL["EE_BootsSkald_F_Any"] = "eb75a7fe7632c6f48b03d3b47b6baa4c";
                        m_AllEEL["EE_BootsSkald_M_Any"] = "c68a442f3b2126a43ad8ef3a26fb09f5";
                        m_AllEEL["EE_BootsSlayer_F_Any"] = "ac96923991bd6994f85960aca84f41db";
                        m_AllEEL["EE_BootsSorcerer_F_Any"] = "ef4aa2f0e097c6b438de2e360b46fec5";
                        m_AllEEL["EE_BootsSorcerer_M_Any"] = "9baad8d02adf3154db03d42411fcf50f";
                        m_AllEEL["EE_BootsWarpriest_F_Any"] = "2fa8d66f0cc43d74e874cec3a0384780";
                        m_AllEEL["EE_BootsWarpriest_M_Any"] = "35c3d4e6709a3734fa6e9cffa6203585";
                        m_AllEEL["EE_BootsWithSteelGood_F_Any"] = "683a9da928e968c4cb13ffb6534d474c";
                        m_AllEEL["EE_BootsWithSteelGood_M_Any"] = "51bdabc594549d24c8ef97d0f1977ce1";
                        m_AllEEL["EE_BootsWolfPelt_F_Any"] = "973d65136eb36a84aba0225fc7fb764b";
                        m_AllEEL["EE_BootsWolfPelt_M_Any"] = "aa629e4c14c44e3429452e66eb7a01a0";
                        m_AllEEL["EE_BracersArcaneGoldGems_F_Any"] = "a244ccb4775ad5442ad9016f678ffb29";
                        m_AllEEL["EE_BracersArcaneGoldGems_M_Any"] = "433bec65ed996914d82c4f57319fe666";
                        m_AllEEL["EE_BracersArcaneNormalGems_F_Any"] = "46e54382f318c6b429862aaa8e351a98";
                        m_AllEEL["EE_BracersArcaneNormalGems_M_Any"] = "fd60b79f75c0897448135f5ba4ad5064";
                        m_AllEEL["EE_BracersArcaneSilverGems_F_Any"] = "5e781bc14ffb9164582254cf83ab4c33";
                        m_AllEEL["EE_BracersArcaneSilverGems_M_Any"] = "13fee5866674e0f4fbd0c97297fca1fe";
                        m_AllEEL["EE_BracersArchery_F_Any"] = "f01138e42b0c52542876cd017d4a4045";
                        m_AllEEL["EE_BracersArchery_M_Any"] = "4a5c0e780c0455c43931051342931656";
                        m_AllEEL["EE_BracersCavalier_F_Any"] = "6ce79932eb9172840bbc97a4ff915460";
                        m_AllEEL["EE_BracersKineticist_F_Any"] = "16538d4eb80be624f99fc13b916781fa";
                        m_AllEEL["EE_BracersKineticist_M_Any"] = "bc570740e21a19541b1ea42f6ad971bf";
                        m_AllEEL["EE_BracersLeather_F_Any"] = "a2b14f6cdb114904bb72a2ae8e5f8447";
                        m_AllEEL["EE_BracersLeather_M_Any"] = "0153de2afe0e0f843bb872320327b9bf";
                        m_AllEEL["EE_BracersOrnate1_F_Any"] = "3a8003609905aef4ea8a413ef883feb1";
                        m_AllEEL["EE_BracersOrnate1_M_Any"] = "90757cf5f6b4dfe4095b522c702c5be7";
                        m_AllEEL["EE_BracersOrnate2_F_Any"] = "ce527beb17fb00845b8dc5f121c3f0d9";
                        m_AllEEL["EE_BracersOrnate2_M_Any"] = "2f38f3c1129a4744bb6250a61d22a319";
                        m_AllEEL["EE_BreastplateAreshkagal_F_Any"] = "3eaa7678d451d8042bb06f4f852c5efb";
                        m_AllEEL["EE_BreastplateAreshkagal_M_Any"] = "8639c7dc4ccac9a43ad3d4105fe9e2c4";
                        m_AllEEL["EE_BreastplateDragonhideChainmail_F_Any"] = "bf70af9d92e07e24e8948cb601dd7e2b";
                        m_AllEEL["EE_BreastplateDragonhideChainmail_M_Any"] = "6f888641f537dfd4baf54c7274de9637";
                        m_AllEEL["EE_BreastplateDragonhideNormal_F_Any"] = "4afb4a47301cbdf48aa4cd0f55195ed5";
                        m_AllEEL["EE_BreastplateDragonhideNormal_M_Any"] = "717ef99dc1f0bb84ebcb91c89797563c";
                        m_AllEEL["EE_BreastplateHellknight_F_Any"] = "b27b45c4e26dfe04cb4854f17d9c8a35";
                        m_AllEEL["EE_BreastplateHellknight_M_Any"] = "d7fab60b4b847d44a8092aa82195c094";
                        m_AllEEL["EE_BreastplateHoly_F_Any"] = "92814031aa847d24fbe3f95705a7f775";
                        m_AllEEL["EE_BreastplateHoly_M_Any"] = "0664adfcb215a864bbc8c9ea93be9656";
                        m_AllEEL["EE_BreastplateKnightAdamantite_F_Any"] = "ebcb6c3e93730244a9f97ad5aa153fef";
                        m_AllEEL["EE_BreastplateKnightAdamantite_M_Any"] = "9b665033c1529974c81cffe032979314";
                        m_AllEEL["EE_BreastplateKnightPainted_F_Any"] = "897536b66d22b514c97ebaa48bf7ba3a";
                        m_AllEEL["EE_BreastplateKnightPainted_M_Any"] = "625dad930029f5845a3a4f6924276441";
                        m_AllEEL["EE_BreastplateKnight_F_Any"] = "3e56acb412da1184ca7eb62b25108f64";
                        m_AllEEL["EE_BreastplateKnight_M_Any"] = "d92f8b4f9e84fa846b7b5d8dcfa48f79";
                        m_AllEEL["EE_BreastplateMongrel_F_Any"] = "c9a3c8e71d1b038498c27674a0ad3e14";
                        m_AllEEL["EE_BreastplateMongrel_M_Any"] = "c07cea3147c1f4142993477211932460";
                        m_AllEEL["EE_BreastplateSarkorianWedding_F_Any"] = "92bb80f31526042458226791eb7bf132";
                        m_AllEEL["EE_BreastplateSarkorianWedding_M_Any"] = "78e4298d7c3161c46a44b6aa886a061a";
                        m_AllEEL["EE_Brows01Lann_M_MM"] = "bb3f983be9db37548a7edfc6e901dd6f";
                        m_AllEEL["EE_Brows01Wenduag_F_MM"] = "1a17bfa0558d7344589afc58cb78a119";
                        m_AllEEL["EE_Brows01_F_AA"] = "1e0d27717796f3e4bbaf3792dbb61b0d";
                        m_AllEEL["EE_Brows01_F_CM"] = "6ddf1cc3e70191d4686459521c724d49";
                        m_AllEEL["EE_Brows01_F_DE"] = "f598cc13f1dc94d4a9acf093eb8ea0b0";
                        m_AllEEL["EE_Brows01_F_DH"] = "8c6574534f66ee747980e7ea0d7ffce5";
                        m_AllEEL["EE_Brows01_F_DW"] = "4d07fd1f9d98e9c46a0c9d8072ff65ba";
                        m_AllEEL["EE_Brows01_F_EL"] = "fe9084a6ec2bfed488b6cf1725c7e253";
                        m_AllEEL["EE_Brows01_F_GN"] = "d4748c73843f7c043a9ab543bde726e4";
                        m_AllEEL["EE_Brows01_F_HE"] = "9ab83dd3a06ba6a4e97900bd6ffc4aab";
                        m_AllEEL["EE_Brows01_F_HL"] = "39470782db146664ba5e1f9bc40ca72a";
                        m_AllEEL["EE_Brows01_F_HM"] = "6831469a4e2bc664f9622bdfbf5ed30c";
                        m_AllEEL["EE_Brows01_F_HO"] = "1ccdb39c81af9694790d6bc20743a0f5";
                        m_AllEEL["EE_Brows01_F_SU"] = "77edca23b8aef374a9e5a8c13ffd7756";
                        m_AllEEL["EE_Brows01_F_TL"] = "276a3a4bbe9fe144f827f7272a1fa6c7";
                        m_AllEEL["EE_Brows01_M_AA"] = "dc8f099f7ef1cc042992f12e5109add5";
                        m_AllEEL["EE_Brows01_M_CM"] = "b4e2f0626da730d4d94a155ca188fd95";
                        m_AllEEL["EE_Brows01_M_DE"] = "0ea8de7e3b2864b4286a732993f8e0a9";
                        m_AllEEL["EE_Brows01_M_DH"] = "5e91fa83718b67c409a01a909937fcab";
                        m_AllEEL["EE_Brows01_M_DW"] = "92f96b292effb144ba17e21b8daac640";
                        m_AllEEL["EE_Brows01_M_EL"] = "56d8927affad74740ae89e4f5de90abc";
                        m_AllEEL["EE_Brows01_M_GN"] = "01db8d0023ab2bf4b88074c597308ea6";
                        m_AllEEL["EE_Brows01_M_HE"] = "b82a9602d003e1544a3550cde6a8c8e5";
                        m_AllEEL["EE_Brows01_M_HL"] = "9f1f143f59215914daff8dbab9ae1f7e";
                        m_AllEEL["EE_Brows01_M_HM"] = "3714b9226b0ca694185f1dfc58163aac";
                        m_AllEEL["EE_Brows01_M_HO"] = "9849deaae24f2b6478d4f8794dcda047";
                        m_AllEEL["EE_Brows01_M_SU"] = "3d62ae4d0948f7640a4473712e19a5a3";
                        m_AllEEL["EE_Brows01_M_TL"] = "00052c3a98ee9ec4895609b6d28d18ef";
                        m_AllEEL["EE_Brows02Afro_F_AA"] = "41130f28b3ef6ea4ea9846f338cf8d1f";
                        m_AllEEL["EE_Brows02Afro_F_DH"] = "e617d3b1be3182c498a01d1facdcc697";
                        m_AllEEL["EE_Brows02Afro_F_HE"] = "f96271a54ae6e7647b48d80c7de02b24";
                        m_AllEEL["EE_Brows02Afro_F_HM"] = "9cd0bfaa032d25c42b796427d261e67b";
                        m_AllEEL["EE_Brows02Afro_M_DH"] = "0b2d657e028cf1d4ca0d0a8587de3819";
                        m_AllEEL["EE_Brows02Afro_M_HE"] = "1f09bfef10b84b146aa88f59da36d092";
                        m_AllEEL["EE_Brows02Arueshalae_F_SU"] = "56215222c6c7f794591cd4b73fe37272";
                        m_AllEEL["EE_Brows02Asian_F_HO"] = "102b124fd26468140a92c57b85d61562";
                        m_AllEEL["EE_Brows02Asian_M_HO"] = "ac745cba03847ca4aaf5d9776108a8a7";
                        m_AllEEL["EE_Brows02Ember_F_EL"] = "e1889a03b6d90c04eacd2ce26d18eb0d";
                        m_AllEEL["EE_Brows02Greybor_M_DW"] = "61e871bb560a1dc4d9145362098d113e";
                        m_AllEEL["EE_Brows02Nurah_F_HL"] = "437992ffe3b608c4f861e340534ee314";
                        m_AllEEL["EE_Brows02Regill_M_GN"] = "9a0353226ccfc8141a47d16dfe0661f7";
                        m_AllEEL["EE_Brows02Sosiel_M_HM"] = "270c431237f65f74e95eb54939c313bf";
                        m_AllEEL["EE_Brows02_F_DW"] = "58cb30915ae848f4984419c590a97d74";
                        m_AllEEL["EE_Brows02_F_GN"] = "151f92f446d8a7c48b4244593f4b1cd7";
                        m_AllEEL["EE_Brows02_F_TL"] = "b4faf5b1eab446b4a8b2302a2361d87d";
                        m_AllEEL["EE_Brows02_M_AA"] = "8d45a4dd7f6df0c4eb5a9582bd905c3e";
                        m_AllEEL["EE_Brows02_M_EL"] = "c3f169e6fd2eed94f81772b58f3f13d1";
                        m_AllEEL["EE_Brows02_M_HL"] = "88bc58acd74141d4fadda6188a985b0a";
                        m_AllEEL["EE_Brows02_M_TL"] = "838af06479607e84b9edca8d98dd1160";
                        m_AllEEL["EE_Brows03Asian_M_HE"] = "98e46229e9e796946a3c9acc90a3ee1c";
                        m_AllEEL["EE_Brows03Camellia_F_HE"] = "3369b3b2213d6e94c941f969c8d97bf7";
                        m_AllEEL["EE_Brows03Staunton_M_DW"] = "e8d14984946d1b64abb99ba4544bba78";
                        m_AllEEL["EE_Brows03_F_AA"] = "4dcdd5efed224624484fdea320ff329b";
                        m_AllEEL["EE_Brows03_F_DH"] = "a9c3f093262a95745a6b58ee476191b6";
                        m_AllEEL["EE_Brows03_F_DW"] = "faefa3fce49b3534cbc8b0ed18ed5a3f";
                        m_AllEEL["EE_Brows03_F_EL"] = "f1340bf789a73934088c1ce34599a270";
                        m_AllEEL["EE_Brows03_F_GN"] = "b6dd187900e97c94baacdf6c684b48ea";
                        m_AllEEL["EE_Brows03_F_HL"] = "9854be21a9201484199d9315fe02d9a5";
                        m_AllEEL["EE_Brows03_F_HM"] = "01eba6553e2c24e45a81ded03dd6b09e";
                        m_AllEEL["EE_Brows03_F_HO"] = "5b73b8cf5a405c9488c9cea45293cc36";
                        m_AllEEL["EE_Brows03_M_AA"] = "3dcf04b16d360df44a5432c6b6cb103c";
                        m_AllEEL["EE_Brows03_M_DH"] = "45eff2b0d6b630e458ef569b1b6fde55";
                        m_AllEEL["EE_Brows03_M_EL"] = "d6da687ba68840541a8977b8284d6789";
                        m_AllEEL["EE_Brows03_M_GN"] = "4f49fbc23474a6a48a5456a7989f01c7";
                        m_AllEEL["EE_Brows03_M_HL"] = "ecbabb4400df5d24d9bbc2d032012089";
                        m_AllEEL["EE_Brows03_M_HM"] = "ffa5f7d7ba3c1e949b856bd93c0fd657";
                        m_AllEEL["EE_Brows03_M_HO"] = "ae7cdb7061d05ba478b9535b55a02cc5";
                        m_AllEEL["EE_Brows04Daeran_M_AA"] = "82378e98f50571f40a27188815b12973";
                        m_AllEEL["EE_Brows04_F_AA"] = "5dec26ebb4834654f898fb4cd55dcdee";
                        m_AllEEL["EE_Brows04_F_DH"] = "c3ce95b7a9dc3ea4b9563453695549b4";
                        m_AllEEL["EE_Brows04_F_DW"] = "f1a20de29d5436d4b888ccf01983a473";
                        m_AllEEL["EE_Brows04_F_EL"] = "bf289900e6a57c44bae4f7581ad7f289";
                        m_AllEEL["EE_Brows04_F_GN"] = "654280777dff35844b14c3288ee8ca01";
                        m_AllEEL["EE_Brows04_F_HE"] = "6f7acd99fe1abae4090c6639db5cffa7";
                        m_AllEEL["EE_Brows04_F_HL"] = "4dea70c2dcbe09f4596810dcabf7f445";
                        m_AllEEL["EE_Brows04_F_HM"] = "59b3949189a066d4298a6b39348613e9";
                        m_AllEEL["EE_Brows04_F_HO"] = "d041597dc58f55449a829fb6ecb3100c";
                        m_AllEEL["EE_Brows04_F_TL"] = "7de258962ecb55c4395c765cf7366fbe";
                        m_AllEEL["EE_Brows04_M_DH"] = "b86e75a3df830d849ab774378ae57fe3";
                        m_AllEEL["EE_Brows04_M_DW"] = "be51b91f9455b9146a89a9395d2596d4";
                        m_AllEEL["EE_Brows04_M_EL"] = "e80a6c95f76795645aa711ca0efe35ca";
                        m_AllEEL["EE_Brows04_M_GN"] = "2e62c703933eeff4da16c1e13961f80b";
                        m_AllEEL["EE_Brows04_M_HE"] = "50bcb93eda325cd4495450f18c2c46fb";
                        m_AllEEL["EE_Brows04_M_HL"] = "92533b3da37e53749a848dde423b1b24";
                        m_AllEEL["EE_Brows04_M_HM"] = "d5de06117150dfd49996c1b39a6f911e";
                        m_AllEEL["EE_Brows04_M_HO"] = "b4983242e32e12a449cdc22ea37b40fd";
                        m_AllEEL["EE_Brows04_M_TL"] = "eb97a0ecfd0a0bd48ba45d219c68d1e6";
                        m_AllEEL["EE_Brows05_F_AA"] = "cbaaf10cdc136e84f94bae8ec29032ff";
                        m_AllEEL["EE_Brows05_F_DH"] = "571ba7d00807ff4439fe6db76686eba1";
                        m_AllEEL["EE_Brows05_F_EL"] = "823b2b37bcb1cb6459f49bafb727732b";
                        m_AllEEL["EE_Brows05_F_HE"] = "d8c177c3e8c4cfc46af6baa9f7428713";
                        m_AllEEL["EE_Brows05_F_HM"] = "22f54993daa9e2445b8d66ae3858fa1c";
                        m_AllEEL["EE_Brows05_F_TL"] = "998915befac22674eaddf4835f729c38";
                        m_AllEEL["EE_Brows05_M_AA"] = "d315403539f52124c90083d85671eefb";
                        m_AllEEL["EE_Brows05_M_DH"] = "99bffb6e508ca434d90147d54697efb6";
                        m_AllEEL["EE_Brows05_M_EL"] = "566c9cfc44946594db072e299492339e";
                        m_AllEEL["EE_Brows05_M_HE"] = "5011840583080194ea35775400824188";
                        m_AllEEL["EE_Brows05_M_HM"] = "bdcb8b8da31f11d4d9ab509b6da6798c";
                        m_AllEEL["EE_Brows05_M_TL"] = "1b5f6042c3a8c6c45a7d9bd0aa5dcd60";
                        m_AllEEL["EE_Brows06_F_DH"] = "864393b05164ced46b79746e25155df5";
                        m_AllEEL["EE_Brows06_F_HM"] = "1b65443c1d64a5e498ba5a0978448694";
                        m_AllEEL["EE_Brows06_M_AA"] = "b6a8ce12ecfd73a478fff585e9cdd0e2";
                        m_AllEEL["EE_Brows06_M_DH"] = "0a5b99acf89b86042aa8fb605edc9225";
                        m_AllEEL["EE_Brows06_M_HM"] = "29d1fcc93ce41134fa0a3b658c309fc0";
                        m_AllEEL["EE_Brows07_F_HM"] = "0e10c614b32622f42aba82f5392ef69f";
                        m_AllEEL["EE_Brows07_M_HM"] = "1715ed841a65d3a47a83b0ece039fa02";
                        m_AllEEL["EE_Brows08_F_HM"] = "c21fdfab8811f854e9c6a30c86963049";
                        m_AllEEL["EE_Brows08_M_HM"] = "e3597c16fff925448b82e661dab5b00b";
                        m_AllEEL["EE_Brows09Seelah_F_HM"] = "97144baadf90eaf4b8ae10551d9e94e8";
                        m_AllEEL["EE_Brows09_M_HM"] = "b4f44953d39f5cb4ab5c291935d5e374";
                        m_AllEEL["EE_Brows10_F_HM"] = "c2f29b22df77f0542b6e4a72b323858c";
                        m_AllEEL["EE_Brows10_M_HM"] = "a049bb2e2fc4c2444ac5a76adfd8da96";
                        m_AllEEL["EE_Brows11Nenio_F_HM"] = "44e188f73b4f8684d945d61766cfcfef";
                        m_AllEEL["EE_Brows11_M_HM"] = "3fdfe900b2f16c142a23a707ed8b28ad";
                        m_AllEEL["EE_Brows13_M_HM"] = "1122ed4b4494a884f922245c5bf1cd50";
                        m_AllEEL["EE_Brows14_F_HM"] = "0671abed8e0232d42adbe7cc006334bb";
                        m_AllEEL["EE_CapeTabard_F_Any"] = "6d61987c6621ddd40892c5ff404313b0";
                        m_AllEEL["EE_CapeTabard_M_Any"] = "161b74f7113ffe14cb9b34f46404cf77";
                        m_AllEEL["EE_CavalierAccessories_F_Any"] = "b2fe1a2aa7dd60c41a185dfdfca2e33f";
                        m_AllEEL["EE_CavalierAccessories_M_Any"] = "e2e011242ea29fd4593d6cfbd06c8a2b";
                        m_AllEEL["EE_Cavalier_F_Any"] = "46d879e169045334190df70681415f35";
                        m_AllEEL["EE_Cavalier_M_Any"] = "ae1e5e4fc4163094ba8bc06dec79d325";
                        m_AllEEL["EE_ChainmailArmyNormal_F_Any"] = "eea9f06aae895ff48a02103642a9ff96";
                        m_AllEEL["EE_ChainmailArmyNormal_M_Any"] = "25ace4e03ea9cd24390c0b6cc5a70aa2";
                        m_AllEEL["EE_ChainmailArmyPainted_F_Any"] = "89e928d30d99ffd48a7fcb5c2798c587";
                        m_AllEEL["EE_ChainmailArmyPainted_M_Any"] = "7fb5ce4f721c54544b6d5c13c1705d3b";
                        m_AllEEL["EE_ChainmailCelestialMagic_F_Any"] = "6c66d95f31d3092499b535d92269ed9f";
                        m_AllEEL["EE_ChainmailCelestialMagic_M_Any"] = "a9269c91314a0694e8b89520bcc7cd4c";
                        m_AllEEL["EE_ChainmailCleric_F_Any"] = "f55e3f23c04161e448b6189eb588a6f9";
                        m_AllEEL["EE_ChainmailCleric_M_Any"] = "fbc313f3336beee43b1fa0345134b0ae";
                        m_AllEEL["EE_ChainmailEvilClericOrnate_F_Any"] = "847240a3277159447afce8a796530458";
                        m_AllEEL["EE_ChainmailEvilClericOrnate_M_Any"] = "967379cbc31d12d40a9e59c0e47d6bf8";
                        m_AllEEL["EE_ChainmailKiller_F_Any"] = "33cb53066508a2d4fb80a2bf6ff34004";
                        m_AllEEL["EE_ChainmailKiller_M_Any"] = "81ebfbdecc8546d42aafc79eda0fc285";
                        m_AllEEL["EE_ChainmailKnightNormal_F_Any"] = "981020597cbf1bf47aa61459a965958d";
                        m_AllEEL["EE_ChainmailKnightNormal_M_Any"] = "bc96ff1eb3b70c54ba82108834176736";
                        m_AllEEL["EE_ChainmailKnightTabard_F_Any"] = "7780a7e881ec5304ca723783e9172a76";
                        m_AllEEL["EE_ChainmailKnightTabard_M_Any"] = "d04ccd7f55d320b4bb448fa336ee2b04";
                        m_AllEEL["EE_ChainshirtArmy_F_Any"] = "589d6e94f2b550c42a75967e2b3734b0";
                        m_AllEEL["EE_ChainshirtArmy_M_Any"] = "d6bc55184e2adbc42b0bf01c186a9449";
                        m_AllEEL["EE_ChainshirtAssassin_F_Any"] = "bdc3700451b52f946ba534996e297f67";
                        m_AllEEL["EE_ChainshirtAssassin_M_Any"] = "ca4ed21de7fb70648b2163643c7ba60d";
                        m_AllEEL["EE_ChainshirtEvil_F_Any"] = "588fe25b8983e2c40bc8a7d508ac4ac2";
                        m_AllEEL["EE_ChainshirtEvil_M_Any"] = "9f183152b2a0e6947a0074e7b37eb754";
                        m_AllEEL["EE_ChainshirtHolyMithral_F_Any"] = "5dc87f7abe53ef5489c063a0e7ae2efc";
                        m_AllEEL["EE_ChainshirtHolyMithral_M_Any"] = "796228687af488a4c90d2cecc8c6f0cb";
                        m_AllEEL["EE_ChainshirtNoble_F_Any"] = "b8993b9688ba4604f9781ed8bd68a1d0";
                        m_AllEEL["EE_ChainshirtNoble_M_Any"] = "d6a6d6d1cf11b174788d94de78360a3d";
                        m_AllEEL["EE_Citizen1_F_Any"] = "219251ace928b22498163da6c4bcf3d1";
                        m_AllEEL["EE_Citizen1_M_Any"] = "6a1c5dbf0b40e724e937dc7e703404f8";
                        m_AllEEL["EE_ClericAccessories_F_Any"] = "27ead771b0074234186b6f790929f081";
                        m_AllEEL["EE_ClericAccessories_M_Any"] = "dc7275260882bda469d53d7f2f1a73d1";
                        m_AllEEL["EE_Cleric_F_Any"] = "3e695b3fbf0bba746b1296135f757212";
                        m_AllEEL["EE_Cleric_M_Any"] = "78c8676607b0f604488531c4a00f5ae0";
                        m_AllEEL["EE_CloakArcane1Colored_F_Any"] = "f9729fe1eef65ba41b26d514e77f0e20";
                        m_AllEEL["EE_CloakArcane1Colored_M_Any"] = "4fa39b79b485e6c44bfe448c7dd811f7";
                        m_AllEEL["EE_CloakArcane1_F_Any"] = "4470c0a8b91b62147b444f0bcf6070fc";
                        m_AllEEL["EE_CloakArcane1_M_Any"] = "9b6b10f303b170d4692401962702af30";
                        m_AllEEL["EE_CloakArcane2Colored_F_Any"] = "35490a96d9b86e548b346798e0fc3bb2";
                        m_AllEEL["EE_CloakArcane2Colored_M_Any"] = "4fbd2afc435411240925743c3aee5295";
                        m_AllEEL["EE_CloakArcane2_F_Any"] = "8eda078a8711e2e4f90196d90791a2a7";
                        m_AllEEL["EE_CloakArcane2_M_Any"] = "6a6d16dc0a49a064a943ba0d9ea66689";
                        m_AllEEL["EE_CloakBacker1White_F_Any"] = "c65822dc9b2ecca46bad6990c5355f25";
                        m_AllEEL["EE_CloakBacker1White_M_Any"] = "364970997fe2b244fb164a29f6faf551";
                        m_AllEEL["EE_CloakBacker1_F_Any"] = "564ef6d16f95fa440b1351052f021452";
                        m_AllEEL["EE_CloakBacker1_M_Any"] = "a4bb4dc686dd9314c96380335d91fbc9";
                        m_AllEEL["EE_CloakBacker2_F_Any"] = "83ce3e2605a019b4abd941ef8693cd89";
                        m_AllEEL["EE_CloakBacker2_M_Any"] = "f6340111cdacf1b41b5e9afb5c369777";
                        m_AllEEL["EE_CloakDemonicCultistCasterColored_F_Any"] = "cb281cf73613dcf47820f63fc4447e0c";
                        m_AllEEL["EE_CloakDemonicCultistCasterColored_M_Any"] = "7434876d4c63a2f4b8151c978c8efea3";
                        m_AllEEL["EE_CloakDemonicCultistCasterDeskari_F_Any"] = "024a975305c26af4f9a7ce0edffb4696";
                        m_AllEEL["EE_CloakDemonicCultistCasterDeskari_M_Any"] = "f8737ea2a1bf1d04c9a18c125d9444de";
                        m_AllEEL["EE_CloakDemonicCultistCasterRed_F_Any"] = "a5bcadc067b0f2b40a62e775f59252bb";
                        m_AllEEL["EE_CloakDemonicCultistCasterRed_M_Any"] = "c424fb1902e9fb94db8fff43869f0774";
                        m_AllEEL["EE_CloakInquisitor_F_Any"] = "76d0c86b205084a4aac49fbd51db03ea";
                        m_AllEEL["EE_CloakInquisitor_M_Any"] = "03fdfd7c62386b64a9ea8a98006c6065";
                        m_AllEEL["EE_CloakKnight1_F_Any"] = "eabf563cc0ab3564aa010740231b2ae3";
                        m_AllEEL["EE_CloakKnight1_M_Any"] = "3effc60ab1880834e9e12f90d24325c4";
                        m_AllEEL["EE_CloakKnight2_F_Any"] = "ab0b4a08188f539419bcc84a8df0dcde";
                        m_AllEEL["EE_CloakKnight2_M_Any"] = "61dfc94eca2c9fb45ac2cab3604ca3ce";
                        m_AllEEL["EE_CloakMythicAeon_F_Any"] = "aa10ca268ca261e4e88e307e469cf01d";
                        m_AllEEL["EE_CloakMythicAeon_M_Any"] = "4d706c76c5eecb1458d5aa391fe02398";
                        m_AllEEL["EE_CloakMythicAngel_F_Any"] = "005066051ce536545a4a86c425421e21";
                        m_AllEEL["EE_CloakMythicAngel_M_Any"] = "4757421cef25f054a8aa43c4ad3540b7";
                        m_AllEEL["EE_CloakMythicAzata_F_Any"] = "51af24aa2446ba64e8ca919192036369";
                        m_AllEEL["EE_CloakMythicAzata_M_Any"] = "7d65def281f52e4458f5d82f4fdd1844";
                        m_AllEEL["EE_CloakMythicDemon_F_Any"] = "0174c068a88e5d945ab2caaf46d222fe";
                        m_AllEEL["EE_CloakMythicDemon_M_Any"] = "e4c5afab5ffd18841b02d69f4ad20f8f";
                        m_AllEEL["EE_CloakMythicDevil_F_Any"] = "b09c34f541d450047905718ac1cc99a8";
                        m_AllEEL["EE_CloakMythicDevil_M_Any"] = "514696e7bb9131b43aab0005049cd191";
                        m_AllEEL["EE_CloakMythicDragon_F_Any"] = "d7823c95d6794544db63a601ac3616b3";
                        m_AllEEL["EE_CloakMythicDragon_M_Any"] = "f8024df84d1bc944da9911de6b7f93a8";
                        m_AllEEL["EE_CloakMythicLegend_F_Any"] = "9bc82916361024c4fa3b7cf32eec6811";
                        m_AllEEL["EE_CloakMythicLegend_M_Any"] = "d5527ed5a75cdd64687b1246bc3f5557";
                        m_AllEEL["EE_CloakMythicLich_F_Any"] = "bd1fefc4ebb1b37419a460fc8b62216f";
                        m_AllEEL["EE_CloakMythicLich_M_Any"] = "e8f987f1da596214596efe01bac9e102";
                        m_AllEEL["EE_CloakMythicSwarm_F_Any"] = "0af1606bb77eb2c49a24eedd23ca0aa3";
                        m_AllEEL["EE_CloakMythicSwarm_M_Any"] = "209879522a9b240468173d2972a34cd3";
                        m_AllEEL["EE_CloakMythicTrickster_F_Any"] = "b5d43ff4235cfae448b76f2f433db06a";
                        m_AllEEL["EE_CloakMythicTrickster_M_Any"] = "3ee6b85b5d79a1b4faab09e04ee66339";
                        m_AllEEL["EE_CloakWildHuntColored_F_Any"] = "d7ff7fc9e03e2394da6cdd696ba680d3";
                        m_AllEEL["EE_CloakWildHuntColored_M_Any"] = "4226d9b9144377349b668b6165b92a35";
                        m_AllEEL["EE_CloakWildHunt_F_Any"] = "8eddafef05e390048909b95453074d8b";
                        m_AllEEL["EE_CloakWildHunt_M_Any"] = "2d1cd8e3c21dbdf4c8f09a17fff7e8ca";
                        m_AllEEL["EE_CloakWolfBlack_F_Any"] = "3018db2f0c60ea044adbf213580b348c";
                        m_AllEEL["EE_CloakWolfBlack_M_Any"] = "49901e1d4669e3c489944c740df3749a";
                        m_AllEEL["EE_CloakWolfNormal_F_Any"] = "936d56386c22b27428ad462b7b169246";
                        m_AllEEL["EE_CloakWolfNormal_M_Any"] = "9ae9adaaa4033e644a88a18c7ce9b2ab";
                        m_AllEEL["EE_CloakWolfWarg_F_Any"] = "ed1cf6532ef2fb747b4bdf4be193f838";
                        m_AllEEL["EE_CloakWolfWarg_M_Any"] = "0369456c71257ef478495d949d16b047";
                        m_AllEEL["EE_CloakWolfWinter_F_Any"] = "f3fe3e5bc97f85f4eb91129c75b5506e";
                        m_AllEEL["EE_CloakWolfWinter_M_Any"] = "da08796fd73421b45b44a3c9947d3716";
                        m_AllEEL["EE_ClothNoble1_F_Any"] = "5b7c39625c789684f9a2a35b89cd45ec";
                        m_AllEEL["EE_ClothNoble1_M_Any"] = "6b733c2a5deb0a34ea24be68002d1969";
                        m_AllEEL["EE_ClothNoble2_F_Any"] = "a88163dba67a85941946a58aa294d094";
                        m_AllEEL["EE_ClothNoble2_M_Any"] = "3c09a5f3edfa10843979c6381a8cb499";
                        m_AllEEL["EE_ClothNobleCamellia_F_Any"] = "64d2e3a0b30eb604791cf4cacff060e4";
                        m_AllEEL["EE_CoifChainmailArmyNormal_F_DW"] = "8abe0a766f2db4f4a830f6da8d3dd8cb";
                        m_AllEEL["EE_CoifChainmailArmyNormal_F_EL"] = "f08c0e68217a6f64ea9e765ae51ce78a";
                        m_AllEEL["EE_CoifChainmailArmyNormal_F_GN"] = "43c34c81708e96e488128b552277f043";
                        m_AllEEL["EE_CoifChainmailArmyNormal_F_HE"] = "9a37867b793fee54f8674b642fc4cbcb";
                        m_AllEEL["EE_CoifChainmailArmyNormal_F_HL"] = "21beea3d6056a5349b76ff47ee78217a";
                        m_AllEEL["EE_CoifChainmailArmyNormal_F_HM"] = "42546a7f4f9f0864ba04ab1eeae69014";
                        m_AllEEL["EE_CoifChainmailArmyNormal_F_HO"] = "12749ab57d029d948af2208f49e669e8";
                        m_AllEEL["EE_CoifChainmailArmyNormal_F_OD"] = "b52407d9ccacf4c4badf2fe4dacffe3c";
                        m_AllEEL["EE_CoifChainmailArmyNormal_F_TL"] = "8eb520a5846a31248b382925bec1702a";
                        m_AllEEL["EE_CoifChainmailArmyNormal_M_DW"] = "9382662a39a734340bc01ea2c5c9bb6e";
                        m_AllEEL["EE_CoifChainmailArmyNormal_M_EL"] = "26418c65edc478645aa0bac02fe45f4c";
                        m_AllEEL["EE_CoifChainmailArmyNormal_M_GN"] = "520195d0d7dc6604d9ecf90774c79a55";
                        m_AllEEL["EE_CoifChainmailArmyNormal_M_HE"] = "19932166f9fd0d94888293e61035a873";
                        m_AllEEL["EE_CoifChainmailArmyNormal_M_HL"] = "d7b78101a68307445afb81024791e49a";
                        m_AllEEL["EE_CoifChainmailArmyNormal_M_HM"] = "8ea251d1ff6169d4db12f4b5ec9b1607";
                        m_AllEEL["EE_CoifChainmailArmyNormal_M_HO"] = "85ec4053a94d80e43bff7ca41ba1fb0c";
                        m_AllEEL["EE_CoifChainmailArmyNormal_M_OD"] = "c97ae84ec701e094b9c443df89378576";
                        m_AllEEL["EE_CoifChainmailArmyNormal_M_TL"] = "4568107a6ed2a6645a2bbcf684503f3f";
                        m_AllEEL["EE_CoifChainmailArmyPainted_F_DW"] = "d6c5b91f79e907445986b2bce73a9ec6";
                        m_AllEEL["EE_CoifChainmailArmyPainted_F_EL"] = "9ca8f1d987e986f45b2200f9db4b6dc8";
                        m_AllEEL["EE_CoifChainmailArmyPainted_F_GN"] = "135127efdd104984b8efe4eb1d3d65be";
                        m_AllEEL["EE_CoifChainmailArmyPainted_F_HE"] = "1b1e65caeb4086346a4ffd1b3b6940b3";
                        m_AllEEL["EE_CoifChainmailArmyPainted_F_HL"] = "b20d871936a20204a9b68c75ec43d8b1";
                        m_AllEEL["EE_CoifChainmailArmyPainted_F_HM"] = "1bbda92b9aabbc440a0487da7c45a7b4";
                        m_AllEEL["EE_CoifChainmailArmyPainted_F_HO"] = "5f5b89bdaa4246245b693f60f7ac3b13";
                        m_AllEEL["EE_CoifChainmailArmyPainted_F_OD"] = "a4016ec482237ed4084dcf8a7376ac27";
                        m_AllEEL["EE_CoifChainmailArmyPainted_F_TL"] = "35c0f1f4dd63b2f458c821c27956aa8b";
                        m_AllEEL["EE_CoifChainmailArmyPainted_M_DW"] = "f786495fd9558534ea63948bd6c5b807";
                        m_AllEEL["EE_CoifChainmailArmyPainted_M_EL"] = "84662e9893c4ba9489e1b42ed6a8d3f9";
                        m_AllEEL["EE_CoifChainmailArmyPainted_M_GN"] = "e5ffe5328dd9ba545b86d459e2468e13";
                        m_AllEEL["EE_CoifChainmailArmyPainted_M_HE"] = "99d3afdc39f08e240b40ae5efa8f4118";
                        m_AllEEL["EE_CoifChainmailArmyPainted_M_HL"] = "2106a4b1cb23ee74cbfd34ebd4730dca";
                        m_AllEEL["EE_CoifChainmailArmyPainted_M_HM"] = "d06a4d463d5afdb4eaa559ca585e5a66";
                        m_AllEEL["EE_CoifChainmailArmyPainted_M_HO"] = "a5efe4fff2b0cb14387ca47687ef4372";
                        m_AllEEL["EE_CoifChainmailArmyPainted_M_OD"] = "54e4a7d576d93de4981f9aa44ff1815b";
                        m_AllEEL["EE_CoifChainmailArmyPainted_M_TL"] = "bde7225f0c2fa8244b9b5e4991b5e828";
                        m_AllEEL["EE_DirtColored_U_Any"] = "43f0cb8c221129f4ea8a9fa797a2a4b9";
                        m_AllEEL["EE_DirtLight_U_Any"] = "baeb2579fed32f1499a5161bf8db761b";
                        m_AllEEL["EE_DirtMedium_U_Any"] = "f55400c332b754b4485bbd8987a43490";
                        m_AllEEL["EE_DressCitizen_F_Any"] = "d5862186d1c3db6468e1a351675bd8c7";
                        m_AllEEL["EE_DressPeasant_F_Any"] = "61990d724b6f0b44d8667cd5fb7b919c";
                        m_AllEEL["EE_DressSuccubusColored_F_Any"] = "52504b9ab9d45a64eb51c39e219c2baa";
                        m_AllEEL["EE_DressSuccubus_F_Any"] = "279abdaaf6b394e4aa777a4124dcec79";
                        m_AllEEL["EE_DruidAccessories_F_Any"] = "6ccf5d8f3c4465e47a58a303390d5f93";
                        m_AllEEL["EE_DruidAccessories_M_Any"] = "83a94a49872d1344585352512ff2f285";
                        m_AllEEL["EE_Druid_F_Any"] = "2afad0f92b9cd5f43aa0c657dbdaa4ab";
                        m_AllEEL["EE_Druid_M_Any"] = "3c04c2832c76dc9448bba0d91e6a3458";
                        m_AllEEL["EE_EMPTY_HairStyleColors"] = "b85db19d7adf6aa48b5dd2bb7bfe1502";
                        m_AllEEL["EE_EMPTY_WarpaintColors"] = "b8b615fabc0f7af448fb68f009113c61";
                        m_AllEEL["EE_EmberOutfit_F_Any"] = "cc13360245c272c4f96a1c830ca30e38";
                        m_AllEEL["EE_EyesShinyIris_U_Any"] = "a47bac4deb099fc4b86a2e01bb425cc5";
                        m_AllEEL["EE_FULLEMPTY"] = "8a008a34d7462524c9b96132a28bb606";
                        m_AllEEL["EE_FacePaint01_U_Any"] = "bc3feec9a89a55f4f865ea272c94b7f8";
                        m_AllEEL["EE_FacePaint02_U_Any"] = "2c8bf4780120d4246ad32fcfee23dd59";
                        m_AllEEL["EE_FacePaint03_U_Any"] = "b4291d549fa60b64095069ef70c97c22";
                        m_AllEEL["EE_FacePaint04_U_Any"] = "9d43f14716a359b41bf55953ea97516f";
                        m_AllEEL["EE_FacePaint05_U_Any"] = "4caf34265b86fc84b901b601a17d8cc1";
                        m_AllEEL["EE_FacePaint06_U_Any"] = "918595b81f5d7b949b0615198f5b8c69";
                        m_AllEEL["EE_FacePaint07_U_Any"] = "eef7ffa8e62b73d4eb6ba78bd60c39f1";
                        m_AllEEL["EE_FacePaint08_U_Any"] = "54179ae842c50b04e83c23ede1a78d33";
                        m_AllEEL["EE_FacePaint09_U_Any"] = "4512799db39a39a43ad82a02cbb7df64";
                        m_AllEEL["EE_FacePaint10_U_Any"] = "bd77065a32da9fc43a80a695d9420e84";
                        m_AllEEL["EE_FacePaint11_U_Any"] = "88f65f8d65c984b479976316cea3e3bc";
                        m_AllEEL["EE_FacePaint12_U_Any"] = "2796c4c3c9114df47bdf5924e367dfdb";
                        m_AllEEL["EE_FacePaint13_U_Any"] = "f9e8e86b27106d04f9e7b3acc63bbe50";
                        m_AllEEL["EE_FacePaint14_U_Any"] = "0d0837c6124e70c43ab1ffc043a6f6ba";
                        m_AllEEL["EE_FacePaint15_U_Any"] = "1c9ba560c29dbc14ab538890f5f1b130";
                        m_AllEEL["EE_FacePaint16_U_Any"] = "232426a9d9fd4ca4b83cb58396e1d7f7";
                        m_AllEEL["EE_FacePaint17_U_Any"] = "cb3466f6666cbec4f93d45f3dbda4b70";
                        m_AllEEL["EE_FacePaint18_U_Any"] = "8a25b2227688c0943a59fb471b9b740a";
                        m_AllEEL["EE_FacePaint19_U_Any"] = "8c5189716feb61241a68b45835a72851";
                        m_AllEEL["EE_FacePaint20_U_Any"] = "4e829d4aabd124f49aabf8b82468bb0e";
                        m_AllEEL["EE_FacePaint21_U_Any"] = "afea3383a2b216947a4a682b2f8e6eba";
                        m_AllEEL["EE_FacePaint22_U_Any"] = "881f3297d3ac74c45802443ef9d1675b";
                        m_AllEEL["EE_FacePaint23_U_Any"] = "ac1c3309701e88d4ea238bbd77f7c04f";
                        m_AllEEL["EE_FacePaint24_U_Any"] = "ac1858ef69f22e4498d5407131099f1d";
                        m_AllEEL["EE_FacePaint25_U_Any"] = "4874a3fbeac307e45853044615ae42ff";
                        m_AllEEL["EE_FacePaint26_U_Any"] = "52854618251a7ab439627f37b285d386";
                        m_AllEEL["EE_FacePaint27_U_Any"] = "8128c475040f4144ea08e8d27c2a06c2";
                        m_AllEEL["EE_FacePaint28_U_Any"] = "b42cd5b711da4754db98455c052cc99c";
                        m_AllEEL["EE_FacePaint29_U_Any"] = "efd2c7d6dc3da584190f0b3963dca7c7";
                        m_AllEEL["EE_FacePaint30_U_Any"] = "1129d53e32345a24c9ad02f585ab8333";
                        m_AllEEL["EE_FacePaint31_U_Any"] = "a55c32177a1778547b736b355d52487d";
                        m_AllEEL["EE_FacePaint32_U_Any"] = "a2f2790623286cb42ac48ff01026f899";
                        m_AllEEL["EE_FacePaint33_U_Any"] = "790d88a5799b6de40abdb0681f12358e";
                        m_AllEEL["EE_FacePaint34_U_Any"] = "ba7afeebe75e5f449b2f795010f7fde6";
                        m_AllEEL["EE_FacePaint35_U_Any"] = "a616bf262fdb1474aa91af7bfdc4bee9";
                        m_AllEEL["EE_FacePaint36_U_Any"] = "048f3a47cfa1b1849a66687b779ccb3d";
                        m_AllEEL["EE_FacePaint37_U_Any"] = "bd07ef08134cdc845be9d9144e7400bb";
                        m_AllEEL["EE_FighterAccessories_F_Any"] = "91a8a967871c8774eb531afe62942cab";
                        m_AllEEL["EE_FighterAccessories_M_Any"] = "c138b719e4e95534d838822c5f126389";
                        m_AllEEL["EE_Fighter_F_Any"] = "8277514a290c9a94098f71682dc30f6a";
                        m_AllEEL["EE_Fighter_M_Any"] = "990c1cd8c38f7df429e46e77bceda9a6";
                        m_AllEEL["EE_FireWings_F"] = "8604b675977361043978ecf56122f389";
                        m_AllEEL["EE_FullplateDemodand_M_Any"] = "4d9bdd155beb7b740a11671ebc7aef31";
                        m_AllEEL["EE_FullplateDeskari_F_Any"] = "14229d7df361a734a985f323800589ad";
                        m_AllEEL["EE_FullplateDeskari_M_Any"] = "0d08f332ce63f354396c8ad5be055127";
                        m_AllEEL["EE_FullplateDragonscale_F_Any"] = "cdb8e37e48b379b49b6e3e6924e7cca7";
                        m_AllEEL["EE_FullplateDragonscale_M_Any"] = "afb313accc4499c4ebf430a937b28575";
                        m_AllEEL["EE_FullplateEvilUndead_F_Any"] = "0287ceb0e26c08b4586571c4d08e661f";
                        m_AllEEL["EE_FullplateEvilUndead_M_Any"] = "a2f9edb48a4553045b554731df306f4a";
                        m_AllEEL["EE_FullplateEvil_F_Any"] = "86860c7f3bb66f04c98f576e5cb580c2";
                        m_AllEEL["EE_FullplateEvil_M_Any"] = "31687dd7a45ecaa46bdd740c71474f6f";
                        m_AllEEL["EE_FullplateHellknight_F_Any"] = "edff6cb8cf71591408d8145e2b1a9070";
                        m_AllEEL["EE_FullplateHellknight_M_Any"] = "61c43aba6811c184ab210bcd28d6b737";
                        m_AllEEL["EE_FullplateHolyAdamantite_F_Any"] = "28eed6f3c8751444fb37636f4eaae7d7";
                        m_AllEEL["EE_FullplateHolyAdamantite_M_Any"] = "9203d101fd403ca42a21310da28ea0d9";
                        m_AllEEL["EE_FullplateHolyGold_F_Any"] = "7788696b3b141e4409e43d69591d13a0";
                        m_AllEEL["EE_FullplateHolyGold_M_Any"] = "259cf203d4e82134e9d0029dfeebaf2d";
                        m_AllEEL["EE_FullplateHolyMithral_F_Any"] = "6e8c2a22022444342be9ec3e6bfd015c";
                        m_AllEEL["EE_FullplateHolyMithral_M_Any"] = "55676a4c9cda0c64b863e99ca8db924b";
                        m_AllEEL["EE_FullplateHoly_F_Any"] = "b9842a171de7e1140ad13206d9f228ee";
                        m_AllEEL["EE_FullplateHoly_M_Any"] = "9294c686b61566e488b0eb05ba084f57";
                        m_AllEEL["EE_FullplateKnight_F_Any"] = "6a16a0a5a76241f4d83ea827d4837e6e";
                        m_AllEEL["EE_FullplateKnight_M_Any"] = "f645de0a63343da42a4c8dfabedd431f";
                        m_AllEEL["EE_FullplateMithralArcane_F_Any"] = "98e4605fd092dbe439b53a308ae5fc92";
                        m_AllEEL["EE_FullplateMithralArcane_M_Any"] = "f93a8758336981d4e8cb54274854faf5";
                        m_AllEEL["EE_FullplateOrnateEvil_F_Any"] = "757bcb35d2855864683c5fe70c51df0b";
                        m_AllEEL["EE_FullplateOrnateEvil_M_Any"] = "8aa95ce5ac9e54d409818171028e5409";
                        m_AllEEL["EE_FullplateOrnateMetal_F_Any"] = "22dd1e43c837a1b4994026035a849ed6";
                        m_AllEEL["EE_FullplateOrnateMetal_M_Any"] = "82575b0bd44fe974ebe27ace3a5da01a";
                        m_AllEEL["EE_FullplateOrnateNormal_F_Any"] = "ea2efc170a333c24f9fe9e2b01494a3d";
                        m_AllEEL["EE_FullplateOrnateNormal_M_Any"] = "da7159b0431b7044dbf5013872e75061";
                        m_AllEEL["EE_FullplateRoyal_F_Any"] = "0aec12428d87d014687589ca57559395";
                        m_AllEEL["EE_FullplateRoyal_M_Any"] = "ae97e7cb49fadc844bfcd893b3d7cd34";
                        m_AllEEL["EE_GhoulModArmHook_U_GL"] = "28527f9f1b5bd194c94352925b11d1e2";
                        m_AllEEL["EE_GhoulModArmSpikes_U_GL"] = "3f9586d94586b57419e3e3448635354c";
                        m_AllEEL["EE_GhoulModArmSword_U_GL"] = "0fe6b5fe7f5544444b8a3a46aade6500";
                        m_AllEEL["EE_GhoulModMetalLeg_U_GL"] = "4e95739f3dfc60143a1e442bf40ebdc3";
                        m_AllEEL["EE_GhoulModSpineSpikes_U_GL"] = "5e08b5fe07ddcfa44ad3c8453be1bdf2";
                        m_AllEEL["EE_GlovesBacker_F_Any"] = "2d91cb5909edf2c44b650ec737d84a46";
                        m_AllEEL["EE_GlovesBacker_M_Any"] = "04ff7c523ae7aec49a351de21bee915a";
                        m_AllEEL["EE_GlovesBarbarian_F_Any"] = "7269e9e86ed248143afd0c78a4c3582a";
                        m_AllEEL["EE_GlovesBarbarian_M_Any"] = "faaf54d71d07b2740966309b142d1b21";
                        m_AllEEL["EE_GlovesDueling_F_Any"] = "b4eca622f677ac140be82485a0073f1b";
                        m_AllEEL["EE_GlovesDueling_M_Any"] = "a14164990b3989f45a60922006404793";
                        m_AllEEL["EE_GlovesManticore_F_Any"] = "53b13cba2eca7e94eac65716ecec61d3";
                        m_AllEEL["EE_GlovesManticore_M_Any"] = "c3251d8c8e19edd4dbe1389dc34e35d0";
                        m_AllEEL["EE_GlovesWizard_F_Any"] = "b047fb6943c25de4fa4d915fb34bba4c";
                        m_AllEEL["EE_GlovesWizard_M_Any"] = "7464996723425ab44843fa0325048e1c";
                        m_AllEEL["EE_GogglesBrain_F_DW"] = "f009611a07187a34da790732cd1f8e5f";
                        m_AllEEL["EE_GogglesBrain_F_EL"] = "02fc1b6f2f4581c4c9513c8a2a58b7f3";
                        m_AllEEL["EE_GogglesBrain_F_GN"] = "a89c19a66430c3745bf3829afe431531";
                        m_AllEEL["EE_GogglesBrain_F_HE"] = "b5c3bddc7d80b724dbd34b19a19d82f7";
                        m_AllEEL["EE_GogglesBrain_F_HM"] = "59e6c1db15cb28141b44eb495e641c0e";
                        m_AllEEL["EE_GogglesBrain_F_HO"] = "724e56e45a468ff46928b84fdad62958";
                        m_AllEEL["EE_GogglesBrain_M_DW"] = "93ac705eb11b9fa458f8f733c423eabd";
                        m_AllEEL["EE_GogglesBrain_M_EL"] = "d5498982d316fa644a968cb7f315e22f";
                        m_AllEEL["EE_GogglesBrain_M_GN"] = "7ba677a717b4ec24b9c8fa623f4c809b";
                        m_AllEEL["EE_GogglesBrain_M_HE"] = "031f32d172bfd1f449cb43c462b4f7f1";
                        m_AllEEL["EE_GogglesBrain_M_HM"] = "c959f5389c952f749911e3475eb4468e";
                        m_AllEEL["EE_GogglesBrain_M_HO"] = "c9a07d2d90001184981188d346805661";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_DW"] = "7c301879b80a55548b109806b12abb4c";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_EL"] = "584519ccb8c807142a37d0b3594d6e0b";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_GN"] = "011c84e428c2ab447b0314bcd4318d45";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_HE"] = "db9993e7dabe1964fb272c00f904a6f5";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_HL"] = "ed5beee2ee9e37a4fbb73fca350f1455";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_HM"] = "6fcc896c47277804d8c1c0dfe4ac96b8";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_HO"] = "3530b9b32dda53545b9203bb3c6995d9";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_KT"] = "ba58e4cb90d38814bad584209c877ec9";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_OD"] = "336400200899a6945aef60575b0bc386";
                        m_AllEEL["EE_GogglesEyesOfEagle_F_TL"] = "6b5862329d651694eb4a04ec0685ef94";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_DW"] = "f600eaca599c2684ea87aec68eb61f8a";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_EL"] = "83ca5cba5de28454ba13f5e52c865a36";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_GN"] = "d46465e909ffcb04f990766984a92a8b";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_HE"] = "8bd3f4c72f2851b4f8196e924ea6cfde";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_HL"] = "50a286a67f1a0c54299b96fb5d3b679b";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_HM"] = "7445145db6f1f764b9d5442aac9b92b6";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_HO"] = "508e47bd962fcef49b76d470c11a20a0";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_KT"] = "2101c874e16aef84393752eba09bf349";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_OD"] = "db1d985a50da90f41ab3d1c59f89cdf2";
                        m_AllEEL["EE_GogglesEyesOfEagle_M_TL"] = "2e8a868ac2542bb4e83dd2ec30e2f5c0";
                        m_AllEEL["EE_GogglesMaskCommon_F_DW"] = "b31f83a466e0180429ac7b720381c9b1";
                        m_AllEEL["EE_GogglesMaskCommon_F_EL"] = "1fbb5134261afcb488acf6b59d2b2f27";
                        m_AllEEL["EE_GogglesMaskCommon_F_GN"] = "40d7fdd7a334d9044af3197410e83711";
                        m_AllEEL["EE_GogglesMaskCommon_F_HE"] = "f1bae041282d18a419f85715df7c602e";
                        m_AllEEL["EE_GogglesMaskCommon_F_HL"] = "2f82af9380d5f7848b4062226b2c919c";
                        m_AllEEL["EE_GogglesMaskCommon_F_HM"] = "2fb74e546de34e5449f1bade0dff95de";
                        m_AllEEL["EE_GogglesMaskCommon_F_HO"] = "c8716b144f1bf7b49ad50f8102969cfe";
                        m_AllEEL["EE_GogglesMaskCommon_F_KT"] = "9488db3eb77054d4c99863d556253012";
                        m_AllEEL["EE_GogglesMaskCommon_F_OD"] = "275984f893cfff847869916a1d130422";
                        m_AllEEL["EE_GogglesMaskCommon_F_TL"] = "3de388684fe0d584b8535a7eaa37a9da";
                        m_AllEEL["EE_GogglesMaskCommon_M_DW"] = "07ce044b7a7003740b35067db73d9b86";
                        m_AllEEL["EE_GogglesMaskCommon_M_EL"] = "a863c7749f9ce3b4bbbf13c64ae48fff";
                        m_AllEEL["EE_GogglesMaskCommon_M_GN"] = "3e3e49ddfcdf25745837f13af469b24d";
                        m_AllEEL["EE_GogglesMaskCommon_M_HE"] = "334fd250aa2af134d838b2df9e871d3c";
                        m_AllEEL["EE_GogglesMaskCommon_M_HL"] = "05be12f5c1a5faa4c8308919f358db1f";
                        m_AllEEL["EE_GogglesMaskCommon_M_HM"] = "bd2d9871a5c265942891b547f29ca842";
                        m_AllEEL["EE_GogglesMaskCommon_M_HO"] = "698db021e42dd8a48a3ad345c674f3c4";
                        m_AllEEL["EE_GogglesMaskCommon_M_KT"] = "4d1931e234a31934f8860b3c4585dafe";
                        m_AllEEL["EE_GogglesMaskCommon_M_OD"] = "e21bd7540c46c364db0eccc9c8c7fc66";
                        m_AllEEL["EE_GogglesMaskCommon_M_TL"] = "10faf3efad173b64ebae3bbd6c24f0d9";
                        m_AllEEL["EE_GogglesMaskTrickster_F_DW"] = "e1188155df966c9468b2bb1895cd3e42";
                        m_AllEEL["EE_GogglesMaskTrickster_F_EL"] = "8cad74c464c6e32439d04c78c119fce5";
                        m_AllEEL["EE_GogglesMaskTrickster_F_GN"] = "91df2e9d7b95fae46b36ed18e420f44d";
                        m_AllEEL["EE_GogglesMaskTrickster_F_HE"] = "d7ed2105a5b45ad4ba83c6774dd446ec";
                        m_AllEEL["EE_GogglesMaskTrickster_F_HL"] = "1378cfa4cf01f3a4b8d26a65eb535259";
                        m_AllEEL["EE_GogglesMaskTrickster_F_HM"] = "fd7d294b1b535714498537d74f6b5a3a";
                        m_AllEEL["EE_GogglesMaskTrickster_F_HO"] = "1e4bc1e720018584da07cb36be610dc4";
                        m_AllEEL["EE_GogglesMaskTrickster_F_KT"] = "8b6fe6efb435fff4cae97b852d99d1d5";
                        m_AllEEL["EE_GogglesMaskTrickster_F_OD"] = "041a3a0a57184f542ae759f916ee7440";
                        m_AllEEL["EE_GogglesMaskTrickster_F_TL"] = "502a2b86d5e36274aaf2f415b06fb6dd";
                        m_AllEEL["EE_GogglesMaskTrickster_M_DW"] = "b8857a7db85dddd4ca471352311b6eed";
                        m_AllEEL["EE_GogglesMaskTrickster_M_EL"] = "83beb29c5191d574f8a1c672cff184da";
                        m_AllEEL["EE_GogglesMaskTrickster_M_GN"] = "134460f9bb04eef4f9c4fa605d456c10";
                        m_AllEEL["EE_GogglesMaskTrickster_M_HE"] = "5ebf396ff8e0a54489668d4acafc35bc";
                        m_AllEEL["EE_GogglesMaskTrickster_M_HL"] = "1fcf508968231ce42bf1e4d147c39e77";
                        m_AllEEL["EE_GogglesMaskTrickster_M_HM"] = "62ba4382e3fe2ab409580509a50e66d2";
                        m_AllEEL["EE_GogglesMaskTrickster_M_HO"] = "7f1e9998a0ade584c97bc63370f187b9";
                        m_AllEEL["EE_GogglesMaskTrickster_M_KT"] = "dff4150c8d9e38c4fa51c6325fd10790";
                        m_AllEEL["EE_GogglesMaskTrickster_M_OD"] = "644c13c960ee85f468a3ae02ff832279";
                        m_AllEEL["EE_GogglesMaskTrickster_M_TL"] = "71ec4a84fac628c4bace22db0d6114fa";
                        m_AllEEL["EE_GogglesMonocle_F_DW"] = "97a37dc7c9c473b47b7543df363d9c46";
                        m_AllEEL["EE_GogglesMonocle_F_EL"] = "ced071253664a1646be50e22964fa498";
                        m_AllEEL["EE_GogglesMonocle_F_GN"] = "ea81cec7979f7f34ab938810e7bfd7d3";
                        m_AllEEL["EE_GogglesMonocle_F_HE"] = "f00deafb18ce4ea41a915d5880bd70dd";
                        m_AllEEL["EE_GogglesMonocle_F_HL"] = "f8bb7ce0588b65b42a3897ed972e42be";
                        m_AllEEL["EE_GogglesMonocle_F_HM"] = "c19c26e7f149c1d4ebd1bd78dade3616";
                        m_AllEEL["EE_GogglesMonocle_F_HO"] = "e78cfc573e37a004d9d034a0a33afe46";
                        m_AllEEL["EE_GogglesMonocle_F_KT"] = "cceecb6bbf27e124fabbd6b82cc235df";
                        m_AllEEL["EE_GogglesMonocle_F_OD"] = "984a0eeb16d511b43a3021e6cddc1fb2";
                        m_AllEEL["EE_GogglesMonocle_F_TL"] = "8e4f7e25334118a459f233de23893b9d";
                        m_AllEEL["EE_GogglesMonocle_M_DW"] = "d63821262f3abbc44855b485f9f31671";
                        m_AllEEL["EE_GogglesMonocle_M_EL"] = "500d1a17fffa3494d8a2a8b7c186f02d";
                        m_AllEEL["EE_GogglesMonocle_M_GN"] = "ec90278331ee1a44f85a6d5335792090";
                        m_AllEEL["EE_GogglesMonocle_M_HE"] = "8dcf5d30c1ea88f42b451cbc592e360e";
                        m_AllEEL["EE_GogglesMonocle_M_HL"] = "b3348d6977ad272489d55c430191eac9";
                        m_AllEEL["EE_GogglesMonocle_M_HM"] = "5477fe160b0ae844cb3ff681d9e81b10";
                        m_AllEEL["EE_GogglesMonocle_M_HO"] = "af9ebd8caadd3ac4a8ec240b223a36b6";
                        m_AllEEL["EE_GogglesMonocle_M_KT"] = "7938515615d6d024a9e5a54120c44793";
                        m_AllEEL["EE_GogglesMonocle_M_OD"] = "73eab6f387f00704399c94cdc7efe510";
                        m_AllEEL["EE_GogglesMonocle_M_TL"] = "01a3e8d75ac27c541aa13bfd0db34883";
                        m_AllEEL["EE_Hair00ShortCurly_F_HL"] = "28b4bc8f2eb55f14fbac57b010781d16";
                        m_AllEEL["EE_Hair00ShortCurly_M_HL"] = "40388f373a2013a4d9abf5d895c1b3f1";
                        m_AllEEL["EE_Hair00Short_M_DW"] = "03adea0c666c7f64a8871459e3303bfd";
                        m_AllEEL["EE_Hair00Short_M_HE"] = "76cfc339d08471f4da919dcbdd2fceb1";
                        m_AllEEL["EE_Hair00Short_M_HM"] = "def666224ba24df4e954c03049b29a53";
                        m_AllEEL["EE_Hair00Short_M_SU"] = "b17cc9c21bdcbab4faa26d7922ceb387";
                        m_AllEEL["EE_Hair00Slick_F_AA"] = "5ad5704dd51861a4b80931cd4d33ebe6";
                        m_AllEEL["EE_Hair00Slick_F_CM"] = "ae5a13adfc75b7048b1c84dd4eae56b3";
                        m_AllEEL["EE_Hair00Slick_F_DH"] = "2599b71369003d645b0442088e4edb54";
                        m_AllEEL["EE_Hair00Slick_F_DW"] = "72c0753cedfac164b867ebfaa542ca63";
                        m_AllEEL["EE_Hair00Slick_F_EL"] = "91895a6776bc5724089fd999f659e834";
                        m_AllEEL["EE_Hair00Slick_F_GN"] = "c4ef44f3bc3df2341868c9a4fbc04fc3";
                        m_AllEEL["EE_Hair00Slick_F_HE"] = "e557a6ffddb60c04191dee26d69f3c01";
                        m_AllEEL["EE_Hair00Slick_F_HM"] = "7709e5f38fb90814888406e3409b66a7";
                        m_AllEEL["EE_Hair00Slick_F_HO"] = "5abd733990de55e4bbbd1fd83734dd0d";
                        m_AllEEL["EE_Hair00Slick_F_TL"] = "d71d2e53fce0f1d4baad8b20c8266676";
                        m_AllEEL["EE_Hair00Slick_M_AA"] = "76cad0a86acd5ce4d925992877bcd4fd";
                        m_AllEEL["EE_Hair00Slick_M_CM"] = "0e8477fa2766c9948b324277a4a62464";
                        m_AllEEL["EE_Hair00Slick_M_DE"] = "890ece7d775ba9d4aa6320f83c6ee2c3";
                        m_AllEEL["EE_Hair00Slick_M_DH"] = "ddf96f252720cf643b36f1142fe76e9d";
                        m_AllEEL["EE_Hair00Slick_M_EL"] = "4da3c565974930a40b51950ee671895e";
                        m_AllEEL["EE_Hair00Slick_M_GN"] = "f5c0b47bdcf6b104d8f89a1073f52f4e";
                        m_AllEEL["EE_Hair00Slick_M_HE"] = "34bafee70e116274fabdead2fc6123be";
                        m_AllEEL["EE_Hair00Slick_M_TL"] = "1730d9ec670411b49b6e4c0222abbd25";
                        m_AllEEL["EE_Hair00Trim_M_AA"] = "3040534437e729945a629892a687e578";
                        m_AllEEL["EE_Hair00Trim_M_DH"] = "3230ef6d378340c4db977e21e14a2c1b";
                        m_AllEEL["EE_Hair00Trim_M_DW"] = "360a1b4e88ac8884a8a207c62e240ff9";
                        m_AllEEL["EE_Hair00Trim_M_HM"] = "329cf540a8faed64284c067bace8bbc9";
                        m_AllEEL["EE_Hair00Trim_M_HO"] = "f54ad7e7b212e9c408a0d0c8db2f6553";
                        m_AllEEL["EE_Hair00Trim_M_SU"] = "319708d0957456147a012616baa2a8f0";
                        m_AllEEL["EE_Hair01BrushUpRegill_M_GN"] = "282152e0b7ad09844a43f32a9f0cbe32";
                        m_AllEEL["EE_Hair01CrownBraid_F_DW"] = "34bb68b3e4f03be44a1f0611a09530fc";
                        m_AllEEL["EE_Hair01DreadSeelah_F_HM"] = "fc3fc0e45a70a0e42b6aed10baf794f0";
                        m_AllEEL["EE_Hair01FauxHawk_F_GN"] = "1181aaf62805c3d48a50b975723d583f";
                        m_AllEEL["EE_Hair01Greybor_M_DW"] = "74b57930206a93547a5e2602685fe6a9";
                        m_AllEEL["EE_Hair01Lann_M_MM"] = "94d7e4e2793dbde4bb0eb1f6cf20bdff";
                        m_AllEEL["EE_Hair01LongCamelia_F_HE"] = "7c55ae7f07c9d4741b4837bd305d3ec0";
                        m_AllEEL["EE_Hair01LongEmber_F_DE"] = "54d548806c074cd4daf902c91caba237";
                        m_AllEEL["EE_Hair01LongEmber_F_EL"] = "304b84351ffbde24190e56724178df5b";
                        m_AllEEL["EE_Hair01Long_F_OD"] = "7f632035eba6e0d4a99b4167e68311cb";
                        m_AllEEL["EE_Hair01MediumCurly_F_HL"] = "698ea7576ec3a88418f113fc2f8cb8ea";
                        m_AllEEL["EE_Hair01MediumSide_M_EL"] = "3cacf7ac2cd982045a004da87f5ce012";
                        m_AllEEL["EE_Hair01MediumSlick_F_HO"] = "b060910ad6ad374448443af5b14dc387";
                        m_AllEEL["EE_Hair01Medium_F_TL"] = "195bf2c26a914dc439121c5fdbded81f";
                        m_AllEEL["EE_Hair01Medium_M_HO"] = "5a7cf42ad52a27b41970e59b5169d893";
                        m_AllEEL["EE_Hair01Medium_M_TL"] = "d174619e428101e41b5675bd6286b1d4";
                        m_AllEEL["EE_Hair01MohawkWenduag_F_MM"] = "ad6c23c1e8e7e374b9864dea8fcc381d";
                        m_AllEEL["EE_Hair01PonytailClassic_M_AA"] = "b34b1532310a01440ac95dea9d0956e1";
                        m_AllEEL["EE_Hair01PonytailClassic_M_CM"] = "be625dc3da06a564888c325938ca7e41";
                        m_AllEEL["EE_Hair01PonytailClassic_M_DH"] = "5c097223379d575429338508055b6585";
                        m_AllEEL["EE_Hair01PonytailClassic_M_HE"] = "42e7c3c18d52a4343994b84fb24404af";
                        m_AllEEL["EE_Hair01PonytailClassic_M_HM"] = "ee3945f41269aed4b9fcb6304c3fda79";
                        m_AllEEL["EE_Hair01PonytailClassic_M_SU"] = "d40f502da620e3949bdcc66477236661";
                        m_AllEEL["EE_Hair01Ponytail_M_OD"] = "71ff31b2715715d4186df3df4c027ef5";
                        m_AllEEL["EE_Hair01SideKare_F_AA"] = "1565a9c6b87149342a3b39e6e70a7fac";
                        m_AllEEL["EE_Hair01Uncombed_M_HL"] = "5be39f9c4c2efe3449697cd18e3382cf";
                        m_AllEEL["EE_Hair02BobCut_F_DW"] = "5234c949717920c49a37f78b0425cd4d";
                        m_AllEEL["EE_Hair02BraidNurah_F_HL"] = "c165e6837a09f354ea643884d73349fa";
                        m_AllEEL["EE_Hair02CurlyWoljif_M_TL"] = "e60c9687e2e852143b0ddd32d4d65c0b";
                        m_AllEEL["EE_Hair02FrenchBraid_F_DE"] = "3897a5ce68b630548bb85db0a68a465a";
                        m_AllEEL["EE_Hair02FrenchBraid_F_EL"] = "db566d919de425443bd9ae0a37de3ec9";
                        m_AllEEL["EE_Hair02LongFrontBraids_M_DW"] = "520a58ef81234414e9c9ed4098db6e8c";
                        m_AllEEL["EE_Hair02LongGreasy_F_HO"] = "7f2ea7fe76e725f4493bde6f3fb6c321";
                        m_AllEEL["EE_Hair02LongGreasy_M_HO"] = "e8c406599ddec12468d2310ba8420f14";
                        m_AllEEL["EE_Hair02MediumAnevia_F_AA"] = "e3a41513e3314e047ae3bbb41a00e7e4";
                        m_AllEEL["EE_Hair02MediumAnevia_F_CM"] = "2138e447019e0054ea50b839771a55db";
                        m_AllEEL["EE_Hair02MediumAnevia_F_DH"] = "fc77867c044d74c43b2f44a81129411e";
                        m_AllEEL["EE_Hair02MediumAnevia_F_HM"] = "37c94a80df80dd944a85ce72d0cd7b22";
                        m_AllEEL["EE_Hair02MediumAnevia_F_SU"] = "4b6876c286fb36945a7776a9860939e9";
                        m_AllEEL["EE_Hair02MediumEmo_M_AA"] = "91683796385f12548a22743d0c2061de";
                        m_AllEEL["EE_Hair02MediumStraight_M_GN"] = "cad04c33305fa1e40a209ddcbd2c3837";
                        m_AllEEL["EE_Hair02MediumTinyBraid_M_EL"] = "54aae291e3449c14792bbe2592228d4d";
                        m_AllEEL["EE_Hair02MediumWild_F_TL"] = "c81febf186ba543438e5dec7d1c9bcf7";
                        m_AllEEL["EE_Hair02Military_M_DH"] = "3e255bdd4face9d48b6ba9ab4e8c6b35";
                        m_AllEEL["EE_Hair02Military_M_HM"] = "88c2650d77d9a3c4a8a861fa0d8d0aae";
                        m_AllEEL["EE_Hair02Mohawk_F_OD"] = "a5ae610bb609f6441bbde48668ef70a5";
                        m_AllEEL["EE_Hair02Mohawk_M_OD"] = "72f950be8e9316141ad454d6708129d9";
                        m_AllEEL["EE_Hair02PonytailUpper_F_GN"] = "93f1f6c976851f547a17c0779d29e531";
                        m_AllEEL["EE_Hair02Savage_M_HL"] = "b8b1ad579cccee542bf90e30428301f3";
                        m_AllEEL["EE_Hair02SideKare_F_HE"] = "19411a87224e19540beaa6ef2d4ec8dd";
                        m_AllEEL["EE_Hair02SideKare_M_HE"] = "ac422faaaa2e20747b8ea88e80af486e";
                        m_AllEEL["EE_Hair03LongBraids_M_EL"] = "e6cb686fb8663734f96ceeacfa2e2400";
                        m_AllEEL["EE_Hair03LongCurly_M_HM"] = "7e499d0408e81f545989c7379225756d";
                        m_AllEEL["EE_Hair03LongWavy_F_HE"] = "de99a284a347d35438251f35fd12d63b";
                        m_AllEEL["EE_Hair03LongWavy_M_HE"] = "9dd5b5f95b692c446910de6cd5ff7a3e";
                        m_AllEEL["EE_Hair03LongWild_F_TL"] = "afa22656ed5030c4ba273583ba2b3a16";
                        m_AllEEL["EE_Hair03LongWild_M_TL"] = "50eac92ba30862940be4f70d329d070a";
                        m_AllEEL["EE_Hair03Long_F_DH"] = "af187dac5e1588d4a8fef2448d6ca779";
                        m_AllEEL["EE_Hair03Long_F_HM"] = "c229d194613b246449ea3a57bb615681";
                        m_AllEEL["EE_Hair03MediumCombedBack_M_HO"] = "bfe521e258ee7b644bf6cca3ca988bf5";
                        m_AllEEL["EE_Hair03MediumMess_M_AA"] = "6f7020d7788477d498233b40095320ff";
                        m_AllEEL["EE_Hair03Medium_F_HL"] = "8045964289f590b4ebd3f700302cdfd7";
                        m_AllEEL["EE_Hair03Medium_M_HL"] = "7f24e471c2623154fa9a6c853948e918";
                        m_AllEEL["EE_Hair03MohawkTwoBraids_M_DW"] = "c5f98e5c96206e7449ee18acda006d81";
                        m_AllEEL["EE_Hair03OneSideMohawk_F_HO"] = "37cbcead4468082438ee2b77ba4b9b38";
                        m_AllEEL["EE_Hair03OnionCut_M_GN"] = "230f45428a98714489da83ff48b002c9";
                        m_AllEEL["EE_Hair03Pompadour_F_EL"] = "21099c170f7b8344d90d6f034fb5554c";
                        m_AllEEL["EE_Hair03PonytailAsymmetric_F_AA"] = "118df5f79be3d664ca97624ace52a46e";
                        m_AllEEL["EE_Hair03Porcupine_F_GN"] = "26c8179538195f74a99745513b5678b6";
                        m_AllEEL["EE_Hair03Short_F_OD"] = "b1ecaedfe0f5c944d845e5ffd9e7f1a4";
                        m_AllEEL["EE_Hair03Short_M_OD"] = "16d278ed786110340bbd8703eb3df032";
                        m_AllEEL["EE_Hair03TwoDonuts_F_DW"] = "e04d4d716916150459079a0af3c5c2ba";
                        m_AllEEL["EE_Hair04Crown_M_OD"] = "e29cfa5c0ab60c34d9c0310f448230f8";
                        m_AllEEL["EE_Hair04GnomishCrest_M_GN"] = "edfb07a592589d3498d307d8321631de";
                        m_AllEEL["EE_Hair04Linzi_F_HL"] = "7e4093b9c0a319a409392b0df8fda75b";
                        m_AllEEL["EE_Hair04LongBack_F_AA"] = "7522188768d3bcc4f9691eb3d0e68cfc";
                        m_AllEEL["EE_Hair04LongBangs_M_AA"] = "529d34f25213726479ef973a47a02c8f";
                        m_AllEEL["EE_Hair04LongCurly_F_HE"] = "918f52fa9e49f6d439c70abc652f89f7";
                        m_AllEEL["EE_Hair04LongCurly_M_HL"] = "08239741c456c4149a95bd14688fc9f7";
                        m_AllEEL["EE_Hair04LongStraight_M_EL"] = "d69743688fc27584887a7c29a774289e";
                        m_AllEEL["EE_Hair04LongTwoBraids_F_DW"] = "7122c7023ed192b4baa5de268f2da0ba";
                        m_AllEEL["EE_Hair04LongWavy_M_DH"] = "38b879c4f1c3e3f4687fe89fe9d3abd2";
                        m_AllEEL["EE_Hair04LongWavy_M_HM"] = "303578a648d8d344b8d3a9a94fe24d5a";
                        m_AllEEL["EE_Hair04Long_M_HE"] = "79947d8418bc88a46af55abefe1868bf";
                        m_AllEEL["EE_Hair04MediumArueshalae_F_HM"] = "fb79b0f415a030f4b9983e61b7d480fa";
                        m_AllEEL["EE_Hair04MediumArueshalae_F_SU"] = "a62b78dbb66a68247b03168502abfc0e";
                        m_AllEEL["EE_Hair04MediumUncombed_F_GN"] = "f83f7bc24f6d28c4494a4ed82f068d63";
                        m_AllEEL["EE_Hair04Mohawk_M_TL"] = "5509e7a1d63e1b14097292be114e4d00";
                        m_AllEEL["EE_Hair04PixieLong_F_OD"] = "ca4204da40d572b47a2de7415abca7a1";
                        m_AllEEL["EE_Hair04PonyHawk_F_HO"] = "9356e94d2208cf540a34ff3a7c8f2267";
                        m_AllEEL["EE_Hair04PonyHawk_M_DW"] = "23f7230410a592b41b751421a5682da5";
                        m_AllEEL["EE_Hair04PonyTailLush_F_EL"] = "9ec441743ea20c5488e7f497992042ed";
                        m_AllEEL["EE_Hair04Ponyhawk_M_HO"] = "e51b3c4fa15baae45a13b5ad8e28c94b";
                        m_AllEEL["EE_Hair05BobCut_F_AA"] = "3a5c17ae553da8f4a828402b24bcf3d4";
                        m_AllEEL["EE_Hair05BobCut_F_DH"] = "a27662b6ffbceb64094060aa368eeb32";
                        m_AllEEL["EE_Hair05BobCut_F_HM"] = "16befdf0bdb05424891138d02a718848";
                        m_AllEEL["EE_Hair05BraidShavedTemples_F_GN"] = "ef55d74e89da45042bd26bb52e974471";
                        m_AllEEL["EE_Hair05LibertySpikes_F_OD"] = "1ae45e229dc4dbc4a83ef2064b6ba0c4";
                        m_AllEEL["EE_Hair05LongBraids_F_HO"] = "6b1ddf341f07e49469a29a6b8571a1db";
                        m_AllEEL["EE_Hair05LongTwoBraids_M_DW"] = "f4d5575b3af79a449b086380799535e0";
                        m_AllEEL["EE_Hair05LongWavyBraids_F_TL"] = "ee38a6b141c8ac54697fba55ce144094";
                        m_AllEEL["EE_Hair05LongWavy_F_HL"] = "ebeb7bd740edaf04d871b1d040c10fa0";
                        m_AllEEL["EE_Hair05LongWavy_M_TL"] = "05eb3b064eb501149b1715156850bb8f";
                        m_AllEEL["EE_Hair05Mature_M_GN"] = "3b83462520d7d9145b3479454dae6bd0";
                        m_AllEEL["EE_Hair05MediumBun_M_AA"] = "d0d147e4a9ff9c345b336501018c17b2";
                        m_AllEEL["EE_Hair05MediumCurly_M_HO"] = "a6ec284f75ea1f440a55447bb02cf90d";
                        m_AllEEL["EE_Hair05MediumMess_M_HL"] = "bee1ad535ae23fe43920e76c0ff5f574";
                        m_AllEEL["EE_Hair05Medium_M_DH"] = "32af38f1ad4239542a6ddf37d1bc5d87";
                        m_AllEEL["EE_Hair05Medium_M_HM"] = "739bcec114cc0974bbe60990afc9f8fd";
                        m_AllEEL["EE_Hair05Military_M_HE"] = "9d474dc56ec45af4ebf39d3ef68200d1";
                        m_AllEEL["EE_Hair05PonyHawk_F_DW"] = "393a4c979e29f9d40bf97bc79d1ad3f5";
                        m_AllEEL["EE_Hair05PonytailClassic_F_HE"] = "ab19b4ef03602ed439eb87552e0ca67b";
                        m_AllEEL["EE_Hair05Senior_M_OD"] = "25b5299d3149437459ed66977a7a1385";
                        m_AllEEL["EE_Hair06BobCut_F_OD"] = "e50163688063ff04cbe8f6d6235678e8";
                        m_AllEEL["EE_Hair06BrushUp_M_GN"] = "210cecceca9218949a501bd033ef5bc2";
                        m_AllEEL["EE_Hair06Daeran_M_AA"] = "7530921e80f4a9943ba2bca026ddf3a7";
                        m_AllEEL["EE_Hair06DreadTail_F_TL"] = "103e1f478c298a748bb13445840bc4c5";
                        m_AllEEL["EE_Hair06LongWavyBraids_F_AA"] = "62dfe086393d26943b7e756fb2569247";
                        m_AllEEL["EE_Hair06LongWavy_F_DH"] = "92098e7849f8b8742bcbc03605016e85";
                        m_AllEEL["EE_Hair06LongWavy_F_HM"] = "30d504db6b8cbe94dbc82d2437c8b468";
                        m_AllEEL["EE_Hair06MediumAnevia_F_EL"] = "131955108c91c2448a78f8021ca358a9";
                        m_AllEEL["EE_Hair06MediumAnevia_F_GN"] = "7e8c5b3e1973c5449bf921ca01073e62";
                        m_AllEEL["EE_Hair06MediumAnevia_F_HE"] = "a5efed3983dd97342a04e8335b9a8bdc";
                        m_AllEEL["EE_Hair06MediumBun_M_DH"] = "bc84e2564823a6743a2fbfe56c6ce4f8";
                        m_AllEEL["EE_Hair06MediumBun_M_EL"] = "d82a4cc22803f8d4a895d8cb50013dab";
                        m_AllEEL["EE_Hair06MediumBun_M_HM"] = "609143dbf7607f6419babaf5748b82dc";
                        m_AllEEL["EE_Hair06MediumBun_M_TL"] = "58115a09ef40db046adc4bb99c1ec5b8";
                        m_AllEEL["EE_Hair06MediumCurly_M_HL"] = "dafb0ac1abb39534b9c8581b2c8619eb";
                        m_AllEEL["EE_Hair06MediumSlick_M_HE"] = "c7534bfe96280584884e17f8a076cf63";
                        m_AllEEL["EE_Hair06Pompadour_M_OD"] = "70a19b1705cc0b34db6bd9cd73f12f87";
                        m_AllEEL["EE_Hair07BobCut_F_HE"] = "fc59094132d3b99418d82bd12a3f50f8";
                        m_AllEEL["EE_Hair07LongBangs_M_DH"] = "ffb641f88970a1f46bf9bed683d4efcc";
                        m_AllEEL["EE_Hair07LongCamelia_F_AA"] = "b6ed73aa8db434a48afab56b6296181d";
                        m_AllEEL["EE_Hair07Long_F_EL"] = "57c595a0dece66f4283e888dc52d9df1";
                        m_AllEEL["EE_Hair07MediumBun_M_HE"] = "26f2556760b2121418cb7a909e0e80bc";
                        m_AllEEL["EE_Hair07Military_M_AA"] = "4049f905c9dc7714793c654a577774b4";
                        m_AllEEL["EE_Hair07Mohawk_M_HM"] = "222890293b0f66145a400eae3432868d";
                        m_AllEEL["EE_Hair07NobleBraids_F_DW"] = "1762cab3d178f53489f43ab791b87f9c";
                        m_AllEEL["EE_Hair07PixieCut_F_GN"] = "42fc80a711b8e2d4eb07fdd9c7822c4b";
                        m_AllEEL["EE_Hair07PixieCut_F_HO"] = "5930529c9d8eaa041b457dd7a722dd32";
                        m_AllEEL["EE_Hair07PonytailClassic_F_DH"] = "830e116d49d302346ab03e0e5009c39b";
                        m_AllEEL["EE_Hair07PonytailClassic_F_HM"] = "f32da5106fa223844b88c426e36b5821";
                        m_AllEEL["EE_Hair07PonytailClassic_F_TL"] = "01adb2fc579b26a419a6ea83867c824b";
                        m_AllEEL["EE_Hair07PonytailClassic_M_DE"] = "bd4323ff9cc3c264f956228134395807";
                        m_AllEEL["EE_Hair07PonytailClassic_M_DW"] = "06e9c7725f9cc6349a363be23ae1e542";
                        m_AllEEL["EE_Hair07PonytailClassic_M_EL"] = "8e9394a7a860ead42b2d5acdfb35e3f5";
                        m_AllEEL["EE_Hair07PonytailClassic_M_HO"] = "095e60d6935c4c6479a94cff9880df07";
                        m_AllEEL["EE_Hair08LongCurly_F_AA"] = "738b1694d13be8e4a994a9e48247a318";
                        m_AllEEL["EE_Hair08LongWavyBraids_F_HE"] = "c8edacbc502d42242a5911ba000a411e";
                        m_AllEEL["EE_Hair08LongWavy_M_AA"] = "3049a579a436de245b9a307e72c34c5a";
                        m_AllEEL["EE_Hair08MediumEmo_M_DH"] = "8778ae88736ef1b43bf8e0a63724df2c";
                        m_AllEEL["EE_Hair08MediumSide_M_GN"] = "61c42687c5cccef4eb8958bbc2297caa";
                        m_AllEEL["EE_Hair08MediumSlick_M_HM"] = "128110ed2a1f2f2438f690b1850149d7";
                        m_AllEEL["EE_Hair08Mohawk_F_TL"] = "d90a0bf179ad5884a98092b58d8f76ad";
                        m_AllEEL["EE_Hair08SideKare_F_DH"] = "293c10a3e2836b74bb3da7d02b4cb6e7";
                        m_AllEEL["EE_Hair08SideKare_F_EL"] = "39d65ebde5c324f41821b36258791ee5";
                        m_AllEEL["EE_Hair08SideKare_F_HM"] = "3fa56cc5d206ca142bde8f93ad089a02";
                        m_AllEEL["EE_Hair08SideKare_F_HO"] = "e780cdb097a1d154b9cf9635b191f832";
                        m_AllEEL["EE_Hair09BowlCut_F_HM"] = "a7848ed746958e74ab7e9549dd4db16c";
                        m_AllEEL["EE_Hair09LongCamelia_F_DH"] = "a819551f9c43d2248aa53c50c642bfca";
                        m_AllEEL["EE_Hair09MediumEmo_M_HM"] = "3e4bb0a0defe2f74e9fb07ee4fb68d8f";
                        m_AllEEL["EE_Hair10LongBangs_M_HM"] = "acdcfd7609f88ae49833e4f10656190e";
                        m_AllEEL["EE_Hair10SideBunNenio_F_HM"] = "db6e295d58b7a6a4686fa4c4b5b4b527";
                        m_AllEEL["EE_Hair11PonytailUpper_F_HM"] = "1f19aaaa1870e2b4b8bd99d36211ddf6";
                        m_AllEEL["EE_Hair13LongCurly_F_HM"] = "b40cf766b28e512438678fc9e2aa34f1";
                        m_AllEEL["EE_Hair14LongWavyBraids_F_HM"] = "779458079f7718c4bb960d9cef195339";
                        m_AllEEL["EE_HalfplateDemonic_F_Any"] = "8dd40451dbc645f4bbf634388b63b1a1";
                        m_AllEEL["EE_HalfplateDemonic_M_Any"] = "983caeaaa0560c14e945dc5491253210";
                        m_AllEEL["EE_HalfplateKnightNormal_F_Any"] = "86c22e5c4c7ea0a4b8c1c43d90fe725e";
                        m_AllEEL["EE_HalfplateKnightNormal_M_Any"] = "3f1bc6195175f1c4b85b8393975d391a";
                        m_AllEEL["EE_HalfplateKnightPainted_F_Any"] = "b279c8f46a6ac804da5e473f75b8a777";
                        m_AllEEL["EE_HalfplateKnightPainted_M_Any"] = "68076e88a2a2bf744b58e066c3c140fa";
                        m_AllEEL["EE_HaramakiCommon_F_Any"] = "85136da833fde59478a443a925523849";
                        m_AllEEL["EE_HaramakiCommon_M_Any"] = "275234a68f48bd949aa0ef41cefd9364";
                        m_AllEEL["EE_HaramakiKnight_F_Any"] = "85ae0def5df3ea543a9b1bc429972675";
                        m_AllEEL["EE_HaramakiKnight_M_Any"] = "29dc6ed54aa5c9544b14996b49873ffc";
                        m_AllEEL["EE_HatArchmage_F_DW"] = "b57ec6f160575b04a9bb247d7e6988de";
                        m_AllEEL["EE_HatArchmage_F_EL"] = "eb353fbe4d34f91419023eae1a0c627e";
                        m_AllEEL["EE_HatArchmage_F_GN"] = "85cc9e89714980f45998430cba32f979";
                        m_AllEEL["EE_HatArchmage_F_HE"] = "0d64e6202b41f9b43bda50828275295a";
                        m_AllEEL["EE_HatArchmage_F_HL"] = "50979bfaf80653a4c839c8caafa4985f";
                        m_AllEEL["EE_HatArchmage_F_HM"] = "0851a6a91a9c1374798d02c25e9b8b7d";
                        m_AllEEL["EE_HatArchmage_F_HO"] = "84af8a6e83abd0b4fb28649c0a2b2a16";
                        m_AllEEL["EE_HatArchmage_F_KT"] = "90cedd6ae55b48b46914b91fd94f2cd5";
                        m_AllEEL["EE_HatArchmage_F_OD"] = "f8c86112a4312b94dbc110c7a062e184";
                        m_AllEEL["EE_HatArchmage_F_TL"] = "80a2e35a455bb3b43b81e4d8a2ab2ac4";
                        m_AllEEL["EE_HatArchmage_M_DW"] = "4f014a1baf321604aa8578b33b430337";
                        m_AllEEL["EE_HatArchmage_M_EL"] = "0240b07a29d9b8142938c16a147d9041";
                        m_AllEEL["EE_HatArchmage_M_GN"] = "e88f7f90fae2aed45891a94d69389c5a";
                        m_AllEEL["EE_HatArchmage_M_HE"] = "af735da922a1abd419272e7d546f065c";
                        m_AllEEL["EE_HatArchmage_M_HL"] = "0ed967dad9f576c4890520cfca700b56";
                        m_AllEEL["EE_HatArchmage_M_HM"] = "30c4a6c3fedffa84cbb08c3af2d3473d";
                        m_AllEEL["EE_HatArchmage_M_HO"] = "f0deff26909ae924ebad74a075220a99";
                        m_AllEEL["EE_HatArchmage_M_KT"] = "365546f7791cbfa40abe47503d808d75";
                        m_AllEEL["EE_HatArchmage_M_OD"] = "809e31e3e8d3cff4c889efae0a34cc14";
                        m_AllEEL["EE_HatArchmage_M_TL"] = "77a7596b8cd400b47b132f5942e41168";
                        m_AllEEL["EE_HatAreshkagal_F_DW"] = "ea9e94f9426855c46b9dba64c034ef11";
                        m_AllEEL["EE_HatAreshkagal_F_EL"] = "f25dcd9a09478ec47a9103c7ed5c99dc";
                        m_AllEEL["EE_HatAreshkagal_F_GN"] = "cf947d0b9bf89274e8400e6392ba412f";
                        m_AllEEL["EE_HatAreshkagal_F_HE"] = "51983acc1ec2cce4b99589c56746ab14";
                        m_AllEEL["EE_HatAreshkagal_F_HL"] = "79f526bec91c3614ca67b6008fefa40d";
                        m_AllEEL["EE_HatAreshkagal_F_HM"] = "500af969f32095b4fa3a2d9f2bf8dff9";
                        m_AllEEL["EE_HatAreshkagal_F_HO"] = "928a578f6ee834a43943e4d555d3ccec";
                        m_AllEEL["EE_HatAreshkagal_F_KT"] = "e6f78a11a9263744289f240d311220c1";
                        m_AllEEL["EE_HatAreshkagal_F_OD"] = "574b19b5c83624d4d836895c246b39c0";
                        m_AllEEL["EE_HatAreshkagal_F_TL"] = "33d7360f5f508754e9889e9aef88ed66";
                        m_AllEEL["EE_HatAreshkagal_M_DW"] = "cc7afd7d39b2a3c49ab99f70d86d33fa";
                        m_AllEEL["EE_HatAreshkagal_M_EL"] = "5a1733261c402c64f96a9949ed98d789";
                        m_AllEEL["EE_HatAreshkagal_M_GN"] = "7860f1cf50894b5439b359d2746a68b1";
                        m_AllEEL["EE_HatAreshkagal_M_HE"] = "11d9eabbf998c6f46aea0c395a403c57";
                        m_AllEEL["EE_HatAreshkagal_M_HL"] = "e48db775a974d114f9efe9978a76148b";
                        m_AllEEL["EE_HatAreshkagal_M_HM"] = "9f5ac8b0520e8b845a1505ecfd920d2f";
                        m_AllEEL["EE_HatAreshkagal_M_HO"] = "0354f7d43bcceb940a5a712e9706ce18";
                        m_AllEEL["EE_HatAreshkagal_M_KT"] = "8f11e16186bdef34cba5c36c16b6180e";
                        m_AllEEL["EE_HatAreshkagal_M_OD"] = "e8ed825c56d8a2e449248455f35db5db";
                        m_AllEEL["EE_HatAreshkagal_M_TL"] = "3a9d36d2e1e44a84ebd20437208d2c67";
                        m_AllEEL["EE_HatBard_F_DW"] = "3c26e80407f86c84caaa52a55d70a3ed";
                        m_AllEEL["EE_HatBard_F_EL"] = "0318c91839f86d24b90c8c0fd8f9bff3";
                        m_AllEEL["EE_HatBard_F_GN"] = "be2b77bddf442b94fb66057d42910868";
                        m_AllEEL["EE_HatBard_F_HE"] = "3574467618f4d6246bcb6a4bfbc98922";
                        m_AllEEL["EE_HatBard_F_HL"] = "5274b05e6c0e0e74db99176d9cc6e8bc";
                        m_AllEEL["EE_HatBard_F_HM"] = "9ffcbb2f7fb8f6c4c876fe0e9e64f18c";
                        m_AllEEL["EE_HatBard_F_HO"] = "dab28ee8ecc89314fbf0ab122c08fd57";
                        m_AllEEL["EE_HatBard_F_KT"] = "8e55d3c99ece08249bafdaad7ec70cd0";
                        m_AllEEL["EE_HatBard_F_OD"] = "f84a4fc7e8084a24fa8ece13fcec05ba";
                        m_AllEEL["EE_HatBard_F_TL"] = "02b8d50080336e14a838a0cb6ba201f8";
                        m_AllEEL["EE_HatBard_M_DW"] = "a9507f02ff5afce48b39635936b4103d";
                        m_AllEEL["EE_HatBard_M_EL"] = "0cd673df9e7987944ab99174464b229d";
                        m_AllEEL["EE_HatBard_M_GN"] = "ea67209efa110294c8addfd2c1e82c95";
                        m_AllEEL["EE_HatBard_M_HE"] = "74aa9b53964be2c4c8be22eeb7a9905f";
                        m_AllEEL["EE_HatBard_M_HL"] = "1635622a83768d64ea33c600a4e260b0";
                        m_AllEEL["EE_HatBard_M_HM"] = "ef1ab72407f2e1646a4f7f98b9e81aa4";
                        m_AllEEL["EE_HatBard_M_HO"] = "844ab2963615e024ca589802cc78eae6";
                        m_AllEEL["EE_HatBard_M_KT"] = "8d5f11b037dea10458960b33bb4b2823";
                        m_AllEEL["EE_HatBard_M_OD"] = "fcc945b79a0883f4fbaef18c608f49e0";
                        m_AllEEL["EE_HatBard_M_TL"] = "1b0b5f27663f6e848b2504fd4ecb8dee";
                        m_AllEEL["EE_HatExperts_F_DW"] = "481952a562d80a145a888536afd9bbcc";
                        m_AllEEL["EE_HatExperts_F_EL"] = "a2092a9f7fa9d084f98b8e6b1a06aa1e";
                        m_AllEEL["EE_HatExperts_F_GN"] = "4bcf9acd1be032e458646b53e36507c2";
                        m_AllEEL["EE_HatExperts_F_HE"] = "192319734539748449b9c166e17b2885";
                        m_AllEEL["EE_HatExperts_F_HL"] = "94c72f9b14a48ab4e983898d87f55696";
                        m_AllEEL["EE_HatExperts_F_HM"] = "470d0ae420671114b86adf6be699ea51";
                        m_AllEEL["EE_HatExperts_F_HO"] = "0f31d11fefa59fe4eadb0b0762aded7a";
                        m_AllEEL["EE_HatExperts_F_KT"] = "17a5197be01f9b84b9a9ac4a1f15c291";
                        m_AllEEL["EE_HatExperts_F_OD"] = "8511539365a0c374f9535d2bba71b32b";
                        m_AllEEL["EE_HatExperts_F_TL"] = "c926e64b64a25d248b5f389120231565";
                        m_AllEEL["EE_HatExperts_M_DW"] = "a54d77d74937dca41b0a0a5e20af1ae1";
                        m_AllEEL["EE_HatExperts_M_EL"] = "cb730f938401d74458feb85936344613";
                        m_AllEEL["EE_HatExperts_M_GN"] = "ee824fd7eefb9a74690207423e691183";
                        m_AllEEL["EE_HatExperts_M_HE"] = "e344bc59b642a634d80b46cd7ba185ca";
                        m_AllEEL["EE_HatExperts_M_HL"] = "4ec254f0238655c4b9f4162193850a0b";
                        m_AllEEL["EE_HatExperts_M_HM"] = "5ed870f898ce66043a42ebb336650e18";
                        m_AllEEL["EE_HatExperts_M_HO"] = "9c53e7b6aeb66204d84092c5d3ce2aae";
                        m_AllEEL["EE_HatExperts_M_KT"] = "990d506cf2aef124ab2334970e715599";
                        m_AllEEL["EE_HatExperts_M_OD"] = "7e571fb2883b69143a9a6be2b5d402e2";
                        m_AllEEL["EE_HatExperts_M_TL"] = "4e4c419483e7c05449dc01920dac9fcd";
                        m_AllEEL["EE_HatFarmer1_F_DW"] = "7e553193a6d3de44281e673fc8756a64";
                        m_AllEEL["EE_HatFarmer1_F_EL"] = "c8745c7f9a6adf244b3d0a1bbb7bd95d";
                        m_AllEEL["EE_HatFarmer1_F_GN"] = "3d001e0c485ee544e87062b062aa28a4";
                        m_AllEEL["EE_HatFarmer1_F_HE"] = "02a96192f7844a0469b383e9299dcb7f";
                        m_AllEEL["EE_HatFarmer1_F_HL"] = "287b97ce7d95b15498b432c9b89177cd";
                        m_AllEEL["EE_HatFarmer1_F_HM"] = "267953f3be0ec2c4f9d397908bb8ae98";
                        m_AllEEL["EE_HatFarmer1_F_HO"] = "ee1c1468ef31e8c4c98a37eaac131f78";
                        m_AllEEL["EE_HatFarmer1_F_OD"] = "d7525dc23da1c4847b8d2c5327b3d312";
                        m_AllEEL["EE_HatFarmer1_F_TL"] = "492196a6f5ca3514aab7055a4c1ad38d";
                        m_AllEEL["EE_HatFarmer1_M_DW"] = "2005e8fbae450b147a7f947782d64dea";
                        m_AllEEL["EE_HatFarmer1_M_EL"] = "715c2154ef783b045a4a5b1576a72a5a";
                        m_AllEEL["EE_HatFarmer1_M_GN"] = "232aaaa4f1588c74f963b63397d96993";
                        m_AllEEL["EE_HatFarmer1_M_HE"] = "70c3c23ee9ea7c1439446564fe4530e7";
                        m_AllEEL["EE_HatFarmer1_M_HL"] = "1c4b1b16fd58bfe4da850e71cf9e78ca";
                        m_AllEEL["EE_HatFarmer1_M_HM"] = "2c9c24e03fb53d6479c6657ef3ff9106";
                        m_AllEEL["EE_HatFarmer1_M_HO"] = "54376c96e832ced4c92b4ea8ca0a4065";
                        m_AllEEL["EE_HatFarmer1_M_KT"] = "b209f53c51bb5384db0e878b6fe1bfc6";
                        m_AllEEL["EE_HatFarmer1_M_OD"] = "c0af25f35925e80429ef8c94c8c7c5ea";
                        m_AllEEL["EE_HatFarmer1_M_TL"] = "2c97358df6a8e304f81db79d53ef6811";
                        m_AllEEL["EE_HatFarmer2_F_DW"] = "46de1f9d77a675f438eb4dcea023e97e";
                        m_AllEEL["EE_HatFarmer2_F_EL"] = "585662fd836c5e54aa68496419286224";
                        m_AllEEL["EE_HatFarmer2_F_GN"] = "d3a203c1502a3d44fb8aa2e7dc568fde";
                        m_AllEEL["EE_HatFarmer2_F_HE"] = "ab8c4505b2c85554292f8363acfa1e42";
                        m_AllEEL["EE_HatFarmer2_F_HL"] = "7da8a3999e980464e8b08947f40adcb7";
                        m_AllEEL["EE_HatFarmer2_F_HM"] = "b35a9f294b71c9c489be55b7347c6805";
                        m_AllEEL["EE_HatFarmer2_F_HO"] = "51007860fb4a0084f954fec254e6f291";
                        m_AllEEL["EE_HatFarmer2_F_OD"] = "5247896bed258334cbc497372e33dad3";
                        m_AllEEL["EE_HatFarmer2_F_TL"] = "99c90db1c305d6343a0dd8a16e2a224c";
                        m_AllEEL["EE_HatFarmer2_M_DW"] = "f8be50ccafc8fb2489542fbfb14f33d3";
                        m_AllEEL["EE_HatFarmer2_M_EL"] = "337b26a5fc1638e4b8321198dec2a470";
                        m_AllEEL["EE_HatFarmer2_M_GN"] = "b1ba257e1f994644b836a3fe08e8e8ba";
                        m_AllEEL["EE_HatFarmer2_M_HE"] = "22cb424c8563cd344a378556b3bf542a";
                        m_AllEEL["EE_HatFarmer2_M_HL"] = "561ddfc8be24d454594a61f460bcabdf";
                        m_AllEEL["EE_HatFarmer2_M_HM"] = "9e4b4e3002b8f1c43bfe21ebf8904bc8";
                        m_AllEEL["EE_HatFarmer2_M_HO"] = "1b6c066c8e1b6644ea1a28d8103e6ffc";
                        m_AllEEL["EE_HatFarmer2_M_OD"] = "ff18797012d53c84c9fb3a2cdcb6cfbb";
                        m_AllEEL["EE_HatFarmer2_M_TL"] = "deacdae272fdef94199b35f2a334a2ad";
                        m_AllEEL["EE_HatFarmer3_F_DW"] = "7242a1b497dc33f4c82b0ee9c0f9f47b";
                        m_AllEEL["EE_HatFarmer3_F_EL"] = "a884af1d95996734dac4f298c5c7411e";
                        m_AllEEL["EE_HatFarmer3_F_GN"] = "def81b91fe52f164dbcfbf81c6fa882b";
                        m_AllEEL["EE_HatFarmer3_F_HE"] = "145b9940457747442869cac81f86903f";
                        m_AllEEL["EE_HatFarmer3_F_HL"] = "468f50ca3696da246ad3a3bec219f263";
                        m_AllEEL["EE_HatFarmer3_F_HM"] = "2b9f23e1cfa7a6047a01f390d7088274";
                        m_AllEEL["EE_HatFarmer3_F_HO"] = "19dc605f8d35d8747a979682cd165c1d";
                        m_AllEEL["EE_HatFarmer3_F_KT"] = "26680ebd055e13b429f6ddf24473f2d5";
                        m_AllEEL["EE_HatFarmer3_F_OD"] = "5f22e9cd2309ef7439efbd66734695e5";
                        m_AllEEL["EE_HatFarmer3_F_TL"] = "27c4cfb1373f1d145a924bc8837b6de4";
                        m_AllEEL["EE_HatFarmer3_M_DW"] = "ec9a0db12337a834b9c45de377285a9f";
                        m_AllEEL["EE_HatFarmer3_M_EL"] = "c8af02dd23b939b44a55c30e46704282";
                        m_AllEEL["EE_HatFarmer3_M_GN"] = "f6c6b66029708dd458577102fdd80c71";
                        m_AllEEL["EE_HatFarmer3_M_HE"] = "af6379294aac1c24aa16499d91fc4c7a";
                        m_AllEEL["EE_HatFarmer3_M_HL"] = "e22e148aa2341e44cb22d2f7a630f551";
                        m_AllEEL["EE_HatFarmer3_M_HM"] = "17e25fdb7acbfb9418794a353e229ce3";
                        m_AllEEL["EE_HatFarmer3_M_HO"] = "2f593e225921efc47987e46a7eb2d94f";
                        m_AllEEL["EE_HatFarmer3_M_KT"] = "3416301bf29bfc4418d5ecbcc63f3341";
                        m_AllEEL["EE_HatFarmer3_M_OD"] = "4a6f2036880255d43b32c9c5557926b3";
                        m_AllEEL["EE_HatFarmer3_M_TL"] = "88a3accea48b41243a42a8261ab9c3af";
                        m_AllEEL["EE_HatIllusionist_F_DW"] = "e89695a37ded4b847868dbe5c47ab030";
                        m_AllEEL["EE_HatIllusionist_F_EL"] = "22b45213563ba144082678db951ed26f";
                        m_AllEEL["EE_HatIllusionist_F_GN"] = "23b43de350027b547a151919c8deb3fb";
                        m_AllEEL["EE_HatIllusionist_F_HE"] = "7bfaf4708c005f24d9688d5ff9a8f601";
                        m_AllEEL["EE_HatIllusionist_F_HL"] = "44c44a40884a0d840bca89ce3f1907d6";
                        m_AllEEL["EE_HatIllusionist_F_HM"] = "94b991d9152d8fd4dacac44e1eb13d30";
                        m_AllEEL["EE_HatIllusionist_F_HO"] = "26c3e618dd209574492df773094a5f33";
                        m_AllEEL["EE_HatIllusionist_F_KT"] = "d460f07592613d141909db4e4be7b96b";
                        m_AllEEL["EE_HatIllusionist_F_OD"] = "22570cc72571fd94594b525c9daa0337";
                        m_AllEEL["EE_HatIllusionist_F_TL"] = "9a293686d1338984c9e4190117e3b5fd";
                        m_AllEEL["EE_HatIllusionist_M_DW"] = "38853f9947b36c641b67aef1b2a78ac0";
                        m_AllEEL["EE_HatIllusionist_M_EL"] = "0bdcdebb9cc21a640b31e25ae517ade4";
                        m_AllEEL["EE_HatIllusionist_M_GN"] = "12e05f414e5159e4d8acf20ab86ada1f";
                        m_AllEEL["EE_HatIllusionist_M_HE"] = "b47741488e617a84eaada6ed9100a0ad";
                        m_AllEEL["EE_HatIllusionist_M_HL"] = "f6712e43ce5a6ba41825e3bce6dde4f7";
                        m_AllEEL["EE_HatIllusionist_M_HM"] = "057bfdca26392f44d92d8423787a572e";
                        m_AllEEL["EE_HatIllusionist_M_HO"] = "db0a4d231920f91478d82c241828da32";
                        m_AllEEL["EE_HatIllusionist_M_KT"] = "af305ac96aa7a9b42bb0de40858fe393";
                        m_AllEEL["EE_HatIllusionist_M_OD"] = "de2b9c7cd90af264d8a0b445fcf997ff";
                        m_AllEEL["EE_HatIllusionist_M_TL"] = "3692e899b52fd5e4e8bd70b3bf0d3182";
                        m_AllEEL["EE_HatInquisitor2_F_DW"] = "b8864d5bb25910347b18e08c422818be";
                        m_AllEEL["EE_HatInquisitor2_F_EL"] = "73aee3105bf76fb418c1a81b04b7b970";
                        m_AllEEL["EE_HatInquisitor2_F_GN"] = "4128603ed552f154d99b54349384509c";
                        m_AllEEL["EE_HatInquisitor2_F_HE"] = "bda06f1aff965bd4c8ca3f1dece01f17";
                        m_AllEEL["EE_HatInquisitor2_F_HL"] = "a3ce3d8897bdc78418c7b469a9a4bf72";
                        m_AllEEL["EE_HatInquisitor2_F_HM"] = "d00b126000179a64e868a0d1d5065f70";
                        m_AllEEL["EE_HatInquisitor2_F_HO"] = "f8ec0463fdc54c94095f8fcc59177e94";
                        m_AllEEL["EE_HatInquisitor2_F_KT"] = "c0d9b7d13edf341438bff7b89e52e5f9";
                        m_AllEEL["EE_HatInquisitor2_F_OD"] = "93b7a9958e63aac4eaa418a1a343d298";
                        m_AllEEL["EE_HatInquisitor2_F_TL"] = "67e53da4a70066b46a0f6ec8adf0dace";
                        m_AllEEL["EE_HatInquisitor2_M_DW"] = "243193b8f9ac41a45972d95abd6508d8";
                        m_AllEEL["EE_HatInquisitor2_M_EL"] = "ae217ba78ab79f14dac2195af6bd6a9a";
                        m_AllEEL["EE_HatInquisitor2_M_GN"] = "77a927628486f134698be530cb3ea043";
                        m_AllEEL["EE_HatInquisitor2_M_HE"] = "54ce502cb0c292c4cb1b7f0f6994e528";
                        m_AllEEL["EE_HatInquisitor2_M_HL"] = "4b53657047e7e5648b67cccc32288b45";
                        m_AllEEL["EE_HatInquisitor2_M_HM"] = "4a234880b5a94714fb7d4185daeda622";
                        m_AllEEL["EE_HatInquisitor2_M_HO"] = "9e11b9786354f4f4381ab8a68b5cbf9b";
                        m_AllEEL["EE_HatInquisitor2_M_KT"] = "56d65d2a71b1ece419abe1ec4e440b54";
                        m_AllEEL["EE_HatInquisitor2_M_OD"] = "5bc2e43f95a219d48ba13fa7c2b515b1";
                        m_AllEEL["EE_HatInquisitor2_M_TL"] = "e955e7d61e4d8374ca740e93471ab857";
                        m_AllEEL["EE_HatMarksman_F_DW"] = "b535f3b4729534843a28f32d7c8fc4e6";
                        m_AllEEL["EE_HatMarksman_F_EL"] = "c8ebf62fdc2497c47bf04c1aca6b629b";
                        m_AllEEL["EE_HatMarksman_F_GN"] = "87f184ed9652f5c4a85d9e6bab149ed6";
                        m_AllEEL["EE_HatMarksman_F_HE"] = "5717e227a94934a44b7fc3c8ce3f4dcf";
                        m_AllEEL["EE_HatMarksman_F_HL"] = "4ce5a8680209a924eb7ff171ff71d93a";
                        m_AllEEL["EE_HatMarksman_F_HM"] = "742bf8ab65e55874dad3ce0c30fe0f0a";
                        m_AllEEL["EE_HatMarksman_F_HO"] = "d4fce563bbf237843ba0a1165dba23f8";
                        m_AllEEL["EE_HatMarksman_F_KT"] = "62d2bae8a96e87e4788c9be3ec944b52";
                        m_AllEEL["EE_HatMarksman_F_OD"] = "902bb78543d27df4f9a578c701117af9";
                        m_AllEEL["EE_HatMarksman_F_TL"] = "2760940b1baa74244b8af4f57da8d5dc";
                        m_AllEEL["EE_HatMarksman_M_DW"] = "ddd1961faec1c7340acfc677ffe7443b";
                        m_AllEEL["EE_HatMarksman_M_EL"] = "fd7c14a6318cb9149afa7724bad0bbeb";
                        m_AllEEL["EE_HatMarksman_M_GN"] = "4070f4a4744cf654ca6e414e47bc694a";
                        m_AllEEL["EE_HatMarksman_M_HE"] = "a9b71e9d11d458f4aab2b47258d02e9f";
                        m_AllEEL["EE_HatMarksman_M_HL"] = "a2e6d4ace8dff9c4d9793c38503445da";
                        m_AllEEL["EE_HatMarksman_M_HM"] = "cb81ca02f47de17479dcf2e34543f59b";
                        m_AllEEL["EE_HatMarksman_M_HO"] = "37ed29172ebba0046bb15834604deae5";
                        m_AllEEL["EE_HatMarksman_M_KT"] = "d94ad8e7e38b93b4289b303e89e7e621";
                        m_AllEEL["EE_HatMarksman_M_OD"] = "92de008e29f43944ca553756e3aeeaaf";
                        m_AllEEL["EE_HatMarksman_M_TL"] = "cae342326cf78404e9b0e20f598d1cd8";
                        m_AllEEL["EE_HatTricornFeathered_F_DW"] = "52ff533ef9a706447b22e48c3a4ea616";
                        m_AllEEL["EE_HatTricornFeathered_F_EL"] = "3cf53054286fc64429195f82f5bd5d61";
                        m_AllEEL["EE_HatTricornFeathered_F_GN"] = "039a69ffc08156b478782d3424b72ad4";
                        m_AllEEL["EE_HatTricornFeathered_F_HE"] = "92e4d077834f8114ebb12af8bf096da0";
                        m_AllEEL["EE_HatTricornFeathered_F_HL"] = "454ea616fedede646ba709a69ecf4105";
                        m_AllEEL["EE_HatTricornFeathered_F_HM"] = "9268b9af20790b745a528f9a328c515d";
                        m_AllEEL["EE_HatTricornFeathered_F_HO"] = "0772d23adcb5a6c4da573f85de8a9bb2";
                        m_AllEEL["EE_HatTricornFeathered_F_KT"] = "ddc2bea69d3895541bc4d9ace6c85259";
                        m_AllEEL["EE_HatTricornFeathered_F_OD"] = "e798423adfe4d0e4198760d0bb1e8f16";
                        m_AllEEL["EE_HatTricornFeathered_F_TL"] = "791772c21d75a444693ef612b0bf5ee5";
                        m_AllEEL["EE_HatTricornFeathered_M_DW"] = "8e80bd8b97a9ed8418c0a42e5d92de53";
                        m_AllEEL["EE_HatTricornFeathered_M_EL"] = "f30ea379e52d3194d9b10b6015bf518b";
                        m_AllEEL["EE_HatTricornFeathered_M_GN"] = "50f0b8c185424a7498baa5195fec35f9";
                        m_AllEEL["EE_HatTricornFeathered_M_HE"] = "4387c2420e9cee14c9ad4c3f964924c8";
                        m_AllEEL["EE_HatTricornFeathered_M_HL"] = "4126cfa9c1bf91343933e61b355e9209";
                        m_AllEEL["EE_HatTricornFeathered_M_HM"] = "ae731f4e76b9d5c4bb0794a8cfcd7364";
                        m_AllEEL["EE_HatTricornFeathered_M_HO"] = "109e264d2ea373f4aa2b519676966704";
                        m_AllEEL["EE_HatTricornFeathered_M_KT"] = "d65e65a4040618649aaaf2f7a448043b";
                        m_AllEEL["EE_HatTricornFeathered_M_OD"] = "bf657815619b0f54a9208975f57bcd2f";
                        m_AllEEL["EE_HatTricornFeathered_M_TL"] = "a90e0f216ed080840859685a7fbea89a";
                        m_AllEEL["EE_HatWitch_F_DW"] = "b1dad6653aeb67244937467fb80b9012";
                        m_AllEEL["EE_HatWitch_F_EL"] = "a6f50772523d11f499b6c2c255e7cd42";
                        m_AllEEL["EE_HatWitch_F_GN"] = "4e15af5fb1fcd04478d282f82f4f3f50";
                        m_AllEEL["EE_HatWitch_F_HE"] = "caa7b398bf709c343b60adba1d5639dc";
                        m_AllEEL["EE_HatWitch_F_HL"] = "d664e401b6c51654f8c28ab4370b0335";
                        m_AllEEL["EE_HatWitch_F_HM"] = "e5a0b08ab2a4ec046954b660df9e43b1";
                        m_AllEEL["EE_HatWitch_F_HO"] = "a88c670424827914b94b6757c1ec8f70";
                        m_AllEEL["EE_HatWitch_F_KT"] = "25cef79a817b0c947944c5e2f1d7cfc2";
                        m_AllEEL["EE_HatWitch_F_OD"] = "0724766ab7516ce4ea771300887549cd";
                        m_AllEEL["EE_HatWitch_F_TL"] = "cb44e32f0b1ec714ba426cbd9a7a47d6";
                        m_AllEEL["EE_HatWitch_M_DW"] = "51e0c881c0328f24fbda2af7645dd6e5";
                        m_AllEEL["EE_HatWitch_M_EL"] = "52583f5f800eb054ea89285c7e5b6cd6";
                        m_AllEEL["EE_HatWitch_M_GN"] = "8e7195dc30e4637429446c45b4f59ad7";
                        m_AllEEL["EE_HatWitch_M_HE"] = "fc4b922bbb7f9d942bdfaea34d567cd3";
                        m_AllEEL["EE_HatWitch_M_HL"] = "e01078aaac589784ab05a6271966288a";
                        m_AllEEL["EE_HatWitch_M_HM"] = "1437d9b4ebf636141819919d95768eb2";
                        m_AllEEL["EE_HatWitch_M_HO"] = "1c1ea0ef576542b418b23eb193e014d2";
                        m_AllEEL["EE_HatWitch_M_KT"] = "83956856200c08d4bb6bc73b18ea557c";
                        m_AllEEL["EE_HatWitch_M_OD"] = "f07df504744f9ee45b3f217785ddb3ef";
                        m_AllEEL["EE_HatWitch_M_TL"] = "5927eaa13e7bd2f47aa2ad9ac629081e";
                        m_AllEEL["EE_Head01A_F_AA"] = "691eaa2873cddc543afafdfbde22c29f";
                        m_AllEEL["EE_Head01A_F_KT"] = "0412d70836fe0b947884dcaf8f345f22";
                        m_AllEEL["EE_Head01A_M_AA"] = "cf88c5aea47268e4c82578270d4e9c33";
                        m_AllEEL["EE_Head01A_M_KT"] = "f899a03df5f1be5448a40693a7ce3572";
                        m_AllEEL["EE_Head01B_F_AA"] = "0d1b18dccc6e9dc4e9b574dbd6d550ec";
                        m_AllEEL["EE_Head01B_F_KT"] = "187c2d326f6b1184eb985010ae35cc22";
                        m_AllEEL["EE_Head01B_M_AA"] = "722dcfafc90a8e948b97dea17e5e2383";
                        m_AllEEL["EE_Head01B_M_KT"] = "f7ccdeb39f9d45145b8a73dbd902aa6e";
                        m_AllEEL["EE_Head01C_F_KT"] = "8661c51a25896ee40a724bcfa82fa05f";
                        m_AllEEL["EE_Head01C_M_KT"] = "09629795c40f6874aa82e8545582595e";
                        m_AllEEL["EE_Head01Lann_M_MM"] = "3db40a2abb296674fa67abf04a6be08f";
                        m_AllEEL["EE_Head01Wenduag_F_MM"] = "1a5c533c0b1a7b849895ff7bc1bc3dde";
                        m_AllEEL["EE_Head01_F_CM"] = "adf16fac763c9c542bdc7023deaed992";
                        m_AllEEL["EE_Head01_F_DE"] = "7ebaa5640e231dd4c8d38e444125237e";
                        m_AllEEL["EE_Head01_F_DH"] = "fa221031116367e42bade4a48bfbdf81";
                        m_AllEEL["EE_Head01_F_DW"] = "9450284cb917a814fadd2ef4a7567cc6";
                        m_AllEEL["EE_Head01_F_EL"] = "88de141bc54dac341807774895458f03";
                        m_AllEEL["EE_Head01_F_GN"] = "0b5ca4949682a5e49ad3bbaac1002bae";
                        m_AllEEL["EE_Head01_F_HE"] = "7368eafa3f5ee9d4bb9739b80faefdc3";
                        m_AllEEL["EE_Head01_F_HL"] = "daab1ee723617ff4ea5f57615d696fe5";
                        m_AllEEL["EE_Head01_F_HM"] = "971d4ec9ff97af447b415c8eb4c5b0b5";
                        m_AllEEL["EE_Head01_F_HO"] = "263deaab2915da84d93e4c2733d1eda4";
                        m_AllEEL["EE_Head01_F_OD"] = "28680b13930e71543aae24947f6f8abf";
                        m_AllEEL["EE_Head01_F_SU"] = "939370b9218391540b6add533b473b4a";
                        m_AllEEL["EE_Head01_F_TL"] = "7b27b2063f548794e845e0ee8ea7b91b";
                        m_AllEEL["EE_Head01_F_ZB"] = "f4ee24031a828844eb470281d865c2c2";
                        m_AllEEL["EE_Head01_M_CM"] = "6b3a5fb88dbe8d34a867a8608bfd884e";
                        m_AllEEL["EE_Head01_M_DE"] = "33bfbbb3454f452498c81b0ac89afa1c";
                        m_AllEEL["EE_Head01_M_DH"] = "10d1ba990b1b9994ab75793188323178";
                        m_AllEEL["EE_Head01_M_DW"] = "6aaa747cde3013843865f8570ca65e27";
                        m_AllEEL["EE_Head01_M_EL"] = "8f09863992345be4ebc299f18309aa7c";
                        m_AllEEL["EE_Head01_M_GL"] = "95da445819ab073439a31dde22c3f5ce";
                        m_AllEEL["EE_Head01_M_GN"] = "a8519e2af4fa77e4e96077f4ad84cc22";
                        m_AllEEL["EE_Head01_M_HE"] = "c2091e3d655f6c2409d2bbc354bbee65";
                        m_AllEEL["EE_Head01_M_HL"] = "1e4cafa72c2a2f5468c83868873f31ec";
                        m_AllEEL["EE_Head01_M_HM"] = "4eea3ef5f2e01474ba5b03fe28324ad3";
                        m_AllEEL["EE_Head01_M_HO"] = "717e2297037cfe747b0dc68e30a29eed";
                        m_AllEEL["EE_Head01_M_OD"] = "ea09e2afbb944ac46a16d616d50fe281";
                        m_AllEEL["EE_Head01_M_SU"] = "f507bac03a8cbd144a1b7c9dc3542aee";
                        m_AllEEL["EE_Head01_M_TL"] = "6ae0e2be0e8f9f54981033b4a61f11ed";
                        m_AllEEL["EE_Head01_M_ZB"] = "54467c755531d854da4618a3d4b4c8b6";
                        m_AllEEL["EE_Head02A_F_KT"] = "6123f562a2835e14a9b1405cf93686ba";
                        m_AllEEL["EE_Head02A_M_KT"] = "8227821b57a45cc499fd4586be79a7ba";
                        m_AllEEL["EE_Head02AfroA_F_AA"] = "77c839f8984b4d349904d23914fbfaa0";
                        m_AllEEL["EE_Head02AfroA_M_AA"] = "8966ee57ce6db704da5a08fa35802ae5";
                        m_AllEEL["EE_Head02AfroB_F_AA"] = "c9dd2fb38a628ae42abd7ce4dc96e4f5";
                        m_AllEEL["EE_Head02AfroB_M_AA"] = "a61ddc35348e38d40be01c773f87e83f";
                        m_AllEEL["EE_Head02Afro_F_DH"] = "0a7c27b37884adf47b7eae341998a26e";
                        m_AllEEL["EE_Head02Afro_F_HE"] = "c8f9110b660479a43a49a9c0bf776cb1";
                        m_AllEEL["EE_Head02Afro_F_HM"] = "536cf2cb6e39c01478ec2230865aa34f";
                        m_AllEEL["EE_Head02Afro_M_DH"] = "7010e3455a285b24f8d1353ff982dbc4";
                        m_AllEEL["EE_Head02Afro_M_HE"] = "da5c9d5affe49134bb88c6bc4cdfd1d5";
                        m_AllEEL["EE_Head02Arueshalae_F_SU"] = "367d9a9444d82464d9ee09707a5aebc2";
                        m_AllEEL["EE_Head02Asian_F_HO"] = "3395eea497b4c2f43a2044b82a4eb3c1";
                        m_AllEEL["EE_Head02Asian_M_HO"] = "eab410cf83fc51d40a8d3e602a15d821";
                        m_AllEEL["EE_Head02B_F_KT"] = "b25c60a3e25c93c428f540c81bb6b55f";
                        m_AllEEL["EE_Head02B_M_KT"] = "be22df3367aa97e41806fc6fab31a976";
                        m_AllEEL["EE_Head02C_F_KT"] = "52a6af5594fe4644f8023b46034491dc";
                        m_AllEEL["EE_Head02C_M_KT"] = "7e3ab82670f15a045b767f5d6466f2a0";
                        m_AllEEL["EE_Head02Ember_F_EL"] = "86f27fd89aeef2641b4202b34df99c86";
                        m_AllEEL["EE_Head02Greybor_M_DW"] = "376f6f11fdaa67946962fc9f7afc7e93";
                        m_AllEEL["EE_Head02Nurah_F_HL"] = "a3f03306ae2808e43ac62ae31ca0c525";
                        m_AllEEL["EE_Head02Regill_M_GN"] = "e56cfb36299b37441aff09d98da4a7e2";
                        m_AllEEL["EE_Head02Sosiel_M_HM"] = "b4391c415e9de0f4d9a30d2d0f8ea09b";
                        m_AllEEL["EE_Head02Wight_F_ZB"] = "b12b6242dc0db384ab4766d88bd7b01e";
                        m_AllEEL["EE_Head02Wight_M_ZB"] = "dfeec626aeadd944c90f97b309ad3ee7";
                        m_AllEEL["EE_Head02_F_DW"] = "f3555e3e4948a2a47a8aae2ce8a83200";
                        m_AllEEL["EE_Head02_F_GN"] = "b803440a324753a49b861ef4b83ba46b";
                        m_AllEEL["EE_Head02_F_OD"] = "ae255f4bda091434d9749303072d3d2f";
                        m_AllEEL["EE_Head02_F_TL"] = "4a4afd8ea46ff2e438bb078495bd3531";
                        m_AllEEL["EE_Head02_M_EL"] = "1324171abd40c254184b767dfe669f47";
                        m_AllEEL["EE_Head02_M_GL"] = "b2f09125a27e9d947a865f257b20397e";
                        m_AllEEL["EE_Head02_M_HL"] = "dc909ad95bed35447899d1e940e254dd";
                        m_AllEEL["EE_Head02_M_OD"] = "f5129be1a471a834b9a1753ac5445df2";
                        m_AllEEL["EE_Head02_M_TL"] = "b354195728faa79449de9b3197f3b449";
                        m_AllEEL["EE_Head03A_F_KT"] = "fe61066c32dce2746a8382eed3f800a9";
                        m_AllEEL["EE_Head03A_M_KT"] = "ab2f104d7da82b04096926588b1d9931";
                        m_AllEEL["EE_Head03AsianA_F_AA"] = "850dc46595447d647b5a8b2ca5c14585";
                        m_AllEEL["EE_Head03AsianA_M_AA"] = "5a4dede9dc7084448999a379aed38148";
                        m_AllEEL["EE_Head03AsianB_F_AA"] = "e1ea47e80c91ef740ba05e0205245bac";
                        m_AllEEL["EE_Head03AsianB_M_AA"] = "dc157b1ca60ba0240a4dcdd24526c442";
                        m_AllEEL["EE_Head03Asian_F_DH"] = "4932afc2c15ee2c4dae23fcea181187a";
                        m_AllEEL["EE_Head03Asian_F_HM"] = "022cceb94e2e34f4298f1a69e5402967";
                        m_AllEEL["EE_Head03Asian_M_DH"] = "34698163d24dfe44bb57a61fd11ba266";
                        m_AllEEL["EE_Head03Asian_M_HE"] = "a817e92ab29c61f43993f7468c739723";
                        m_AllEEL["EE_Head03Asian_M_HM"] = "08f56c3654d2d424a8cf05d656a07bbe";
                        m_AllEEL["EE_Head03B_F_KT"] = "3e06d6c73dbfc784faf21d5263e84881";
                        m_AllEEL["EE_Head03B_M_KT"] = "47009e9da206fea4d98ccffdd824de26";
                        m_AllEEL["EE_Head03BodakDamaged_F_ZB"] = "6a53ce7eb7c3cd54f8356d411aa15103";
                        m_AllEEL["EE_Head03BodakDamaged_M_ZB"] = "17b5d981caac5384d90f14a67cd1b407";
                        m_AllEEL["EE_Head03C_F_KT"] = "b5557b26e1cb015439856936706e0f91";
                        m_AllEEL["EE_Head03C_M_KT"] = "f425aeb797e5d2e47a9d197be278583f";
                        m_AllEEL["EE_Head03Camellia_F_HE"] = "2ef1574b4ed61e84791ee3d9148ce4df";
                        m_AllEEL["EE_Head03Dwarf_M_ZB"] = "f0f85f09f4224fb4c8e0d8a984981eb9";
                        m_AllEEL["EE_Head03Staunton_M_DW"] = "ff0fa561246a130458848779cc4dc14c";
                        m_AllEEL["EE_Head03_F_DW"] = "a9f158095e313be40b93a2aac3e32ec7";
                        m_AllEEL["EE_Head03_F_EL"] = "3353daed386e3984494d07b635a46f22";
                        m_AllEEL["EE_Head03_F_GN"] = "db2b627b11f46b04abc1b17fc21a76cc";
                        m_AllEEL["EE_Head03_F_HL"] = "f9768da2381844f4ab7ff3d0ef55c4c0";
                        m_AllEEL["EE_Head03_F_HO"] = "889e660bae2aa9f41a82c16c10ccfd1d";
                        m_AllEEL["EE_Head03_F_OD"] = "cc588920159c38c42a1a40be010caefa";
                        m_AllEEL["EE_Head03_F_TL"] = "49b32d9af554e6742bed805d80ccde93";
                        m_AllEEL["EE_Head03_M_EL"] = "0e89b65d143a7f34b98ce2f6b5b420ed";
                        m_AllEEL["EE_Head03_M_GL"] = "9445c484a85419b42ba18a7a20ab72f5";
                        m_AllEEL["EE_Head03_M_GN"] = "3698d6355441f1e4ea01ec67dada85c5";
                        m_AllEEL["EE_Head03_M_HL"] = "4ef35de3720ba0948a1cdd4bbe239dcf";
                        m_AllEEL["EE_Head03_M_HO"] = "1eb9b77c2561fed4f841cf16d7e5dfb1";
                        m_AllEEL["EE_Head03_M_OD"] = "79d4c90de2ce25546a70cc4abf57c017";
                        m_AllEEL["EE_Head03_M_TL"] = "a8b95db20e630214dabfb79424494c34";
                        m_AllEEL["EE_Head04A_F_AA"] = "c962ba7e6a4988842b5986046656e5e6";
                        m_AllEEL["EE_Head04B_F_AA"] = "1520b340167ee0e4fa370bbcb70d1963";
                        m_AllEEL["EE_Head04BodakClean_F_ZB"] = "761e4fd643cabdc458a8cf33005e6a89";
                        m_AllEEL["EE_Head04BodakClean_M_ZB"] = "cd37654f90aa027408169afd33f9c10e";
                        m_AllEEL["EE_Head04Daeran_M_AA"] = "cd8d57e009fee654595fa2495b024cd1";
                        m_AllEEL["EE_Head04Good_M_HM"] = "9149601d28921464ab3e8fe0ecd3b462";
                        m_AllEEL["EE_Head04Woljif_M_TL"] = "cdc8632dd9b07744eb7baa143e39bf06";
                        m_AllEEL["EE_Head04_F_DH"] = "d546e45c29e820d42b7e89364c92d413";
                        m_AllEEL["EE_Head04_F_DW"] = "6270ef81077b94c4faf1967ec1e474b8";
                        m_AllEEL["EE_Head04_F_EL"] = "e643d76c861da57418e2d700df12cfe4";
                        m_AllEEL["EE_Head04_F_GN"] = "efcdc4d18513538488375c1786301bc3";
                        m_AllEEL["EE_Head04_F_HE"] = "c3c0a999ac0c17d47a6d0b97f73f2d21";
                        m_AllEEL["EE_Head04_F_HL"] = "1b95041eb7818664fb0dde7c92dbb023";
                        m_AllEEL["EE_Head04_F_HM"] = "5bc92526cbe57564f96530aefe05e4c6";
                        m_AllEEL["EE_Head04_F_HO"] = "2469680c6a387cb46891c29e36915e28";
                        m_AllEEL["EE_Head04_F_OD"] = "f553c7d101e4bf740b33a0a42d79b8c3";
                        m_AllEEL["EE_Head04_F_TL"] = "f7fdd6a03bc43da4da6d913a57f28c7c";
                        m_AllEEL["EE_Head04_M_DH"] = "4bfbf79688b80be40bb342a7e8117c6f";
                        m_AllEEL["EE_Head04_M_DW"] = "9d7fe82eb773eeb4fa0118988f9687ee";
                        m_AllEEL["EE_Head04_M_EL"] = "88cb3430d5dacfa4392870f97083f979";
                        m_AllEEL["EE_Head04_M_GN"] = "8507d5cdf00b74542a9c3b0a99ec4f28";
                        m_AllEEL["EE_Head04_M_HE"] = "238d737b8edc6174685b58babd02d79f";
                        m_AllEEL["EE_Head04_M_HL"] = "c8c9b08a9b5210944a5421e0a1b563f7";
                        m_AllEEL["EE_Head04_M_HO"] = "1cb8a6d149f308b44a880b5f15fb4a9d";
                        m_AllEEL["EE_Head04_M_OD"] = "efe90f170706cd644a63d5c15b918c7d";
                        m_AllEEL["EE_Head05A_F_AA"] = "925e5815d579d8044a478c2882d05f51";
                        m_AllEEL["EE_Head05A_M_AA"] = "ea436409685bf8b49acff98bfb0c7148";
                        m_AllEEL["EE_Head05B_F_AA"] = "744440e13e3416843b0ba90cbddd4744";
                        m_AllEEL["EE_Head05B_M_AA"] = "757d3093306895347bbe8e1f1dc4a13c";
                        m_AllEEL["EE_Head05_F_DH"] = "da0a42f7ffc479148b21ce90d08cc6b9";
                        m_AllEEL["EE_Head05_F_EL"] = "f4308998cd0db524097f01c1083750b0";
                        m_AllEEL["EE_Head05_F_HE"] = "2744ddb25083f4740bd084d40d06b107";
                        m_AllEEL["EE_Head05_F_HM"] = "deb27e32c600dcc42ab7641de03a3fa8";
                        m_AllEEL["EE_Head05_F_TL"] = "a88d1147b9c76364db8d34d956bb6fcb";
                        m_AllEEL["EE_Head05_M_DH"] = "a541a78cb1755304a8c31c383a191a4e";
                        m_AllEEL["EE_Head05_M_EL"] = "1e9eed78359ec1c48b1d66f01058b7a7";
                        m_AllEEL["EE_Head05_M_HE"] = "fbe65d13e062d024ca5c7b99719a370d";
                        m_AllEEL["EE_Head05_M_HM"] = "913b11311ec6b9f48bfa23d1f3423154";
                        m_AllEEL["EE_Head05_M_TL"] = "a957a3408601f9046b56015f6c40a8d3";
                        m_AllEEL["EE_Head06A_M_AA"] = "12810088281d2ad4aabe218c74152234";
                        m_AllEEL["EE_Head06Axiomite_F_EL"] = "7fc29faa99709394e84958df6cd8607b";
                        m_AllEEL["EE_Head06Axiomite_M_EL"] = "834af545d6893cb40a1922e337bc5b0b";
                        m_AllEEL["EE_Head06B_M_AA"] = "9c3e9c6bf61759e48a05c4ee90e2c27a";
                        m_AllEEL["EE_Head06_F_DH"] = "4d682a7f4cc2c884197ab6911c7eddc8";
                        m_AllEEL["EE_Head06_F_HM"] = "1709c29af3fd6634cbcc4cf1a24dd8ee";
                        m_AllEEL["EE_Head06_M_DH"] = "a3203fc1047a6d246be616b2e8d84114";
                        m_AllEEL["EE_Head06_M_HM"] = "61ea9d574de4145469fdf7db7f83c951";
                        m_AllEEL["EE_Head07Villager_M_HM"] = "1649a6cb92729d547a0798d3eb655e13";
                        m_AllEEL["EE_Head07_F_HM"] = "55559eae6e70ef84c879803b7acecb5a";
                        m_AllEEL["EE_Head08_F_HM"] = "f0ae81a538f807f46816f8cb78709115";
                        m_AllEEL["EE_Head08_M_HM"] = "ad54ad44f7a4b404cbab88825d84e7b2";
                        m_AllEEL["EE_Head09Seelah_F_HM"] = "d4dc8479fcc84784c92e6e779dd25493";
                        m_AllEEL["EE_Head09_M_HM"] = "554fe1a46ebf83d40a461eb355c290f1";
                        m_AllEEL["EE_Head10Afro_M_HM"] = "6b509ef0e9ae4fe468d7a72e9a8b6612";
                        m_AllEEL["EE_Head10_F_HM"] = "02ac1a008378ca54e86830e7c949b80d";
                        m_AllEEL["EE_Head11Evil_M_HM"] = "80f9866c56cea9342a354636154d0fae";
                        m_AllEEL["EE_Head11Nenio_F_HM"] = "92baa745e0d29c641bd0525efd76f37c";
                        m_AllEEL["EE_Head12Coloxus_M_HM"] = "072062021cffbc648994bccbd5fd8a17";
                        m_AllEEL["EE_Head12Eritrice_F_KT"] = "dd8405501ffdb6e459f109de82138eb6";
                        m_AllEEL["EE_Head13Alderpash_M_HM"] = "bbd04613fbdf05243a7e72a060799bee";
                        m_AllEEL["EE_Head13Izyagna_F_HM"] = "24e389d5302d97e429dfe22768d1acf8";
                        m_AllEEL["EE_Head13_M_HM"] = "9c086c7c230d1b34f963e3a3f843fb7f";
                        m_AllEEL["EE_Head14_F_HM"] = "52a7bdb6964f65a41b735635a2513edd";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_DW"] = "8d1ad0ec0d6189845a94bba06f53b78d";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_EL"] = "2824a96d7c512094b875df7b70cf14cc";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_GN"] = "c2ab1693bd96854429aaa943363a71fd";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_HE"] = "1fee73b66630c394da91feb00309332f";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_HL"] = "74d0bf25a16750b4db2171e55ead9bd0";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_HM"] = "e387f1db7877ad34abb200cb5c1130a3";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_HO"] = "696081f6dbd4b1c468c12935bd4d13a0";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_KT"] = "fb4502db2eb847749b0c4ed7e022ef61";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_OD"] = "49d0a2db0febcac48ae766cc110e4b55";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_F_TL"] = "4c938b11e3e717d4f90d30ec0d992db3";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_DW"] = "9608fa013a8828d4c8f9363c4a03c1ed";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_EL"] = "331643d6859fa9941aa5856546d5a677";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_GN"] = "18097bc61a24f3546b019bb4b7e521ae";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_HE"] = "960bb1ee5dd50cb42accd4b2d4defb52";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_HL"] = "ed97fb7ab06b88a4bbaf109b62ba24f1";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_HM"] = "82cdf0396a0a5804d9df8fc61bc6aff4";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_HO"] = "76fdc2c769fe9e14aa42c432e613be1d";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_KT"] = "835644ad0df624b4bad9d91c203b5f5e";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_OD"] = "f199de3099795e34693baf22aa1411c1";
                        m_AllEEL["EE_HeadbandCrownHolyAzata_M_TL"] = "27393f774994374409834a55eb2ab879";
                        m_AllEEL["EE_HeadbandEvil_F_DW"] = "e36a250b5e56386489cec7f03f7ca487";
                        m_AllEEL["EE_HeadbandEvil_F_EL"] = "606dbac91ae1e6e4489ff3afb9c0d859";
                        m_AllEEL["EE_HeadbandEvil_F_GN"] = "515d90568044d0d4a9571179b74a3588";
                        m_AllEEL["EE_HeadbandEvil_F_HE"] = "944a9030a973b6a4181b5b6133f1e6f1";
                        m_AllEEL["EE_HeadbandEvil_F_HL"] = "39993e6c29294634289394e55c31d035";
                        m_AllEEL["EE_HeadbandEvil_F_HM"] = "5f895fb7bfba5a54cb0919e83baac1b9";
                        m_AllEEL["EE_HeadbandEvil_F_HO"] = "21162e8ef62290a48b1994460f291455";
                        m_AllEEL["EE_HeadbandEvil_F_KT"] = "33e9ded6a05b9e542acfae14e9e54f88";
                        m_AllEEL["EE_HeadbandEvil_F_OD"] = "7b5339d07cf035c40b966563341efb5a";
                        m_AllEEL["EE_HeadbandEvil_F_TL"] = "1af3cbbc295e9864ebcbfe6b8356178d";
                        m_AllEEL["EE_HeadbandEvil_M_DW"] = "346c9933120c0cd4b8ffb34c19c09368";
                        m_AllEEL["EE_HeadbandEvil_M_EL"] = "fee1fac2aace9d044bc99faaa9acfc64";
                        m_AllEEL["EE_HeadbandEvil_M_GN"] = "3d8d93e366cf8b948a96b88a8765c7a6";
                        m_AllEEL["EE_HeadbandEvil_M_HE"] = "1976f09218f995240b332da83edaf9bf";
                        m_AllEEL["EE_HeadbandEvil_M_HL"] = "4f5bb0fbeece18c41a6321b817693353";
                        m_AllEEL["EE_HeadbandEvil_M_HM"] = "69a0eb5ffd2210249a33cbeabf8bd88f";
                        m_AllEEL["EE_HeadbandEvil_M_HO"] = "679643a43678e0045a9c90a26c5067e4";
                        m_AllEEL["EE_HeadbandEvil_M_KT"] = "264d1231ddca09a4387fc77c588a4fd8";
                        m_AllEEL["EE_HeadbandEvil_M_OD"] = "9fbbae4e4eddf324aba563ce5d94123c";
                        m_AllEEL["EE_HeadbandEvil_M_TL"] = "28277661919f50d40aaa77a4e389f51a";
                        m_AllEEL["EE_HeadbandHoly_F_DW"] = "e575d548619bcf244830a046f611bc3d";
                        m_AllEEL["EE_HeadbandHoly_F_EL"] = "362512c159b1e7542a77a0f95cdb2caa";
                        m_AllEEL["EE_HeadbandHoly_F_GN"] = "c900d133ade46254c89fd2901f6f0fda";
                        m_AllEEL["EE_HeadbandHoly_F_HE"] = "345ba2bb31daa224cbae2eaed43dbbcf";
                        m_AllEEL["EE_HeadbandHoly_F_HL"] = "c327775fec0de6840bdeba23f7461993";
                        m_AllEEL["EE_HeadbandHoly_F_HM"] = "b09108150327b39449fa7d61c1222a41";
                        m_AllEEL["EE_HeadbandHoly_F_HO"] = "02a2db7c0ea665a4cb0c70b3de7dbfe4";
                        m_AllEEL["EE_HeadbandHoly_F_KT"] = "b4fdce3208f3ba64e928dc332346c690";
                        m_AllEEL["EE_HeadbandHoly_F_OD"] = "3ba6428d8198bb540bf7827a3b30c0bc";
                        m_AllEEL["EE_HeadbandHoly_F_TL"] = "e45c5c563e545f9429b9a1163a717017";
                        m_AllEEL["EE_HeadbandHoly_M_DW"] = "20f3dea66440c324a8cfda88b7facf43";
                        m_AllEEL["EE_HeadbandHoly_M_EL"] = "c51554c2d5b94b44c98a59ee33354556";
                        m_AllEEL["EE_HeadbandHoly_M_GN"] = "2eff5a0943d83e140acc1ce1aa096801";
                        m_AllEEL["EE_HeadbandHoly_M_HE"] = "60809e5d3fd8eb948a6b83affc320ecd";
                        m_AllEEL["EE_HeadbandHoly_M_HL"] = "dfd013904b8e7604c83cc99b1e101811";
                        m_AllEEL["EE_HeadbandHoly_M_HM"] = "3ba4e560bb10aee428da465d23a5d7b4";
                        m_AllEEL["EE_HeadbandHoly_M_HO"] = "80c583d36078f974480937f4bc81b750";
                        m_AllEEL["EE_HeadbandHoly_M_KT"] = "f87371f0c4d5a0d40852a145ac2526a9";
                        m_AllEEL["EE_HeadbandHoly_M_OD"] = "41ef49bcb2d78ab45990abf71726b023";
                        m_AllEEL["EE_HeadbandHoly_M_TL"] = "7bb655110d0b03842a7ef7d52bfd6d99";
                        m_AllEEL["EE_HeadbandKineticist_F_DW"] = "b12a365bd36650f4d95764e4bd8e1556";
                        m_AllEEL["EE_HeadbandKineticist_F_EL"] = "2adf7bcd5548c734eb17f321edf51446";
                        m_AllEEL["EE_HeadbandKineticist_F_GN"] = "4b50b05259767ee4e9c938b99db64e75";
                        m_AllEEL["EE_HeadbandKineticist_F_HE"] = "4d67ec8dc89579648823a952bc8bd34f";
                        m_AllEEL["EE_HeadbandKineticist_F_HL"] = "46ee46e9c2847244bb880c19db01180f";
                        m_AllEEL["EE_HeadbandKineticist_F_HM"] = "52fc2ecf8ecc5c04eb3abddc4dac44ec";
                        m_AllEEL["EE_HeadbandKineticist_F_HO"] = "cc47f7e328d457f4180046e4a791009c";
                        m_AllEEL["EE_HeadbandKineticist_F_KT"] = "7acee8eb1c28e2f4fa49cd8a70a9d5c5";
                        m_AllEEL["EE_HeadbandKineticist_F_OD"] = "07aea46f4d843104d8f740b858233dec";
                        m_AllEEL["EE_HeadbandKineticist_F_TL"] = "575329b96909ac844ac4cd135e56ba2f";
                        m_AllEEL["EE_HeadbandKineticist_M_DW"] = "eb4ee4cacd8f81f41b7fab982b51d414";
                        m_AllEEL["EE_HeadbandKineticist_M_EL"] = "e7d5d76ed259ec043b82e684a22db0a2";
                        m_AllEEL["EE_HeadbandKineticist_M_GN"] = "0e8ca7ee6d2724a42b22300cdbdd8bc5";
                        m_AllEEL["EE_HeadbandKineticist_M_HE"] = "e1c56782b0edb9442ac5a415b084fc53";
                        m_AllEEL["EE_HeadbandKineticist_M_HL"] = "f85dbdfdcbc2771449f508e5f25d9418";
                        m_AllEEL["EE_HeadbandKineticist_M_HM"] = "6f4171253d88c514fae5b9a10870a333";
                        m_AllEEL["EE_HeadbandKineticist_M_HO"] = "3045823cb57a4794f9a2d98d83bb3e4e";
                        m_AllEEL["EE_HeadbandKineticist_M_KT"] = "c22191898d27a0d4284c4c13dbcb8991";
                        m_AllEEL["EE_HeadbandKineticist_M_OD"] = "b78bfc36170d87148b4cc3e65363bf2b";
                        m_AllEEL["EE_HeadbandKineticist_M_TL"] = "ff4b2c4a164038245b682b1232df3647";
                        m_AllEEL["EE_HeadbandNeutral_F_DW"] = "a8a84b3c867db7e40a9a03c39dd80f1c";
                        m_AllEEL["EE_HeadbandNeutral_F_EL"] = "75e9ba5942342af4ba8861bca11cb40b";
                        m_AllEEL["EE_HeadbandNeutral_F_GN"] = "99837e222c9edb04e853b90f05883854";
                        m_AllEEL["EE_HeadbandNeutral_F_HE"] = "9253174d0e22c0c4ea4af8fcc7cb0eca";
                        m_AllEEL["EE_HeadbandNeutral_F_HL"] = "307b4f34ec4015b458343b7611507bfc";
                        m_AllEEL["EE_HeadbandNeutral_F_HM"] = "d43530a91b399d047a123a97f12f7583";
                        m_AllEEL["EE_HeadbandNeutral_F_HO"] = "1ef6a67054506874ab7691372bddc7c2";
                        m_AllEEL["EE_HeadbandNeutral_F_KT"] = "5c02bfa237eddeb4fb65de1de2096d17";
                        m_AllEEL["EE_HeadbandNeutral_F_OD"] = "b10f7bead51197c4395d13d9156f1f5b";
                        m_AllEEL["EE_HeadbandNeutral_F_TL"] = "0ede6ff9fd3ea194b8446a3556dcc0be";
                        m_AllEEL["EE_HeadbandNeutral_M_DW"] = "682b48a909302554fbf5d7484ed7c29a";
                        m_AllEEL["EE_HeadbandNeutral_M_EL"] = "dcd77808a7333564c9db5466a6a0ebbb";
                        m_AllEEL["EE_HeadbandNeutral_M_GN"] = "290b714d2109bc44586bc7c20551a4d4";
                        m_AllEEL["EE_HeadbandNeutral_M_HE"] = "374849d20a2c3b9479a8016bfe657a77";
                        m_AllEEL["EE_HeadbandNeutral_M_HL"] = "45d3c1c58b2efe44db041f6cdee94826";
                        m_AllEEL["EE_HeadbandNeutral_M_HM"] = "95a6950094305da4bb14235b67e44f6d";
                        m_AllEEL["EE_HeadbandNeutral_M_HO"] = "2e69342bec1a48e47a6068992912d341";
                        m_AllEEL["EE_HeadbandNeutral_M_KT"] = "e61374f2722e0254d8f7ebef2c9e126d";
                        m_AllEEL["EE_HeadbandNeutral_M_OD"] = "e285274d05364c245a5f45368b663625";
                        m_AllEEL["EE_HeadbandNeutral_M_TL"] = "9dc17ea3072dbb642a64d7765e2be8df";
                        m_AllEEL["EE_HelmetBascinet_F_DW"] = "656b9a29386040b47aeb146cecdc907d";
                        m_AllEEL["EE_HelmetBascinet_F_EL"] = "827b139c7c6dd42418f2350a5ba82249";
                        m_AllEEL["EE_HelmetBascinet_F_GN"] = "e5a1da23af6a2eb4a82e48258b649fa6";
                        m_AllEEL["EE_HelmetBascinet_F_HE"] = "21046d2a733e7bf4e804689a5c5fe2e5";
                        m_AllEEL["EE_HelmetBascinet_F_HL"] = "75d1c583e37dc6a4c95edad345e5487a";
                        m_AllEEL["EE_HelmetBascinet_F_HM"] = "325d0c853552f234b9a413252f345af4";
                        m_AllEEL["EE_HelmetBascinet_F_HO"] = "71b788f34ce14f54a937f70a00440986";
                        m_AllEEL["EE_HelmetBascinet_F_KT"] = "6aecd3de5229b114986f96c285e218df";
                        m_AllEEL["EE_HelmetBascinet_F_TL"] = "c66ee46dedb96e8498a2631463f9afd7";
                        m_AllEEL["EE_HelmetBascinet_M_DW"] = "86a75e7da01259344b860ad04caae8c6";
                        m_AllEEL["EE_HelmetBascinet_M_EL"] = "a4e9f9a74743d7a46884beb1f8b12123";
                        m_AllEEL["EE_HelmetBascinet_M_GN"] = "203a21973a64aa54582920163dfb3436";
                        m_AllEEL["EE_HelmetBascinet_M_HE"] = "122ea439f4421e14abe659c1e5460c28";
                        m_AllEEL["EE_HelmetBascinet_M_HL"] = "60a02390682bbbc45865b1257c8069e7";
                        m_AllEEL["EE_HelmetBascinet_M_HM"] = "06ff9a35de1e46a409dfc26fec10b66e";
                        m_AllEEL["EE_HelmetBascinet_M_HO"] = "268ace29d72200740903dcc49f1d4a17";
                        m_AllEEL["EE_HelmetBascinet_M_KT"] = "863f2dc9be256d24c85df3710655f8ae";
                        m_AllEEL["EE_HelmetBascinet_M_TL"] = "2b2aac547ee95234e89f72e867837d87";
                        m_AllEEL["EE_HelmetBrainHalo_F_HM"] = "1e6ea5843d1cd104ebc861543a523813";
                        m_AllEEL["EE_HelmetBrainHalo_M_HM"] = "cb13c5049d5d3e246885fa1b114b623a";
                        m_AllEEL["EE_HelmetBrainHalo_M_TL"] = "87337583107eea54c912af809856762b";
                        m_AllEEL["EE_HelmetBrainMask_F_DW"] = "8bbad40fe59cb4c4c87ec75b4d9310df";
                        m_AllEEL["EE_HelmetBrainMask_F_HE"] = "5930ae3948fe50842a664a635b9a7b18";
                        m_AllEEL["EE_HelmetBrainMask_F_HM"] = "0f9c532c25d06144f8c7cb14376876e3";
                        m_AllEEL["EE_HelmetBrainMask_F_HO"] = "7947a2a6df77d654ba76b1b4136076e7";
                        m_AllEEL["EE_HelmetBrainMask_F_TL"] = "5b2b0161ea8c70148acb1af2b029d539";
                        m_AllEEL["EE_HelmetBrainMask_M_DW"] = "67bedb0306d549d4eae9900783dca79e";
                        m_AllEEL["EE_HelmetBrainMask_M_HE"] = "b5184cc2d60c27b4f85ea9955b602179";
                        m_AllEEL["EE_HelmetBrainMask_M_HM"] = "c0db80305091de543bf388f57f8e89f9";
                        m_AllEEL["EE_HelmetBrainMask_M_HO"] = "82a431d99981cbc4bae20b80f63f3601";
                        m_AllEEL["EE_HelmetCultistBaphomet_F_DW"] = "c1022b3c7f0a9d049824c247c3922741";
                        m_AllEEL["EE_HelmetCultistBaphomet_F_EL"] = "ac3c3f514329e07419db484822cb7e0d";
                        m_AllEEL["EE_HelmetCultistBaphomet_F_GN"] = "c3ba1c36567b4a44b849098300d7b6cb";
                        m_AllEEL["EE_HelmetCultistBaphomet_F_HE"] = "28f7d64200140cc4e876444468e3e596";
                        m_AllEEL["EE_HelmetCultistBaphomet_F_HL"] = "40ee5a65d7cd7a944a5013383cf7e6cf";
                        m_AllEEL["EE_HelmetCultistBaphomet_F_HM"] = "8c126740cc84893409c2aaa5f466eba3";
                        m_AllEEL["EE_HelmetCultistBaphomet_F_HO"] = "a801c75b7700193479ea66fc23eda300";
                        m_AllEEL["EE_HelmetCultistBaphomet_F_OD"] = "8303b0c283713c645b9e76c2a9913d85";
                        m_AllEEL["EE_HelmetCultistBaphomet_F_TL"] = "8c57f6303abeb2b44aff7fba33f8839f";
                        m_AllEEL["EE_HelmetCultistBaphomet_M_DW"] = "b26939916502a404390daf2a9e1b2961";
                        m_AllEEL["EE_HelmetCultistBaphomet_M_EL"] = "d5671875cdc873e4da394fa242200bc2";
                        m_AllEEL["EE_HelmetCultistBaphomet_M_GN"] = "b865c104ede25604ab358b1fd7c1c7ec";
                        m_AllEEL["EE_HelmetCultistBaphomet_M_HE"] = "166cb218b3523f5429e4a3e334f2a3b5";
                        m_AllEEL["EE_HelmetCultistBaphomet_M_HL"] = "ac9f2f7e8dd996441963eecd00705daa";
                        m_AllEEL["EE_HelmetCultistBaphomet_M_HM"] = "0ba01590c5ef7b440898b51833dae377";
                        m_AllEEL["EE_HelmetCultistBaphomet_M_HO"] = "2a2c38b17a5eced47839afdbedaab389";
                        m_AllEEL["EE_HelmetCultistBaphomet_M_OD"] = "6a809369a604f25409a02090cf13c72f";
                        m_AllEEL["EE_HelmetCultistBaphomet_M_TL"] = "53ebb4d7da6b5344ea80ed209f422636";
                        m_AllEEL["EE_HelmetCultistDeskari_F_DW"] = "c8f21d017fac92342ad2fa7f77e33255";
                        m_AllEEL["EE_HelmetCultistDeskari_F_EL"] = "e8c0295ff9509a04bbb662c4ffe7c506";
                        m_AllEEL["EE_HelmetCultistDeskari_F_GN"] = "820dea471b812924d9ace4c424b9c611";
                        m_AllEEL["EE_HelmetCultistDeskari_F_HE"] = "35afa10ec20635747872f402b3d7d86a";
                        m_AllEEL["EE_HelmetCultistDeskari_F_HL"] = "e113bb57e5aaee048834cd4619c1aab5";
                        m_AllEEL["EE_HelmetCultistDeskari_F_HM"] = "a6389cf39b2aede4291c9180787e0482";
                        m_AllEEL["EE_HelmetCultistDeskari_F_HO"] = "0b1469ed2ae7c2f44ad9a264c82a9388";
                        m_AllEEL["EE_HelmetCultistDeskari_F_OD"] = "9b1c2e45ee32ff74bb1dc1e4da3371e7";
                        m_AllEEL["EE_HelmetCultistDeskari_F_TL"] = "14859772e9fe5364fbf103415b060be6";
                        m_AllEEL["EE_HelmetCultistDeskari_M_DW"] = "bf113008f282f1043828657cd22dd92a";
                        m_AllEEL["EE_HelmetCultistDeskari_M_EL"] = "81a9074d4ac549742998886cc2e9ada6";
                        m_AllEEL["EE_HelmetCultistDeskari_M_GN"] = "7c5b785d57968b344b5afd3a5999b18e";
                        m_AllEEL["EE_HelmetCultistDeskari_M_HE"] = "a9138558263223f4e8d8e09ffe6d7cd1";
                        m_AllEEL["EE_HelmetCultistDeskari_M_HL"] = "93329755a61ac6b438c6234cd44f4132";
                        m_AllEEL["EE_HelmetCultistDeskari_M_HM"] = "bb95c086b0f292345adaa5039c62b2bb";
                        m_AllEEL["EE_HelmetCultistDeskari_M_HO"] = "31513463d1a4e1a44bed322cafb238b5";
                        m_AllEEL["EE_HelmetCultistDeskari_M_OD"] = "db72bcde4eaa945468ed8e193ba1ea02";
                        m_AllEEL["EE_HelmetCultistDeskari_M_TL"] = "18335394355d11f43937f75d3952029c";
                        m_AllEEL["EE_HelmetDoomGuy_F_DW"] = "719d4806a692d59449a1c07621e8d5e3";
                        m_AllEEL["EE_HelmetDoomGuy_F_EL"] = "71a524c6c99afd3478452b97711cd3e7";
                        m_AllEEL["EE_HelmetDoomGuy_F_GN"] = "7990d665f791bbf49ad1f39d2bf0e1a1";
                        m_AllEEL["EE_HelmetDoomGuy_F_HE"] = "ea00846c8ecaf224ca350daf1de68cac";
                        m_AllEEL["EE_HelmetDoomGuy_F_HL"] = "cb29e613792db214a847c767375ccb4e";
                        m_AllEEL["EE_HelmetDoomGuy_F_HM"] = "7db40ec36f8906d45a16be589d98ae07";
                        m_AllEEL["EE_HelmetDoomGuy_F_HO"] = "d3c4e2fc6b230d84a9e8cc15d0746308";
                        m_AllEEL["EE_HelmetDoomGuy_F_OD"] = "e634713d98f4c2e418650d2b8515e915";
                        m_AllEEL["EE_HelmetDoomGuy_F_TL"] = "ec6a843032da3d7419c5dbad11a3e318";
                        m_AllEEL["EE_HelmetDoomGuy_M_DW"] = "87ccc7da26fe2a649826368a689794bd";
                        m_AllEEL["EE_HelmetDoomGuy_M_EL"] = "4610c7dc0b456fb4f8d7e95974027a5e";
                        m_AllEEL["EE_HelmetDoomGuy_M_GN"] = "f97e47445a0d435469d0a0143ecdddee";
                        m_AllEEL["EE_HelmetDoomGuy_M_HE"] = "37b347c58af2b8745a3cdb002b317935";
                        m_AllEEL["EE_HelmetDoomGuy_M_HL"] = "4e62008354403b844b67be2bea116288";
                        m_AllEEL["EE_HelmetDoomGuy_M_HM"] = "52a86583d49bd63458ecb6e4abe81020";
                        m_AllEEL["EE_HelmetDoomGuy_M_HO"] = "0eb7ac7fe58574740839106059ec01c4";
                        m_AllEEL["EE_HelmetDoomGuy_M_OD"] = "6a48dc6c254be24419967db1f84071bd";
                        m_AllEEL["EE_HelmetDoomGuy_M_TL"] = "26d2a56344305e74b9eb8564035412f8";
                        m_AllEEL["EE_HelmetEvilBronze_F_DW"] = "8d463c3193b1e5246ad39846996259fd";
                        m_AllEEL["EE_HelmetEvilBronze_F_EL"] = "2dd1e796f8d829b49887ed0749798885";
                        m_AllEEL["EE_HelmetEvilBronze_F_GN"] = "192a49b19e1dc6943b58950fdfbfc9b1";
                        m_AllEEL["EE_HelmetEvilBronze_F_HE"] = "b6d41e7b62a9fc74fbccdb1ae9b1d1d5";
                        m_AllEEL["EE_HelmetEvilBronze_F_HL"] = "f819425e7b9bda54fa82d19e78a4c11c";
                        m_AllEEL["EE_HelmetEvilBronze_F_HM"] = "d09abd54bd0f7bd4782cd2d988c1df7e";
                        m_AllEEL["EE_HelmetEvilBronze_F_HO"] = "9113c2a81ecca22439b071343ec56e8d";
                        m_AllEEL["EE_HelmetEvilBronze_F_OD"] = "ea2a095f36946ae4a84380363b1c4d34";
                        m_AllEEL["EE_HelmetEvilBronze_F_TL"] = "9c9e16994a116f044b35cd367e973236";
                        m_AllEEL["EE_HelmetEvilBronze_M_DW"] = "f2174f873bdbc1142896bfd447a61ce8";
                        m_AllEEL["EE_HelmetEvilBronze_M_EL"] = "6e566fea7870e4547a156ec173ad4f1b";
                        m_AllEEL["EE_HelmetEvilBronze_M_GN"] = "4610f06af53122a4c97b8e4fc4164547";
                        m_AllEEL["EE_HelmetEvilBronze_M_HE"] = "ed16257653d950349b13194f05327707";
                        m_AllEEL["EE_HelmetEvilBronze_M_HL"] = "63ac50e5625926645a53231b22316a61";
                        m_AllEEL["EE_HelmetEvilBronze_M_HM"] = "d01b68746466da44b807154e4610eca8";
                        m_AllEEL["EE_HelmetEvilBronze_M_HO"] = "fb6fcd1df10e32d4b80cdcd341993783";
                        m_AllEEL["EE_HelmetEvilBronze_M_OD"] = "bc490254cb4e7f245a256e735ffcd862";
                        m_AllEEL["EE_HelmetEvilBronze_M_TL"] = "0b4d70460769d3b4fa265bd3a615bf82";
                        m_AllEEL["EE_HelmetEvilPainted2_F_HM"] = "f1f6a06fe26505e408c95701ecd767ea";
                        m_AllEEL["EE_HelmetEvilPainted_F_DW"] = "22e0dd1616a207d43b28d6d7b80c5c59";
                        m_AllEEL["EE_HelmetEvilPainted_F_EL"] = "c189c2f27c2f0e74199b03f6d6d05be3";
                        m_AllEEL["EE_HelmetEvilPainted_F_GN"] = "388b743851b97e642a12d84a28250343";
                        m_AllEEL["EE_HelmetEvilPainted_F_HE"] = "2ddebd4c2181b0b438e5294dad3d794e";
                        m_AllEEL["EE_HelmetEvilPainted_F_HL"] = "06aecb6d494fae5498da5978463ea115";
                        m_AllEEL["EE_HelmetEvilPainted_F_HM"] = "a118c3f39a97b004e9c14fa00c1dc212";
                        m_AllEEL["EE_HelmetEvilPainted_F_HO"] = "e0451692a417e2c43ba58cc57c6d8404";
                        m_AllEEL["EE_HelmetEvilPainted_F_OD"] = "b46a185acf2592841af413d72dcd98d0";
                        m_AllEEL["EE_HelmetEvilPainted_F_TL"] = "6d64c8c9ba35edd4da42d742460677c1";
                        m_AllEEL["EE_HelmetEvilPainted_M_DW"] = "75591379c2387794ca6666852ddede14";
                        m_AllEEL["EE_HelmetEvilPainted_M_EL"] = "6608f17ef87c7f84d877f875c6a6a86a";
                        m_AllEEL["EE_HelmetEvilPainted_M_GN"] = "db70358ec3bab08488111bc6989a7626";
                        m_AllEEL["EE_HelmetEvilPainted_M_HE"] = "5239c0493293d3249a545a016fef3f2e";
                        m_AllEEL["EE_HelmetEvilPainted_M_HL"] = "f26a80d5b989ddd4a9ccd2e034dc42f7";
                        m_AllEEL["EE_HelmetEvilPainted_M_HM"] = "2934daa9edf07674e8ccc2f33301437a";
                        m_AllEEL["EE_HelmetEvilPainted_M_HO"] = "737142926e0eeb8408cb68e3208a7dfc";
                        m_AllEEL["EE_HelmetEvilPainted_M_OD"] = "343cc07a14e32784786ec7fe3125ca40";
                        m_AllEEL["EE_HelmetEvilPainted_M_TL"] = "9083ae0c8567d1e43bfeb8a3c989fe69";
                        m_AllEEL["EE_HelmetFlatTop_F_DW"] = "7c709193664d58e43a23c9cc93f001be";
                        m_AllEEL["EE_HelmetFlatTop_F_EL"] = "332f60133f6681a4aab7e4721eb15bfc";
                        m_AllEEL["EE_HelmetFlatTop_F_GN"] = "eb5498f9b19fac944b083e6899ec2f0b";
                        m_AllEEL["EE_HelmetFlatTop_F_HE"] = "9fdbbab7f72bddc4ead6ffe724af4033";
                        m_AllEEL["EE_HelmetFlatTop_F_HL"] = "eeb0a70118c55894290aed3c388438fd";
                        m_AllEEL["EE_HelmetFlatTop_F_HM"] = "63d2af45c59827b45ad6574e9e8d41ef";
                        m_AllEEL["EE_HelmetFlatTop_F_HO"] = "49ee717f986642641b956ba109dedcd5";
                        m_AllEEL["EE_HelmetFlatTop_F_KT"] = "da43a9557e13fa346bb1359807433ca5";
                        m_AllEEL["EE_HelmetFlatTop_F_OD"] = "a2943442b66a86246b7b880b244cb18a";
                        m_AllEEL["EE_HelmetFlatTop_F_TL"] = "d9cffbd909e28174e81a8a5a4180b96e";
                        m_AllEEL["EE_HelmetFlatTop_M_DW"] = "42bc219345beddf4aa7e5da86e461ab6";
                        m_AllEEL["EE_HelmetFlatTop_M_EL"] = "4d446a662bcad004986353ad40c5f445";
                        m_AllEEL["EE_HelmetFlatTop_M_GN"] = "391a4b88a48b10b4e82e6997045feee3";
                        m_AllEEL["EE_HelmetFlatTop_M_HE"] = "20e7e61cd81c11046934567306d0ed9c";
                        m_AllEEL["EE_HelmetFlatTop_M_HL"] = "5b78111f95b8ac64cad1ea5f8f478639";
                        m_AllEEL["EE_HelmetFlatTop_M_HM"] = "c10d11de1fb7cf6479914124de64df5a";
                        m_AllEEL["EE_HelmetFlatTop_M_HO"] = "34066eb52577073498bf9f46abfecfe7";
                        m_AllEEL["EE_HelmetFlatTop_M_KT"] = "02fab0251dd813846b41f3b071f594e8";
                        m_AllEEL["EE_HelmetFlatTop_M_OD"] = "611c5e690e1c8bd4ba1a36ed8c35f0af";
                        m_AllEEL["EE_HelmetFlatTop_M_TL"] = "8e713afb196feb24188a83193cee734e";
                        m_AllEEL["EE_HelmetFullplateDemodand_F_DW"] = "ce8fe0fcc2650fd4389fb78c9fa241b7";
                        m_AllEEL["EE_HelmetFullplateDemodand_F_EL"] = "606f78ade929069428dc456ded36e118";
                        m_AllEEL["EE_HelmetFullplateDemodand_F_GN"] = "8a6a8867266ba164b8deada72baf6c6f";
                        m_AllEEL["EE_HelmetFullplateDemodand_F_HE"] = "697c436fe1ecca14fb7e22c49c0766ad";
                        m_AllEEL["EE_HelmetFullplateDemodand_F_HL"] = "a793dec74e8d81d48837cb4e2207b86b";
                        m_AllEEL["EE_HelmetFullplateDemodand_F_HM"] = "65b9481d55fe77049be8548bf86988d5";
                        m_AllEEL["EE_HelmetFullplateDemodand_F_HO"] = "f82aa0fe0b481b64f950b09d0856da7f";
                        m_AllEEL["EE_HelmetFullplateDemodand_F_OD"] = "bacd2460706c21644965da959c26bf35";
                        m_AllEEL["EE_HelmetFullplateDemodand_F_TL"] = "5a42928bf0e94e74bb18303ddc321148";
                        m_AllEEL["EE_HelmetFullplateDemodand_M_DW"] = "193217f44c814c44fa8aff0407552427";
                        m_AllEEL["EE_HelmetFullplateDemodand_M_EL"] = "72f8063c36a7fb643ad1a1bb5f7ca76e";
                        m_AllEEL["EE_HelmetFullplateDemodand_M_GN"] = "0a4ec331ffcb30249a4b063a4857755f";
                        m_AllEEL["EE_HelmetFullplateDemodand_M_HE"] = "44bf8f5ee9a18c646a9462bd4e7a5d15";
                        m_AllEEL["EE_HelmetFullplateDemodand_M_HL"] = "623a081dc47b47c47b92478a2e78cd44";
                        m_AllEEL["EE_HelmetFullplateDemodand_M_HM"] = "fcf7ffba7d12e0449bac4a906e3fe130";
                        m_AllEEL["EE_HelmetFullplateDemodand_M_HO"] = "dfc387b87aaae6441b07470dff9ab4bb";
                        m_AllEEL["EE_HelmetFullplateDemodand_M_OD"] = "d337c79f4b5214e4292fe82cd021be2d";
                        m_AllEEL["EE_HelmetFullplateDemodand_M_TL"] = "068e659ca433fca4d812027706c92b85";
                        m_AllEEL["EE_HelmetFullplateDeskari_F_DW"] = "0a183ec01bd626747943ffd41101b78b";
                        m_AllEEL["EE_HelmetFullplateDeskari_F_EL"] = "a2c6e84dc25cc6949b71ba7eb7e0be83";
                        m_AllEEL["EE_HelmetFullplateDeskari_F_GN"] = "b5c2e2cd223ac43409533f76197aea30";
                        m_AllEEL["EE_HelmetFullplateDeskari_F_HE"] = "2c5d3cb74be0e214f9bac8a53c932865";
                        m_AllEEL["EE_HelmetFullplateDeskari_F_HL"] = "310a5fc8c9889d443a094b8b55b9d369";
                        m_AllEEL["EE_HelmetFullplateDeskari_F_HM"] = "1cac8cbb019f78a46bcc5c41098b6707";
                        m_AllEEL["EE_HelmetFullplateDeskari_F_HO"] = "fefd90f38d2ba694abec48a9a4864e2f";
                        m_AllEEL["EE_HelmetFullplateDeskari_F_OD"] = "b16074d7151deda47b684bc169c844c7";
                        m_AllEEL["EE_HelmetFullplateDeskari_F_TL"] = "192afcda845f725458fa934f85c9fb11";
                        m_AllEEL["EE_HelmetFullplateDeskari_M_DW"] = "3d84ea625ed9feb4c9c8868752754f2a";
                        m_AllEEL["EE_HelmetFullplateDeskari_M_EL"] = "f04ae84f53bdd36449d55f3073ba86a7";
                        m_AllEEL["EE_HelmetFullplateDeskari_M_GN"] = "dbaf568083dd5194ebf5e64171e903a5";
                        m_AllEEL["EE_HelmetFullplateDeskari_M_HE"] = "774ea54cbf2c9524390358747b0a189c";
                        m_AllEEL["EE_HelmetFullplateDeskari_M_HL"] = "01b526c4d23ae5445bbbbcf49746cbb6";
                        m_AllEEL["EE_HelmetFullplateDeskari_M_HM"] = "7146acc3a16719640b2836da5b00e8b9";
                        m_AllEEL["EE_HelmetFullplateDeskari_M_HO"] = "0615e52a3e8da4740a83d8c144bf4a04";
                        m_AllEEL["EE_HelmetFullplateDeskari_M_OD"] = "8a16c143c963a4148a149c363cc3ee5f";
                        m_AllEEL["EE_HelmetFullplateDeskari_M_TL"] = "fd3d05b3db722d04e87693a66361d7b0";
                        m_AllEEL["EE_HelmetFullplateDragonscale_F_DW"] = "6579dfaad87322646a902269dd2ba801";
                        m_AllEEL["EE_HelmetFullplateDragonscale_F_EL"] = "16a2a537f6120aa4f874c9c0dcddd958";
                        m_AllEEL["EE_HelmetFullplateDragonscale_F_GN"] = "1353851e670ad754b87697f08feb37d4";
                        m_AllEEL["EE_HelmetFullplateDragonscale_F_HE"] = "a85412979809cec41acaa145e3eaf018";
                        m_AllEEL["EE_HelmetFullplateDragonscale_F_HL"] = "3b856c84d122a674ab108829e2ac4355";
                        m_AllEEL["EE_HelmetFullplateDragonscale_F_HM"] = "c7d2ba577ee0abd499164ec1cd9d723c";
                        m_AllEEL["EE_HelmetFullplateDragonscale_F_HO"] = "f5b8a318524d1b5499f38216eaf9c053";
                        m_AllEEL["EE_HelmetFullplateDragonscale_F_OD"] = "bc4153c58ff8c7d419eff4761b0c8b30";
                        m_AllEEL["EE_HelmetFullplateDragonscale_F_TL"] = "b7fe94e8275319342a9e942fe5134eb8";
                        m_AllEEL["EE_HelmetFullplateDragonscale_M_DW"] = "ab4dcc8b7c4821244a34b7589da249eb";
                        m_AllEEL["EE_HelmetFullplateDragonscale_M_EL"] = "3c222e718931a644cb52a2b2a92aa999";
                        m_AllEEL["EE_HelmetFullplateDragonscale_M_GN"] = "e9b66b2094a76ea4fa153d976445bd9a";
                        m_AllEEL["EE_HelmetFullplateDragonscale_M_HE"] = "855a7a320e38aad488c353e0f2389851";
                        m_AllEEL["EE_HelmetFullplateDragonscale_M_HL"] = "01dacd12be0f81f4dbaea7d83142606c";
                        m_AllEEL["EE_HelmetFullplateDragonscale_M_HM"] = "9a2c21717d732964f8594db13a22dde2";
                        m_AllEEL["EE_HelmetFullplateDragonscale_M_HO"] = "c817d5d075867614bb8fee5c1401aebd";
                        m_AllEEL["EE_HelmetFullplateDragonscale_M_OD"] = "58cf471d89220634d800e79a4b49a09a";
                        m_AllEEL["EE_HelmetFullplateDragonscale_M_TL"] = "fc7b3417434123947836d5be126a439e";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_F_DW"] = "d28a4c4b664f1404284e1c105a088f84";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_F_EL"] = "266d43d451e75964a82369df4dfba4a5";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_F_GN"] = "97341e293d6cc6448bd5448f97a9b11b";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_F_HE"] = "4edbd3a964862ad4ea62de9c48b265b2";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_F_HL"] = "926e7fadddcff0041892a42e22dbfc19";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_F_HM"] = "c670ff4a98ea28c4d83051dc4edb9523";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_F_HO"] = "0b6fe3dc02c195a49b43fc137d56e9ab";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_F_TL"] = "170e5ab6a3afb4b458605c760737de88";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_M_DW"] = "66e057255c25b7b42966fefb37a98ab2";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_M_EL"] = "6802961b65be2e44db81de57c007cd19";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_M_GN"] = "049ab860a1e8a3f42b31eb4cc567265f";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_M_HE"] = "eb04b91254101284e8eb404f3ee0a2fa";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_M_HL"] = "c9adedbf78d842440a2352ca556ba9c6";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_M_HM"] = "630527ecbc0197f4e87f3fbb63f9d35b";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_M_HO"] = "6f8b85eb10751254784cb93684b3f0c0";
                        m_AllEEL["EE_HelmetFullplateEvilUndead_M_TL"] = "86a7ed9f6575f46409f6002ae82357d8";
                        m_AllEEL["EE_HelmetFullplateEvil_F_DW"] = "e82c085df7c3c8948a746944a70bad9b";
                        m_AllEEL["EE_HelmetFullplateEvil_F_EL"] = "1543a40af05c10c4b97e21835178ade3";
                        m_AllEEL["EE_HelmetFullplateEvil_F_GN"] = "d2c62cdac5bda4641820e7fc4d18c9f4";
                        m_AllEEL["EE_HelmetFullplateEvil_F_HE"] = "b458c37f2fc6fc44f903d5762fe54c7c";
                        m_AllEEL["EE_HelmetFullplateEvil_F_HL"] = "2e7e6de189cbbc14ebbc6d766fb98ff6";
                        m_AllEEL["EE_HelmetFullplateEvil_F_HM"] = "6d4e9dc7c5e6a3346b7543728de9dd31";
                        m_AllEEL["EE_HelmetFullplateEvil_F_HO"] = "8e52d69d2ff459244bdc41b50ba93605";
                        m_AllEEL["EE_HelmetFullplateEvil_F_TL"] = "0b65324527535fd43af510bfe5a8408f";
                        m_AllEEL["EE_HelmetFullplateEvil_M_DW"] = "083ed3c40b63d784d8524048d23654d5";
                        m_AllEEL["EE_HelmetFullplateEvil_M_EL"] = "bcb9fcd017bc0f145a3dbc4c07707203";
                        m_AllEEL["EE_HelmetFullplateEvil_M_GN"] = "c48de549e22101c44a4465dfdfbfad5c";
                        m_AllEEL["EE_HelmetFullplateEvil_M_HE"] = "81175b23e56bdf34eb43fbc808916e5a";
                        m_AllEEL["EE_HelmetFullplateEvil_M_HL"] = "998ae36cc2ca9cc4da968cdf418abf4a";
                        m_AllEEL["EE_HelmetFullplateEvil_M_HM"] = "7a90029d7d182da42a7ba9f532cf2511";
                        m_AllEEL["EE_HelmetFullplateEvil_M_HO"] = "0f7a3a56a1ba62246ba018956ebbf304";
                        m_AllEEL["EE_HelmetFullplateEvil_M_TL"] = "82acdbf9dc730504e8362373a4062985";
                        m_AllEEL["EE_HelmetFullplateHellknight_F_DW"] = "f67a0359fab05094dbbc2517099e832e";
                        m_AllEEL["EE_HelmetFullplateHellknight_F_EL"] = "e13ec7265ed566849b5ad0218c072ce0";
                        m_AllEEL["EE_HelmetFullplateHellknight_F_GN"] = "5f2b62c1952c5804cb9915f6247f4947";
                        m_AllEEL["EE_HelmetFullplateHellknight_F_HE"] = "e9978d0acdec329429a03ac3983d36db";
                        m_AllEEL["EE_HelmetFullplateHellknight_F_HL"] = "9be28f776db79c44694a63584ed751f9";
                        m_AllEEL["EE_HelmetFullplateHellknight_F_HM"] = "0ae75ae9b18f59f40a95d0031e52a5da";
                        m_AllEEL["EE_HelmetFullplateHellknight_F_HO"] = "547b793938b673d4e80a3bf9f2d52022";
                        m_AllEEL["EE_HelmetFullplateHellknight_F_TL"] = "c77802f8df18aa34ea7b6e77e12dd1e8";
                        m_AllEEL["EE_HelmetFullplateHellknight_M_DW"] = "e21e99cd9b8e40b489fd1252bf558036";
                        m_AllEEL["EE_HelmetFullplateHellknight_M_EL"] = "087d1f33cf8697a4e9cfa893a754c0f5";
                        m_AllEEL["EE_HelmetFullplateHellknight_M_GN"] = "e635d51ebff1bf9418fb12c3b2459e49";
                        m_AllEEL["EE_HelmetFullplateHellknight_M_HE"] = "c3933ef185f27d94fa324194b9194eb1";
                        m_AllEEL["EE_HelmetFullplateHellknight_M_HL"] = "6fcac24eb3af14642afb021d01582658";
                        m_AllEEL["EE_HelmetFullplateHellknight_M_HM"] = "b6c982789b8665a458bbde003696865f";
                        m_AllEEL["EE_HelmetFullplateHellknight_M_HO"] = "0cb1d83db7997ec4fa6e45aab59c4218";
                        m_AllEEL["EE_HelmetFullplateHellknight_M_TL"] = "323877f982026d643ad3ddab43b908d4";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_F_DW"] = "ac719962b6cfada4998252929978391f";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_F_EL"] = "59f610a96f1cd7f4dac4843a2cd3310d";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_F_GN"] = "142ae0cd538373b4db15bc6deb928ac7";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_F_HE"] = "bfdb41dce86a3cd44bf59ce83ad1c560";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_F_HL"] = "0ad72a898b83634429b3f6cd4d0e1911";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_F_HM"] = "401f785f37be1f6449780ed430b6ffd0";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_F_HO"] = "ca06f471621245349b12a2fe5a4fae7b";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_F_OD"] = "77fbbab986c976a44bf6127b26d1e8cd";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_F_TL"] = "efaee8f8d2afad049a1bc12dd3768e42";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_M_DW"] = "dbd2edc791fe18f439735609aee785c4";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_M_EL"] = "2729eea4af4189746b0e35eb42516e87";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_M_GN"] = "14686cacb86a5304489e1d216c954cec";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_M_HE"] = "1efb37b1533b3664f9836c44f7a72bfe";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_M_HL"] = "0aaa3bc981b5ad44e9ce9a23c8183594";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_M_HM"] = "61768254b22e6b94dad380c597e13216";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_M_HO"] = "89d3b43a9cef93841b48a88eb9e04455";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_M_OD"] = "b83e88bf43a2bdf408fc366ab8cb6e1f";
                        m_AllEEL["EE_HelmetFullplateHolyAdamantite_M_TL"] = "8bdd1dc2987c094449696872fbae6847";
                        m_AllEEL["EE_HelmetFullplateHolyGold_F_DW"] = "a2a313e7041e01c4bb9dfd7cd9da7ef8";
                        m_AllEEL["EE_HelmetFullplateHolyGold_F_EL"] = "2cc0486e4d14c4a4cb24962dde95f154";
                        m_AllEEL["EE_HelmetFullplateHolyGold_F_GN"] = "ff5fbda298560104480aa936e9cc4480";
                        m_AllEEL["EE_HelmetFullplateHolyGold_F_HE"] = "791f751ac2697764c8058240987ad535";
                        m_AllEEL["EE_HelmetFullplateHolyGold_F_HL"] = "fcf2f595e3625034f80066834fb29329";
                        m_AllEEL["EE_HelmetFullplateHolyGold_F_HM"] = "ca1d65e3f96b12144b2e1fc1af750c81";
                        m_AllEEL["EE_HelmetFullplateHolyGold_F_HO"] = "0443d1dec5a66db4d93769036c58e2c6";
                        m_AllEEL["EE_HelmetFullplateHolyGold_F_OD"] = "267f5c64ea0e49c4396f6d3c67cc9883";
                        m_AllEEL["EE_HelmetFullplateHolyGold_F_TL"] = "c7ab5f4b9061494499ee7a671e370ae5";
                        m_AllEEL["EE_HelmetFullplateHolyGold_M_DW"] = "23d0713fdf636414fb8eabba1bea9f05";
                        m_AllEEL["EE_HelmetFullplateHolyGold_M_EL"] = "33bfa9a0e86c4ac4f8249d19cd8f438b";
                        m_AllEEL["EE_HelmetFullplateHolyGold_M_GN"] = "fe7177a22eb4bc24a8e693a00ce41af0";
                        m_AllEEL["EE_HelmetFullplateHolyGold_M_HE"] = "6e52a906875495944afeefe65da3981e";
                        m_AllEEL["EE_HelmetFullplateHolyGold_M_HL"] = "8718ce8abf5d1c74f9b90a6f22ea4690";
                        m_AllEEL["EE_HelmetFullplateHolyGold_M_HM"] = "5f6f25b63bb7cc4438094a71d3af0131";
                        m_AllEEL["EE_HelmetFullplateHolyGold_M_HO"] = "093b716f73b396247ab82739c6a18d99";
                        m_AllEEL["EE_HelmetFullplateHolyGold_M_OD"] = "85359fa67c68345479b2a3c86c2396a3";
                        m_AllEEL["EE_HelmetFullplateHolyGold_M_TL"] = "1f8cd6b9db0469b41b6b092dc253d844";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_F_DW"] = "f606afd62ab89074eae996f516c98f97";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_F_EL"] = "eb80c1040797355498fcae4f10a23c82";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_F_GN"] = "649551784f8c61f4a8ff91020ab32ffe";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_F_HE"] = "e0c334a2c11515c4291f90cafc3b7bf5";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_F_HL"] = "2f6aab6c002c4974cb031afb3aa0440f";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_F_HM"] = "6ac359bc4a920744e884afc137d4b713";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_F_HO"] = "c4a3f4b02a0c4bb408e6e921eabd01b0";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_F_OD"] = "5bf592583caab0541be46f58f14bc73b";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_F_TL"] = "97dd8b14eb5b0e541aca31384198ef4e";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_M_DW"] = "a2daa94ea365fde42801301a5b4ff24a";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_M_EL"] = "05193fd8116a60b4888e1f1a69008c3c";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_M_GN"] = "5d88cb40ee8717949b6f99895de5a2c9";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_M_HE"] = "cf07006537f90754dbfd24d6703455bd";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_M_HL"] = "33a8344dae4e7324d88157672e57dbd3";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_M_HM"] = "324410f24d2599441ad4869ca46c9b88";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_M_HO"] = "03af4411ed9c3fe45b3f5dc5d2a03c21";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_M_OD"] = "73588bd60db771441ba16197fbd37030";
                        m_AllEEL["EE_HelmetFullplateHolyMithral_M_TL"] = "0204f4d01f592d64ca7695e250310a17";
                        m_AllEEL["EE_HelmetFullplateHoly_F_DW"] = "30086ea5cd77a4845b3ae958d7b0c1c7";
                        m_AllEEL["EE_HelmetFullplateHoly_F_EL"] = "ae4ce0bcd87e7a344bf16ac6dffe7c0b";
                        m_AllEEL["EE_HelmetFullplateHoly_F_GN"] = "8a327774bc4f19646a19b50148839139";
                        m_AllEEL["EE_HelmetFullplateHoly_F_HE"] = "15a6d5e0462aaed41a6ec529c668ab4c";
                        m_AllEEL["EE_HelmetFullplateHoly_F_HL"] = "2d731ddb4483c59439bb43a74d68edcb";
                        m_AllEEL["EE_HelmetFullplateHoly_F_HM"] = "cb5f3584c1e3b9447bac2269949b6ab0";
                        m_AllEEL["EE_HelmetFullplateHoly_F_HO"] = "b9db4171711365441a3aa17e85b5c736";
                        m_AllEEL["EE_HelmetFullplateHoly_F_OD"] = "fb1f6e86fad81ed4694f7147def5ccd7";
                        m_AllEEL["EE_HelmetFullplateHoly_F_TL"] = "fb97029d902309c448a0ce81eba756ec";
                        m_AllEEL["EE_HelmetFullplateHoly_M_DW"] = "771e214ec9059ec43828c277dc1565b9";
                        m_AllEEL["EE_HelmetFullplateHoly_M_EL"] = "487503733326eb04da9f15175a78dc93";
                        m_AllEEL["EE_HelmetFullplateHoly_M_GN"] = "e6f4dc7b689df3c4c9b396cf63d411b5";
                        m_AllEEL["EE_HelmetFullplateHoly_M_HE"] = "519aed42fe9331e4b808f2c63d086a23";
                        m_AllEEL["EE_HelmetFullplateHoly_M_HL"] = "c8b1098ac0d661d43b039d70ec6b7113";
                        m_AllEEL["EE_HelmetFullplateHoly_M_HM"] = "c27486cd51843864ca8fe8efa7269a36";
                        m_AllEEL["EE_HelmetFullplateHoly_M_HO"] = "536486545f77c114db2127aad0fff572";
                        m_AllEEL["EE_HelmetFullplateHoly_M_OD"] = "ec771b8caf8b35f4cab4da76517dec06";
                        m_AllEEL["EE_HelmetFullplateHoly_M_TL"] = "fc6e0eb8672d0f548997006ed2423545";
                        m_AllEEL["EE_HelmetFullplateKnight_F_DW"] = "9917e2cad186892459d832bdcd3380ec";
                        m_AllEEL["EE_HelmetFullplateKnight_F_EL"] = "1ff22a1a7dbf1ad47ae72b0fc649915e";
                        m_AllEEL["EE_HelmetFullplateKnight_F_GN"] = "8c8c9845fbba12d42b6dfb996d3132f8";
                        m_AllEEL["EE_HelmetFullplateKnight_F_HE"] = "472c7d3fa367c134b808be74ce857bde";
                        m_AllEEL["EE_HelmetFullplateKnight_F_HL"] = "1973fad77ff8e8649bcff5af4bb6f4c8";
                        m_AllEEL["EE_HelmetFullplateKnight_F_HM"] = "50397fe87117c204aa9bda43a017a725";
                        m_AllEEL["EE_HelmetFullplateKnight_F_HO"] = "396a8f8c30f8721498eda26243933066";
                        m_AllEEL["EE_HelmetFullplateKnight_F_TL"] = "c8051f39acdbef943ab82fa03599cee3";
                        m_AllEEL["EE_HelmetFullplateKnight_M_DW"] = "595baa015a198ba4fbf19ae35ebceaa8";
                        m_AllEEL["EE_HelmetFullplateKnight_M_EL"] = "ba66ab45bc487194bac5033f0c98ecc8";
                        m_AllEEL["EE_HelmetFullplateKnight_M_GN"] = "310b43d33e882c34a9c0db5d3cf9071a";
                        m_AllEEL["EE_HelmetFullplateKnight_M_HE"] = "9d35da025dbe64b4bbed465818476752";
                        m_AllEEL["EE_HelmetFullplateKnight_M_HL"] = "d5d422ee52e882c4187787b91ecde33d";
                        m_AllEEL["EE_HelmetFullplateKnight_M_HM"] = "a68117c722b11a44e83bf0a1d3e2a032";
                        m_AllEEL["EE_HelmetFullplateKnight_M_HO"] = "a593b6e0290fa0341864df3fbcc54a36";
                        m_AllEEL["EE_HelmetFullplateKnight_M_TL"] = "441f3eaccaade844aad66494d8ff775c";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_F_DW"] = "01823d156c9b59244833b07174bf35c6";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_F_EL"] = "b7f0ce598dd78cd4097c40104ab468d8";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_F_GN"] = "7d710aa5f4f1b094e8bed4a7ef3cdbfc";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_F_HE"] = "497413e321d2e2446bc2f352d009b8fd";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_F_HL"] = "7ac09b41ada4e254b9c1ceafb6d5b755";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_F_HM"] = "d7de4a590dbfb7446bd3043ace62761b";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_F_HO"] = "52bc5e4a6ad74d64cb3109fde0fb9ae9";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_F_TL"] = "ecd6cc4207fd3ad4cb60abad0a702bad";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_M_DW"] = "356e5761b89512d439cca2bfbc66f3cb";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_M_EL"] = "e6a31ad1fe0ef964a82ead4b6e89b6f1";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_M_GN"] = "ee1344c2f5c285a4fb497f99b29a44ac";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_M_HE"] = "39ecf48f4b898444cbf11760b9810c6c";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_M_HL"] = "a42616bfb5dd41b42aa653f8bc36ac31";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_M_HM"] = "12dabc9e081cb1844be0bdcd3cd5a4e4";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_M_HO"] = "84a5373c6e4327e4293641aeb0ba9457";
                        m_AllEEL["EE_HelmetFullplateMithralArcane_M_TL"] = "eff2b02a09ad30f44a4833550b6c4933";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_F_DW"] = "ef191d1dfbdb8a642b3b6aa825f2515f";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_F_EL"] = "e9794a8b842da2342bba87d3fa54b318";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_F_GN"] = "41683f8054423d84db454acde6e8ab71";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_F_HE"] = "f1563e52dc0344d4cb3874044efae2b5";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_F_HL"] = "d743160a23244dc47872f68109201c80";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_F_HM"] = "d9fa026df9d61c347914e61dd3c18b29";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_F_HO"] = "f6b6ed9f1b775c743ab88d17142ef2bb";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_F_OD"] = "666e8a6df80082848bce065fa53fb393";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_F_TL"] = "c25fec07802cfaa44ba74889eafe8d39";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_M_DW"] = "4e04b742e14bd1b45bcbb6aa4b803caa";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_M_EL"] = "470e38d1d21e35f48a27c14355255fba";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_M_GN"] = "40b240e602205e34097f80d2e240909b";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_M_HE"] = "77e1d9f0d66b8ad4bb2a09681f3a37e1";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_M_HL"] = "e0fa0f17cc708bd47aa693adca2cb21d";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_M_HM"] = "b47a8487b80e12d4a91fb2e3075ca451";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_M_HO"] = "f4a3f1073e1b3944ea7e3524b502fcb7";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_M_OD"] = "9c47856ee3ff0db45b50983e240e1eb7";
                        m_AllEEL["EE_HelmetFullplateOrnateEvil_M_TL"] = "b83be8514f0578248a0131680ab692b5";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_F_DW"] = "65a93ea68bb9d534a8f1fc27edb11dd2";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_F_EL"] = "78bd0c80d549ab9468dd2ed9e5f758c8";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_F_GN"] = "969403dc72ec7b64bb087e09674d0af9";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_F_HE"] = "a288fdfce225da942b90925e3fd0f2c6";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_F_HL"] = "6f081d660c1733140b59ac0e774b3a76";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_F_HM"] = "1fe36f16b3e22ce4a99778cfe9a00c78";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_F_HO"] = "550613718f99ccd438d2804cb0e656fb";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_F_OD"] = "2c114b5d50c95c141968e67a899b68d2";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_F_TL"] = "32d20748bb2f2dc4c9017d09c2045610";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_M_DW"] = "ff773e6c90b7dcd4ab5f3b608d7fb91e";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_M_EL"] = "4567f9c9192c0f847b342b0c9c44343d";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_M_GN"] = "335fe841aa759684c9224e54739dbbc7";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_M_HE"] = "6d417523ae6f02046a9368c72301d727";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_M_HL"] = "8b35dc0c600a45f4fb72d9632e5a7394";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_M_HM"] = "12ec6d8f879ff5c41a5eb03f330da4c2";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_M_HO"] = "1a930deb4c6016c44b2a1742ec41fbea";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_M_OD"] = "b4c9aabff8af96442a32639fb90a81e0";
                        m_AllEEL["EE_HelmetFullplateOrnateMetal_M_TL"] = "f58907e254790b94592cdecf019468a9";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_F_DW"] = "89115fe16d4c31745ace63469fdf8f82";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_F_EL"] = "6a6db3937db33d048b05c3336af8abb4";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_F_GN"] = "11f79cb14aa7c4440b6c0b74f8b3a55f";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_F_HE"] = "83f637e6928bd8a4aad8c625420d50a4";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_F_HL"] = "0c687d2e1b397e344be50ea3d8cdc928";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_F_HM"] = "fb5779504355c0a4099f89d668cc289b";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_F_HO"] = "bd8778a8193ff1f4bb14d6901622efd7";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_F_OD"] = "3f9d23b2d0f814c4d808661f19c0ff13";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_F_TL"] = "1e2c643122dc3e34c8738a7f943cbe1e";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_M_DW"] = "daa1271ce2f615540baa7ed0dd1b4d60";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_M_EL"] = "eb800f100dab2ba4c99f147bf401231d";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_M_GN"] = "d5dbc9c8038d0744dbbd62e5b814e6bc";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_M_HE"] = "ffe405406cad64248a3f428875c79512";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_M_HL"] = "cbe5c004211927849902c31aa43ddd67";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_M_HM"] = "256f6476f6a30b247adcf1c50f86a2f3";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_M_HO"] = "833a1146be95095478786ac6466923af";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_M_OD"] = "33e812d74e19c444186305c125cca9f5";
                        m_AllEEL["EE_HelmetFullplateOrnateNormal_M_TL"] = "31c784f0f951afe4e9f1fe7999b20fbd";
                        m_AllEEL["EE_HelmetFullplateRoyal_F_DW"] = "e993c947e51f30848b84021b0c496af1";
                        m_AllEEL["EE_HelmetFullplateRoyal_F_EL"] = "b8aca5424d977ea4082e85f1ba8693ac";
                        m_AllEEL["EE_HelmetFullplateRoyal_F_GN"] = "67047d26067d34b4e8cbc290875406ed";
                        m_AllEEL["EE_HelmetFullplateRoyal_F_HE"] = "ce967c925357f5345be488ae98b706ab";
                        m_AllEEL["EE_HelmetFullplateRoyal_F_HL"] = "f715fb4866a4c5b4fae4a10ef3ea1bf1";
                        m_AllEEL["EE_HelmetFullplateRoyal_F_HM"] = "4d52c4b695ce18d439dc7186ff8bcaf5";
                        m_AllEEL["EE_HelmetFullplateRoyal_F_HO"] = "444e2cda7ee3eb447893796a5c6011ed";
                        m_AllEEL["EE_HelmetFullplateRoyal_F_TL"] = "eebb2315f8aa5724c8bc674da5cc61a0";
                        m_AllEEL["EE_HelmetFullplateRoyal_M_DW"] = "7b57ecab605686547afcd8d402817e2e";
                        m_AllEEL["EE_HelmetFullplateRoyal_M_EL"] = "ad4a7953a707c8e4eb479240d5af6af0";
                        m_AllEEL["EE_HelmetFullplateRoyal_M_GN"] = "58875730e91092643a1405252b41db31";
                        m_AllEEL["EE_HelmetFullplateRoyal_M_HE"] = "f5117828edbf9974a9a063ed226db003";
                        m_AllEEL["EE_HelmetFullplateRoyal_M_HL"] = "d7a3f07838a830f448cf581bf2222423";
                        m_AllEEL["EE_HelmetFullplateRoyal_M_HM"] = "98a48d2cf47ce1b47964b85b5608c9a7";
                        m_AllEEL["EE_HelmetFullplateRoyal_M_HO"] = "be948ad5ca5957b439bba1f9f79e8286";
                        m_AllEEL["EE_HelmetFullplateRoyal_M_TL"] = "39c5112c52cf4984b8128d1d51413c90";
                        m_AllEEL["EE_HelmetHellknightOfficer_F_DW"] = "51598bc1b222f1e4b8e402eac960c24a";
                        m_AllEEL["EE_HelmetHellknightOfficer_F_EL"] = "7c4074ed9855b20438e7c8628ca2d932";
                        m_AllEEL["EE_HelmetHellknightOfficer_F_GN"] = "902674e0f1b0c354ea46398a4dd5ac4e";
                        m_AllEEL["EE_HelmetHellknightOfficer_F_HE"] = "b5cf8c4c643069e4c86ec23d27ed5665";
                        m_AllEEL["EE_HelmetHellknightOfficer_F_HL"] = "7bbc44f2572d3ed488b038568674333d";
                        m_AllEEL["EE_HelmetHellknightOfficer_F_HM"] = "e6f84ae1b5b17cf4fba644cc0e9d19f7";
                        m_AllEEL["EE_HelmetHellknightOfficer_F_HO"] = "ce62c45dd7795be4e81cfca0e1e8a9fb";
                        m_AllEEL["EE_HelmetHellknightOfficer_F_OD"] = "fbe6dc909db8a024387b5e68e3f5c85f";
                        m_AllEEL["EE_HelmetHellknightOfficer_F_TL"] = "28b65317ab695484599c7108a9f78250";
                        m_AllEEL["EE_HelmetHellknightOfficer_M_DW"] = "19743f96a6688ed4e9c66be35f314b1f";
                        m_AllEEL["EE_HelmetHellknightOfficer_M_EL"] = "bc9b03914db4a344092be9dab38840d2";
                        m_AllEEL["EE_HelmetHellknightOfficer_M_GN"] = "c11b583ceff2c4441b85ad98c679f9fa";
                        m_AllEEL["EE_HelmetHellknightOfficer_M_HE"] = "12c1b0ac9a00eae4bba1ba02ffd052e2";
                        m_AllEEL["EE_HelmetHellknightOfficer_M_HL"] = "6571a0e8b204a87479974426c3c13405";
                        m_AllEEL["EE_HelmetHellknightOfficer_M_HM"] = "f2d871505364a5c498e38c4348cb5f82";
                        m_AllEEL["EE_HelmetHellknightOfficer_M_HO"] = "7dea3805444d25b438e9b3d51bba1224";
                        m_AllEEL["EE_HelmetHellknightOfficer_M_OD"] = "5fedd425f541b194580cba494b114456";
                        m_AllEEL["EE_HelmetHellknightOfficer_M_TL"] = "6c0428f39cc4cfb4e87486f90c8fb476";
                        m_AllEEL["EE_HelmetHellknightSignifer_F_DW"] = "0e2a8003881d78d4099058fa4e1f84fe";
                        m_AllEEL["EE_HelmetHellknightSignifer_F_EL"] = "4709d4fff1cfd8b45a1743b936696f9e";
                        m_AllEEL["EE_HelmetHellknightSignifer_F_GN"] = "d0e54038a1cc8dc4dbb1f9f4ee854392";
                        m_AllEEL["EE_HelmetHellknightSignifer_F_HE"] = "31ee22db00d98cb4ca953fbd297f68ed";
                        m_AllEEL["EE_HelmetHellknightSignifer_F_HL"] = "4e1c66a9741be374d9dae4c7773cd448";
                        m_AllEEL["EE_HelmetHellknightSignifer_F_HM"] = "5a4ac5fc336e8ae4286c0e0f8f30f499";
                        m_AllEEL["EE_HelmetHellknightSignifer_F_HO"] = "debc30b8fadaaf44c836eeabf295eca0";
                        m_AllEEL["EE_HelmetHellknightSignifer_F_OD"] = "e8a52741f0fd63f4c882c6bb1e6af3b3";
                        m_AllEEL["EE_HelmetHellknightSignifer_F_TL"] = "3a46f31201724a14695168ce60088b80";
                        m_AllEEL["EE_HelmetHellknightSignifer_M_DW"] = "c5967d28b3da52e4fabadacb95ab6638";
                        m_AllEEL["EE_HelmetHellknightSignifer_M_EL"] = "d389daee24152ae4f8ca4868b79974f9";
                        m_AllEEL["EE_HelmetHellknightSignifer_M_GN"] = "e21ced7b071ae6245a7ad7f0064bdae7";
                        m_AllEEL["EE_HelmetHellknightSignifer_M_HE"] = "4ac65d9bb55e3fd428a61e1ef47e62e1";
                        m_AllEEL["EE_HelmetHellknightSignifer_M_HL"] = "7daade80f63a1ad4e98c74918e659e25";
                        m_AllEEL["EE_HelmetHellknightSignifer_M_HM"] = "883201b57d4fbf34a9ee4eb4eb97b1fe";
                        m_AllEEL["EE_HelmetHellknightSignifer_M_HO"] = "057de457982f1674885de88705c9991b";
                        m_AllEEL["EE_HelmetHellknightSignifer_M_OD"] = "5d90b05d2afbcd248bfe260dffe4b005";
                        m_AllEEL["EE_HelmetHellknightSignifer_M_TL"] = "dd2b135702119424dad800fd5b663daf";
                        m_AllEEL["EE_HelmetKettleHat_F_DW"] = "a32ed1c47918c334ba71526df14799f6";
                        m_AllEEL["EE_HelmetKettleHat_F_EL"] = "3d1fb9113a74ded43aeb43d797e0a832";
                        m_AllEEL["EE_HelmetKettleHat_F_GN"] = "aa3c082e856298244b9454bb978b0919";
                        m_AllEEL["EE_HelmetKettleHat_F_HE"] = "934999f183e33d34682993abf68f4836";
                        m_AllEEL["EE_HelmetKettleHat_F_HL"] = "eb7fa4a9a6a1d2d4aaf790fb23d466e4";
                        m_AllEEL["EE_HelmetKettleHat_F_HM"] = "cf0f6286b5ce7c24580eb52c9143e036";
                        m_AllEEL["EE_HelmetKettleHat_F_HO"] = "1e8c783656c838c4cb3cbdefc7c683bb";
                        m_AllEEL["EE_HelmetKettleHat_F_KT"] = "548c63da29174714a8df1b30165dfcce";
                        m_AllEEL["EE_HelmetKettleHat_F_OD"] = "1f33981c8c823e844af70ea0dfebeb8e";
                        m_AllEEL["EE_HelmetKettleHat_F_TL"] = "b8609459a1f9ef744add48825b24de9d";
                        m_AllEEL["EE_HelmetKettleHat_M_DW"] = "6b7dfdd3bcf4860468240f3b2cd7ca27";
                        m_AllEEL["EE_HelmetKettleHat_M_EL"] = "2a3ae5635074a4444acab0bee1780d82";
                        m_AllEEL["EE_HelmetKettleHat_M_GN"] = "3cb55d39cabfbdd4683f0e258a5f8027";
                        m_AllEEL["EE_HelmetKettleHat_M_HE"] = "8a7b9e11264d1bc43b6b15fdb12d9852";
                        m_AllEEL["EE_HelmetKettleHat_M_HL"] = "ac8864591911db54f87269d32d47de46";
                        m_AllEEL["EE_HelmetKettleHat_M_HM"] = "a43c44debb3c61c4caada9e5b013850f";
                        m_AllEEL["EE_HelmetKettleHat_M_HO"] = "2da7e441e1f39c54b9893ed5dac4a711";
                        m_AllEEL["EE_HelmetKettleHat_M_KT"] = "994e919233bfd4743a32298505133872";
                        m_AllEEL["EE_HelmetKettleHat_M_OD"] = "22426ecef9d549f47b80b8fd514eff7e";
                        m_AllEEL["EE_HelmetKettleHat_M_TL"] = "36ae68be5ddc3e042aaed80f8cbff46c";
                        m_AllEEL["EE_HideBandits_F_Any"] = "44550c97c6b7b1e40a127b95d9e2d483";
                        m_AllEEL["EE_HideBandits_M_Any"] = "75d18d87db01814419f7c9738eb9ca14";
                        m_AllEEL["EE_HideBarbarian_F_Any"] = "09b948408632e3848a981e8c453caf4b";
                        m_AllEEL["EE_HideBarbarian_M_Any"] = "e28a3bba38d03aa4c9a6e3032d505af5";
                        m_AllEEL["EE_HideDemonic_F_Any"] = "6efbac27597c77f45a36fe74c6045f0e";
                        m_AllEEL["EE_HideDemonic_M_Any"] = "e4ec9d88028120346b9c1ecc4e620b09";
                        m_AllEEL["EE_HideMongrel_F_Any"] = "cf0de02a2b8f5334f8b80f89a4da2599";
                        m_AllEEL["EE_HideMongrel_M_Any"] = "11834ed6aeb5b9646bedf8f8d80b19b8";
                        m_AllEEL["EE_HideMosaic_F_Any"] = "f742bd182432fba478511ed3db2eb380";
                        m_AllEEL["EE_HideMosaic_M_Any"] = "de225b7c5462d05428febd7ee14e17e9";
                        m_AllEEL["EE_HoodRobeNecromancer_F_EL"] = "e881ad0a58e6c8f4cab383267bf5be64";
                        m_AllEEL["EE_HoodRobeNecromancer_F_HM"] = "3eea5035ccc6b6b4d9596c873e652584";
                        m_AllEEL["EE_HoodRobeNecromancer_M_EL"] = "1191d7f47fd049e448147d64af03f196";
                        m_AllEEL["EE_HoodRobeNecromancer_M_HM"] = "6ff78852e0385a240815f50b73abbbe0";
                        m_AllEEL["EE_HoodRobeNecromancer_M_TL"] = "539ffa3104a55074dab1bc9c5da3f396";
                        m_AllEEL["EE_HoodRobeWrigglingMan_M_HM"] = "17c46fd8e23340843ab8fcdfadca272b";
                        m_AllEEL["EE_Horns01_F_CM"] = "ab044ce99b5322a4980b1e05ec27ee4c";
                        m_AllEEL["EE_Horns01_F_SU"] = "020cb621cebd72e40abc3f857ab377b8";
                        m_AllEEL["EE_Horns01_F_TL"] = "665d8734bad3ce648be0786de09f5f4c";
                        m_AllEEL["EE_Horns01_M_CM"] = "89d5c728d197b1344bf942b4ef55f0a1";
                        m_AllEEL["EE_Horns01_M_SU"] = "f44f0bcefb40afd4b86e241599b83660";
                        m_AllEEL["EE_Horns01_M_TL"] = "a68e700e882e1b8438df4ba92d247daf";
                        m_AllEEL["EE_Horns02Arueshalae_F_SU"] = "7be5b63938c84504ea8307fc460eaadc";
                        m_AllEEL["EE_Horns02_F_CM"] = "72a085c1a2b697f4f95f6a54f315bd51";
                        m_AllEEL["EE_Horns02_F_TL"] = "ccd35c8508752f04582e7d3a55248afe";
                        m_AllEEL["EE_Horns02_M_CM"] = "d4f6c34e0812b0840aeb365a04032be9";
                        m_AllEEL["EE_Horns02_M_TL"] = "cecad00f0d5d83f4ba37aa45c11c1bbe";
                        m_AllEEL["EE_Horns03_F_TL"] = "0d78bd95d563f3441bf8349bebfb48bf";
                        m_AllEEL["EE_Horns03_M_TL"] = "d1dde611ca8670845978721380de3bdc";
                        m_AllEEL["EE_Horns04Woljif_M_TL"] = "c2186f297c1bc3e4c86283495e3f1ee6";
                        m_AllEEL["EE_Horns04_F_TL"] = "262ee4d8ababfce4ba1cd8a1bf60c999";
                        m_AllEEL["EE_Horns05_F_TL"] = "dea42645c2027524ca40d93e4265e247";
                        m_AllEEL["EE_Horns05_M_TL"] = "16265666ad82f9d4e8a3ae1a563179b9";
                        m_AllEEL["EE_HornsDevilExecutioner_F_HM"] = "9932a8b7a0b57a8429dc5a53657d7b06";
                        m_AllEEL["EE_HornsDevilExecutioner_M_HM"] = "12902d1b55f8a04489a78763e7b3cf7e";
                        m_AllEEL["EE_HornsMythicDemon_F_DW"] = "cba4f0bef0795074ea69026ce39619a8";
                        m_AllEEL["EE_HornsMythicDemon_F_EL"] = "f57d221b59c543b4db95249474479bca";
                        m_AllEEL["EE_HornsMythicDemon_F_GN"] = "014d90aa6069c6e40ba13638ba4db3e0";
                        m_AllEEL["EE_HornsMythicDemon_F_HE"] = "dc6a6d787d93ddb48ab62016197d7a93";
                        m_AllEEL["EE_HornsMythicDemon_F_HL"] = "e94ef271d69e22d4a8f4e47073d62b7e";
                        m_AllEEL["EE_HornsMythicDemon_F_HM"] = "27938e89c4af34547b3248720d4a63c7";
                        m_AllEEL["EE_HornsMythicDemon_F_HO"] = "7e9929a4a294c084ca474ea77e7a2199";
                        m_AllEEL["EE_HornsMythicDemon_F_KT"] = "8b50b98781e65e74fa72deb8d108b100";
                        m_AllEEL["EE_HornsMythicDemon_F_OD"] = "401241286fa966e48ab733ebbc2f4e64";
                        m_AllEEL["EE_HornsMythicDemon_F_TL"] = "139094effff52e6409ba78453d06eb5d";
                        m_AllEEL["EE_HornsMythicDemon_M_DW"] = "7dc6b57e7fd60e1418c62fb59f93bdd7";
                        m_AllEEL["EE_HornsMythicDemon_M_EL"] = "67a4dad5a83d1c948b0881676cdae046";
                        m_AllEEL["EE_HornsMythicDemon_M_GN"] = "5d82bdcaff34a8945855285fec0478e0";
                        m_AllEEL["EE_HornsMythicDemon_M_HE"] = "024704d21c53c154790ad034c1140f41";
                        m_AllEEL["EE_HornsMythicDemon_M_HL"] = "1b3a0f20c7cc25c448a57b1f75e6f917";
                        m_AllEEL["EE_HornsMythicDemon_M_HM"] = "cbdf6590c0ea5b0499fea79a6d961ed1";
                        m_AllEEL["EE_HornsMythicDemon_M_HO"] = "c23c1a59d8fe5ba498660e35bb5f4c2a";
                        m_AllEEL["EE_HornsMythicDemon_M_KT"] = "4e77edbcb38ceb44a9e6da580a32b309";
                        m_AllEEL["EE_HornsMythicDemon_M_OD"] = "0dec118245ddaf042bf6ee64e3e8122d";
                        m_AllEEL["EE_HornsMythicDemon_M_TL"] = "ead29a1518a407d4dbba5d34d30dd2b7";
                        m_AllEEL["EE_HornsMythicDevilBig_F_DW"] = "95ef6a95aef87734fa7c54d887648cf6";
                        m_AllEEL["EE_HornsMythicDevilBig_F_EL"] = "e36b1b5e2201c1040a293fb6c584a2fd";
                        m_AllEEL["EE_HornsMythicDevilBig_F_GN"] = "881ff0ad41244f54795c8f83d02a9a5f";
                        m_AllEEL["EE_HornsMythicDevilBig_F_HE"] = "b1acb88dc5d1ff345b2d7a27b97ff863";
                        m_AllEEL["EE_HornsMythicDevilBig_F_HL"] = "d1e64f171368a7b4bb83e7665237b774";
                        m_AllEEL["EE_HornsMythicDevilBig_F_HM"] = "3a16282ed96518c42830be54243e781d";
                        m_AllEEL["EE_HornsMythicDevilBig_F_HO"] = "d33298bde6c0be54f882b32afc6ab9bb";
                        m_AllEEL["EE_HornsMythicDevilBig_F_KT"] = "9bbdc555e0eac2b4785384c13fad45d5";
                        m_AllEEL["EE_HornsMythicDevilBig_F_OD"] = "21060323907e549469974583dbc88f9d";
                        m_AllEEL["EE_HornsMythicDevilBig_F_TL"] = "b5e073c4f2674c74a83b094ae322ec55";
                        m_AllEEL["EE_HornsMythicDevilBig_M_DW"] = "48bccbccd86793644822ef774d862ee5";
                        m_AllEEL["EE_HornsMythicDevilBig_M_EL"] = "3b44eec2681eec94d9eca3d6fc421daf";
                        m_AllEEL["EE_HornsMythicDevilBig_M_GN"] = "f515f2ccee253c14088a9c956f03d3d7";
                        m_AllEEL["EE_HornsMythicDevilBig_M_HE"] = "6addb8f8121a9f246a46bc36c7874876";
                        m_AllEEL["EE_HornsMythicDevilBig_M_HL"] = "9ff38738c44d8d140af3780977ae559e";
                        m_AllEEL["EE_HornsMythicDevilBig_M_HM"] = "e04609a1d70d91c43a69f3fa6442412f";
                        m_AllEEL["EE_HornsMythicDevilBig_M_HO"] = "37323fa644397e941851d6d2d996abb8";
                        m_AllEEL["EE_HornsMythicDevilBig_M_KT"] = "e9cd9cde45b89ba4990955fe30e78664";
                        m_AllEEL["EE_HornsMythicDevilBig_M_OD"] = "dc174f4debb43f840b826edd8dab4d9f";
                        m_AllEEL["EE_HornsMythicDevilBig_M_TL"] = "a2c825e83ab4300469447fa1678bf618";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_DW"] = "c59b69dcb10bd6c458b2ba1fb9abf2d0";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_EL"] = "b566ef1e202aa7647a762f5c4b676fd6";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_GN"] = "d97e89e3c2c86c04194436b18418f6f8";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_HE"] = "0be2e490e3c3189428f90415064ecb4a";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_HL"] = "4ef1ea55d16c45b4692b2874dff62122";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_HM"] = "47b7f27eadcb5614788090f3cf208c29";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_HO"] = "e608a273c27be214bb144c023ba05116";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_KT"] = "e4589887338da6649961f3da2c0cc701";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_OD"] = "6d3aa4e9f4a94e74c87894e96b412c2a";
                        m_AllEEL["EE_HornsMythicDevilSmall_F_TL"] = "1eb3a5e6a64226449a1ba4ab60e1fd1e";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_DW"] = "2c39312561b93344c91cf471abc45b5f";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_EL"] = "ffc04f0b3073ead45a7ad130c2276e82";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_GN"] = "87ed5af01f2af63478bedf9ee6a94ac8";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_HE"] = "a13e6861188774b4088f85fec533c898";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_HL"] = "1b4d9a618423baf4993746b6b814b250";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_HM"] = "4b4279cbe7fa2444e9617b34b4ccfdd3";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_HO"] = "cc39016612aabd74cb2b60bbe6760931";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_KT"] = "d0c9b60e147e05040bf06b423f916d8d";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_OD"] = "5fec2efa61497e946bd5f0a8588acd22";
                        m_AllEEL["EE_HornsMythicDevilSmall_M_TL"] = "9c16bb6dddc2f8e48aa6e4ad68ace7bd";
                        m_AllEEL["EE_HornsMythicDragon_F_DW"] = "3b9e41df8dc57134aa0024db825d4812";
                        m_AllEEL["EE_HornsMythicDragon_F_EL"] = "16f9bf80619b3b941b32d03ed7d2d4fc";
                        m_AllEEL["EE_HornsMythicDragon_F_GN"] = "4abd09dd157471b4f95f7b1555dc55dc";
                        m_AllEEL["EE_HornsMythicDragon_F_HE"] = "55327879c2a22384a99b4965a9cf805f";
                        m_AllEEL["EE_HornsMythicDragon_F_HL"] = "34e4cbc747eb5aa48a87cb14ae0be9be";
                        m_AllEEL["EE_HornsMythicDragon_F_HM"] = "e3becd2c763630a4bac5deb0d14573ab";
                        m_AllEEL["EE_HornsMythicDragon_F_HO"] = "0f1ba2b1af610a2448339803452d535e";
                        m_AllEEL["EE_HornsMythicDragon_F_KT"] = "d7c723c91eed6594db175665bd06c432";
                        m_AllEEL["EE_HornsMythicDragon_F_OD"] = "3775a1850e2b4154d99face728dd3bb7";
                        m_AllEEL["EE_HornsMythicDragon_F_TL"] = "a34bd66d1ae3f0343ae112254419ede4";
                        m_AllEEL["EE_HornsMythicDragon_M_DW"] = "4a24db5776e6d2141be0a8c1189e108c";
                        m_AllEEL["EE_HornsMythicDragon_M_EL"] = "a01eb815b412eac438470adc4d81dfa2";
                        m_AllEEL["EE_HornsMythicDragon_M_GN"] = "00af5416bccaa144bbc44cf677733cfc";
                        m_AllEEL["EE_HornsMythicDragon_M_HE"] = "196fe2e3406406d4bbef12efadbed728";
                        m_AllEEL["EE_HornsMythicDragon_M_HL"] = "42c5df76d62636a4a9f2565cc6b272bd";
                        m_AllEEL["EE_HornsMythicDragon_M_HM"] = "2a352d26e5c9a234a8bbbc81f6e2372b";
                        m_AllEEL["EE_HornsMythicDragon_M_HO"] = "41ce3d9c83aaaa84a8eee75dc2b14909";
                        m_AllEEL["EE_HornsMythicDragon_M_KT"] = "304b6b5607470bc489153930a03da8ee";
                        m_AllEEL["EE_HornsMythicDragon_M_OD"] = "e81b9cb2df95f6c49b9e077ed9f46ffc";
                        m_AllEEL["EE_HornsMythicDragon_M_TL"] = "0f6c1d1859088424d85ec6b872c132de";
                        m_AllEEL["EE_HornsSkeleton_F_DW"] = "12c7ff3a92b8713428eaceb01ac9b48c";
                        m_AllEEL["EE_HornsSkeleton_F_EL"] = "0e75fda397fa06a4481667e7281e4ea7";
                        m_AllEEL["EE_HornsSkeleton_F_GN"] = "760a1b3c204652d4d921c4e3ffce4d4c";
                        m_AllEEL["EE_HornsSkeleton_F_HL"] = "f599850541e79dd41b1ff42a3894e4c6";
                        m_AllEEL["EE_HornsSkeleton_F_HM"] = "61b1f8b3a96c93541a322d6c609ade56";
                        m_AllEEL["EE_HornsSkeleton_F_TL"] = "cea99d99eeabafb4f9e77bb84dcfcf3f";
                        m_AllEEL["EE_HornsSkeleton_M_DW"] = "5d83ffbbda65cf645bcf4ed6aa871ca2";
                        m_AllEEL["EE_HornsSkeleton_M_EL"] = "dfd0ce83f630d3242a102aede2b3f36a";
                        m_AllEEL["EE_HornsSkeleton_M_GN"] = "2b472c8dcaa33164d832fa9242c70670";
                        m_AllEEL["EE_HornsSkeleton_M_HL"] = "50c1a9b7e226a7340a357b55ea74a995";
                        m_AllEEL["EE_HornsSkeleton_M_HM"] = "fd29ef04a3577d64aaad460f6bb9d6af";
                        m_AllEEL["EE_HornsSkeleton_M_SN"] = "b88202050d9e56140b452d10ee952319";
                        m_AllEEL["EE_HornsSkeleton_M_TL"] = "2ce2247d7efd20c41a1493211e2a2e42";
                        m_AllEEL["EE_HunterAccessories_F_Any"] = "903738a5bed89724f9196c9b71cce430";
                        m_AllEEL["EE_HunterAccessories_M_Any"] = "c50e8be0fcefdb34ea8a6cf9286a230e";
                        m_AllEEL["EE_Hunter_F_Any"] = "6e3b4bec2e32e9c4ebb5881514cd0cd5";
                        m_AllEEL["EE_Hunter_M_Any"] = "db11ab5ada5f1484cb1d05f2455bcdb1";
                        m_AllEEL["EE_InnKeeper_M_Any"] = "0dd2bdd5874ad5b42a4a8be8e66d512f";
                        m_AllEEL["EE_InquisitorAccessories_F_Any"] = "b5db7f26cdb3fb949b16a2d88de0e920";
                        m_AllEEL["EE_InquisitorAccessories_M_Any"] = "db25be3becf55bb499b6ad5ddaad6640";
                        m_AllEEL["EE_InquisitorClassHat_F_DW"] = "0752ac559740454438d36bf830385984";
                        m_AllEEL["EE_InquisitorClassHat_F_EL"] = "7f43ce58b9f352a4293ec83ae2e33404";
                        m_AllEEL["EE_InquisitorClassHat_F_GN"] = "d9ee6ed81776d1a47ad13c641b673207";
                        m_AllEEL["EE_InquisitorClassHat_F_HE"] = "6d12cce7d2fc36548b97517f05033677";
                        m_AllEEL["EE_InquisitorClassHat_F_HL"] = "71a809bcb73f8af4c97be7e5095a5677";
                        m_AllEEL["EE_InquisitorClassHat_F_HM"] = "3cfbe84a4829c0c4c9d0c6fafa0082ef";
                        m_AllEEL["EE_InquisitorClassHat_F_HO"] = "8f2a63d4e887ac6428aac49b953c446e";
                        m_AllEEL["EE_InquisitorClassHat_F_OD"] = "c814a50eab74acf42a96dc3cb58c1fa4";
                        m_AllEEL["EE_InquisitorClassHat_F_TL"] = "57abd7bc924d38b45a254a05c31bf35e";
                        m_AllEEL["EE_InquisitorClassHat_M_DW"] = "e44dfa536f250ce488f87ed259bac13d";
                        m_AllEEL["EE_InquisitorClassHat_M_EL"] = "b744cf6676aa5aa4c931d381cde0d326";
                        m_AllEEL["EE_InquisitorClassHat_M_GN"] = "97235428df7bd6e4eacdb2dd4bddabb5";
                        m_AllEEL["EE_InquisitorClassHat_M_HE"] = "d1097a94a3b00444687bf48ff9d05394";
                        m_AllEEL["EE_InquisitorClassHat_M_HL"] = "0d722bf1b0d619d4ebea9a696489d292";
                        m_AllEEL["EE_InquisitorClassHat_M_HM"] = "941cca07ade10834bb4f96de87980db7";
                        m_AllEEL["EE_InquisitorClassHat_M_HO"] = "b649ee00e3952934ab331fb4431d01bc";
                        m_AllEEL["EE_InquisitorClassHat_M_OD"] = "af6a9036aed8c2c42bad627971cbc57a";
                        m_AllEEL["EE_InquisitorClassHat_M_TL"] = "15ad77cf5bf6d3140b48b2115d94f065";
                        m_AllEEL["EE_Inquisitor_F_Any"] = "2d67e529246cc754390a5c92d5ee50dd";
                        m_AllEEL["EE_Inquisitor_M_Any"] = "1d070a314c6b6cc4c8cb25535962542e";
                        m_AllEEL["EE_KineticistAccessories_F_Any"] = "16db9eaeb326aa04c85fa7fde940b236";
                        m_AllEEL["EE_KineticistAccessories_M_Any"] = "85e1930d5f4fe35498ec29c8dc689d53";
                        m_AllEEL["EE_KineticistTattooAir_F_Any"] = "aecc5905323948449b4cd3bfe36e5daf";
                        m_AllEEL["EE_KineticistTattooEarth_F_Any"] = "e14893636b8829744a74aaefdb89b2a9";
                        m_AllEEL["EE_KineticistTattooFire_F_Any"] = "f83e7c4b89bffef4eb2892a78649e855";
                        m_AllEEL["EE_KineticistTattooFire_M_Any"] = "798a55ee60c63ba498c34925957b7e89";
                        m_AllEEL["EE_KineticistTattooWater_F_Any"] = "4f3b814e21590074b987eb8d6ac67943";
                        m_AllEEL["EE_KineticistTattooWater_M_Any"] = "7b8e989f8cca2be428c8362ef64569d4";
                        m_AllEEL["EE_Kineticist_F_Any"] = "4eb7ca3ef38cbb6449c2a7db101d20fa";
                        m_AllEEL["EE_Kineticist_M_Any"] = "1aa1272c6312fc449b33f81f2f39bf6e";
                        m_AllEEL["EE_LeatherBandits_F_Any"] = "1f4d9676b514da6409aea4992c8f054a";
                        m_AllEEL["EE_LeatherBandits_M_Any"] = "e5696f96a8a5f9249bc9ac5dc908367b";
                        m_AllEEL["EE_LeatherBlack_F_Any"] = "66147bc848a6f5f42a9180c524c53454";
                        m_AllEEL["EE_LeatherBlack_M_Any"] = "6a141e03cbaa1a348857ffb76a3a5f15";
                        m_AllEEL["EE_LeatherDemonic_F_Any"] = "40a7fdcd4337abe48b87386dc3bbf083";
                        m_AllEEL["EE_LeatherDemonic_M_Any"] = "14fb1cfcf0dca4945a8e4f45252ac53b";
                        m_AllEEL["EE_LeatherEvil_F_Any"] = "48e0c7ebd77299f4f8293ca2558ab742";
                        m_AllEEL["EE_LeatherEvil_M_Any"] = "c7e40bac07270a14ab7e075344232cf1";
                        m_AllEEL["EE_LeatherHolyAzata_F_Any"] = "06dab821e2e497c46bbf5e20dce33d00";
                        m_AllEEL["EE_LeatherMagus_F_Any"] = "a95af774788eaf344875fd3084b17468";
                        m_AllEEL["EE_LeatherMagus_M_Any"] = "c129a9489a3148b41bc4f5e44b78acd6";
                        m_AllEEL["EE_LeatherRanger_F_Any"] = "2fbba7f75c63deb4388fe4e5b4562ec2";
                        m_AllEEL["EE_LeatherRanger_M_Any"] = "6fb90d785069c10498a1d8487db87fde";
                        m_AllEEL["EE_LeatherThieves_F_Any"] = "4fe59d43474d84940bb3117082e23acf";
                        m_AllEEL["EE_LeatherThieves_M_Any"] = "30fd0fecd59faad4e82262b7d5d740fc";
                        m_AllEEL["EE_LichTest_M_HM"] = "d5ef8af171417834da5804207d75eb1f";
                        m_AllEEL["EE_LivingArmorBody_M_HM"] = "c246bfe9cb9c66f41b7e2ac9b9f237de";
                        m_AllEEL["EE_MagusAccessories_F_Any"] = "e002625d7f6cd264b9e2022602676e28";
                        m_AllEEL["EE_MagusAccessories_M_Any"] = "9f34d4d54461eb646bf17aed09a15e72";
                        m_AllEEL["EE_Magus_F_Any"] = "76450bca7e0cbf44ca98c26eee988bd8";
                        m_AllEEL["EE_Magus_M_Any"] = "50b7e43a87b0a9340b65b56eb13ddaf8";
                        m_AllEEL["EE_MaskCommonCultist_F_DW"] = "5cc97483be118464faace104f0bfa7c1";
                        m_AllEEL["EE_MaskCommonCultist_F_EL"] = "bc5a480f484e5094c86628c76fa4fadc";
                        m_AllEEL["EE_MaskCommonCultist_F_GN"] = "12bb66ff7ebc74b4482a4afabfd795c3";
                        m_AllEEL["EE_MaskCommonCultist_F_HE"] = "38410d4adfc9b474abc15e704e30f776";
                        m_AllEEL["EE_MaskCommonCultist_F_HL"] = "19f69cce654fa344db9a078500c5a9ee";
                        m_AllEEL["EE_MaskCommonCultist_F_HM"] = "2e9e4ec45f52f334bad60332d1205e94";
                        m_AllEEL["EE_MaskCommonCultist_F_HO"] = "329a315a633adc64a81ed556b92c9bdd";
                        m_AllEEL["EE_MaskCommonCultist_F_OD"] = "7de3592b8e0ba1b4ea21aa276318423e";
                        m_AllEEL["EE_MaskCommonCultist_F_TL"] = "35d52dc85b411ce439929f3dfd9fab14";
                        m_AllEEL["EE_MaskCommonCultist_M_DW"] = "a6ca6042cbc15bf48959656805ff061e";
                        m_AllEEL["EE_MaskCommonCultist_M_EL"] = "dca51c49e64ef6d429936cea60bd6711";
                        m_AllEEL["EE_MaskCommonCultist_M_GN"] = "34af68fb5daf8814b8eec4eae86b272d";
                        m_AllEEL["EE_MaskCommonCultist_M_HE"] = "b8703a6839a4eb847b70c61a3d978fc9";
                        m_AllEEL["EE_MaskCommonCultist_M_HL"] = "98aef4cd5be564a4f867a5d2d6b7a85b";
                        m_AllEEL["EE_MaskCommonCultist_M_HM"] = "42f44c812c08bf841a8bf5cc1d4c143d";
                        m_AllEEL["EE_MaskCommonCultist_M_HO"] = "3263f9276b5993e4487ec8d15bec313c";
                        m_AllEEL["EE_MaskCommonCultist_M_OD"] = "2dd721b5916e3a6418d3d03de5a24140";
                        m_AllEEL["EE_MaskCommonCultist_M_TL"] = "7b2ec1d8cb0db2747a804240c6b6fb60";
                        m_AllEEL["EE_MaskCultistAreshkagal_F_DW"] = "4bf3ab811ed3b3b48bc1be41af9ba4a7";
                        m_AllEEL["EE_MaskCultistAreshkagal_F_EL"] = "9f53da1a968adfd4686856658a0ca1ef";
                        m_AllEEL["EE_MaskCultistAreshkagal_F_GN"] = "72a3603e841864f4bbb615c328d23d3f";
                        m_AllEEL["EE_MaskCultistAreshkagal_F_HE"] = "5147650cdca63b1488d6c899558d78c7";
                        m_AllEEL["EE_MaskCultistAreshkagal_F_HL"] = "584655d93b3de4a459a03718e1b1ec5b";
                        m_AllEEL["EE_MaskCultistAreshkagal_F_HM"] = "bca2133fb144c2a46a869ac920ce83a8";
                        m_AllEEL["EE_MaskCultistAreshkagal_F_HO"] = "d276238b00ccbaa48a7b92a1139286bf";
                        m_AllEEL["EE_MaskCultistAreshkagal_F_OD"] = "92866481036185b478c185b72981d961";
                        m_AllEEL["EE_MaskCultistAreshkagal_F_TL"] = "43b9a4af13baa83469ae6362113762a9";
                        m_AllEEL["EE_MaskCultistAreshkagal_M_DW"] = "8fc856e15483caa4084b62dd4c045dec";
                        m_AllEEL["EE_MaskCultistAreshkagal_M_EL"] = "7cc22ffec0175774c91582c4b765c30c";
                        m_AllEEL["EE_MaskCultistAreshkagal_M_GN"] = "0f11d732ffc272348bdd13d293cd1afe";
                        m_AllEEL["EE_MaskCultistAreshkagal_M_HE"] = "3eb6c6c5b4aa2504abfa50ca97f5b054";
                        m_AllEEL["EE_MaskCultistAreshkagal_M_HL"] = "f75b41596df2f0849a72435c16dbdfa1";
                        m_AllEEL["EE_MaskCultistAreshkagal_M_HM"] = "7e741f9d22b45b74db9a7cb03d480d4f";
                        m_AllEEL["EE_MaskCultistAreshkagal_M_HO"] = "9d620184fc34c844ea286800e41a8f80";
                        m_AllEEL["EE_MaskCultistAreshkagal_M_OD"] = "cb5073691ef51c245be3bf9715991548";
                        m_AllEEL["EE_MaskCultistAreshkagal_M_TL"] = "847ffb3871dc0da41b56f1baebf990e1";
                        m_AllEEL["EE_MaskEvilMagic_F_DW"] = "bad366dfb569d5f4891fd0861e3f82f9";
                        m_AllEEL["EE_MaskEvilMagic_F_EL"] = "6b1ea541bdbc7074994c793cd8b4af5c";
                        m_AllEEL["EE_MaskEvilMagic_F_GN"] = "f294c8731c91599488e3f3ed3b936c06";
                        m_AllEEL["EE_MaskEvilMagic_F_HE"] = "feecf43016be4f2488bf2c63bb2c6878";
                        m_AllEEL["EE_MaskEvilMagic_F_HL"] = "91d82da0d4318bc4f8a00b82c68130e4";
                        m_AllEEL["EE_MaskEvilMagic_F_HM"] = "01274b45bca3790499fe994bee9ae612";
                        m_AllEEL["EE_MaskEvilMagic_F_HO"] = "74802caf899e1594d96d48d38eaf5dac";
                        m_AllEEL["EE_MaskEvilMagic_F_OD"] = "295164fad6324a744b188ecab0fde717";
                        m_AllEEL["EE_MaskEvilMagic_F_TL"] = "e261414bd8c38064a983b20adef8451e";
                        m_AllEEL["EE_MaskEvilMagic_M_DW"] = "d2d73d95af3f99a49b43058dd8a1092d";
                        m_AllEEL["EE_MaskEvilMagic_M_EL"] = "9870b609af5c3de4e879386229ea29d0";
                        m_AllEEL["EE_MaskEvilMagic_M_GN"] = "38b92d2d368b3ff469d635c3c5447b65";
                        m_AllEEL["EE_MaskEvilMagic_M_HE"] = "7f573a7056c097a4eb788f6218bd941c";
                        m_AllEEL["EE_MaskEvilMagic_M_HL"] = "e3cc76d6f2f1aff41824702e5b3b9f55";
                        m_AllEEL["EE_MaskEvilMagic_M_HM"] = "00d32b79a4d5e0b4ea5d3a933190b971";
                        m_AllEEL["EE_MaskEvilMagic_M_HO"] = "83a10719da7260b4dac2950dd678b624";
                        m_AllEEL["EE_MaskEvilMagic_M_OD"] = "10c0f3a2615a0674c8153d6ab280994d";
                        m_AllEEL["EE_MaskEvilMagic_M_TL"] = "ee63dd2111a30f94b9a1c29e4033b1cf";
                        m_AllEEL["EE_MaskMummy_M_ZB"] = "867398437a6cccf42bb3e225c6f61ead";
                        m_AllEEL["EE_MemitimHelmet_F_DW"] = "f9a1092f4a0f7694d9d369c67a4631cd";
                        m_AllEEL["EE_MemitimHelmet_F_EL"] = "ce96bdb0e73273e41994cfad87b8fbd2";
                        m_AllEEL["EE_MemitimHelmet_F_GN"] = "785872b44099d8d4c9979e06b4c9c143";
                        m_AllEEL["EE_MemitimHelmet_F_HE"] = "e5e565aff7d880140867e486ff745538";
                        m_AllEEL["EE_MemitimHelmet_F_HL"] = "d5f4b290447b09249b48bccd115fda41";
                        m_AllEEL["EE_MemitimHelmet_F_HM"] = "8e4192c5acddff6409b1755a77b33aae";
                        m_AllEEL["EE_MemitimHelmet_F_HO"] = "732c6b727671ced418e2a78f8245ec62";
                        m_AllEEL["EE_MemitimHelmet_F_OD"] = "f32b434e2c3a2024b97d64b1b1029fa6";
                        m_AllEEL["EE_MemitimHelmet_F_TL"] = "f37a74738f4a1f84bb4895365829b73a";
                        m_AllEEL["EE_MemitimHelmet_M_DW"] = "f313500195e772645a9320178b6ffc55";
                        m_AllEEL["EE_MemitimHelmet_M_EL"] = "3b3432e210a478f46addd32c143eba11";
                        m_AllEEL["EE_MemitimHelmet_M_GN"] = "766fe019a8b74f342bc80b1531364991";
                        m_AllEEL["EE_MemitimHelmet_M_HE"] = "9950cd193944d0645919d722cf9f57ff";
                        m_AllEEL["EE_MemitimHelmet_M_HL"] = "e890514634a24f04dab069c622fcb940";
                        m_AllEEL["EE_MemitimHelmet_M_HM"] = "2a66e7daf415a5040819b50bb8369f74";
                        m_AllEEL["EE_MemitimHelmet_M_HO"] = "e734e806007773947bf5ad90ef8d2ce4";
                        m_AllEEL["EE_MemitimHelmet_M_OD"] = "6f3ee9bf44eb5f34c9d852e830cf56ac";
                        m_AllEEL["EE_MemitimHelmet_M_TL"] = "ea155fb6576fc654a8b187d90441e2db";
                        m_AllEEL["EE_Memitim_F_Any"] = "01ff9252e8a456047adad718baee1c52";
                        m_AllEEL["EE_Memitim_M_Any"] = "3e011751cb9e6fe44bf4db1824d34c07";
                        m_AllEEL["EE_MongrelEars01_F_EL"] = "50fad90ddf42c87438991f89fe7da0b5";
                        m_AllEEL["EE_MongrelEars01_F_GN"] = "f24e4373c5b4fcf4e8a268d64a62f9d1";
                        m_AllEEL["EE_MongrelEars01_F_HM"] = "be14de00304a17c4cb88dc78c377eb2a";
                        m_AllEEL["EE_MongrelEars01_M_EL"] = "c13f1acd759692f47ba2994395332eb0";
                        m_AllEEL["EE_MongrelEars01_M_HO"] = "ac7ac00db33bec748ac63580ebd25190";
                        m_AllEEL["EE_MongrelEars02_F_HE"] = "e58568acd055a3c468192fb814c47c32";
                        m_AllEEL["EE_MongrelEars02_F_HM"] = "88fcdcdc8e9e8d24f9a7d7c91a6054a5";
                        m_AllEEL["EE_MongrelEars02_F_TL"] = "0fc9fb5bdedf99a48a9c2cad47f13ac9";
                        m_AllEEL["EE_MongrelEars02_M_EL"] = "1ddb30ce2724af74686e5c21dd36f342";
                        m_AllEEL["EE_MongrelEars02_M_GN"] = "6d20f507852d81545a38de5c33774384";
                        m_AllEEL["EE_MongrelEars02_M_HM"] = "b5b2e84af4b3f4e429efaba22fb53789";
                        m_AllEEL["EE_MongrelEars02_M_OD"] = "cca09cdd75a9c604aad3299722514b9b";
                        m_AllEEL["EE_MongrelHorns01_F_DW"] = "5f9f5f3315f286a4d97918347b28eea1";
                        m_AllEEL["EE_MongrelHorns01_F_EL"] = "10cc8cd2baa38384291f7640cd7227bb";
                        m_AllEEL["EE_MongrelHorns01_F_GN"] = "394a429941adb66489229d721c7ad61a";
                        m_AllEEL["EE_MongrelHorns01_F_HL"] = "59ed3e2b1cc341046a68b55f0da80305";
                        m_AllEEL["EE_MongrelHorns01_F_HM"] = "784bb887aceb5a045996974a71e78875";
                        m_AllEEL["EE_MongrelHorns01_F_TL"] = "378fd4b65e9799e40adbe18b702a0dbf";
                        m_AllEEL["EE_MongrelHorns01_M_DW"] = "11e8c436c8c969844a3defbfca96efdc";
                        m_AllEEL["EE_MongrelHorns01_M_EL"] = "8439da993972eca44b8c7f555e33f780";
                        m_AllEEL["EE_MongrelHorns01_M_GN"] = "408401bf088b0ad4a9a9747be66bb67d";
                        m_AllEEL["EE_MongrelHorns01_M_HL"] = "9a17220e985b8404487082cb82980875";
                        m_AllEEL["EE_MongrelHorns01_M_HM"] = "7ae57d4a307b8cc47acd57cf56c99b5e";
                        m_AllEEL["EE_MongrelHorns01_M_TL"] = "fcbe9a67ef4b4f14aa3aec7501eb0993";
                        m_AllEEL["EE_MongrelHorns02_F_DW"] = "170f88fc0bd84d84d9b0aface2307604";
                        m_AllEEL["EE_MongrelHorns02_F_EL"] = "fdf97bfe79e511943b5f4e39e87e330d";
                        m_AllEEL["EE_MongrelHorns02_F_GN"] = "da4bbee5eeadb90439229a6703ccfbdf";
                        m_AllEEL["EE_MongrelHorns02_F_HL"] = "a45c52286c60a954bbe81e9ac0740795";
                        m_AllEEL["EE_MongrelHorns02_F_HM"] = "ce26bc3e359a0b84c9095b2a012bc877";
                        m_AllEEL["EE_MongrelHorns02_F_TL"] = "6131624f5969745449ab39e371d96e60";
                        m_AllEEL["EE_MongrelHorns02_M_DW"] = "d7f72406fc0ff5c4ab463fb3932a8e41";
                        m_AllEEL["EE_MongrelHorns02_M_EL"] = "01fe52a14df1d8f4dad1bd2b4d064d9b";
                        m_AllEEL["EE_MongrelHorns02_M_GN"] = "c3f01fb66b55e354392321770961088c";
                        m_AllEEL["EE_MongrelHorns02_M_HL"] = "323ddc3b2ba0d1c49995cf755e6a0b0a";
                        m_AllEEL["EE_MongrelHorns02_M_HM"] = "fed897cada6bd7e4196260cd6543e31b";
                        m_AllEEL["EE_MongrelHorns02_M_OD"] = "7eb1d2d08ee08424f8699b0732857951";
                        m_AllEEL["EE_MongrelHorns02_M_TL"] = "2e661c135188d594d81aeda28028bf3c";
                        m_AllEEL["EE_MongrelHorns03_F_DW"] = "cc71eae536cd1a147b8e4d5a7430dc65";
                        m_AllEEL["EE_MongrelHorns03_F_EL"] = "3a80435506254fa4eb692d3e02b8bd8f";
                        m_AllEEL["EE_MongrelHorns03_F_GN"] = "2d79c418febe6c74d991625b8d4a153c";
                        m_AllEEL["EE_MongrelHorns03_F_HL"] = "ab525b1dbe2928540bd8be8d408dd45d";
                        m_AllEEL["EE_MongrelHorns03_F_HM"] = "10d6c6e9b22b3a64bb8fdca64b77ed2a";
                        m_AllEEL["EE_MongrelHorns03_F_TL"] = "bef7b1ee620abae41af0d03f81be5fc5";
                        m_AllEEL["EE_MongrelHorns03_M_DW"] = "d0eb291feec381a43a46c49d8170ee94";
                        m_AllEEL["EE_MongrelHorns03_M_EL"] = "7d49bbefc2d0ad94e9adf219091aac6b";
                        m_AllEEL["EE_MongrelHorns03_M_GN"] = "458655d1da1bbc74e8d5b50a589914b8";
                        m_AllEEL["EE_MongrelHorns03_M_HE"] = "c2a30d01daaed7a408bd45e04528e578";
                        m_AllEEL["EE_MongrelHorns03_M_HL"] = "dbdd1794de34186498684a56bf0b270d";
                        m_AllEEL["EE_MongrelHorns03_M_HM"] = "e900a1a0ecd6d754ea05b63fb38334bb";
                        m_AllEEL["EE_MongrelHorns03_M_TL"] = "5b6da001117e08c47a4e565f2ff46895";
                        m_AllEEL["EE_MongrelHorns04_F_DW"] = "32f51be24f611bf48993dd1a771a9f46";
                        m_AllEEL["EE_MongrelHorns04_F_EL"] = "c1002d33ade3c024b8e7f12667e83be6";
                        m_AllEEL["EE_MongrelHorns04_F_GN"] = "a98a6a30183a73947812638378888591";
                        m_AllEEL["EE_MongrelHorns04_F_HL"] = "e4c14ae69bdc69a48aaafcee3905b738";
                        m_AllEEL["EE_MongrelHorns04_F_HM"] = "67a781ca294d40441b5ae7a81330ca83";
                        m_AllEEL["EE_MongrelHorns04_F_TL"] = "11f0c68e14be24847b8d78fb13c9733b";
                        m_AllEEL["EE_MongrelHorns04_M_DW"] = "705d79a24744b654c8db4641ddc5d69d";
                        m_AllEEL["EE_MongrelHorns04_M_EL"] = "d5fba06ff684d294cadeb32d82214672";
                        m_AllEEL["EE_MongrelHorns04_M_GN"] = "6b34aa9d47ca6d0499bc8b8ccb75146e";
                        m_AllEEL["EE_MongrelHorns04_M_HL"] = "e2a56613816943840bc32865116c36ea";
                        m_AllEEL["EE_MongrelHorns04_M_HM"] = "3c1269cc50173ec4bbcaca741205ccd5";
                        m_AllEEL["EE_MongrelHorns04_M_HO"] = "3152c7330d1798d48897d6858955b802";
                        m_AllEEL["EE_MongrelHorns04_M_TL"] = "2d0d40fe4ad859d4dbc77376ca0e868b";
                        m_AllEEL["EE_MongrelHorns05_F_TL"] = "fe2d8bf332b38cf459ea989490228099";
                        m_AllEEL["EE_MongrelSkin01Lizard_U_Any"] = "86127616283ae7741ae3e813904865cc";
                        m_AllEEL["EE_MongrelSkin02Lizard_U_Any"] = "7109791d63944254589b908564604c79";
                        m_AllEEL["EE_MongrelSkin03Furry_U_Any"] = "1aeb459da29dca341a78317170eec262";
                        m_AllEEL["EE_MongrelSkin04Stone_U_Any"] = "33c1acb9aaa60dc448691b6d63fcb22f";
                        m_AllEEL["EE_Monk_F_Any"] = "aa80cc719e9a6b547acff056819f366a";
                        m_AllEEL["EE_Monk_M_Any"] = "ded027dd3a059ae4aa1e8cd93e450b03";
                        m_AllEEL["EE_MummyDecorated_F_Any"] = "06e81704f75542b4cba369849ce59d08";
                        m_AllEEL["EE_MummyDecorated_M_Any"] = "18e615432defceb43907ad30a75e1ced";
                        m_AllEEL["EE_MummyNormal_F_Any"] = "765c30d4184fc6043b66a3cdc5cbb015";
                        m_AllEEL["EE_MummyNormal_M_Any"] = "117c8629e53d13d4cab01c86b96d8244";
                        m_AllEEL["EE_MythicLichBody_F_Any"] = "c4249b6bed4970948b631d0972dbbc8c";
                        m_AllEEL["EE_MythicLichBody_M_Any"] = "0c53133cb474d4f40bc977d4127bbe2a";
                        m_AllEEL["EE_MythicLichDamage_F_Any"] = "28b918db82f266e4ba7e9e345cf2bcf6";
                        m_AllEEL["EE_MythicLichDamage_M_Any"] = "4e174730821e658449f5e5080401af9c";
                        m_AllEEL["EE_MythicLichHead_F_DW"] = "9ad2f69e9f1b59e4f871cbd51d9d6cd5";
                        m_AllEEL["EE_MythicLichHead_F_EL"] = "e0a7648f17e21a74c9cbae94b3597bc3";
                        m_AllEEL["EE_MythicLichHead_F_GN"] = "f303e2a80eeb18e448ae9a72cc5f0cb6";
                        m_AllEEL["EE_MythicLichHead_F_HE"] = "ebcb85416a817ea4db220153b9406060";
                        m_AllEEL["EE_MythicLichHead_F_HL"] = "57d20695b1c57aa41a1be49c83a070fe";
                        m_AllEEL["EE_MythicLichHead_F_HM"] = "1534dc55474ce4e4e89556633c9933a7";
                        m_AllEEL["EE_MythicLichHead_F_HO"] = "e7d7a53965308d14284d940b73afa114";
                        m_AllEEL["EE_MythicLichHead_F_KT"] = "dc65c06b107f220498ee123af6012a05";
                        m_AllEEL["EE_MythicLichHead_F_OD"] = "1968d6a8893a4104b93b20f16c758e1d";
                        m_AllEEL["EE_MythicLichHead_F_TL"] = "db505e09b68cdf74bbb28c54ddec25b0";
                        m_AllEEL["EE_MythicLichHead_M_DW"] = "1d7ce901944b5db4f941d84148b0861b";
                        m_AllEEL["EE_MythicLichHead_M_EL"] = "baf27ad82057ae74a9779fa5d35c6b83";
                        m_AllEEL["EE_MythicLichHead_M_GN"] = "d54e6d29d74536f419dd2cf1fc4e1f1d";
                        m_AllEEL["EE_MythicLichHead_M_HE"] = "6ccd667c59eeb7d4099482113fd9de32";
                        m_AllEEL["EE_MythicLichHead_M_HL"] = "1f57f9f494205e74b887aa765654a09a";
                        m_AllEEL["EE_MythicLichHead_M_HM"] = "df142f50551477f438d4613a0d798690";
                        m_AllEEL["EE_MythicLichHead_M_HO"] = "5c34887de667b074c847574251afe54e";
                        m_AllEEL["EE_MythicLichHead_M_KT"] = "4a5fe5a9b0f79f7418718287aa7d7458";
                        m_AllEEL["EE_MythicLichHead_M_OD"] = "8e93e9c84f6d59d4682eb7b85abb1df6";
                        m_AllEEL["EE_MythicLichHead_M_TL"] = "d1e269bf145810143bf3083101dcbf0c";
                        m_AllEEL["EE_MythicLich_Desaturation"] = "b668bf5efab301f4cae3f01dc81d0cf1";
                        m_AllEEL["EE_MythicTrickster_Saturation"] = "82b3066a6aaf41f43a30d4db8b361a57";
                        m_AllEEL["EE_NPCAccessoriesBelt1_F_Any"] = "7eb0cfee44877224da82f5a437699e16";
                        m_AllEEL["EE_NPCAccessoriesBelt1_M_Any"] = "3d030011a902e8d4cab26db1dd778843";
                        m_AllEEL["EE_NPCAccessoriesNecklace1_F_Any"] = "73e920d0843306f4aa0fda8d3e32b2d0";
                        m_AllEEL["EE_NPCAccessoriesNecklace1_M_Any"] = "c74bcc5862a72724e9c9f54d32908897";
                        m_AllEEL["EE_NPCAccessoriesNecklace2_F_Any"] = "d9b326add9df57940a8715640760fa38";
                        m_AllEEL["EE_NPCAccessoriesNecklace2_M_Any"] = "7a9ffbffc23c24542b45f1c42bd5e18d";
                        m_AllEEL["EE_NPCAccessoriesNecklace3_F_Any"] = "2cc49ec615c7a4f4491e81dba44008ab";
                        m_AllEEL["EE_NPCAccessoriesNecklace3_M_Any"] = "83b9f539571024f40a8cc865c025800c";
                        m_AllEEL["EE_NPCAccessoriesNecklace4_F_Any"] = "072b604519e11ed44b50af384e388cc5";
                        m_AllEEL["EE_NPCAccessoriesNecklace4_M_Any"] = "878d46fdc81085a45a46d5788978747d";
                        m_AllEEL["EE_NPCAccessoriesNecklace5_F_Any"] = "99ac392fc698b7a4bb059aa6f97ae3e3";
                        m_AllEEL["EE_NPCAccessoriesNecklace5_M_Any"] = "d306c3eb2e2f98f43b44ebb736c59ad1";
                        m_AllEEL["EE_NPCAccessoriesNecklace6_F_Any"] = "47dd7c493f4cfed438cea9dd49d4ce27";
                        m_AllEEL["EE_NPCAccessoriesNecklace6_M_Any"] = "88ee2081bb6031f489403bbcc139da44";
                        m_AllEEL["EE_NPCEyesBlackGlossy_U_Any"] = "e36b8a3c0ca453f44b2e2e25beb9a328";
                        m_AllEEL["EE_NPCEyesBlackMatte_U_Any"] = "84e061c83c4588f44ab5ebe81489c6b3";
                        m_AllEEL["EE_NPCEyesShinyColored_U_Any"] = "c5487e7e903d25a40be683767f3df0b4";
                        m_AllEEL["EE_NPCMogrelOutfit01_F_Any"] = "0d24a6f6668d1b6459e5b825eb73b256";
                        m_AllEEL["EE_NPCMogrelOutfit01_M_Any"] = "0f923fc4dbfb7e64fbfb6e7fd39d6fa3";
                        m_AllEEL["EE_NPCMogrelOutfit02_F_Any"] = "42ccf7fe5b783a54993680defb865c3b";
                        m_AllEEL["EE_NPCMogrelOutfit02_M_Any"] = "d1da517e81915ea4e86776826c87090c";
                        m_AllEEL["EE_NPCMogrelOutfit03_F_Any"] = "58ecd6ecfee077d43a2ab9b6c13aff7d";
                        m_AllEEL["EE_NPCMogrelOutfit03_M_Any"] = "24b2b315ebeb6a74cae114ee6ee9b8b5";
                        m_AllEEL["EE_NPCMogrelOutfit04_F_Any"] = "e6092318e7aabf2468818245055b7268";
                        m_AllEEL["EE_NPCMogrelOutfit04_M_Any"] = "05c52f83dba9092458a9ce1755329779";
                        m_AllEEL["EE_NPCMogrelOutfit05_F_Any"] = "7682c975fd928ee4e9dc3b984ab89800";
                        m_AllEEL["EE_NPCMogrelOutfit05_M_Any"] = "de67256421603004fb2a1d3491097629";
                        m_AllEEL["EE_NPCMogrelOutfit06_F_Any"] = "e914c2f65dbe5054d9a94ef61c09342f";
                        m_AllEEL["EE_NPCMogrelOutfit06_M_Any"] = "89f05387dee834e4b87e9894563945c4";
                        m_AllEEL["EE_NPCMogrelOutfit07_F_Any"] = "8935a96cfc7bb5b49bc01cc6ac263971";
                        m_AllEEL["EE_NPCMogrelOutfit07_M_Any"] = "239b089a273167046a7bd336dac1e98a";
                        m_AllEEL["EE_NPCMogrelOutfit08_F_Any"] = "466bfc1dfac630f4f8a8276ddc7678b6";
                        m_AllEEL["EE_NPCMogrelOutfit08_M_Any"] = "fd5a41605abd74c43bc9782bc8ed0923";
                        m_AllEEL["EE_NPCMogrelOutfit09_F_Any"] = "1fb8f57409d4a914fbcceb1b249bfd7d";
                        m_AllEEL["EE_NPCMogrelOutfit09_M_Any"] = "2057777d8ee1eda41a6e153e69d7be5b";
                        m_AllEEL["EE_NPCMogrelOutfit10_F_Any"] = "be04b4fcbdc8af34987ccf82f5a341a8";
                        m_AllEEL["EE_NPCMogrelOutfit10_M_Any"] = "cb4f82c5ef6b53540943d2197dee886f";
                        m_AllEEL["EE_NPCRobeHolyAzataColored_M_Any"] = "ffaf5209cd14cd94a8415df5102d208f";
                        m_AllEEL["EE_NPCRobeHolyAzata_M_Any"] = "4ab2a23aab4712f408b243d1273b8d2f";
                        m_AllEEL["EE_NenioBag_F_Any"] = "5d80530cb5b267649a37f8f00efd8644";
                        m_AllEEL["EE_NenioOutfit_F_Any"] = "0a909ccc5fbbcf3449ae2d723f1f5cb8";
                        m_AllEEL["EE_Nurah_F_Any"] = "9d6cf7902fd18924491ce7f4d1d7c667";
                        m_AllEEL["EE_OracleAccessories_F_Any"] = "62089609a8723a54d901d1747d18e52f";
                        m_AllEEL["EE_OracleAccessories_M_Any"] = "4d6c4763d605b3b42b053495a268a7f5";
                        m_AllEEL["EE_OracleClassHat_F_DW"] = "4fcb624cac29acb4fa1b773339b040a2";
                        m_AllEEL["EE_OracleClassHat_F_EL"] = "83d7ff40ec7691f4ba31f80869825e2e";
                        m_AllEEL["EE_OracleClassHat_F_GN"] = "9b16e85d50887c54ba86dd6827aa1f0c";
                        m_AllEEL["EE_OracleClassHat_F_HE"] = "e2dfc49c9099a374aadb4962206dd58d";
                        m_AllEEL["EE_OracleClassHat_F_HL"] = "ff6e6d0cdc415a44aaa189c51f61c62e";
                        m_AllEEL["EE_OracleClassHat_F_HM"] = "a30aa957324a2f543989365f02a66eeb";
                        m_AllEEL["EE_OracleClassHat_F_HO"] = "91010041c4fc30d469b6201f37640e70";
                        m_AllEEL["EE_OracleClassHat_F_OD"] = "220d5fc9fd71f7b4685d1755285d3a65";
                        m_AllEEL["EE_OracleClassHat_F_TL"] = "afd7096f463291d49a248c579b73edff";
                        m_AllEEL["EE_OracleClassHat_M_DW"] = "b892b11f786510b438972886c0db3d8e";
                        m_AllEEL["EE_OracleClassHat_M_EL"] = "dc5dbdba96dd12446aae97a821be54d6";
                        m_AllEEL["EE_OracleClassHat_M_GN"] = "2455a03925cc5124385d07624081ee7b";
                        m_AllEEL["EE_OracleClassHat_M_HE"] = "962214ba04dae20418138bfc75c189c1";
                        m_AllEEL["EE_OracleClassHat_M_HL"] = "423d8e835fdd29748ac583262bcced3c";
                        m_AllEEL["EE_OracleClassHat_M_HM"] = "903ed36d8e82b3f4ab91d83a2756831d";
                        m_AllEEL["EE_OracleClassHat_M_HO"] = "d724b4b8def539648a3c2b8886e8fbad";
                        m_AllEEL["EE_OracleClassHat_M_OD"] = "8cb6c2738d4323b4483628e9cf1fbcc6";
                        m_AllEEL["EE_OracleClassHat_M_TL"] = "65b45fc2c402b914688fc1501774e762";
                        m_AllEEL["EE_Oracle_F_Any"] = "66135ed4197da2f4885636e8d37cad25";
                        m_AllEEL["EE_Oracle_M_Any"] = "4fcd15394bba8834b9f43a720065166a";
                        m_AllEEL["EE_PaddedArmy_F_Any"] = "0306313873fc5664694d8b48131efd1f";
                        m_AllEEL["EE_PaddedArmy_M_Any"] = "420ed0abb289bb145a4c799eb2732d61";
                        m_AllEEL["EE_PaddedNoble_F_Any"] = "e167aa10f32f10f46b5f77cf1e41a6e1";
                        m_AllEEL["EE_PaddedNoble_M_Any"] = "73c3d829d514c2d49b73994dd69e81f9";
                        m_AllEEL["EE_PaddedSimple_F_Any"] = "e4ea110242cb0ff4584bd784d0fb8e0f";
                        m_AllEEL["EE_PaddedSimple_M_Any"] = "8c165f2459c3d2248a170ba29f319c51";
                        m_AllEEL["EE_PaddedThieves_F_Any"] = "9092e91dcc55c184faeb944cdaf5f12a";
                        m_AllEEL["EE_PaddedThieves_M_Any"] = "49d5075c0ffaf504d9c09c42893d2625";
                        m_AllEEL["EE_PaladinAccessories_F_Any"] = "06295435bf527fc4c8b6a988e87ed911";
                        m_AllEEL["EE_PaladinAccessories_M_Any"] = "d69b4cec1e6dd8343996222015236233";
                        m_AllEEL["EE_Paladin_F_Any"] = "2f3e9f56eb0a2ee41a83a4001f536759";
                        m_AllEEL["EE_Paladin_M_Any"] = "98d34b949234954429bfd93e508734be";
                        m_AllEEL["EE_PantsBard_F_Any"] = "cb9c6794dc67a3848b56dc7b8bb255bf";
                        m_AllEEL["EE_PantsBard_M_Any"] = "58d22708dcb577f429feba0124906bf0";
                        m_AllEEL["EE_PantsBloodrager_F_Any"] = "44e46faeaae9f0c43b3d180197f567e9";
                        m_AllEEL["EE_PantsBloodrager_M_Any"] = "cd8a2f876469507408ca118c8608bed0";
                        m_AllEEL["EE_PantsFarmer1_F_Any"] = "c29d929bc7143d94aa72f4dafe4c2988";
                        m_AllEEL["EE_PantsFarmer1_M_Any"] = "f5822d5ce8c720143b81ddd2c01c3cd1";
                        m_AllEEL["EE_PantsFarmer2_F_Any"] = "233345d6027e0ce46b381ee9321a7303";
                        m_AllEEL["EE_PantsFarmer2_M_Any"] = "43fe5ba33b6064d4c92226c03ebbbf96";
                        m_AllEEL["EE_PantsHunter_F_Any"] = "4216b8e8d678fc04daa91ca58f4a7303";
                        m_AllEEL["EE_PantsHunter_M_Any"] = "662370f028eb91a469afd8993228ef7f";
                        m_AllEEL["EE_PantsInquisitor_F_Any"] = "62274d8a7e8afe44bb086992fbc504ab";
                        m_AllEEL["EE_PantsInquisitor_M_Any"] = "f76efebf514b0604da9b74bf33ed6398";
                        m_AllEEL["EE_PantsKineticist_F_Any"] = "a1cdb190e82de4544ae5806d307b9adf";
                        m_AllEEL["EE_PantsKineticist_M_Any"] = "159b60ccbac700648982bca815851a1f";
                        m_AllEEL["EE_PantsRanger_F_Any"] = "557db741723f4734eb4222e3a4ebc87f";
                        m_AllEEL["EE_PantsRanger_M_Any"] = "a2f5ebdbf86c92b428ee8359b6aa444b";
                        m_AllEEL["EE_PantsRogue_F_Any"] = "95cbb845fa432044488add36e3a6fe5b";
                        m_AllEEL["EE_PantsRogue_M_Any"] = "23eea60535f82c44fa38c2918a963654";
                        m_AllEEL["EE_PantsSkald_F_Any"] = "3cd5d6c79550cf645bac700772f7ae69";
                        m_AllEEL["EE_PantsSkald_M_Any"] = "5659a749ca44d9a4eb41d998da2facab";
                        m_AllEEL["EE_PantsSorcerer_F_Any"] = "6591e1d7a4244784e99863d4977ff57c";
                        m_AllEEL["EE_PantsSorcerer_M_Any"] = "98246c01bbf901947af4878a294b595a";
                        m_AllEEL["EE_Peasant1_F_Any"] = "a1077a0dc1069c049b6ee93f6f982fdf";
                        m_AllEEL["EE_Peasant1_M_Any"] = "77b76b2517d3d7e4ab74ea06409b7619";
                        m_AllEEL["EE_PilgrimBackpack_F_Any"] = "99244012dbf6d0c41be571218a50b0bc";
                        m_AllEEL["EE_PilgrimBackpack_M_Any"] = "dccc689fd5d47a14b9215eea7c478876";
                        m_AllEEL["EE_PilgrimHood_F_EL"] = "50531527ce6d66141a5304071340a55a";
                        m_AllEEL["EE_PilgrimHood_F_HE"] = "0da9798ab4024084982975bddc066237";
                        m_AllEEL["EE_PilgrimHood_F_HM"] = "14b044b234f835b4ba2736bb1b63ec94";
                        m_AllEEL["EE_PilgrimHood_F_TL"] = "fc31fd74fcbb9e64cbf84751f95b0a30";
                        m_AllEEL["EE_PilgrimHood_M_EL"] = "928ec59890f9f63418fc2b33499463f1";
                        m_AllEEL["EE_PilgrimHood_M_HE"] = "5b30d6ad7337a334bb3114b010ee47d5";
                        m_AllEEL["EE_PilgrimHood_M_HM"] = "818365352f8dd324b8a6376d8409ee81";
                        m_AllEEL["EE_PilgrimHood_M_HO"] = "b5dfc391d13d52845aa1e94075ce3f8d";
                        m_AllEEL["EE_Pilgrim_F_Any"] = "646e6e1854888c848a9e274659a1eb1f";
                        m_AllEEL["EE_Pilgrim_M_Any"] = "0c680a29ab66d164ca632dc7d5e4947b";
                        m_AllEEL["EE_RangerAccessories_F_Any"] = "1867e53a1a881c644bea8392faaa5a86";
                        m_AllEEL["EE_RangerAccessories_M_Any"] = "fa64c7bb4b115994fbe726b032035716";
                        m_AllEEL["EE_RangerClassHat_F_DW"] = "a3fdd45ea2458a947bd73ed44db37df5";
                        m_AllEEL["EE_RangerClassHat_F_EL"] = "48baba720c065c847a889a7b6595edcf";
                        m_AllEEL["EE_RangerClassHat_F_GN"] = "b269ffb088464b44899410f477a9c6eb";
                        m_AllEEL["EE_RangerClassHat_F_HE"] = "67f337e9475bf0c459447d0e63084531";
                        m_AllEEL["EE_RangerClassHat_F_HL"] = "33d1f6fc2b31fad449d76bc4c2841836";
                        m_AllEEL["EE_RangerClassHat_F_HM"] = "9f74c45486296534bb2685e766c78162";
                        m_AllEEL["EE_RangerClassHat_F_HO"] = "c0a5970e41f95ab488fee9c9a065e25c";
                        m_AllEEL["EE_RangerClassHat_F_OD"] = "4b35740ec438db3448fedc16342700ab";
                        m_AllEEL["EE_RangerClassHat_F_TL"] = "719a090fa3c6f7245a712d1bd7fcfdf5";
                        m_AllEEL["EE_RangerClassHat_M_DW"] = "4f96fed14c681e44dab31f6bfaa2323b";
                        m_AllEEL["EE_RangerClassHat_M_EL"] = "0f759a928ca1d4d439f18a09b454b3fb";
                        m_AllEEL["EE_RangerClassHat_M_GN"] = "ea86b86de73b8f347b5de4b10d27bb81";
                        m_AllEEL["EE_RangerClassHat_M_HE"] = "d73e6ba1de8dfe741abb6ed3c2b4f958";
                        m_AllEEL["EE_RangerClassHat_M_HL"] = "ddae6f0b314d7a042a04a93cb0f5ce24";
                        m_AllEEL["EE_RangerClassHat_M_HM"] = "f0537b3edb271c544bb0df349cdff7b1";
                        m_AllEEL["EE_RangerClassHat_M_HO"] = "b5d5543d36de67247a3cb93a743126fc";
                        m_AllEEL["EE_RangerClassHat_M_OD"] = "02e4d9f71ec9f8f4db59b5ac0c4d12ec";
                        m_AllEEL["EE_RangerClassHat_M_TL"] = "a5bd1117b3cc4084983e1355536eb09d";
                        m_AllEEL["EE_Ranger_F_Any"] = "db02335bae5eca84b906e4735e707bc8";
                        m_AllEEL["EE_Ranger_M_Any"] = "b7ef3397847d08545a181eb5537ddfdb";
                        m_AllEEL["EE_RobeArcane1_F_Any"] = "ae81e2120af1d3e4398e12c6ca4274ad";
                        m_AllEEL["EE_RobeArcane1_M_Any"] = "fddad059437322e41bf69dfad34a3b76";
                        m_AllEEL["EE_RobeArcane2Red_F_Any"] = "760f47ca55674ce438e94a031cbac812";
                        m_AllEEL["EE_RobeArcane2Red_M_Any"] = "8e047454d7e21be49934f8681e24dc90";
                        m_AllEEL["EE_RobeArcane3Green_F_Any"] = "950803e540669b3489944bb0910510db";
                        m_AllEEL["EE_RobeArcane3Green_M_Any"] = "e6c822e4153e89843ad4fb510fa12a99";
                        m_AllEEL["EE_RobeArcane4Brown_F_Any"] = "c2b589e861322be408847dfd2a055e35";
                        m_AllEEL["EE_RobeArcane4Brown_M_Any"] = "0c20cec699130ab4095e3b82a5884f11";
                        m_AllEEL["EE_RobeArchmage_F_Any"] = "eb30010332759b14fb0a1b27004c4b38";
                        m_AllEEL["EE_RobeArchmage_M_Any"] = "3235b6648e9eba441bf2361fc131afba";
                        m_AllEEL["EE_RobeAreshkagal_F_Any"] = "fff14cef83752af45bc35605a6092c43";
                        m_AllEEL["EE_RobeAreshkagal_M_Any"] = "71889b99c57a72b4ba5046892afa63cc";
                        m_AllEEL["EE_RobeBlackenedRags_F_Any"] = "d9933f4368ccf12438daaae132665296";
                        m_AllEEL["EE_RobeBlackenedRags_M_Any"] = "85a7321cbc0c16e4b9e1b8bf1c24784b";
                        m_AllEEL["EE_RobeDruid_F_Any"] = "b36e452b9bd190a4b9dd52ca58b7152d";
                        m_AllEEL["EE_RobeDruid_M_Any"] = "eee22f6bb42d51441903dd97340ecb9c";
                        m_AllEEL["EE_RobeEvil1_F_Any"] = "4be264e294b99004cabe8429c87b21f6";
                        m_AllEEL["EE_RobeEvil1_M_Any"] = "0bd2a5e0c50dc7645b3a461496b57c47";
                        m_AllEEL["EE_RobeLich_F_Any"] = "7d77e8a598b07fd49a11d850a6d8f796";
                        m_AllEEL["EE_RobeLich_M_Any"] = "3e9eaea0b7c1d4345b9e5f30c76ccc0c";
                        m_AllEEL["EE_RobeMagic_F_Any"] = "f40d35d543177674ba8a82adfb17c915";
                        m_AllEEL["EE_RobeMagic_M_Any"] = "057f84375f4eb864fb00f1ed6a4908e7";
                        m_AllEEL["EE_RobeMonkEvil_F_Any"] = "c97a6b4ff6795b34780f5037bb304a14";
                        m_AllEEL["EE_RobeMonkEvil_M_Any"] = "438b4eea5986a6948ae5dd3cd25d5a9b";
                        m_AllEEL["EE_RobeMonkHoly_F_Any"] = "39510897ff1a7df44aa379c305cd64d9";
                        m_AllEEL["EE_RobeMonkHoly_M_Any"] = "4e3ab99e33ce02749b886435ccf4d1bd";
                        m_AllEEL["EE_RobeMonkNeutral_F_Any"] = "0efb8b51cb3f04b4d8889e052aa6df6f";
                        m_AllEEL["EE_RobeMonkNeutral_M_Any"] = "c431d80bbd4804f40a067cee9197ed65";
                        m_AllEEL["EE_RobeNecromancer_F_Any"] = "7b4876b8951acc14a8f720b3deb71df6";
                        m_AllEEL["EE_RobeNecromancer_M_Any"] = "c62a258f474556841aec0235f1d69c4d";
                        m_AllEEL["EE_RobeShaman_F_Any"] = "72ea4f893093e1345b6444ae5ff17fdb";
                        m_AllEEL["EE_RobeShaman_M_Any"] = "8b3905060271f9c4393c8ee03acb9542";
                        m_AllEEL["EE_RobeSkald_M_Any"] = "74ecde82f310875448679c744b99868a";
                        m_AllEEL["EE_RobeWrigglingMan_F_Any"] = "61b082376f4384a4bb5241043b1b9f50";
                        m_AllEEL["EE_RobeWrigglingMan_M_Any"] = "beea836b3598a4f49993e69fabe40014";
                        m_AllEEL["EE_RogueAccessoriesGreybor_M_Any"] = "6a56a97f7dd6e194096b52e601ea2ee4";
                        m_AllEEL["EE_RogueAccessories_F_Any"] = "67d82fc7662a522449d5dc8ed622e33a";
                        m_AllEEL["EE_RogueAccessories_M_Any"] = "54de61e669f916543b96da841357d2ff";
                        m_AllEEL["EE_RogueClassHat_F_DW"] = "c22d5b6445a0bdf4dae7f2c8c00d7db3";
                        m_AllEEL["EE_RogueClassHat_F_EL"] = "a6d16aa615d39414a990383c9bce4aed";
                        m_AllEEL["EE_RogueClassHat_F_GN"] = "6e19d0b7300598e4db94a55ddd8d940a";
                        m_AllEEL["EE_RogueClassHat_F_HE"] = "5ba4f4b32016e7d449de91e53bc49c4f";
                        m_AllEEL["EE_RogueClassHat_F_HL"] = "c0d0dfceb4f20144297d0123fdbe798e";
                        m_AllEEL["EE_RogueClassHat_F_HM"] = "dfbea338cb4fdf24684ef47803c9a1bb";
                        m_AllEEL["EE_RogueClassHat_F_HO"] = "63c69237e598ae14fbef5f248b0be82e";
                        m_AllEEL["EE_RogueClassHat_F_OD"] = "c1d5483e1d0820344aa363a138d58935";
                        m_AllEEL["EE_RogueClassHat_F_TL"] = "167faf0fda239b4418afcbd74463d246";
                        m_AllEEL["EE_RogueClassHat_M_DW"] = "6b495d718d4f625429594905f7fe6068";
                        m_AllEEL["EE_RogueClassHat_M_EL"] = "f7a1ddc9417a3ed49a10b01e97b5058a";
                        m_AllEEL["EE_RogueClassHat_M_GN"] = "e7385040cd0a2e941bba7714eb18f943";
                        m_AllEEL["EE_RogueClassHat_M_HE"] = "373b55039f154e2409837ee0085d9238";
                        m_AllEEL["EE_RogueClassHat_M_HL"] = "5aca6248ddfcae640bc53c2fc43b3dd8";
                        m_AllEEL["EE_RogueClassHat_M_HM"] = "19f861054cc76294a8fe1e674230fb57";
                        m_AllEEL["EE_RogueClassHat_M_HO"] = "f860db891ddf3bc4db45fd90ae568156";
                        m_AllEEL["EE_RogueClassHat_M_OD"] = "86f0f1da1af574340a8f7a0d3a5a760c";
                        m_AllEEL["EE_RogueClassHat_M_TL"] = "e8791ab8d3bab234c9a15c9aef79ff68";
                        m_AllEEL["EE_RogueOutfitGreybor_M_Any"] = "74c2cb934b512874299475efd202f909";
                        m_AllEEL["EE_Rogue_F_Any"] = "dc822f0446c675a45809202953fa52a7";
                        m_AllEEL["EE_Rogue_M_Any"] = "1f538abc2802c5649b7ce177183f88c8";
                        m_AllEEL["EE_ScalemailArmy_F_Any"] = "d02253fca4c8cd64b80ae6a3776b2f58";
                        m_AllEEL["EE_ScalemailArmy_M_Any"] = "a4824cfaf43411147bd6a164905242e9";
                        m_AllEEL["EE_ScalemailEvil_F_Any"] = "81590aec66b7c9d42a9156d58829e034";
                        m_AllEEL["EE_ScalemailEvil_M_Any"] = "e9647ee8da549ca498e3a194054ca27f";
                        m_AllEEL["EE_ScalemailKnight_F_Any"] = "2c0d5c3821343204299b2efcc6d16f16";
                        m_AllEEL["EE_ScalemailKnight_M_Any"] = "8881162afb1c40c4d87c8be14ed8d298";
                        m_AllEEL["EE_Scar01_U_Any"] = "9bc938424cc0b4d4998d4568033137b5";
                        m_AllEEL["EE_Scar02_U_Any"] = "e33309f0f0678394499d3e3e265d4d12";
                        m_AllEEL["EE_Scar03_U_Any"] = "ab22afc4552e78b4d8288d00e4ed7580";
                        m_AllEEL["EE_Scar04_U_Any"] = "970b9b529e37f4e46b9370c764f0a985";
                        m_AllEEL["EE_Scar05_U_Any"] = "74b70d7f9f2f3a843992646ed95eb758";
                        m_AllEEL["EE_Scar06_U_Any"] = "1c716dc61bf63b24a820cc5788f5d303";
                        m_AllEEL["EE_Scar07_U_Any"] = "43fb465989df7024386d847bad0fbe6f";
                        m_AllEEL["EE_Scar08_U_Any"] = "81b7c498a309e9449be80bcfa534d4c4";
                        m_AllEEL["EE_Scar09_U_Any"] = "ea083b1830ac3ff4a9514ecb1f571d81";
                        m_AllEEL["EE_Scar10_U_Any"] = "6c23d2ee735877e40b625045f60d034a";
                        m_AllEEL["EE_Scar11_U_Any"] = "50618850d97f29d4c80de62e594b4052";
                        m_AllEEL["EE_Scar12_U_Any"] = "9ccd1af1f51ce014daaacdf4e48ed66d";
                        m_AllEEL["EE_Scars01Ember_U_EL"] = "a5ce3d53774ed6844a64f25820d82b3b";
                        m_AllEEL["EE_Shaman_F_Any"] = "37848e532247a1844aac0aa83a5d1b79";
                        m_AllEEL["EE_Shaman_M_Any"] = "31814c23fe547e14f80b3ed086d79457";
                        m_AllEEL["EE_ShirtBard_F_Any"] = "717e8c4fedecbda48821dcb7daf97068";
                        m_AllEEL["EE_ShirtBard_M_Any"] = "beb16504c359f1f4383359ed7fbf7742";
                        m_AllEEL["EE_ShirtClean_M_Any"] = "4f1a2e5caa6093f48a4fe04c4d919761";
                        m_AllEEL["EE_ShirtCommon_F_Any"] = "4c220c049298df440a62640c4a6fbd59";
                        m_AllEEL["EE_ShirtCommon_M_Any"] = "650488a28a8ce7d4ca8e8210d9c9e4a1";
                        m_AllEEL["EE_ShirtFighter_F_Any"] = "ce37e49e7f9aeee4f9d42b709218a49b";
                        m_AllEEL["EE_ShirtFighter_M_Any"] = "ad9995a0477d20343b74d39a6ee8dc19";
                        m_AllEEL["EE_ShirtInquisitor_F_Any"] = "be10b732267080e4db7e34e980d1faa8";
                        m_AllEEL["EE_ShirtInquisitor_M_Any"] = "e792691c3d34e3c4ab585cce187c5719";
                        m_AllEEL["EE_ShirtMagic_F_Any"] = "17ce37c5a76dbdc4784c56a2a8a25b79";
                        m_AllEEL["EE_ShirtMagic_M_Any"] = "19da86a779ad888439af94c814e145d9";
                        m_AllEEL["EE_ShirtNoble2_M_Any"] = "0cd0f524e4559084b99aa71e69df5d1a";
                        m_AllEEL["EE_ShirtNoble_F_Any"] = "257e91f47b80aa3419a1eba5c440ae38";
                        m_AllEEL["EE_ShirtPaladin_F_Any"] = "2f17f3a7efc16b54abfd39b868dddab0";
                        m_AllEEL["EE_ShirtPaladin_M_Any"] = "15c8009ca74c359468f060b2fb64ae30";
                        m_AllEEL["EE_ShirtPeasant1_F_Any"] = "3ae396c212dd3c949b4161adaaa83034";
                        m_AllEEL["EE_ShirtPeasant1_M_Any"] = "0f0330d28241f77419394399059025de";
                        m_AllEEL["EE_ShirtRogue_F_Any"] = "d689d60a29415ad47966b20b51667a29";
                        m_AllEEL["EE_ShirtRogue_M_Any"] = "a687a36ae96c02e4ea5ebadc6af7989b";
                        m_AllEEL["EE_ShirtWizard_F_Any"] = "26bb647cdc9a52c4a818e2c5a5c040c1";
                        m_AllEEL["EE_ShirtWizard_M_Any"] = "cf31570deffd0324783cf83499a2ba0f";
                        m_AllEEL["EE_SkaldAccessories_F_Any"] = "22f69f5cb34d3514fa1bf6ae5b5b596e";
                        m_AllEEL["EE_SkaldAccessories_M_Any"] = "454c81f0ec7c28347b9c727f54179d5d";
                        m_AllEEL["EE_Skald_F_Any"] = "b192a07d32ad16846acda60742aee47a";
                        m_AllEEL["EE_Skald_M_Any"] = "f5c26c2eb27b07c458cb385bd03ed22e";
                        m_AllEEL["EE_SkeletonDamage_F_SN"] = "6a6d128a03cba71429cde48912c53cb4";
                        m_AllEEL["EE_SkeletonDamage_M_SN"] = "ebd347f3ee476b844bf1ea803c409791";
                        m_AllEEL["EE_SkinMythicDemon1_F_Any"] = "8c1f1801126dc2444ab2ea2312f28b03";
                        m_AllEEL["EE_SkinMythicDemon1_F_KT"] = "c198f180c6b693343b9f1f031e9fe08f";
                        m_AllEEL["EE_SkinMythicDemon1_M_Any"] = "21f2c5004173a514982d8491a55734d4";
                        m_AllEEL["EE_SkinMythicDemon1_M_KT"] = "8cbe10793e2b03b47a3be0a0ce0263d0";
                        m_AllEEL["EE_SkinMythicDemon2Tomaz_M_Any"] = "80cb9a85c8098cd448cee0bf7d1f4de8";
                        m_AllEEL["EE_SkinMythicDemon2_F_Any"] = "a665e2687fbcc5246b85ef7ace24107b";
                        m_AllEEL["EE_SkinMythicDemon2_F_KT"] = "86e182b836710fe438b0094237533015";
                        m_AllEEL["EE_SkinMythicDemon2_M_Any"] = "02e52c3d561c3a943a62acf88a0e1086";
                        m_AllEEL["EE_SkinMythicDemon2_M_KT"] = "c4e728537d9ed6a44973cd354bc972db";
                        m_AllEEL["EE_SkinMythicDragon1_F_Any"] = "4d2b25bbc008c784986ecd485239dfd1";
                        m_AllEEL["EE_SkinMythicDragon1_M_Any"] = "af56a281a7e456448a98259b17602bba";
                        m_AllEEL["EE_SkinMythicDragon2_F_Any"] = "9283a1387d9b3ea498cbaec745ef4da9";
                        m_AllEEL["EE_SkinMythicDragon2_M_Any"] = "2220a3540505da943933df8add825e15";
                        m_AllEEL["EE_Skull_F_SN"] = "40259a0f9ca14df428b8f98221447e5a";
                        m_AllEEL["EE_Skull_M_SN"] = "f68b65529cd93cd44ad7dae136f28f07";
                        m_AllEEL["EE_SlayerAccessories_F_Any"] = "1a941a3b6431f4e4fa2a91b26e967d1c";
                        m_AllEEL["EE_SlayerAccessories_M_Any"] = "bafbd2902605f604bb97f9f73d343ce4";
                        m_AllEEL["EE_SlayerClassHat_F_DW"] = "efeb832274ce32044aa566e8b408d87a";
                        m_AllEEL["EE_SlayerClassHat_F_EL"] = "da02cc7fc42797249a1a4da140db1e2b";
                        m_AllEEL["EE_SlayerClassHat_F_GN"] = "c5a7b0ed3fa6acd4483a621464c56137";
                        m_AllEEL["EE_SlayerClassHat_F_HE"] = "3ce49ecea0467364e871457ab2ac68c6";
                        m_AllEEL["EE_SlayerClassHat_F_HL"] = "f7f6ea35d46c7b642af4116597267a42";
                        m_AllEEL["EE_SlayerClassHat_F_HM"] = "acf10efcb4884944caa78f860850b420";
                        m_AllEEL["EE_SlayerClassHat_F_HO"] = "cb528fa5629f6754f93d58d74b706f70";
                        m_AllEEL["EE_SlayerClassHat_F_OD"] = "44803c11e4562324b900ba0bcfdc89f5";
                        m_AllEEL["EE_SlayerClassHat_F_TL"] = "f5e12c083b1452641b7a6dc4ef98bcbe";
                        m_AllEEL["EE_SlayerClassHat_M_DW"] = "9d2f404e28bd71e4f8fc68460a591442";
                        m_AllEEL["EE_SlayerClassHat_M_EL"] = "c1b74a25f29b0f941ba724f3dacf826a";
                        m_AllEEL["EE_SlayerClassHat_M_GN"] = "039fe801b8f7b9c4e8cc9ff48235912d";
                        m_AllEEL["EE_SlayerClassHat_M_HE"] = "dd2b0de26c85e96429fb940f54ad3949";
                        m_AllEEL["EE_SlayerClassHat_M_HL"] = "180a43f57a448ab499adb11a346fd5e5";
                        m_AllEEL["EE_SlayerClassHat_M_HM"] = "72f58850f57eafb448ce92a672353cf8";
                        m_AllEEL["EE_SlayerClassHat_M_HO"] = "80521d4cfadee4d4db4d274a74a6d1e3";
                        m_AllEEL["EE_SlayerClassHat_M_OD"] = "bf67bc24306f2da4cbe4258789c15754";
                        m_AllEEL["EE_SlayerClassHat_M_TL"] = "4117e80ae8cb8794fbf95f39b9da5533";
                        m_AllEEL["EE_Slayer_F_Any"] = "1924a1f2be8323843ab93a9fab0c72f7";
                        m_AllEEL["EE_Slayer_M_Any"] = "657c8bc6887532a43bdf799aad6c970b";
                        m_AllEEL["EE_SorcererAccessories_F_Any"] = "b9a1009f8f1387c4f891e37a24e189ca";
                        m_AllEEL["EE_SorcererAccessories_M_Any"] = "195163e220b10fb43be4d86038cdb72f";
                        m_AllEEL["EE_Sorcerer_F_Any"] = "a212481fa5646dd428e4c7fd9f720c8d";
                        m_AllEEL["EE_Sorcerer_M_Any"] = "ae5e71563e46899428dd0205914391db";
                        m_AllEEL["EE_SpiderLegsWenduag_Any"] = "770e4c98dc77b1d44ada14e4d615c86b";
                        m_AllEEL["EE_StuddedBandits_F_Any"] = "8fa8a82050507644eb053b9c9326994e";
                        m_AllEEL["EE_StuddedBandits_M_Any"] = "13628dcdfb568554b9bbf4be8ae74646";
                        m_AllEEL["EE_StuddedBarbarian_F_Any"] = "f09d9f4dd46ee6244bec941157644d73";
                        m_AllEEL["EE_StuddedBarbarian_M_Any"] = "27cce61b3fa49434fa7f5e7feae01300";
                        m_AllEEL["EE_StuddedEvilUndead_F_Any"] = "1b5fe8348e7cb964bb5104019653c990";
                        m_AllEEL["EE_StuddedEvilUndead_M_Any"] = "b232f433f32600549bee8cfe4aa29ab4";
                        m_AllEEL["EE_StuddedHoly_F_Any"] = "be63f54b98a81fe4db93fdd697ab259f";
                        m_AllEEL["EE_StuddedHoly_M_Any"] = "4d528522c030ddc4a8280468f0aee459";
                        m_AllEEL["EE_StuddedRanger_F_Any"] = "eb02c2f51b091e64d9081634f463489a";
                        m_AllEEL["EE_StuddedRanger_M_Any"] = "c8f05db94dadf3748be7d73bb9928b54";
                        m_AllEEL["EE_TabardCultistBaphometLeader_F_Any"] = "3fd65fbddf6f48544a0732f99e141090";
                        m_AllEEL["EE_TabardCultistBaphometLeader_M_Any"] = "f50b354ff6fbaeb4a826d0e46e4f0051";
                        m_AllEEL["EE_TabardCultistBaphomet_F_Any"] = "395a121c6587c8541ba2bb7348f42444";
                        m_AllEEL["EE_TabardCultistBaphomet_M_Any"] = "be60988331912b443ae0ecbf92494080";
                        m_AllEEL["EE_TabardCultistDeskariLeader_F_Any"] = "89b4ea91f38e8e948b5103ec20933587";
                        m_AllEEL["EE_TabardCultistDeskariLeader_M_Any"] = "05a6e05bfe77c294c8dfb02b9ddfc11c";
                        m_AllEEL["EE_TabardCultistDeskari_F_Any"] = "a8bbe6c05d7ecdf4aba6c4c2b6b2b6a5";
                        m_AllEEL["EE_TabardCultistDeskari_M_Any"] = "a679eb0a4087c2f4c8ddcf017255b74a";
                        m_AllEEL["EE_TabardCultistDirtyBlackGlyph_F_Any"] = "fe29ab172c4d49f4ab0a573789302e33";
                        m_AllEEL["EE_TabardCultistDirtyGlyph_F_Any"] = "9219793df66326a4598219f3945ee805";
                        m_AllEEL["EE_TabardCultistDirtyGlyph_M_Any"] = "3769d2fc38c5196468c739a25486ff49";
                        m_AllEEL["EE_TabardCultistDirtyOutfitLayer_M_Any"] = "83f15f8524398084d92439a45c245ca9";
                        m_AllEEL["EE_TabardCultistDirty_F_Any"] = "26e1629d8696d4b4a83e1b44416121cd";
                        m_AllEEL["EE_TabardCultistDirty_M_Any"] = "5fdd3d9b9c887b940a62f34132cfbe1f";
                        m_AllEEL["EE_TabardCultistKabriri_F_Any"] = "723ada08ab2deaa40a9b6b1f3a481dc5";
                        m_AllEEL["EE_TabardCultistKabriri_M_Any"] = "0bc3b256c400adc4d84b4fad6dab27b1";
                        m_AllEEL["EE_TabardCultistNormal_F_Any"] = "ff4628215803f514dbb90621ec4c8450";
                        m_AllEEL["EE_TabardCultistNormal_M_Any"] = "e415a170973e50c438872909606d559f";
                        m_AllEEL["EE_TabardKnightCrusaderBlue1_F_Any"] = "6cd734d6f1c687c4ea2a4ecf1f873bde";
                        m_AllEEL["EE_TabardKnightCrusaderBlue1_M_Any"] = "a3b785d520dbee64eb9e799573b94821";
                        m_AllEEL["EE_TabardKnightCrusaderBlue2_F_Any"] = "e59e949cdc1374f47b4daa72c6c0dc0e";
                        m_AllEEL["EE_TabardKnightCrusaderBlue2_M_Any"] = "2d439bb51cc14bd4ebf3d72a94cb6178";
                        m_AllEEL["EE_TabardKnightEagleGuard_F_Any"] = "bb03f98a24341db4495ca54114ea6cf9";
                        m_AllEEL["EE_TabardKnightEagleGuard_M_Any"] = "22c349315f9e0ce48b49a539e3df91d5";
                        m_AllEEL["EE_TabardKnightGeneric1_F_Any"] = "f9900f7104bf96646a6388187f1aba39";
                        m_AllEEL["EE_TabardKnightGeneric1_M_Any"] = "6f9ed4849c68b4a4c900f637e46c29be";
                        m_AllEEL["EE_TabardKnightGeneric2_F_Any"] = "5edcfd3856cc4f74b8c596a751d8a2ff";
                        m_AllEEL["EE_TabardKnightGeneric2_M_Any"] = "19efabdbfd7bc7146b9690bf4665129b";
                        m_AllEEL["EE_TabardKnightGeneric3_F_Any"] = "4c190657620978d43bbab993591e4468";
                        m_AllEEL["EE_TabardKnightGeneric3_M_Any"] = "6985ad87bf06eab42bc46a92d395ee7c";
                        m_AllEEL["EE_TabardKnightGeneric4_F_Any"] = "eaee094faca12b54496d12e0e5261433";
                        m_AllEEL["EE_TabardKnightGeneric4_M_Any"] = "de651904ab014b746a2efe40cc3ff416";
                        m_AllEEL["EE_TabardKnightGeneric5_F_Any"] = "f49c4bc2fcefe1a40b4f80846b9ba561";
                        m_AllEEL["EE_TabardKnightGeneric5_M_Any"] = "64b329d32f1028549a06d0b4c4f4e0fe";
                        m_AllEEL["EE_TabardKnightGeneric6_F_Any"] = "7d902b9c04ce2cc4d9a81b051f267fc6";
                        m_AllEEL["EE_TabardKnightGeneric6_M_Any"] = "c0edb40f0836a854599bbfe92365f27b";
                        m_AllEEL["EE_TabardKnightIomedae_F_Any"] = "0c97e220e3394eb478f141c8ec7047c6";
                        m_AllEEL["EE_TabardKnightIomedae_M_Any"] = "33987fa5ecec22d43a85520ef02e48e0";
                        m_AllEEL["EE_TabardKnightKenabres_F_Any"] = "6c8ad391deaeb144fbe3d8697ffd2d8e";
                        m_AllEEL["EE_TabardKnightKenabres_M_Any"] = "5f5c739e8cd574a468c192a7a02e6826";
                        m_AllEEL["EE_TabardKnightMendev_F_Any"] = "39687a97c6075ec4584480d8358fc1fa";
                        m_AllEEL["EE_TabardKnightMendev_M_Any"] = "4cae0babb6256824caa59531846f2f24";
                        m_AllEEL["EE_TabardKnightNormal_F_Any"] = "382c9ecbcafb3b44aa2a2a4a2d28b25a";
                        m_AllEEL["EE_TabardKnightNormal_M_Any"] = "7ed79a002266b584ebce185e91d8f5ee";
                        m_AllEEL["EE_TabardNarrowNormalOutfitLayer_M_Any"] = "7d1fd35dec8e2424ca7e11e9ab708860";
                        m_AllEEL["EE_TabardNarrowNormal_F_Any"] = "f57789e8f51dd194fb2c442252e9322f";
                        m_AllEEL["EE_TabardNarrowNormal_M_Any"] = "8c651ca3013426e40b9d3385938b3aa0";
                        m_AllEEL["EE_TabardNarrowRecruit_F_Any"] = "9ace19a9816607246bca999adaa4774c";
                        m_AllEEL["EE_TabardNarrowRecruit_M_Any"] = "813eb9549acc44e47acb59537fcf552c";
                        m_AllEEL["EE_TabardPaladin_F_Any"] = "80674f3f679bdb845bc61f01c0eea60e";
                        m_AllEEL["EE_TabardPaladin_M_Any"] = "cc526f7229a5a834698a0681552a9ba1";
                        m_AllEEL["EE_TailMythicDemon1_Any"] = "1d93ba28b0c93fd40a577ddf187856b4";
                        m_AllEEL["EE_TailMythicDemon2_Any"] = "ab9ab7d63fb738249a1a1e9ec6b6c4ff";
                        m_AllEEL["EE_TailMythicDragon_Any"] = "4d431e5c5a1bdf7468ad6bcc0ee79872";
                        m_AllEEL["EE_TailWenduag_F_MM"] = "4c5631f45b647754d809690ca4b490a3";
                        m_AllEEL["EE_Tail_F_KT"] = "da5f2e4e562752d418a5de06e16183b6";
                        m_AllEEL["EE_Tail_M_KT"] = "fdd3c295239e4f5468b8685342bfe5dd";
                        m_AllEEL["EE_Tail_SU"] = "31fcdf5af347f994f8fbf51fcbad7b41";
                        m_AllEEL["EE_Tail_SU_Any"] = "e42abf4734e52ae4382410278043ae96";
                        m_AllEEL["EE_Tail_Skeleton_M_Any"] = "42c1b7d62bfc2b74c914569015c1e25a";
                        m_AllEEL["EE_Tail_TL"] = "d15dbf2912cd6f94492a9e1053aa0ebd";
                        m_AllEEL["EE_TraderAccessories_F_Any"] = "fac5248d9ffac914bb5b08c1e291a811";
                        m_AllEEL["EE_TraderAccessories_M_Any"] = "f42eccc4ace85e348a9e3ca5c7864f47";
                        m_AllEEL["EE_TraderHat_F_GN"] = "61d2c435b879d7748a5b2e7fc92e3ff3";
                        m_AllEEL["EE_TraderHat_F_HE"] = "c2d719fe2c244ae4fbbc9b8113184f7f";
                        m_AllEEL["EE_TraderHat_F_HL"] = "35ea28e1b46718a43b744ebd3125682f";
                        m_AllEEL["EE_TraderHat_F_HM"] = "0163ff265f0c62146a2c103cebd753ba";
                        m_AllEEL["EE_TraderHat_M_DW"] = "521f9a5143e6e1f4dad2aaad3b7d9f31";
                        m_AllEEL["EE_TraderHat_M_HE"] = "391229840caa9b1468f0cb7ed1eebb35";
                        m_AllEEL["EE_TraderHat_M_HL"] = "da4a67cf161abd84386023c6ecf5a588";
                        m_AllEEL["EE_TraderHat_M_HM"] = "48c3eebd38e6d554297958a58475ed50";
                        m_AllEEL["EE_TraderHat_M_KT"] = "a5c5daea7f17765478f0ed080f4d8528";
                        m_AllEEL["EE_TraderHat_M_TL"] = "0c49757127c2ed5418c32e963b9a61fc";
                        m_AllEEL["EE_Trader_F_Any"] = "bbc7f6ef5ecd27b4e8d53c504d080e76";
                        m_AllEEL["EE_Trader_M_Any"] = "d91a234c3b774f946a8355c1e98da9d5";
                        m_AllEEL["EE_UnderwearDancerDemonic_F_Any"] = "16610d2f44fd17e4a9eb0d4cfcd4cb27";
                        m_AllEEL["EE_UnderwearDancerDemonic_M_Any"] = "bc41a568d4b8ed344aa26a7251bb0aab";
                        m_AllEEL["EE_UnderwearDancer_F_Any"] = "12bcc4600d574fa48803c82efd186d06";
                        m_AllEEL["EE_UnderwearDancer_M_Any"] = "df8f7966bbaf786479939c82374e5e01";
                        m_AllEEL["EE_UnderwearMetal_F_Any"] = "c3a81e8120339764582453f3d02f8a83";
                        m_AllEEL["EE_UnderwearMetal_M_Any"] = "3505d39edd7517d46aa492571290c8b9";
                        m_AllEEL["EE_UnderwearNormal_F_Any"] = "14f8645017b27e441adc9a15b2b6e874";
                        m_AllEEL["EE_UnderwearNormal_M_Any"] = "a1a0d015c441a454eb3fd21d0da361e7";
                        m_AllEEL["EE_UnderwearRagged_F_Any"] = "4f894239a5970714d86466c768045b53";
                        m_AllEEL["EE_UnderwearRagged_M_Any"] = "1469ffdfc1ddc854fa068d6b48e85c0e";
                        m_AllEEL["EE_WarpriestAccessories_F_Any"] = "166ac63e67358ce42abb2ac3292f19c1";
                        m_AllEEL["EE_WarpriestAccessories_M_Any"] = "cc29bd5f9dc0a1641897f4e45abd5984";
                        m_AllEEL["EE_Warpriest_F_Any"] = "e49676c703785c749bea0aa63ba79cfd";
                        m_AllEEL["EE_Warpriest_M_Any"] = "da4d4f059cef00d419c6b1974bc78ffb";
                        m_AllEEL["EE_WenduagHood_F_MM"] = "67ba900c786b1524996b82a070dc39d5";
                        m_AllEEL["EE_WenduagOutfit_F_MM"] = "275f3a988c6122f43a005d2db76349e9";
                        m_AllEEL["EE_WiingsColoxus_M"] = "8d5653e689ee3d142ab413004338dacd";
                        m_AllEEL["EE_WingsAngelicMythic_F"] = "f3e3e309560d1f94a8f3c7b29016ef69";
                        m_AllEEL["EE_WingsAngelicMythic_M"] = "e810ab7d95644cf41979f83c06576eb4";
                        m_AllEEL["EE_WingsAngelic_Black_F"] = "da9f766f4de989f4e865a2d019b55098";
                        m_AllEEL["EE_WingsAngelic_Black_M"] = "876fbc0d239695a4790358a3be5d7c53";
                        m_AllEEL["EE_WingsAngelic_F"] = "419520afa5191ee4bb8d33fc75d2fd29";
                        m_AllEEL["EE_WingsAngelic_M"] = "632dbfaaf86513645a465598fa536892";
                        m_AllEEL["EE_WingsAzataMythic_F"] = "c6366318b30792944b31b5fccc1db3c8";
                        m_AllEEL["EE_WingsAzataMythic_M"] = "c6d020c1203745041b449706dcacee2d";
                        m_AllEEL["EE_WingsAzata_F"] = "225e273480af01348b7aaacf4d2f0c54";
                        m_AllEEL["EE_WingsAzata_M"] = "8c1a370599cf04343a490f71322bf167";
                        m_AllEEL["EE_WingsColoxus_F"] = "faf4e32b97ebb614fbb321e36d207c8a";
                        m_AllEEL["EE_WingsDemonic_F"] = "317c89eb4850e0a45a5eb3e4ae0124a9";
                        m_AllEEL["EE_WingsDemonic_M"] = "23b3eed8f78e69c40a2c6d416cac2f9e";
                        m_AllEEL["EE_WingsDiabolic_M"] = "f845b1c66b2f7804cb53d17654ad7424";
                        m_AllEEL["EE_WingsDraconic_M"] = "c466520b1c587c94dbefcc7e9fcf22ca";
                        m_AllEEL["EE_WingsHeraldCorrupted"] = "b4b185de320eca0429c60716f6501ec9";
                        m_AllEEL["EE_WingsMythicDevil_F"] = "214b59c14bada944c83041f18dcec3d2";
                        m_AllEEL["EE_WingsMythicDevil_M"] = "85e618f6c710a44498148cf99ca20c83";
                        m_AllEEL["EE_WingsMythicDragon_F"] = "2a8485b6546982b449cac01acb0c9c0d";
                        m_AllEEL["EE_WingsMythicDragon_M"] = "eafa30048103d204eb5178405fa2a369";
                        m_AllEEL["EE_Wings_F_SU"] = "96122c0bb0c483e46a53b8c9d05a1c39";
                        m_AllEEL["EE_Wings_M_SU"] = "89343a8dc170abb46ae15c11775e867a";
                        m_AllEEL["EE_WitchAccessories_F_Any"] = "92142bab58db84343b9d5b4707e4abbd";
                        m_AllEEL["EE_WitchAccessories_M_Any"] = "3b6c3a2d980edbf448f38c14b4ad36da";
                        m_AllEEL["EE_Witch_F_Any"] = "502d462d8f0ca3b4daea0509bea89e23";
                        m_AllEEL["EE_Witch_M_Any"] = "4fa188e944b822b458ddee3bba57902d";
                        m_AllEEL["EE_WizardAccessories_F_Any"] = "64abd9c4d6565de419f394f71a2d496f";
                        m_AllEEL["EE_WizardAccessories_M_Any"] = "04244d527b8a1f14db79374bc802aaaa";
                        m_AllEEL["EE_WizardClassHat_F_DW"] = "a1f13eb531fadaa49aba8709bd21ed94";
                        m_AllEEL["EE_WizardClassHat_F_EL"] = "f3fa46503138cc442a02976b7282befa";
                        m_AllEEL["EE_WizardClassHat_F_GN"] = "8434b027f09d66741931898961d56acb";
                        m_AllEEL["EE_WizardClassHat_F_HE"] = "813dc5999c0583648b080a5685d4f2bf";
                        m_AllEEL["EE_WizardClassHat_F_HL"] = "fac668bd54ee6164fa1da25f4892a9bd";
                        m_AllEEL["EE_WizardClassHat_F_HM"] = "d63b0acca4e67d54b866c8348226232b";
                        m_AllEEL["EE_WizardClassHat_F_HO"] = "751fbf66c6a379548ab37258f650fc70";
                        m_AllEEL["EE_WizardClassHat_F_OD"] = "035d482fdb2daef45a69852a17cc2cf9";
                        m_AllEEL["EE_WizardClassHat_F_TL"] = "52df3b696129f7e48abbecf407bf6f8d";
                        m_AllEEL["EE_WizardClassHat_M_DW"] = "8a378deb2a2c30642a5abc572d800852";
                        m_AllEEL["EE_WizardClassHat_M_EL"] = "5d4c9065eccbacc438626e7c2ee17e49";
                        m_AllEEL["EE_WizardClassHat_M_GN"] = "493c4e9110e0a46438f213ff3ac78220";
                        m_AllEEL["EE_WizardClassHat_M_HE"] = "d9a30808994d6144ca8e847680889d86";
                        m_AllEEL["EE_WizardClassHat_M_HL"] = "1175e260ac2c6f14996af9ddca252995";
                        m_AllEEL["EE_WizardClassHat_M_HM"] = "c14bacf8e85402240a919b78d71cf9c8";
                        m_AllEEL["EE_WizardClassHat_M_HO"] = "d9368e1b9e1759a41b71888d9571711b";
                        m_AllEEL["EE_WizardClassHat_M_OD"] = "722318bc705b49244860aee0a5c2da07";
                        m_AllEEL["EE_WizardClassHat_M_TL"] = "c439ab83284b7944593566c739fc878e";
                        m_AllEEL["EE_Wizard_F_Any"] = "1e4797f3425461946a0ae2986d16c85d";
                        m_AllEEL["EE_Wizard_M_Any"] = "5470be096a195b0408115392fc742b41";
                        m_AllEEL["EE_ZombieWoundArmLeft_U_ZB"] = "0eecac24055232e4ba72479c7592c9cb";
                        m_AllEEL["EE_ZombieWoundArmRight_U_ZB"] = "415d1c45e783cbd4db580763ee480705";
                        m_AllEEL["EE_ZombieWoundHandLeft_U_ZB"] = "70fa0521e7369ad4c81fd0a48f093b75";
                        m_AllEEL["EE_ZombieWoundHandRight_U_ZB"] = "88f93a41bd3572243b131198a9016c0c";
                        m_AllEEL["EE_ZombieWoundLegLeft_U_ZB"] = "55a497edb9988984caba0f91ecabf718";
                        m_AllEEL["EE_ZombieWoundLegRight_U_ZB"] = "928c3aba64799654cb90ad5b2fea8d0c";
                        m_AllEEL["EE_ZombieWoundTorsoBack_U_ZB"] = "c4803dabe92e5b24492ac5d24d079ff8";
                        m_AllEEL["EE_ZombieWoundTorsoBottom_U_ZB"] = "610f6a149143d9b4a9f861df100b2e15";
                        m_AllEEL["EE_ZombieWoundTorsoTop_U_ZB"] = "d16662aaeb75ecb44ae6baee2b16b922";
                        //m_AllEEL.OrderBy(a => a.Key);
                    }
                    return m_AllEEL;
                }
                catch (Exception e)
                {
                    Main.logger.Error(e.ToString());
                    return new Dictionary<string,string>();
                }
            }
        }

        public static Dictionary<BlueprintRef, string> Helm
        {
            get
            {
                if (!loaded) Init();
                return m_Helm;
            }
        }

        public static Dictionary<BlueprintRef, string> Cloak
        {
            get
            {
                if (!loaded) Init();
                return m_Cloak;
            }
        }

        public static Dictionary<BlueprintRef, string> Glasses
        {
            get
            {
                if (!loaded) Init();
                return m_Glasses;
            }
        }

        public static Dictionary<BlueprintRef, string> Shirt
        {
            get
            {
                if (!loaded) Init();
                return m_Shirt;
            }
        }

        public static Dictionary<BlueprintRef, string> Armor
        {
            get
            {
                if (!loaded) Init();
                return m_Armor;
            }
        }

        public static Dictionary<BlueprintRef, string> Bracers
        {
            get
            {
                if (!loaded) Init();
                return m_Bracers;
            }
        }

        public static Dictionary<BlueprintRef, string> Gloves
        {
            get
            {
                if (!loaded) Init();
                return m_Gloves;
            }
        }

        public static Dictionary<BlueprintRef, string> Boots
        {
            get
            {
                if (!loaded) Init();
                return m_Boots;
            }
        }

        public static Dictionary<ResourceRef, string> Units
        {
            get
            {
                if (!loaded) Init();
                return m_Units; ;
            }
        }

        public static Dictionary<string, Dictionary<BlueprintRef, string>> Weapons
        {
            get
            {
                if (!loaded) Init();
                return m_Weapons;
            }
        }

        public static Dictionary<BlueprintRef, string> MythicOptions
        {
            get
            {
                if (!loaded) Init();
                return m_MythicOptions;
            }
        }

        public static Dictionary<BlueprintRef, string> WeaponEnchantments
        {
            get
            {
                if (!loaded) Init();
                return m_WeaponEnchantments;
            }
        }

        public static Dictionary<ResourceRef, string> Tattoos
        {
            get
            {
                if (m_Tattoo.Count == 0)
                {
                    m_Tattoo["326c1affb2a6a26489921bf588f717b6"] = "EE_KineticistTattooWind_U";
                    m_Tattoo["23b9e367a73b5534d918675405de5aa0"] = "EE_KineticistTattooEarth_U";
                    m_Tattoo["c4aee0b105e3e7e45994f4d8619a5974"] = "EE_KineticistTattooFire_U";
                    m_Tattoo["5dcf740907a3ec94bb4deeac33f0c2b3"] = "EE_KineticistTattooWater_U";
                }
                return m_Tattoo;
            }
        }

        public static Dictionary<string, string> WingsEE
        {
            get
            {
                if (m_WingsEE.Count == 0)
                {
                    m_WingsEE["EE_Wings_M_SU"] = "89343a8dc170abb46ae15c11775e867a";
                    m_WingsEE["EE_Wings_F_SU"] = "96122c0bb0c483e46a53b8c9d05a1c39";
                    m_WingsEE["EE_WingsAngelic_M"] = "632dbfaaf86513645a465598fa536892";
                    m_WingsEE["EE_WingsAngelic_F"] = "419520afa5191ee4bb8d33fc75d2fd29";
                    m_WingsEE["EE_WingsAzata_F"] = "225e273480af01348b7aaacf4d2f0c54";
                    m_WingsEE["EE_WingsDemonic_M"] = "23b3eed8f78e69c40a2c6d416cac2f9e";
                    m_WingsEE["EE_WingsAngelic_Black_F"] = "da9f766f4de989f4e865a2d019b55098";
                    m_WingsEE["EE_WingsColoxus_F"] = "faf4e32b97ebb614fbb321e36d207c8a";
                    m_WingsEE["EE_WingsDemonic_F"] = "317c89eb4850e0a45a5eb3e4ae0124a9";
                    //m_WingsEE["EE_WingsAngelicMythic_M"] ="e810ab7d95644cf41979f83c06576eb4";
                    m_WingsEE["EE_WingsAngelicMythic_F"] = "f3e3e309560d1f94a8f3c7b29016ef69";
                    m_WingsEE["EE_WingsAzataMythic_M"] = "c6d020c1203745041b449706dcacee2d";
                    m_WingsEE["EE_WingsAzataMythic_F"] = "c6366318b30792944b31b5fccc1db3c8";
                    m_WingsEE["EE_WingsMythicDevil_M"] = "85e618f6c710a44498148cf99ca20c83";
                    m_WingsEE["EE_WingsMythicDevil_F"] = "214b59c14bada944c83041f18dcec3d2";
                    m_WingsEE["EE_WingsMythicDragon_M"] = "eafa30048103d204eb5178405fa2a369";
                    m_WingsEE["EE_WingsMythicDragon_F"] = "2a8485b6546982b449cac01acb0c9c0d";
                    m_WingsEE["EE_WingsAngelic_Black_M"] = "876fbc0d239695a4790358a3be5d7c53";
                    m_WingsEE["EE_WingsDiabolic_M"] = "f845b1c66b2f7804cb53d17654ad7424";
                    m_WingsEE["EE_WingsAzata_M"] = "8c1a370599cf04343a490f71322bf167";
                    m_WingsEE["EE_WingsHeraldCorrupted"] = "b4b185de320eca0429c60716f6501ec9";
                    m_WingsEE["EE_WingsDraconic_M"] = "c466520b1c587c94dbefcc7e9fcf22ca";
                    m_WingsEE["EE_FireWings_F"] = "8604b675977361043978ecf56122f389";
                    //BuildEELookup();
                }
                return m_WingsEE;
            }
        }

        public static Dictionary<string, string> HornsEE
        {
            get
            {
                if (m_HornsEE.Count == 0)
                {
                    m_HornsEE["EE_Horns01_M_SU"] = "f44f0bcefb40afd4b86e241599b83660";
                    m_HornsEE["EE_Horns02_M_TL"] = "cecad00f0d5d83f4ba37aa45c11c1bbe";
                    m_HornsEE["EE_Horns01_F_TL"] = "665d8734bad3ce648be0786de09f5f4c";
                    m_HornsEE["EE_Horns05_F_TL"] = "dea42645c2027524ca40d93e4265e247";
                    m_HornsEE["EE_Horns01_M_TL"] = "a68e700e882e1b8438df4ba92d247daf";
                    m_HornsEE["EE_Horns04Woljif_M_TL"] = "c2186f297c1bc3e4c86283495e3f1ee6";
                    m_HornsEE["EE_Horns03_M_TL"] = "d1dde611ca8670845978721380de3bdc";
                    m_HornsEE["EE_Horns05_M_TL"] = "16265666ad82f9d4e8a3ae1a563179b9";
                    m_HornsEE["EE_MongrelHorns01_F_EL"] = "10cc8cd2baa38384291f7640cd7227bb";
                    m_HornsEE["EE_Horns03_F_TL"] = "0d78bd95d563f3441bf8349bebfb48bf";
                    m_HornsEE["EE_HornsSkeleton_M_HM"] = "fd29ef04a3577d64aaad460f6bb9d6af";
                    m_HornsEE["EE_HornsSkeleton_F_HM"] = "61b1f8b3a96c93541a322d6c609ade56";
                    m_HornsEE["EE_HornsSkeleton_M_EL"] = "dfd0ce83f630d3242a102aede2b3f36a";
                    m_HornsEE["EE_HornsSkeleton_F_EL"] = "0e75fda397fa06a4481667e7281e4ea7";
                    m_HornsEE["EE_HornsSkeleton_M_DW"] = "5d83ffbbda65cf645bcf4ed6aa871ca2";
                    m_HornsEE["EE_HornsSkeleton_F_DW"] = "12c7ff3a92b8713428eaceb01ac9b48c";
                    m_HornsEE["EE_HornsSkeleton_M_HL"] = "50c1a9b7e226a7340a357b55ea74a995";
                    m_HornsEE["EE_HornsSkeleton_F_HL"] = "f599850541e79dd41b1ff42a3894e4c6";
                    m_HornsEE["EE_HornsSkeleton_M_GN"] = "2b472c8dcaa33164d832fa9242c70670";
                    m_HornsEE["EE_HornsSkeleton_F_GN"] = "760a1b3c204652d4d921c4e3ffce4d4c";
                    m_HornsEE["EE_HornsSkeleton_M_TL"] = "2ce2247d7efd20c41a1493211e2a2e42";
                    m_HornsEE["EE_HornsSkeleton_F_TL"] = "cea99d99eeabafb4f9e77bb84dcfcf3f";
                    m_HornsEE["EE_MongrelHorns01_M_HM"] = "7ae57d4a307b8cc47acd57cf56c99b5e";
                    m_HornsEE["EE_MongrelHorns01_F_HM"] = "784bb887aceb5a045996974a71e78875";
                    m_HornsEE["EE_MongrelHorns01_M_EL"] = "8439da993972eca44b8c7f555e33f780";
                    m_HornsEE["EE_MongrelHorns01_M_DW"] = "11e8c436c8c969844a3defbfca96efdc";
                    m_HornsEE["EE_MongrelHorns01_F_DW"] = "5f9f5f3315f286a4d97918347b28eea1";
                    m_HornsEE["EE_MongrelHorns01_M_HL"] = "9a17220e985b8404487082cb82980875";
                    m_HornsEE["EE_MongrelHorns01_F_HL"] = "59ed3e2b1cc341046a68b55f0da80305";
                    m_HornsEE["EE_MongrelHorns01_M_GN"] = "408401bf088b0ad4a9a9747be66bb67d";
                    m_HornsEE["EE_MongrelHorns01_F_GN"] = "394a429941adb66489229d721c7ad61a";
                    m_HornsEE["EE_MongrelHorns01_M_TL"] = "fcbe9a67ef4b4f14aa3aec7501eb0993";
                    m_HornsEE["EE_MongrelHorns01_F_TL"] = "378fd4b65e9799e40adbe18b702a0dbf";
                    m_HornsEE["EE_MongrelHorns02_M_HM"] = "fed897cada6bd7e4196260cd6543e31b";
                    m_HornsEE["EE_MongrelHorns02_F_HM"] = "ce26bc3e359a0b84c9095b2a012bc877";
                    m_HornsEE["EE_MongrelHorns02_M_EL"] = "01fe52a14df1d8f4dad1bd2b4d064d9b";
                    m_HornsEE["EE_MongrelHorns02_F_EL"] = "fdf97bfe79e511943b5f4e39e87e330d";
                    m_HornsEE["EE_MongrelHorns02_M_DW"] = "d7f72406fc0ff5c4ab463fb3932a8e41";
                    m_HornsEE["EE_MongrelHorns02_F_DW"] = "170f88fc0bd84d84d9b0aface2307604";
                    m_HornsEE["EE_MongrelHorns02_M_HL"] = "323ddc3b2ba0d1c49995cf755e6a0b0a";
                    m_HornsEE["EE_MongrelHorns02_F_HL"] = "a45c52286c60a954bbe81e9ac0740795";
                    m_HornsEE["EE_MongrelHorns02_M_GN"] = "c3f01fb66b55e354392321770961088c";
                    m_HornsEE["EE_MongrelHorns02_F_GN"] = "da4bbee5eeadb90439229a6703ccfbdf";
                    m_HornsEE["EE_MongrelHorns02_M_TL"] = "2e661c135188d594d81aeda28028bf3c";
                    m_HornsEE["EE_MongrelHorns02_F_TL"] = "6131624f5969745449ab39e371d96e60";
                    m_HornsEE["EE_MongrelHorns03_M_HM"] = "e900a1a0ecd6d754ea05b63fb38334bb";
                    m_HornsEE["EE_MongrelHorns03_F_HM"] = "10d6c6e9b22b3a64bb8fdca64b77ed2a";
                    m_HornsEE["EE_MongrelHorns03_M_EL"] = "7d49bbefc2d0ad94e9adf219091aac6b";
                    m_HornsEE["EE_MongrelHorns03_F_EL"] = "3a80435506254fa4eb692d3e02b8bd8f";
                    m_HornsEE["EE_MongrelHorns03_M_DW"] = "d0eb291feec381a43a46c49d8170ee94";
                    m_HornsEE["EE_MongrelHorns03_F_DW"] = "cc71eae536cd1a147b8e4d5a7430dc65";
                    m_HornsEE["EE_MongrelHorns03_M_HL"] = "dbdd1794de34186498684a56bf0b270d";
                    m_HornsEE["EE_MongrelHorns03_F_HL"] = "ab525b1dbe2928540bd8be8d408dd45d";
                    m_HornsEE["EE_MongrelHorns03_M_GN"] = "458655d1da1bbc74e8d5b50a589914b8";
                    m_HornsEE["EE_MongrelHorns03_F_GN"] = "2d79c418febe6c74d991625b8d4a153c";
                    m_HornsEE["EE_MongrelHorns03_M_TL"] = "5b6da001117e08c47a4e565f2ff46895";
                    m_HornsEE["EE_MongrelHorns03_F_TL"] = "bef7b1ee620abae41af0d03f81be5fc5";
                    m_HornsEE["EE_MongrelHorns04_M_HM"] = "3c1269cc50173ec4bbcaca741205ccd5";
                    m_HornsEE["EE_MongrelHorns04_F_HM"] = "67a781ca294d40441b5ae7a81330ca83";
                    m_HornsEE["EE_MongrelHorns04_M_EL"] = "d5fba06ff684d294cadeb32d82214672";
                    m_HornsEE["EE_MongrelHorns04_F_EL"] = "c1002d33ade3c024b8e7f12667e83be6";
                    m_HornsEE["EE_MongrelHorns04_M_DW"] = "705d79a24744b654c8db4641ddc5d69d";
                    m_HornsEE["EE_MongrelHorns04_F_DW"] = "32f51be24f611bf48993dd1a771a9f46";
                    m_HornsEE["EE_MongrelHorns04_M_HL"] = "e2a56613816943840bc32865116c36ea";
                    m_HornsEE["EE_MongrelHorns04_F_HL"] = "e4c14ae69bdc69a48aaafcee3905b738";
                    m_HornsEE["EE_MongrelHorns04_M_GN"] = "6b34aa9d47ca6d0499bc8b8ccb75146e";
                    m_HornsEE["EE_MongrelHorns04_F_GN"] = "a98a6a30183a73947812638378888591";
                    m_HornsEE["EE_MongrelHorns04_M_TL"] = "2d0d40fe4ad859d4dbc77376ca0e868b";
                    m_HornsEE["EE_MongrelHorns04_F_TL"] = "11f0c68e14be24847b8d78fb13c9733b";
                    m_HornsEE["EE_HornsSkeleton_M_SN"] = "b88202050d9e56140b452d10ee952319";
                    m_HornsEE["EE_Horns01_F_SU"] = "020cb621cebd72e40abc3f857ab377b8";
                    m_HornsEE["EE_Horns02_F_TL"] = "ccd35c8508752f04582e7d3a55248afe";
                    m_HornsEE["EE_Horns04_F_TL"] = "262ee4d8ababfce4ba1cd8a1bf60c999";
                    m_HornsEE["EE_Horns02Arueshalae_F_SU"] = "7be5b63938c84504ea8307fc460eaadc";
                    m_HornsEE["EE_HornsDevilExecutioner_F_HM"] = "9932a8b7a0b57a8429dc5a53657d7b06";
                    m_HornsEE["EE_Horns02_M_CM"] = "d4f6c34e0812b0840aeb365a04032be9";
                    m_HornsEE["EE_Horns01_M_CM"] = "89d5c728d197b1344bf942b4ef55f0a1";
                    m_HornsEE["EE_Horns01_F_CM"] = "ab044ce99b5322a4980b1e05ec27ee4c";
                    m_HornsEE["EE_HornsDevilExecutioner_M_HM"] = "12902d1b55f8a04489a78763e7b3cf7e";
                    m_HornsEE["EE_MongrelHorns04_M_HO"] = "3152c7330d1798d48897d6858955b802";
                    m_HornsEE["EE_MongrelHorns02_M_OD"] = "7eb1d2d08ee08424f8699b0732857951";
                    m_HornsEE["EE_MongrelHorns03_M_HE"] = "c2a30d01daaed7a408bd45e04528e578";
                }
                return m_HornsEE;
            }
        }

        public static Dictionary<string, string> TailsEE
        {
            get
            {
                if (m_TailEE.Count == 0)
                {
                    m_TailEE["EE_Tail_SU"] = "31fcdf5af347f994f8fbf51fcbad7b41";
                    m_TailEE["EE_Tail_TL"] = "d15dbf2912cd6f94492a9e1053aa0ebd";
                    m_TailEE["EE_Tail_F_KT"] = "da5f2e4e562752d418a5de06e16183b6";
                    m_TailEE["EE_Tail_M_KT"] = "fdd3c295239e4f5468b8685342bfe5dd";
                    m_TailEE["EE_Tail_SU_Any"] = "e42abf4734e52ae4382410278043ae96";
                    m_TailEE["EE_Tail_Skeleton_M_Any"] = "42c1b7d62bfc2b74c914569015c1e25a";
                    m_TailEE["EE_TailWenduag_F_MM"] = "4c5631f45b647754d809690ca4b490a3";
                }
                return m_TailEE;
            }
        }

        public static Dictionary<string, string> WingsFX
        {
            get
            {
                if (m_WingsFX.Count == 0)
                {
                    m_WingsFX["FireWings00"] = "43c4887b112892e4aba3f6c3ba0069a4";
                    /// m_WingsFX["AstralDeva_TwoWings"] = "09e250ff222d6894495d9389adaefb63";
                    m_WingsFX["ST_WingsAngelic_Angelic_U_Any"] = "1091271ddadd4a34f8d012428252dd4d";
                    m_WingsFX["ST_WingsAngelic_AngelicBlack_U_Any"] = "524e584f2cd2ff54396f65da40cb8cff";
                    m_WingsFX["ST_WingsDiabolic_Diabolic_U_Any"] = "7662eda306be77a4ab8f57dbf1235cc9";
                    m_WingsFX["ST_WingsMovanicDeva_MovanicDeva_U_Any"] = "d498a870119d1f84299597b1ba20a10e";
                    m_WingsFX["ST_WingsAngelic_Angelic_U_Any"] = "582ffb3c97385fc4d808d829156b0a77";
                    m_WingsFX["GhostWing00"] = "cb64dcc33e635a74e881f7ca6a836a2e";
                    m_WingsFX["ST_WingsDiabolic_Diabolic_U_Any"] = "713e38d506a2b79499e3c42ae66a7459";
                    m_WingsFX["ST_WingsDraconic_DraconicBlack_U_Any"] = "fc4edf7b354b4d34bbac755597ff9ab5";
                    m_WingsFX["ST_WingsDraconic_DraconicBlue_U_Any"] = "fbc4d4318e13da644be5d5ccd248c704";
                    m_WingsFX["ST_WingsDraconic_DraconicBrass_U_Any"] = "19de7a1cd8930af4bbb085c562f3f672";
                    m_WingsFX["ST_WingsDraconic_DraconicBronze_U_Any"] = "8208e5cdfcfa7274c94dbbb33d02a935";
                    m_WingsFX["ST_WingsDraconic_DraconicCopper_U_Any"] = "8c108b863cf485643a893db4a59c05a9";
                    m_WingsFX["ST_WingsDraconic_DraconicGold_U_Any"] = "f0c4a4ea80966d141aa9317e92d4baf1";
                    m_WingsFX["ST_WingsDraconic_DraconicGreen_U_Any"] = "186fc0171b2d9914aa4889d0a58e97e4";
                    m_WingsFX["ST_WingsDraconic_DraconicRed_U_Any"] = "68fea147aeb839642b7bb60b60e84eb8";
                    m_WingsFX["ST_WingsDraconic_DraconicSilver_U_Any"] = "395552f93c1ef5d42817af34b00ebb44";
                    m_WingsFX["ST_WingsDraconic_DraconicWhite_U_Any"] = "b695621f7fb1e7345a45f6224a977d6c";
                }
                return m_WingsFX;
            }
        }

        public static Dictionary<string, string> m_AllEEL = new Dictionary<string, string>();
        private static Dictionary<BlueprintRef, string> m_Cloak = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Helm = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Shirt = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Glasses = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Armor = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Bracers = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Gloves = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Boots = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Warpaint = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Scars = new Dictionary<BlueprintRef, string>();
        private static Dictionary<ResourceRef, string> m_Tattoo = new Dictionary<ResourceRef, string>();
        private static Dictionary<ResourceRef, string> m_Other = new Dictionary<ResourceRef, string>();
        private static Dictionary<ResourceRef, string> m_Units = new Dictionary<ResourceRef, string>();
        private static Dictionary<BlueprintRef, string> m_WeaponEnchantments = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_MythicOptions = new Dictionary<BlueprintRef, string>();
        private static Dictionary<string, string> m_WingsEE = new Dictionary<string, string>();
        private static Dictionary<string, string> m_HornsEE = new Dictionary<string, string>();
        private static Dictionary<string, string> m_TailEE = new Dictionary<string, string>();
        private static Dictionary<string, string> m_WingsFX = new Dictionary<string, string>();
        private static Dictionary<string, Dictionary<BlueprintRef, string>> m_Weapons = new Dictionary<string, Dictionary<BlueprintRef, string>>();
        private static bool loaded = false;

        private static void BuildEquipmentLookup()
        {
            var blueprints = Main.blueprints.Entries.Where(a => a.Type == typeof(KingmakerEquipmentEntity)).OrderBy((bp) => bp.Name);
            /// var blueprints = BluePrintThing.GetBlueprints<BlueprintItemEquipment>()
            foreach (var bp in blueprints)
            {
                if (bp.Name.Contains("Goggles") || bp.Name.Contains("Mask"))
                {
                    if (!m_Glasses.ContainsKey(bp.Guid))
                        m_Glasses[bp.Guid] = bp.Name;
                }
                else if (bp.Name.Contains("Helmet") || bp.Name.Contains("Headband") || bp.Name.Contains("Mask"))
                {
                    if (!m_Helm.ContainsKey(bp.Guid))
                        m_Helm[bp.Guid] = bp.Name;
                }
                else if (bp.Name.Contains("Shirt") || bp.Name.Contains("Robe") || bp.Name.Contains("Tabard"))
                {
                    if (!m_Shirt.ContainsKey(bp.Guid))
                        m_Shirt[bp.Guid] = bp.Name;
                }
                else if (bp.Name.Contains("Armor"))
                {
                    if (!m_Armor.ContainsKey(bp.Guid))
                        m_Armor[bp.Guid] = bp.Name;
                    //if (!m_Glasses.ContainsKey(bp.Guid))
                    //  m_Glasses[bp.Guid] = bp.Name;
                }
                else if (bp.Name.Contains("Bracers"))
                {
                    if (!m_Bracers.ContainsKey(bp.Guid))
                        m_Bracers[bp.Guid] = bp.Name;
                }
                else if (bp.Name.Contains("Gloves"))
                {
                    if (!m_Gloves.ContainsKey(bp.Guid))
                        m_Gloves[bp.Guid] = bp.Name;
                }
                else if (bp.Name.Contains("Boots"))
                {
                    if (!m_Boots.ContainsKey(bp.Guid))
                        m_Boots[bp.Guid] = bp.Name;
                }
                else if (bp.Name.Contains("Cape"))
                {
                    if (!m_Cloak.ContainsKey(bp.Guid))
                        m_Cloak[bp.Guid] = bp.Name;
                }
                else if (bp.Name.Contains("Warpaint"))
                {
                    if (!m_Warpaint.ContainsKey(bp.Guid))
                        m_Warpaint[bp.Guid] = bp.Name;
                }
                else if (bp.Name.Contains("Scar"))
                {
                    if (!m_Scars.ContainsKey(bp.Guid))
                        m_Scars[bp.Guid] = bp.Name;
                }
            }
            // ResourcesLibrary.CleanupLoadedCache();
        }

        /* static void BuildEquipmentLookupOld()
          {
          var bp2 = Main.blueprints.OfType<KingmakerEquipmentEntity>().OrderBy(bp => bp.name);
         /// var blueprints = BluePrintThing.GetBlueprints<BlueprintItemEquipment>()
         var blueprints = Main.blueprints.OfType<BlueprintItemEquipment>()
              .Where(bp => bp.EquipmentEntity != null)
              .OrderBy(bp => bp.EquipmentEntity.name);
          foreach (var bp in blueprints)
          {
              switch (bp.ItemType)
              {
                  case ItemType.Glasses:
                      if (m_Helm.ContainsKey(bp.EquipmentEntity.AssetGuidThreadSafe)) break;
                      m_Glasses[bp.EquipmentEntity.AssetGuidThreadSafe] = bp.EquipmentEntity.name;
                      break;

                  case ItemType.Head:
                      if (m_Helm.ContainsKey(bp.EquipmentEntity.AssetGuidThreadSafe)) break;
                      m_Helm[bp.EquipmentEntity.AssetGuidThreadSafe] = bp.EquipmentEntity.name;
                      break;

                  case ItemType.Shirt:
                      if (m_Shirt.ContainsKey(bp.EquipmentEntity.AssetGuidThreadSafe)) break;
                      m_Shirt[bp.EquipmentEntity.AssetGuidThreadSafe] = bp.EquipmentEntity.name;
                      break;

                  case ItemType.Armor:
                      if (m_Armor.ContainsKey(bp.EquipmentEntity.AssetGuidThreadSafe)) break;
                      m_Armor[bp.EquipmentEntity.AssetGuidThreadSafe] = bp.EquipmentEntity.name;
                      break;

                  case ItemType.Wrist:
                      if (m_Bracers.ContainsKey(bp.EquipmentEntity.AssetGuidThreadSafe)) break;
                      m_Bracers[bp.EquipmentEntity.AssetGuidThreadSafe] = bp.EquipmentEntity.name;
                      break;

                  case ItemType.Gloves:
                      if (m_Gloves.ContainsKey(bp.EquipmentEntity.AssetGuidThreadSafe)) break;
                      m_Gloves[bp.EquipmentEntity.AssetGuidThreadSafe] = bp.EquipmentEntity.name;
                      break;

                  case ItemType.Feet:
                      if (m_Boots.ContainsKey(bp.EquipmentEntity.AssetGuidThreadSafe)) break;
                      m_Boots[bp.EquipmentEntity.AssetGuidThreadSafe] = bp.EquipmentEntity.name;
                      break;

                  default:
                      break;
              }
          }
        }*/

        public static void BuildEELookup()
        {
            foreach (var kv in LibraryThing.GetResourceGuidMap())
            {

                var obj = ResourcesLibrary.TryGetResource<EquipmentEntity>(kv.Key);
                //var go = obj as GameObject;
                if (obj != null) //&& obj.name.Contains("Wing"))
                {
                    //Main.logger.Log("-----[\"" + obj.name + "\"] = ResourcesLibrary.TryGetResource<GameObject>(\"" + kv.Key + "\");");
                    // m_WingsEE[go.name] = go;
                }
                //ResourcesLibrary.CleanupLoadedCache();
            }
            ResourcesLibrary.CleanupLoadedCache();
            /* foreach (var EE in ResourcesLibrary.GetLoadedResourcesOfType<EquipmentEntity>())
        {
        Main.logger.Log("||");
        Main.logger.Log(EE.ToString());
        //string outfitparts = "";
        /*if (VARIABLE.OutfitParts.Count > 0)
        {
          foreach (var VARIA in VARIABLE.OutfitParts)
          {
              outfitparts = outfitparts + VARIA.ToString();
          }
        }*//*
        //Main.logger.Log("Outfitparts");
        if (EE.NameSafe().Contains("Wing") || EE.ToString().Contains("Wing"))
        {
            m_WingsEE[EE.NameSafe()] = EE;
        }
        }*/
        }

        private static void BuildMythicLookup()
        {
            ///var weapons = BluePrintThing.GetBlueprints<BlueprintItemEquipmentHand>().OrderBy((bp) => bp.name);
            var weapons = Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintClassAdditionalVisualSettings)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintClassAdditionalVisualSettings>(b.Guid)).OrderBy((bp) => bp.name);
            foreach (var bp in weapons)
            {
                // Main.logger.Log(bp.name);
                if (!m_MythicOptions.ContainsKey(bp.AssetGuidThreadSafe)) m_MythicOptions[bp.AssetGuidThreadSafe] = bp.name;
            }
        }

        private static void BuildWeaponLookup()
        {
            ///var weapons = BluePrintThing.GetBlueprints<BlueprintItemEquipmentHand>().OrderBy((bp) => bp.name);
            var weapons = (IEnumerable<BlueprintItemEquipmentHand>)Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintItemWeapon)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintItemWeapon>(b.Guid)).OrderBy((bp) => bp.name);
            weapons = Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintItemShield)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintItemShield>(b.Guid)).OrderBy((bp) => bp.name).Concat(weapons);
            foreach (var bp in weapons)
            {
                var visualParameters = bp.VisualParameters;
                var animationStyle = visualParameters.AnimStyle.ToString();
                if (bp.VisualParameters.Model == null) continue;
                Dictionary<BlueprintRef, string> eeList = null;
                if (!m_Weapons.ContainsKey(animationStyle))
                {
                    eeList = new Dictionary<BlueprintRef, string>();
                    m_Weapons[animationStyle] = eeList;
                }
                else
                {
                    eeList = m_Weapons[animationStyle];
                }
                if (eeList.ContainsKey(bp.AssetGuidThreadSafe))
                {
                    continue;
                }
                eeList[bp.AssetGuidThreadSafe] = bp.name;
            }
        }

        private static void BuildWeaponEnchantmentLookup()
        {
            try
            {
                ///var enchantments = BluePrintThing.GetBlueprints<BlueprintWeaponEnchantment>()
                ///
                var enchantments = Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintWeaponEnchantment)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintWeaponEnchantment>(b.Guid))
                        .Where(bp => bp.WeaponFxPrefab != null)
                        .OrderBy(bp => bp.WeaponFxPrefab.AssetId);
                HashSet<string> seen = new HashSet<string>();
                foreach (var enchantment in enchantments)
                {
                    //Main.logger.Log(enchantment.ToString() + " " + enchantment.WeaponFxPrefab.AssetId);
                    if (seen.Contains(enchantment?.WeaponFxPrefab?.AssetId)) continue;
                    seen.Add(enchantment.WeaponFxPrefab.AssetId);
                    var name = enchantment.ToString().Replace("00_WeaponBuff", "");
                    name = name.TrimEnd('_');
                    //if(name != null)
                    m_WeaponEnchantments[enchantment.AssetGuidThreadSafe] = name;
                }
            }
            catch (Exception e)
            {
                Main.logger.Error(e.ToString());
            }
        }

        private static void BuildViewLookup()
        {
            string getViewName(BlueprintUnit bp)
            {
                return bp.NameForAcronym;
                if (!LibraryThing.GetResourceGuidMap().ContainsKey(bp.Prefab.AssetId)) return "NULL";
                var path = LibraryThing.GetResourceGuidMap()[bp.Prefab.AssetId].Split('/');
                return path[path.Length - 1];
            }
            var units = Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintUnit)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintUnit>(b.Guid)).OrderBy(getViewName); ;
            ///var units = BluePrintThing.GetBlueprints<BlueprintUnit>().OrderBy(getViewName);
            foreach (var bp in units)
            {
                if (bp.Prefab.AssetId == "") continue;
                if (!LibraryThing.GetResourceGuidMap().ContainsKey(bp.Prefab.AssetId)) continue;
                if (m_Units.ContainsKey(bp.Prefab.AssetId)) continue;
                m_Units[bp.Prefab.AssetId] = getViewName(bp);
            }
        }

        private static void Init()
        {
            BuildEquipmentLookup();
            BuildWeaponLookup();
            BuildWeaponEnchantmentLookup();
            BuildViewLookup();
            BuildMythicLookup();
            loaded = true;
            ResourcesLibrary.CleanupLoadedCache();
        }
    }
}