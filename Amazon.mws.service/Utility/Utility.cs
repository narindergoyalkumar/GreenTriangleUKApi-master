using Amazon.mws.service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Amazon.mws.service.Utility
{
    public static class Utility
    {
        public static List<Products> Convert_Open_Listings_As_List(string reportPath)
        {
            // create the list to hold the open listing  
            List<Products> openList = new List<Products>();
            // Check to see if the file exists and parse it if it does  
            if (File.Exists(reportPath))
            {
                // Get a stream to the report  
                StreamReader reportReader;
                reportReader = File.OpenText(reportPath);
                // read the file into the list line by line  
                string line;
                while ((line = reportReader.ReadLine()) != null)
                {
                    // Split each line by the tab sequence  
                    string[] columns = line.Split('\t');

                    if(columns[0]!= "snapshot-date")
                    {
                        // Populate the list with Amazon Open listings  
                        Products listing = new Products
                        {
                            Asin = Convert.ToString(columns[3]),
                            ProductName = Convert.ToString(columns[4]),
                            Condition = Convert.ToString(columns[5]),
                            SalesRank = Convert.ToInt32(columns[6]),
                            ProductGroup = Convert.ToString(columns[7]),
                            TotalQuantity = Convert.ToInt32(columns[8]),
                            SellableQuanity = Convert.ToInt32(columns[9]),
                            UnsellableQuantity = Convert.ToInt32(columns[10]),
                            InvAge0To90Days = Convert.ToInt32(columns[11]),
                            InvAge91to181Days = Convert.ToInt32(columns[12]),
                            InvAge181To270Days = Convert.ToInt32(columns[13]),
                            InvAge271To365Days = Convert.ToInt32(columns[14]),
                            InvAge365PlusDays = Convert.ToInt32(columns[15]),
                            UnitsShippedLast24Hrs = Convert.ToInt32(columns[16]),
                            UnitsShippedLast7Days = Convert.ToInt32(columns[17]),
                            UnitsShippedLast30Days = Convert.ToInt32(columns[18]),
                            UnitsShippedLast90Days = Convert.ToInt32(columns[19]),
                            UnitsShippedLast180Days = Convert.ToInt32(columns[20]),
                            UnitsShippedLast365Days = Convert.ToInt32(columns[21]),
                            WeeksOfCoverT7 = Convert.ToString(columns[22]),
                            WeeksOfCoverT30 = Convert.ToString(columns[23]),
                            WeeksOfCoverT90 = Convert.ToString(columns[24]),
                            WeeksOfCoverT180 = Convert.ToString(columns[25]),
                            WeeksOfCoverT365 = Convert.ToString(columns[26]),
                            Currency = Convert.ToString(columns[29]),
                            YourPrice = Convert.ToDecimal(columns[30]),
                            SalesPrice = Convert.ToDecimal(columns[31]),
                        };
                        openList.Add(listing);
                    }
                   
                    // Add the listing to the list  
                    
                }
                reportReader.Close();
            }
            //if (openList != null && openList.Count > 0)
            //    return openList.Skip(1).ToList();
            return openList;
        }
    }
}
