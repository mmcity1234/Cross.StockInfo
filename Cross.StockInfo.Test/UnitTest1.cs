using System;
using Core.Utility.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cross.StockInfo.Test
{
    [TestClass]
    public class UnitTest1
    {
        private const string BDIIndexUrl = "https://fubon-ebrokerdj.fbs.com.tw/Z/ZN/ZNM/CZNM.djbcd?A=FM400007";

        [TestMethod]
        public async void TestMethod1()
        {
            string result = await RestApi.GetTaskAsync<string>(BDIIndexUrl);
            
            Assert.IsNotNull(result);
            Assert.IsNotNull(null);
        }
    }
}
