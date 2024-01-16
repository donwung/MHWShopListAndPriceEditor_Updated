﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopListPriceEditor.Handlers
{
    public class ItemHandler
    {
        #region Item Lists
        private readonly string[] DEFAULT_ITEMS =
        {
            "0001", "0005", "000D", "0011", "0046", "0047", "0048", "004A", "004B", "004F", "0058", "0059",
            "005A", "005C", "0055", "0056", "0057", "0066", "0067", "008A", "008C", "008D", "008F", "0090",
            "0092", "0093", "0095", "0096", "0098", "0099", "009A", "009B", "009D", "009E", "009F", "00A0",
            "00A1", "00A2", "00A3", "00A4", "00A5", "00A7", "00A8", "00A9", "00AE", "00AF", "00B0", "00B1",
            "00B2", "00B4"
        };

        private readonly string[] GEMS =
        {
            "02D7", "02D8", "02D9", "02DA", "02DB", "02DC", "02DD", "02DE", "02DF", "02E0", "02E1", "02E2",
            "02E3", "02E4", "02E5", "02E6", "02E7", "02E8", "02E9", "02EA", "02EB", "02EC", "02ED", "02EE",
            "02EF", "02F0", "02F1", "02F2", "02F3", "02F4", "02F5", "02F6", "02F7", "02F8", "02F9", "02FA",
            "02FB", "02FC", "02FD", "02FE", "02FF", "0300", "0301", "0302", "0303", "0304", "0305", "0306",
            "0307", "0308", "0309", "030A", "030B", "030C", "030D", "030E", "030F", "0310", "0311", "0312",
            "0313", "0314", "0315", "0316", "0317", "0318", "0319", "031A", "031B", "031C", "031D", "031E",
            "031F", "0320", "0321", "0322", "0323", "0324", "0325", "0326", "0327", "0328", "0329", "032A",
            "032B", "032C", "032D", "032E", "032F", "0330", "0331", "0332", "0333", "0334", "0335", "0336",
            "0337", "0338", "0339", "033A", "033B", "033C", "033D", "033E", "033F", "0340", "0341", "0342",
            "0343", "0344", "0345", "0346", "0347", "0348", "0349", "034A", "036A", "036B", "036C", "036D",
            "036E", "07B0", "07B1", "07B2", "08DE" ,"08E0", "08E1", "08E2", "08E3"
        };

        private readonly string[] CONSUMABLES =
        {
            "0001", "0002", "0003", "0004", "0005", "0006", "0007", "0008", "0009", "000A", "000B", "000C",
            "000D", "000E", "000F", "0010", "0011", "0012", "0013", "0014", "0015", "0016", "0017", "0018",
            "0019", "001A", "001B", "001C", "001D", "001E", "001F", "0020", "0021", "0022", "0023", "0024",
            "0025", "0026", "0027", "0028", "0029", "002A", "002B", "002C", "002D", "002E", "002F", "0030",
            "0031", "0032", "0033", "0034", "0035", "0036", "0037", "0038", "0039", "003A", "003B", "003C",
            "003D", "003E", "003F", "0040", "0041", "0042", "0043", "0044", "0045", "0046", "0047", "0048",
            "0049", "004A", "004B", "004C", "004D", "004E", "004F", "0050", "0051", "0052", "0053", "0054",
            "005E", "005F", "0060", "0061", "0062", "0063", "0064", "0065", "0066", "0067", "0068", "0069",
            "006A", "006B", "006C", "006D", "006E", "006F", "0070", "0071", "008A", "008B", "008C", "008D",
            "008E", "008F", "0090", "0091", "0092", "0093", "0094", "0095", "0096", "0097", "0098", "0099",
            "009A", "009B", "009C", "009D", "009E", "009F", "00A0", "00A1", "00A2", "00A3", "00A4", "00A5",
            "00A6", "00A7", "00A8", "00A9", "00AA", "00AB", "00AE", "00AF", "00B0", "00B1", "00B2", "00B3",
            "00B4", "00C3", "00C4", "00C5", "00C6", "00C7"
        };

        private readonly string[] LR_MATERIALS =
        {
            "00CD", "00CE", "00CF", "00D2", "00D3", "00D5", "00D8", "00E1", "00E2", "00E3", "00E4", "00E5",
            "00E6", "00EA", "00EB", "00EC", "00ED", "00EE", "00F3", "00F5", "00F7", "00F9", "00FB", "00FD",
            "00FF", "0101", "0103", "0108", "0109", "010B", "010D", "010F", "0110", "0111", "0114", "0115",
            "0118", "011C", "011D", "0121", "0122", "0125", "0126", "0129", "012A", "012B", "012E", "012F",
            "0130", "0131", "0135", "0136", "0137", "0138", "013D", "013E", "013F", "0140", "0141", "0146",
            "0147", "0148", "0149", "014A", "014B", "014F", "0150", "0151", "0152", "0157", "0158", "0159",
            "015A", "015B", "0160", "0161", "0162", "0163", "0164", "0165", "016B", "016C", "016D", "016E",
            "016F", "0176", "0177", "0178", "0179", "017E", "017F", "0180", "0181", "0186", "0187", "0188",
            "0189", "018A", "018F", "0190", "0191", "0192", "0194", "0198", "0199", "019A", "019B", "019C",
            "019D", "01A3", "01A4", "01A5", "01A6", "01A7", "01A8", "01AE", "01AF", "01B0", "01B1", "01B2",
            "01B3", "01B4", "01BE", "01BF", "01C0", "01C1", "01C2", "01C3", "01CB", "01CC", "01CD", "01CE",
            "01D3", "01D4", "01D5", "01D6", "01D8"
        };

        private readonly string[] HR_MATERIALS =
        {
            "00D0", "00D1", "00D4", "00D6", "00D7", "00D9", "00DA", "00DB", "00E7", "00E8", "00E9", "00EF",
            "00F0", "00F1", "00F2", "00F4", "00F6", "00F8", "00FA", "00FC", "00FE", "0100", "0102", "0104",
            "0105", "0106", "0107", "010A", "010C", "010E", "0112", "0113", "0116", "0117", "0119", "011A",
            "011B", "011E", "011F", "0120", "0123", "0124", "0127", "0128", "012C", "012D", "0132", "0133",
            "0134", "0139", "013A", "013B", "013C", "0142", "0143", "0144", "0145", "014C", "014D", "014E",
            "0153", "0154", "0155", "0156", "015C", "015D", "015E", "015F", "0166", "0167", "0168", "0169",
            "016A", "0170", "0171", "0172", "0173", "0174", "0175", "017A", "017B", "017C", "017D", "0182",
            "0183", "0184", "0185", "018B", "018C", "018D", "018E", "0195", "0196", "0197", "019E", "019F",
            "01A0", "01A1", "01A2", "01A9", "01AA", "01AB", "01AC", "01AD", "01B5", "01B6", "01B7", "01B8",
            "01B9", "01BA", "01BB", "01BC", "01BD", "01C4", "01C5", "01C6", "01C7", "01C8", "01C9", "01CA",
            "01CF", "01D0", "01D1", "01D9", "01DA", "01DB", "01DC", "01DD", "01DE", "01DF", "01E0", "01E1",
            "01E2", "01E3", "01E4", "01E5", "01E6", "01E7", "01E8", "01E9", "01EA", "01EB", "01EC", "01ED",
            "01EE", "01EF", "01F0", "01F1", "01F2", "01F4", "01F5", "01F6", "01F7", "01F8", "01F9", "01FA",
            "01FB", "01FC", "01FD", "01FE", "01FF", "0201", "0202", "0203", "0204", "0205", "0206", "0207",
            "0208", "0209", "020A", "020B", "020C", "020D", "020E", "020F", "0210", "0211", "0212", "0213",
            "0214", "0215", "0216", "0217", "0218", "0219", "036F", "0370", "0371", "0372", "0373", "0374",
            "0375", "0376", "0377", "0378", "0379", "037A", "037B", "037C", "037F", "0380", "0381", "0382",
            "0383", "0384", "0385"
        };

        // guiding lands mats
        private readonly string[] GL_MATERIALS =
        {
            "00D0"
            // 
            // Decayed Crystal
            // Forest Crystal
            // Prosperous Crystal
            // Guiding Forest Crystal
            // Cracked Crystal
            // Wasteland Crystal
            // Serene Crystal
            // Guiding Wasteland Crystal
            // Pale Crystal
            // Reef Crystal
            // Deepsea Crystal
            // Guiding Reef Crystal
            // Distorted Crystal
            // Effluvial Crystal
            // Twilight Crystal
            // Guiding Effluvial Crystal
            // Melted Crystal
            // Magma Crystal
            // Hellfire Crystal
            // Guiding Magma Crystal
            // Frozen Crystal
            // Rime Crystal
            // Daybreak Crystal
            // Guiding Rime Crystal
            // Mossy Greatbone
            // Woodland Greatbone
            // Slumbering Greatbone
            // Guiding Forest Dragonbone
            // Weathered Cragbone
            // Wasteland Cragbone
            // Tempered Cragbone
            // Guiding Wasteland Dragonbone
            // Vivid Crimsonbone
            // Coral Crimsonbone
            // Vibrant Crimsonbone
            // Guiding Reef Dragonbone
            // Malformed Frenzybone
            // Effluvial Frenzybone
            // Afflicted Frenzybone
            // Guiding Rotted Dragonbone
            // Simmering Wildbone
            // Volcanic Wildbone
            // Dragonscorched Wildbone
            // Guiding Volcanic Dragonbone
            // Hoary Icebone
            // Tundra Icebone
            // Everfrost Icebone
            // Guiding Tundra Dragonbone
            // Fierce Dragonvein Bone
            // Heavy Dragonvein Bone
            // Dragonvein Solidbone
            // Elder Dragonvein Bone
            // Spiritvein Slogbone
            // Spiritvein Solidbone
            // Elder Spiritvein Bone
            // Spiritvein Gem Shard
            // Spiritvein Gem
            // Great Spiritvein Gem
            // Rugged Mane
            // Colorful Plume
            // Fragrant Poison Sac
            // Tempered Poison Sac
            // Hydrated Sac
            // Tempered Torrent Sac
            // Muddy Crown
            // Tempered Crown
            // Blinding Cathode
            // Tempered Cathode
            // Enticing Viperthorn
            // Tempered Viperthorn
            // Ancient Great Horn
            // Tempered Great Horn
            // Flickering Flamepelt
            // Tempered Flamepelt
            // Crackling Thunderpelt
            // Tempered Thunderpelt
            // Queen's Scale
            // Tempered Green Scale
            // Pink Scale
            // Tempered Pink Scale
            // Dazzling Photophore+
            // Rubbery Shell
            // Tempered Rubbery Shell
            // Obsidian Fur
            // Tempered Hide
            // Night Hood
            // Inkstained Oilshell+
            // Tempered Oilshell+
            // Heavy Jaw
            // Incandescent Magmafin
            // Tempered Magmafin
            // Metallic Scute
            // Tempered Scute
            // Coldblooded Icefang
            // Tempered Icefang
            // Sinister Silverpelt
            // Tempered Silverpelt
            // Smooth Icehide
            // Tempered Icehide
            // Crystal Frozenhide
            // Tempered Frosthide
            // Fatal Rendclaw
            // Tempered Rendclaw
            // Soulrender Talon
            // Tempered Talon
            // King's Scale
            // Tempered Red Scale
            // Azure Scale
            // Tempered Azure Scale
            // Twisted Bravehorn
            // Twisted Temperhorn
            // Blackcurl Tyranthorn
            // Blackcurl Temperhorn
            // Assassin Cutwing
            // Tempered Cutwing
            // Smoldering Tailedge
            // Tempered Tailedge
            // Honed Tailblade
            // Tempered Tailblade
            // Glossy Ebonshell
            // Tempered Ebonshell
            // Strongman's Jaw
            // Tempered Jaw
            // Shuddering Darkjaw
            // Tempered Ebonjaw
            // Charged Deathly Shocker
            // Tempered Shocker
            // Crimson Blastscale
            // Tempered Blastscale
            // Bloodstained Ebonhide
            // Spattered Hide
            // Solemn Azure Horn
            // Tempered Azure Horn
            // Hellfire Mane
            // Tempered Crimson Mane
            // Stormcall Steelwing
            // Tempered Steelwing
            // Azure Mane
            // Tempered Azure Mane
            // Aurora Crownhorn
            // Tempered Crownhorn
            // Twilight Fang
            // Tempered Twilight Fang
            // Enchanting Finehide
            // Tempered Trancehide
            // Extinction Greathorn
            // Annihilating Temperhorn
            // Moonlight Scale
            // Tempered Gold Scale
            // Scorching Scale
            // Tempered Silver Scale
            // Gushing Dragonhold
            // Tempered Dragonhold
            // Bloodthirsty Glimmerpelt
            // Tempered Glimmerpelt
        };

        // mr mats
        private readonly string[] BUTTONCATEGORY = {
             "04F9", "04FA", "04F8", "04FB", "04FC", "057E", "057D", "057B", "057A", "057C", "04DF", "0418", "0474", "0475", "0477", "0478", "0473", "0540", "042D", "0580", "04BC", "04BE", "04BD", "04BB", "0470", "046F", "0471", "0472", "04DA", "04DD", "04DC", "04DE", "04DB", "0436", "0458", "0457", "045A", "0459", "045B", "040C", "0523", "0525", "0521", "0462", "0464", "0463", "0461", "0A8E", "052E", "04CA", "04C9", "04CB", "0508", "0506", "04FF", "0501", "0504", "0505", "0503", "04EE", "04EF", "04F0", "04ED", "0423", "0453", "0454", "0456", "0452", "0438", "0425", "0543", "03F3", "0557", "0559", "055C", "055A", "055B", "0558", "0420", "055F", "055D", "0529", "052F", "052D", "052C", "0528", "04C5", "04C4", "04C8", "04C6", "0524", "04CF", "04D0", "04CE", "04CC", "04CD", "051F", "0414", "04B1", "04B2", "04B3", "04B4", "04AF", "0406", "0542", "050D", "0AC5", "0AC8", "0AC9", "0AC7", "0AC6", "0AC4", "0427", "0500", "054F", "0507", "0522", "0424", "0413", "047B", "047D", "047E", "0479", "047A", "0517", "0433", "050C", "050B", "050E", "050F", "050A", "0510", "043B", "0537", "05A3", "0441", "04F2", "04F3", "04F4", "04F7", "04F1", "04F5", "0502", "0534", "0487", "0486", "0488", "0594", "040D", "0497", "0496", "0498", "0494", "0495", "0447", "0448", "0445", "0446", "04B0", "04AA", "0476", "047C", "054E", "04FD", "0431", "0509", "053D", "0587", "043D", "045E", "0460", "045F", "045D", "0439", "053C", "0539", "053B", "053A", "0449", "044A", "0591", "058F", "0590", "0592", "0593", "0455", "041D", "042A", "044C", "044B", "0450", "049C", "0428", "04D2", "04D4", "04D3", "04D1", "04A0", "049F", "04A2", "049D", "04A1", "049E", "0426", "0551", "0556", "0554", "0553", "0552", "0555", "05A4", "0407", "04F6", "041F", "041A", "041B", "0419", "0566", "0564", "0569", "0568", "0567", "04E1", "04E4", "04E5", "04E2", "04E6", "04E0", "04E3", "053E", "053F", "0541", "0493", "0492", "0491", "04A5", "04AB", "04AC", "04AD", "04AE", "04A9", "048F", "0490", "048E", "048D", "040E", "0485", "0484", "042C", "044E", "044F", "0451", "044D", "056D", "0429", "040A", "049A", "049B", "0499", "0535", "0532", "0531", "0536", "0538", "0533", "0530", "04C3", "04B9", "04B6", "04B7", "04B8", "04BA", "04B5", "0480", "0483", "047F", "0482", "0481", "045C", "04A4", "056C", "0571", "0570", "056F", "056E", "056B", "0526", "06FF", "040F", "0561", "043F", "0574", "0579", "0577", "0578", "0576", "0573", "04C0", "04C1", "04C2", "04BF", "0A8D", "0A8F", "057F", "0408", "04A6", "0434", "0519", "051E", "051A", "051C", "051B", "051D", "0520", "054A", "0550", "054D", "054C", "054B", "0412", "04E8", "04EA", "04EB", "04E9", "04EC", "04E7", "0469", "0468", "0467", "0465", "0466", "0422", "041E", "0411", "04C7", "048B", "048C", "0489", "048A", "0421", "04D6", "04D7", "04D9", "04D8", "04D5", "055E", "0563", "0560", "0562", "0544", "0546", "0549", "0545", "0548", "0547", "042F", "052B", "046D", "046C", "046A", "046B", "046E", "0A91", "0435", "0119", "0443", "0511", "0516", "0512", "0514", "0513", "0515", "0518", "0572"
              };

        // TODO: add lv4 gems
        private readonly string[] LVL4_GEMS =
        {
            "00D0"

        };

        private Item[] _hiddenList = (Item[])Application.Current.FindResource("itemsList");
        #endregion

        public string[] itemsDefault => DEFAULT_ITEMS;
        public string[] itemsGems => GEMS;
        public string[] itemsLvl4Gems => LVL4_GEMS;
        public string[] itemsConsumables => CONSUMABLES;
        public string[] matsLR => LR_MATERIALS;
        public string[] matsHR => HR_MATERIALS;
        public string[] matsGL => GL_MATERIALS;
        public string[] matsMR => MR_MATERIALS;

        public Item[] hiddenList
        {
            get => (Item[])Application.Current.FindResource("itemsList");
            set => _hiddenList = value;
        }
    }

    public class Item
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Hex => Convert.ToInt32(this.Key.Substring(4), 16);
        public int ItemOffset { get; set; }
        public int ItemSellPrice { get; set; }
        public int ItemBuyPrice { get; set; }
        public override string ToString()
        {
            return Value;
        }
    }
}
