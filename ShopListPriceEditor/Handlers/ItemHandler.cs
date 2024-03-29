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

        private readonly string[] SINGLE_SKILL_GEMS =
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
            "036E", "07B0", "07B1", "07B2", "08DE" ,"08E0", "08E1", "08E2", "08E3", "07B3", "07B4", "07B5",
            "07B6", "07B7", "07B8", "07B9", "07BA", "07BB", "07BC", "07BD", "07BE", "07BF", "07C0", "07C1",
            "07C2", "07C3", "07C4", "07C5", "07C6", "07C7", "07C8", "07C9", "07CA", "07CB", "07CC", "07CD",
            "07CE", "07CF", "07D0", "07D1", "07D2", "07D3", "07D4", "07D5", "07D6", "07D7", "07D8", "07D9",
            "07DA", "07DB", "07DC", "07DD", "07DE", "07DF", "07E0", "07E1", "07E2", "07E3", "07E4", "07E5",
            "07E6", "07E7", "07E8", "07E9", "07EA", "07EB", "07EC", "07ED", "07EE", "07EF", "07F0", "07F1",
            "07F2", "07F3"
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
        private readonly string[] GL_MATERIALS = {
            "0703", "0704", "0705", "0706", "0707", "0708", "0709", "070A", "070B", "070C", "070D", "070E",
            "070F", "0710", "0711", "0712", "0713", "0714", "0715", "0716", "0717", "0718", "0719", "071A",
            "071B", "071C", "071D", "071E", "071F", "0720", "0721", "0722", "0723", "0724", "0725", "0726",
            "0727", "0728", "0729", "072A", "072B", "072C", "072D", "072E", "072F", "0730", "0731", "0732",
            "0733", "0734", "0735", "0736", "0738", "0739", "073A", "073B", "073C", "073D", "073E", "0740",
            "0742", "0743", "0744", "0745", "0746", "0747", "074C", "074D", "074E", "074F", "0750", "0751",
            "0752", "0753", "0754", "0755", "0756", "0757", "0758", "0759", "075A", "075C", "075D", "075E",
            "075F", "0760", "0762", "0763", "0764", "0766", "0767", "0768", "0769", "076A", "076B", "076C",
            "076D", "076E", "076F", "0770", "0771", "0772", "0773", "0774", "0775", "0776", "0777", "0778",
            "0779", "077A", "077B", "077C", "077D", "077E", "077F", "0780", "0781", "0782", "0783", "0784",
            "0785", "0786", "0787", "0788", "0789", "078A", "078B", "078C", "078D", "078E", "078F", "0790",
            "0791", "0792", "0793", "0794", "0795", "0796", "0797", "0798", "0799", "079A", "079B", "079C",
            "079D", "079E", "079F", "07A0", "07A1", "07A2", "07A3", "07A4", "07A5", "07A6", "07A7" };


        // mr mats
        private readonly string[] MR_MATERIALS_A_L = {
            "04F9", "04FA", "04F8", "04FB", "04FC", "057E", "057D", "057B", "057A", "057C", "04DF", "0418",
            "0474", "0475", "0477", "0478", "0473", "0540", "042D", "0580", "04BC", "04BE", "04BD", "04BB",
            "0470", "046F", "0471", "0472", "04DA", "04DD", "04DC", "04DE", "04DB", "0436", "0458", "0457",
            "045A", "0459", "045B", "040C", "0523", "0525", "0521", "0462", "0464", "0463", "0461", "0A8E",
            "052E", "04CA", "04C9", "04CB", "0508", "0506", "04FF", "0501", "0504", "0505", "0503", "04EE",
            "04EF", "04F0", "04ED", "0423", "0453", "0454", "0456", "0452", "0438", "0425", "0543", "03F3",
            "0557", "0559", "055C", "055A", "055B", "0558", "0420", "055F", "055D", "0529", "052F", "052D",
            "052C", "0528", "04C5", "04C4", "04C8", "04C6", "0524", "04CF", "04D0", "04CE", "04CC", "04CD",
            "051F", "0414", "04B1", "04B2", "04B3", "04B4", "04AF", "0406", "0542", "050D", "0AC5", "0AC8",
            "0AC9", "0AC7", "0AC6", "0AC4", "0427", "0500", "054F", "0507", "0522", "0424", "0413", "047B",
            "047D", "047E", "0479", "047A", "0517", "0433", "050C", "050B", "050E", "050F", "050A", "0510",
            "043B", "0537", "05A3", "0441", "04F2", "04F3", "04F4", "04F7", "04F1", "04F5", "0502", "0534",
            "0487", "0486", "0488", "0594", "040D", "0497", "0496", "0498", "0494", "0495", "0447", "0448",
            "0445", "0446", "04B0", "04AA", "0476", "047C", "054E", "04FD", "0431", "0509", "053D", "0587",
            "043D", "045E", "0460", "045F", "045D", "0439", "053C", "0539", "053B", "053A", "0449", "044A",
            "0591", "058F", "0590", "0592", "0593", "0455", "041D", "042A", "044C", "044B", "0450", "049C",
            "0428", "04D2", "04D4", "04D3", "04D1", "04A0", "049F", "04A2", "049D", "04A1", "049E", "0426",
            "0551", "0556", "0554", "0553", "0552", "0555"
        };


        private readonly string[] MR_MATERIALS_M_Z = {
            "05A4", "0407", "04F6", "041F", "041A", "041B", "0419", "0566", "0564", "0569", "0568", "0567",
            "04E1", "04E4", "04E5", "04E2", "04E6", "04E0", "04E3", "053E", "053F", "0541", "0493", "0492",
            "0491", "04A5", "04AB", "04AC", "04AD", "04AE", "04A9", "048F", "0490", "048E", "048D", "040E",
            "0485", "0484", "042C", "044E", "044F", "0451", "044D", "056D", "0429", "040A", "049A", "049B",
            "0499", "0535", "0532", "0531", "0536", "0538", "0533", "0530", "04C3", "04B9", "04B6", "04B7",
            "04B8", "04BA", "04B5", "0480", "0483", "047F", "0482", "0481", "045C", "04A4", "056C", "0571",
            "0570", "056F", "056E", "056B", "0526", "06FF", "040F", "0561", "043F", "0574", "0579", "0577",
            "0578", "0576", "0573", "04C0", "04C1", "04C2", "04BF", "0A8D", "0A8F", "057F", "0408", "04A6",
            "0434", "0519", "051E", "051A", "051C", "051B", "051D", "0520", "054A", "0550", "054D", "054C",
            "054B", "0412", "04E8", "04EA", "04EB", "04E9", "04EC", "04E7", "0469", "0468", "0467", "0465",
            "0466", "0422", "041E", "0411", "04C7", "048B", "048C", "0489", "048A", "0421", "04D6", "04D7",
            "04D9", "04D8", "04D5", "055E", "0563", "0560", "0562", "0544", "0546", "0549", "0545", "0548",
            "0547", "042F", "052B", "046D", "046C", "046A", "046B", "046E", "0A91", "0435", "0119", "0443",
            "0511", "0516", "0512", "0514", "0513", "0515", "0518", "0572"
        };

        private readonly string[] DOUBLE_SKILL_GEMS = {
            "07F4", "07F5", "07F6", "07F7", "07F8", "07F9", "07FA", "07FB", "07FC", "07FD", "07FE", "07FF",
            "0800", "0801", "0802", "0803", "0804", "0805", "0806", "0807", "0808", "0809", "080A", "080B",
            "080C", "080D", "080E", "080F", "0810", "0811", "0812", "0813", "0814", "0815", "0816", "0817",
            "0818", "0819", "081A", "081B", "081C", "081D", "081E", "081F", "0820", "0821", "0822", "0823",
            "0824", "0825", "0826", "0827", "0828", "0829", "082A", "082B", "082C", "082D", "082E", "082F",
            "0830", "0831", "0832", "0833", "0834", "0835", "0836", "0837", "0838", "0839", "083A", "083B",
            "083C", "083D", "083E", "083F", "0840", "0841", "0842", "0843", "0844", "0845", "0846", "0847",
            "0848", "0849", "084A", "084B", "084C", "084D", "084E", "084F", "0850", "0851", "0852", "0853",
            "0854", "0855", "0856", "0857", "0858", "0859", "085A", "085B", "085C", "085D", "085E", "085F",
            "0860", "0861", "0862", "0863", "0864", "0865", "0866", "0867", "0868", "0869", "086A", "086B",
            "086C", "086D", "086E", "086F", "0870", "0871", "0872", "0873", "0874", "0875", "0876", "0877",
            "0878", "0879", "087A", "087B", "087C", "087D", "087E", "087F", "0880", "0881", "0882", "0883",
            "0884", "0885", "0886", "0887", "0888", "0889", "088A", "088B", "088C", "088D", "088E", "088F",
            "0890", "0891", "0892", "0893", "0894", "0895", "0896", "0897", "0898", "0899", "089A", "089B",
            "089C", "089D", "089E", "089F", "08A0", "08A1", "08A2", "08A3", "08A4", "08A5", "08A6", "08A7",
            "08A8", "08A9", "08AA", "08AB", "08AC", "08AD", "08AE", "08AF", "08B0", "08B1", "08B2", "08B3",
            "08B4", "08B5", "08B6", "08B7", "08B8", "08B9", "08BA", "08BB", "08BC", "08BD", "08BE", "08BF",
            "08C0", "08C1", "08C2", "08C3", "08C4", "08C5", "08C6", "08C7", "08C8", "08C9", "08CA", "08CB",
            "08CC", "08CD", "08CE", "08CF", "08D0", "08D1", "08D2", "08D3", "08D4", "08D5", "08D6", "08D7",
            "08D8", "08D9", "08DA", "08DB", "08DC", "08DD" };

        private Item[] _hiddenList = (Item[])Application.Current.FindResource("itemsList");
        #endregion

        public string[] itemsDefault => DEFAULT_ITEMS;
        public string[] itemsSingleSkillGems => SINGLE_SKILL_GEMS;
        public string[] itemsDoubleSkillGems => DOUBLE_SKILL_GEMS;
        public string[] itemsConsumables => CONSUMABLES;
        public string[] matsLR => LR_MATERIALS;
        public string[] matsHR => HR_MATERIALS;
        public string[] matsGL => GL_MATERIALS;
        public string[] matsMR_A_L => MR_MATERIALS_A_L;
        public string[] matsMR_M_Z => MR_MATERIALS_M_Z;

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
