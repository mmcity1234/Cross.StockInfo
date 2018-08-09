using Core.Utility.Network;
using Cross.StockInfo.Common.Helper;
using Cross.StockInfo.Model.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.RestClient
{
    public class MopsRestApi
    {
        public const string StockAutoCompleteUrl = "http://mops.twse.com.tw/mops/web/ajax_autoComplete?co_id={0}";

        /// <summary>
        /// Get the stock list by name or code keyword
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public async Task<List<StockBase>> GetStockList(string stock)
        {
            string url = string.Format(StockAutoCompleteUrl, stock);
            string html = await RestApi.GetHtmlTaskAsync(url);

            var stocks = HtmlHelper.DescendantsPath(html, "//div/ul/li/div/div", node =>
            {                
                string code = HtmlHelper.ReadDocumentValue(node.InnerHtml, "//input", "value");
                string name = node.InnerText.Replace(code, string.Empty).Trim();

                return new StockBase { Name = name, Code = code};
            });

            return stocks;            
        }
    }
}
