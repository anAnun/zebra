using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Comm;
using com.sun.tools.javac;
using com.sun.org.apache.xml.@internal.serialize;
using Microsoft.Win32;
using Zebra.Sdk.Printer;
//using ZSDK_API.Comm;

namespace WpfApp1
{
    class BluetoothDiscovererExample
    {
        //public static void Main()
        //{
        //    try
        //    {
        //        Console.WriteLine("Starting printer discovery.");

        //        BluetoothDiscoveryHandler discoveryHandler = new BluetoothDiscoveryHandler();
        //        BluetoothDiscoverer.FindPrinters(discoveryHandler);
        //        while (!discoveryHandler.DiscoveryComplete)
        //        {
        //            System.Threading.Thread.Sleep(10);
        //        }
        //    }
        //    catch (DiscoveryException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

        public class BluetoothDiscoveryHandler : DiscoveryHandler
        {

            public bool discoveryComplete = false;
            public List<DiscoveredPrinter> printers = new List<DiscoveredPrinter>();

            public void DiscoveryError(string message)
            {
                Console.WriteLine($"An error occurred during discovery: {message}.");
                discoveryComplete = true;
            }

            
            public void DiscoveryFinished()
            {


                Console.WriteLine("DISCOVERY FINISHED");
                
                foreach (DiscoveredPrinter printer in printers)
                {
                    Console.WriteLine(printer);
                    try
                    {
                        //MainWindow.PrintImageTask(printer);
                        //SendZplOverBluetooth(printer.Address);
                        //p = printer;
                        Connection thePrinterConn = new BluetoothConnection(printer.Address);
                        // Open the connection - physical connection is established here.
                        thePrinterConn.Open();
                        //thePrinterConn.Write(byteArr, 120,10)
                        ZebraPrinter printerz = PrintHelper.Connect(thePrinterConn, PrinterLanguage.ZPL);
                        PrintHelper.SetPageLanguage(printerz);
                        //thePrinterConn.Write(Encoding.Default.GetBytes(path));
                        printerz.PrintImage("C:/Users/allan/OneDrive/Desktop/testnewlabel.png", 115, 8);
                        //if (PrintHelper.CheckStatusAfter(printerz))
                        //{
                        //    Console.WriteLine("Labelprinter");
                        //}


                        printerz = PrintHelper.Disconnect(printerz);
                        Console.WriteLine("Done printing");

                        // Make sure the data got to the printer before closing the connection
                        Thread.Sleep(500);

                        // Close the connection to release resources.
                        thePrinterConn.Close();
                    //}
                    }
                    catch { }
                }
                Console.WriteLine($"Discovered {printers.Count} Bluetooth printers.");
                discoveryComplete = true;
            }
            public string OpenFile()
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|BMP files (*.bmp)|*.bmp";
                        //openFileDialog->ShowHelp = true;
                if (openFileDialog.ShowDialog() == true)
                {
                    var str = openFileDialog.FileName;
                    return str;
                }
                else
                {
                    return "false";
                }

            }
            //private async Task doSomething(str)
            //{
            //    Console.WriteLine("KDKDKDKDKDKDKKDKDK");
            //}

            public void FoundPrinter(DiscoveredPrinter printer)
            {
                //Console.WriteLine("FOUND PRINTER");
                try
                {
                    printers.Add(printer);
                    //MainWindow.asdf(printer.ToString());
                    //MainWindow.continuee("asdf");
                    Console.WriteLine(printer);
                    
                    //MainWindow.asdf("dkdkdk");
                    //SendCpclOverBluetooth(printer.ToString());
                }
                catch
                {
                   
                }
                
               
            }

            public bool DiscoveryComplete
            {
                get => discoveryComplete;
            }

            private void SendZplOverBluetooth(String theBtMacAddress)
            {
                try
                {
                    Connection thePrinterConn = new BluetoothConnection(theBtMacAddress);

                    // Open the connection - physical connection is established here.
                    thePrinterConn.Open();










                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|BMP files (*.bmp)|*.bmp";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string path = openFileDialog.FileName;
                        //string path = "https://static.vecteezy.com/system/resources/previews/000/552/728/non_2x/geo-location-pin-vector-icon.jpg";
                        //MemoryStream ms = new MemoryStream();



                        //ZebraPrinter printer = PrintHelper.Connect(discoveredPrinter, PrinterLanguage.ZPL);
                        //PrintHelper.SetPageLanguage(printer);
                        //PrintHelper.SetPageLanguage(printer);
                        //if (PrintHelper.CheckStatus(printer))
                        //{
                        //    printer.PrintImage(path, 115, 10);
                        //    if (PrintHelper.CheckStatusAfter(printer))
                        //    {
                        //        Console.WriteLine("Labelprinter");
                        //    }

                        //}
                        //printer = PrintHelper.Disconnect(printer);
                        //Console.WriteLine("Done printing");



                        //                        PrintUSBTask(theIpAddress, ZPLCommand);



                    }









                    // Defines the ZPL data to be sent.
                    String zplData = "^XA^FO20,20^A0N,25,25^FDThis is a ZPL test.^FS^XZ";

                    // Send the data to the printer as a byte array.
                    thePrinterConn.Write(Encoding.Default.GetBytes(zplData));

                    // Make sure the data got to the printer before closing the connection
                    Thread.Sleep(500);

                    // Close the connection to release resources.
                    thePrinterConn.Close();

                }
                catch (Exception e)
                {

                    // Handle communications error here.
                    Console.Write(e.StackTrace);
                }
            }

            // This example prints "This is a CPCL test." near the top of the label.
            private void SendCpclOverBluetooth(String theBtMacAddress)
            {
                Console.WriteLine("SEND CPCL OVER BLUETOOTH");
                try
                {
                    // Instantiate a connection for given Bluetooth(R) MAC Address.
                    Connection thePrinterConn = new BluetoothConnection(theBtMacAddress);
                    Console.WriteLine(theBtMacAddress);
                    // Open the connection - physical connection is established here.
                    thePrinterConn.Open();

                    // Defines the CPCL data to be sent.
                    String cpclData = "! 0 200 200 210 1\r\n"
                                    + "TEXT 4 0 30 40 This is a CPCL test.\r\n"
                                    + "FORM\r\n"
                                    + "PRINT\r\n";

                    // Send the data to the printer as a byte array.
                    thePrinterConn.Write(Encoding.Default.GetBytes(cpclData));

                    // Make sure the data got to the printer before closing the connection
                    Thread.Sleep(500);

                    // Close the connection to release resources.
                    thePrinterConn.Close();

                }
                catch (Exception e)
                {

                    // Handle communications error here.
                    Console.Write(e.StackTrace);
                }
            }
        }
    }
}
