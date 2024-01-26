using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ShopListPriceEditor.Handlers;
using Microsoft.Win32;


namespace ShopListPriceEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static ItemHandler itemHandler = new ItemHandler();
        public static Item[] _itemlist = new Item[itemHandler.hiddenList.Count()];
        public static List<Item> listBoxIn = new List<Item>();
        public static List<Item> listBoxOut = new List<Item>();
        public static List<List<Item>> listBoxOutUndo;
        public static List<Item> pricesIn = new List<Item>();
        public static byte[] itemDataBin = ShopListPriceEditor.Properties.Resources.itemData;
        public static string filename = "";
        private static string inputFilterText = "";
        private static string outputFilterText = "";
        private static string priceInputFilterText = "";
        private static string priceOutputFilterText = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            Title = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title + " v" + getVersion();

            if (Properties.Settings.Default.SaveDirectory == "")
            {
                Properties.Settings.Default.SaveDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }
            Settings.SelectedIndex = Properties.Settings.Default.InsertMethod;
            Language.SelectedIndex = Properties.Settings.Default.Language;
            InitBoxes();
            InitPrices();

        }

        private void PopulateBoxes(List<string> items)
        {
            Item[] itemlist = new Item[listBoxIn.Count];
            listBoxIn.CopyTo(itemlist, 0);
            if (listBoxOut.Count + items.Count > 255)
            {
                TooManyItemsError();
            }
            else
            {
                if (Properties.Settings.Default.InsertMethod == 0)
                {
                    items.Reverse();
                    foreach (string item in items)
                    {
                        var result = itemlist.SingleOrDefault(x => x.Key.Substring(4) == item);
                        if (result != null)
                        {
                            listBoxIn.Remove(result);
                            listBoxOut.Insert(0, result);
                        }
                    }
                }
                else
                {
                    foreach (string item in items)
                    {
                        var result = itemlist.SingleOrDefault(x => x.Key.Substring(4) == item);
                        if (result != null)
                        {
                            listBoxIn.Remove(result);
                            listBoxOut.Add(result);
                        }
                    }
                }
                ShopListRefresh();
            }
        }

        // this function works for both price and item editor? shit is so confusing
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            string fileName = "";
            string fileExt = "";
            string fileFilter = "";

            if (((MenuItem)sender).Name.Equals("openShopFile"))
            {
                Clear();
                fileName = "shopList.slt";
                fileExt = ".slt";
                fileFilter = "Shop List file | *.slt";
            }
            else if (((MenuItem)sender).Name.Equals("openPriceFile"))
            {
                fileName = "itemData.itm";
                fileExt = ".itm";
                fileFilter = "Item Price file | *.itm";
            }

            OpenFileDialog dlg = new OpenFileDialog
            {
                FileName = fileName,
                DefaultExt = fileExt,
                Filter = fileFilter,
                InitialDirectory = Properties.Settings.Default.SaveDirectory
            };

            if (dlg.ShowDialog() != true) return;

            FileHandler fileHandler = new FileHandler();

            if (((MenuItem)sender).Name.Equals("openShopFile"))
            {
                List<string> items = fileHandler.OpenShopFile(fileName, dlg.FileName);
                PopulateBoxes(items);
            }
            else if (((MenuItem)sender).Name.Equals("openPriceFile"))
            {
                FileHandler.saveFilename = dlg.FileName;
                fileHandler.OpenPriceFile(fileName, dlg.FileName);
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("filteredPriceInput"));
            }

            Properties.Settings.Default.SaveDirectory = System.IO.Path.GetDirectoryName(dlg.FileName);
            Properties.Settings.Default.Save();
        }

        private void ListOutUpdate()
        {
            Item[] itemlist = new Item[listBoxOut.Count];
            listBoxOut.CopyTo(itemlist, 0);
            var currentOutput = output.Items;
            List<string> currentItems = new List<string>();

            foreach (var x in currentOutput)
            {
                currentItems.Add(x.ToString());
            }

            listBoxOut = new List<Item>();
            currentItems.Reverse();

            foreach (string item in currentItems)
            {

                var result = itemlist.SingleOrDefault(x => x.Value == item);

                if (result != null)
                {
                    listBoxOut.Insert(0, result);
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredOutput"));
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            Stream fs;
            string fileName = "";
            string fileExt = "";
            string fileFilter = "";
            FileHandler fileHandler = new FileHandler();

            listBoxOutUndo.Add(new List<Item>(listBoxOut));
            ListOutUpdate();
            /*
            Item[] itemlist = new Item[listBoxOut.Count];
            listBoxOut.CopyTo(itemlist, 0);
            var currentOutput = output.Items;
            List<string> currentItems = new List<string>();

            foreach (var x in currentOutput)
            {
                currentItems.Add(x.ToString());
            }

            listBoxOut = new List<Item>();

            foreach (string item in currentItems)
            {
                Console.WriteLine("item: " + item);
                var result = itemlist.SingleOrDefault(x => x.Value == item);
                Console.WriteLine("result: " + result);
                if (result != null)
                {
                    listBoxOut.Insert(0, result);
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredOutput"));
            */


            if (((MenuItem)sender).Name.Equals("saveShopFile"))
            {
                fileName = "shopList.slt";
                fileExt = ".slt";
                fileFilter = "Shop List file | *.slt";
            }

            if (((MenuItem)sender).Name.Equals("savePriceFile"))
            {
                if (string.IsNullOrEmpty(FileHandler.saveFilename))
                {
                    MessageBox.Show("Unable to save.\n" +
                               "Open a file first.");
                    return;
                }
                fileHandler.SaveFile();
                try
                {
                    Properties.Settings.Default.SaveDirectory = System.IO.Path.GetDirectoryName(FileHandler.saveFilename);
                    Properties.Settings.Default.Save();
                }
                catch (Exception exception)
                {
                    // what the fuck
                    Console.WriteLine("I'm bad and I should feel bad for doing this, but it's late and I don't care.");
                }
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog()
            {
                FileName = fileName,
                DefaultExt = fileExt,
                Filter = fileFilter,
                InitialDirectory = Properties.Settings.Default.SaveDirectory
            };

            if (dlg.ShowDialog() == true && (fs = dlg.OpenFile()) != null)
            {
                Console.WriteLine("mlem ---\n" + dlg.FileName + "mlem ---\n");
                fileHandler.SaveFile(fileName, dlg.FileName, fs);
                Properties.Settings.Default.SaveDirectory = System.IO.Path.GetDirectoryName(dlg.FileName);
                Properties.Settings.Default.Save();
            }
        }

        private void CreateDefaultPriceFile(object sender, RoutedEventArgs e)
        {
            Stream fs;
            FileHandler fileHandler = new FileHandler();
            string fileName = "itemData";
            string fileExt = ".itm";
            string fileFilter = "";

            Console.WriteLine("Writing new itemData.itm");


            SaveFileDialog dlg = new SaveFileDialog()
            {
                FileName = fileName,
                DefaultExt = fileExt,
                Filter = fileFilter,
                InitialDirectory = Properties.Settings.Default.SaveDirectory
            };

            if (dlg.ShowDialog() == true && (fs = dlg.OpenFile()) != null)
            {
                Console.WriteLine("mlem ---\n" + dlg.FileName + "mlem ---\n");
                fileHandler.CreateDefaultPriceFile(dlg.FileName, itemDataBin, fs);
                Properties.Settings.Default.SaveDirectory = System.IO.Path.GetDirectoryName(dlg.FileName);
                Properties.Settings.Default.Save();
            }
        }

        private void LanguageChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceDictionary dict = new ResourceDictionary();

            switch (((sender as ComboBox).SelectedItem as ComboBoxItem).Tag.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("Lang.en-US.xaml", UriKind.Relative);
                    break;
                case "zh-TW":
                    dict.Source = new Uri("Lang.zh-TW.xaml", UriKind.Relative);
                    break;
                case "ja-JP":
                    dict.Source = new Uri("Lang.ja-JP.xaml", UriKind.Relative);
                    break;
                case "zh-Hans":
                    dict.Source = new Uri("Lang.zh-Hans.xaml", UriKind.Relative);
                    break;
                default:
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(dict);
            itemHandler.hiddenList = (Item[])Application.Current.FindResource("itemsList");
            Clear();
            InitBoxes();
            Properties.Settings.Default.Language = Language.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void DefaultItems(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateBoxes(itemHandler.itemsDefault.ToList());
        }

        private void ClearItems(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            if (listBoxOutUndo == null)
            {
                listBoxOutUndo = new List<List<Item>>();
                //listBoxOutUndo.Add(new List<Item>(listBoxOut));
            }
            else
            {
                listBoxOutUndo.Add(new List<Item>(listBoxOut));
            }

            List<Item> itemlist = new List<Item>();
            foreach (Item item in listBoxOut)
            {
                listBoxIn.Add(item);
                itemlist.Add(item);
            }
            foreach (Item item in itemlist)
            {
                listBoxOut.Remove(item);
            }
            Sort();
            ShopListRefresh();
        }

        private void SingleSkillGems(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateBoxes(itemHandler.itemsSingleSkillGems.ToList());
        }

        private void DoubleSkillGems(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateBoxes(itemHandler.itemsDoubleSkillGems.ToList());
        }

        private void AllConsumables(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateBoxes(itemHandler.itemsConsumables.ToList());
        }

        private void LRMaterials(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateBoxes(itemHandler.matsLR.ToList());
        }

        private void HRMaterials(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateBoxes(itemHandler.matsHR.ToList());
        }

        private void GLMaterials(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateBoxes(itemHandler.matsGL.ToList());
        }

        private void MRMaterials_AL(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateBoxes(itemHandler.matsMR_A_L.ToList());
        }

        private void MRMaterials_MZ(object sender, RoutedEventArgs e)
        {
            Clear();
            PopulateBoxes(itemHandler.matsMR_M_Z.ToList());
        }

        private void InitBoxes()
        {
            listBoxIn.Clear();
            Item[] itemlist = new Item[itemHandler.hiddenList.Count()];
            itemHandler.hiddenList.CopyTo(itemlist, 0);
            foreach (Item item in itemlist)
            {
                if (item.Value.Contains("Item ") || item.Value.Contains("DNI"))
                {
                    continue;
                }
                listBoxIn.Add(item);
            }

            ShopListRefresh();
        }

        private void InitPrices()
        {
            pricesIn.Clear();
            itemHandler.hiddenList.CopyTo(_itemlist, 0);
            foreach (Item item in _itemlist)
            {
                pricesIn.Add(item);
            }
        }

        private void Sort(object sender, RoutedEventArgs e)
        {
            ListOutUpdate();
            listBoxOutUndo.Add(new List<Item>(listBoxOut));
            var sortedList = listBoxOut.OrderBy(items => items.Key).ToList();
            listBoxOut = new List<Item>(sortedList);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredOutput"));
            itemCount.Text = "Items in Shop: " + listBoxOut.Count.ToString() + " / 255";
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            if (listBoxOutUndo.Count > 0)
            {

                listBoxOut = new List<Item>(listBoxOutUndo.Last());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredOutput"));
                itemCount.Text = "Items in Shop: " + listBoxOut.Count.ToString() + " / 255";
                listBoxOutUndo.RemoveAt(listBoxOutUndo.Count - 1);
            }
        }

        private void Sort()
        {
            listBoxIn.Sort((x, y) => x.Hex.CompareTo(y.Hex));
            ShopListRefresh();
        }

        private void SendOut(object sender, RoutedEventArgs e)
        {
            ListOutUpdate();
            listBoxOutUndo.Add(new List<Item>(listBoxOut));

            if (input.SelectedItems != null && input.SelectedItems.Count + listBoxOut.Count < 256)
            {
                Item[] selecteditems = new Item[input.SelectedItems.Count];
                input.SelectedItems.CopyTo(selecteditems, 0);
                List<Item> itemlist = new List<Item>();
                if (Properties.Settings.Default.InsertMethod == 0)
                {
                    Array.Reverse(selecteditems);
                    foreach (Item item in selecteditems)
                    {
                        listBoxOut.Insert(0, item);
                        itemlist.Add(item);
                    }
                }
                else
                {
                    foreach (Item item in selecteditems)
                    {
                        listBoxOut.Add(item);
                        itemlist.Add(item);
                    }

                }
                foreach (Item item in itemlist)
                {
                    listBoxIn.Remove(item);
                }
                ShopListRefresh();
            }
            else
            {
                TooManyItemsError();
            }
        }

        private void SendIn(object sender, RoutedEventArgs e)
        {
            ListOutUpdate();
            listBoxOutUndo.Add(new List<Item>(listBoxOut));

            if (output.SelectedItems != null)
            {
                List<Item> itemlist = new List<Item>();
                foreach (Item item in output.SelectedItems)
                {
                    itemlist.Add(item);
                    listBoxIn.Add(item);
                }
                foreach (Item item in itemlist)
                {
                    listBoxOut.Remove(item);
                }
                Sort();
                ShopListRefresh();
            }
        }

        private void InsertMethodChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.InsertMethod = Settings.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        public static void TooManyItemsError()
        {
            MessageBox.Show("Too many items in the output box!\n" +
                            "Maximum item capacity is 255.", "Error");
        }

        public ObservableCollection<Item> filteredInput
        {
            get
            {
                if (String.IsNullOrEmpty(inputFilterText))
                {
                    results.Text = listBoxIn.Count().ToString();
                    return new ObservableCollection<Item>(listBoxIn);
                }
                var filtered = listBoxIn.Where(x => x.Value.ToUpper().Contains(inputFilterText.ToUpper()));
                results.Text = filtered.Count().ToString();
                return new ObservableCollection<Item>(filtered);
            }
        }

        public string filterInputText
        {
            get { return inputFilterText; }
            set
            {
                inputFilterText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredInput"));
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredOutput"));
                }
            }
        }

        public ObservableCollection<Item> filteredOutput
        {
            get
            {
                if (String.IsNullOrEmpty(outputFilterText))
                {
                    return new ObservableCollection<Item>(listBoxOut);
                }
                var filtered = listBoxOut.Where(x => x.Value.ToUpper().Contains(outputFilterText.ToUpper()));
                return new ObservableCollection<Item>(filtered);
            }
        }

        public string filterOutputText
        {
            get { return outputFilterText; }
            set
            {
                outputFilterText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredInput"));
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredOutput"));
                }
            }
        }

        public ObservableCollection<Item> filteredPriceInput
        {
            get
            {
                if (String.IsNullOrEmpty(priceInputFilterText))
                {

                    return new ObservableCollection<Item>(pricesIn);
                }
                var filtered = pricesIn.Where(x => x.Value.ToUpper().Contains(filterPriceInputText.ToUpper()));
                return new ObservableCollection<Item>(filtered);
            }
        }

        public string filterPriceInputText
        {
            get { return priceInputFilterText; }
            set
            {
                priceInputFilterText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredPriceInput"));
                }
            }
        }

        public string filterPriceOutputText
        {
            get { return priceOutputFilterText; }
            set
            {
                priceOutputFilterText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("filteredPriceInput"));
                }
            }
        }

        public void ShopListRefresh()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredInput"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredOutput"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("filteredPriceInput"));
            itemCount.Text = "Items in Shop: " + listBoxOut.Count.ToString() + " / 255";
        }

        private void InputDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SendOut(sender, new RoutedEventArgs());
        }

        private void OutputDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SendIn(sender, new RoutedEventArgs());
        }

        public String getVersion()
        {
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
        }
    }


}
