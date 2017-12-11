using System;
using System.Collections.Generic;
using System.IO;

namespace MyWindowsService
{
    class Parser
    {
        public IList<PurchaseInfo> GetPurchasesInfoFromFile(string path)
        {
            var someParams = Path.GetFileName(path)?.Split('_');
            IList<PurchaseInfo> reports = new List<PurchaseInfo>();
            var reader = new StreamReader(path);
                while (!reader.EndOfStream)
                {
                    var infoParametrs = reader.ReadLine()?.Split(',');
                    if (someParams != null)
                        reports.Add(new PurchaseInfo(someParams[0], someParams[1], infoParametrs?[0], infoParametrs?[1],
                            Convert.ToDouble(infoParametrs?[2])));
                }
            reader.Close();
            return reports;
        }
    }
}
