using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ShopListPriceEditor.Handlers
{
    public class FileHandler
    {
        public static string saveFilename = "";

        public List<string> OpenShopFile(string fileType, string fileName)
        {
            List<string> items = new List<string>();

            byte[] buffer = new byte[2];

            items = new List<string>();

            byte[] input = File.ReadAllBytes(fileName);
            // Shop List
            // New Iceborne header and buffer changed offset requirement.
            for (int i = 14; i < input.Length - 1; i += 14)
            {
                buffer[0] = input[i + 1];
                buffer[1] = input[i];
                items.Add(BitConverter.ToString(buffer).Replace("-", ""));
            }

            return items;
        }

        public void OpenPriceFile(string fileType, string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var itemOffset = Convert.ToInt32(fs.Position = 0x2A); // Item ID Position
                byte offsetIncrement = 0x18;

                foreach (Item item in MainWindow._itemlist)
                {
                    item.ItemOffset = itemOffset;
                    
                    fs.Position += offsetIncrement;

                    var _sell = new byte[4] { (byte)fs.ReadByte(), (byte)fs.ReadByte(), (byte)fs.ReadByte(), (byte)fs.ReadByte() };
                    var sell = new byte[4];
                    var _buy = new byte[4] { (byte)fs.ReadByte(), (byte)fs.ReadByte(), (byte)fs.ReadByte(), (byte)fs.ReadByte() };
                    var buy = new byte[4];

                    item.ItemSellPrice = BitConverter.ToInt32(_sell, 0);
                    item.ItemBuyPrice = BitConverter.ToInt32(_buy, 0);

                    itemOffset = Convert.ToInt32(fs.Position);
                }
            }
        }

        public void SaveFile()
        {
            using (var fs = File.Open(saveFilename, FileMode.Open, FileAccess.Write))
            {
                //var itemOffset = Convert.ToInt32(fs.Position = 0x2A); // Item ID Position
                var itemOffset = Convert.ToInt32(0x2A); // Item ID Position
                fs.Position = 42; // Item ID Start Position
                byte offsetIncrement = 23;//0x17;
                //fs.Position = itemOffset;

                foreach (Item item in MainWindow._itemlist)
                {
                    var itemIDBytes = BitConverter.GetBytes(Convert.ToInt16(item.Hex));
                    fs.WriteByte((byte)itemIDBytes[0]);
                    fs.WriteByte((byte)itemIDBytes[1]);
                    offsetIncrement = 22;//0x16;

                    itemOffset = Convert.ToInt32(fs.Position);
                    itemOffset += offsetIncrement;
                    fs.Position = itemOffset;

                    
                    List<byte> prices = new List<byte>();
                    foreach (var thing in BitConverter.GetBytes(item.ItemSellPrice).ToList())
                    {
                        prices.Add(thing);
                    }

                    foreach (var otherThing in BitConverter.GetBytes(item.ItemBuyPrice).ToList())
                    {
                        prices.Add(otherThing);
                    }

                    foreach (byte data in prices.ToArray())
                    {
                        fs.WriteByte(data);
                    }
                }
                MessageBox.Show("File saved.");
            }
        }

        public void SaveFile(string fileType, string fileName, Stream fs)
        {
            List<byte> items = new List<byte>();

            byte shopListMaxCount = (byte)MainWindow.listBoxOut.Count;
            byte[] shopListHeader = new byte[] { 0x01, 0x10, 0x09, 0x18, 0x19, 0x00, shopListMaxCount, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] shopListBuffer = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            byte[] itemPriceBuffer = new byte[] {};

            // Item index at 0x08 and 0x0A
            byte itemIndex = 0x00;
            items = shopListHeader.ToList();

            foreach (Item item in MainWindow.listBoxOut)
            {
                byte[] buffer = shopListBuffer;
                itemIndex++;

                string hex = item.Key.Substring(4);
                items.Add(Convert.ToByte(int.Parse(hex.Substring(2), System.Globalization.NumberStyles.HexNumber)));
                items.Add(Convert.ToByte(int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber)));

                if (item == MainWindow.listBoxOut[MainWindow.listBoxOut.Count - 1])
                {
                    // If it's the last item in the shop it won't need the last few bytes.
                    buffer = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    buffer[6] = (byte)(itemIndex - 0x01);
                    items.AddRange(buffer);
                }
                else
                {
                    buffer[6] = (byte)(itemIndex - 0x01);
                    buffer[8] = itemIndex;
                    items.AddRange(buffer);
                }
            }
            
            byte[] output = items.ToArray();
            fs.Write(output, 0, output.Length);
            fs.Close();

            Console.WriteLine("Items: " + BitConverter.ToString(output).Replace("-", " "));
        }
    }
}
