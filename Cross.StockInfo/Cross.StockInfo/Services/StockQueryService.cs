
using Core.Utility.Network;
using Cross.StockInfo.Common.Helper;
using Cross.StockInfo.Model.Stock;
using Cross.StockInfo.Model.Stock.Category;
using Cross.StockInfo.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public class StockQueryService : IStockQueryService
    {
        private const string MoneyDjUrl = "https://www.moneydj.com";
        /// <summary>
        /// 股市產業分
        /// </summary>
        private const string StockCategoryUrl = "https://www.moneydj.com/Z/ZH/ZHA/ZHA.djhtm";

        public Task<List<StockCategory>> ListStockCtegory()
        {
            return Task.Run(async () =>
            {
                string html = await RestApi.GetHtmlTaskAsync(StockCategoryUrl, Encoding.GetEncoding(950));

                var categoryResults = HtmlHelper.DescendantsPath(html, "//table//tr",
                    node =>
                    {
                        string classValue = node.FirstChild.Attributes["class"]?.Value;
                        return classValue == "t3t1";
                    },
                    node =>
                    {
                        StockCategory mainCatgory = new StockCategory();
                        List<StockCategory> subCategories = HtmlHelper.Descendants(node.InnerHtml, "a", subNode =>
                        {
                            string name = subNode.InnerText.Trim();
                            string url = MoneyDjUrl + subNode.Attributes["href"].Value;
                            // 大項分類
                            if (subNode.ParentNode.Attributes["width"] == null)
                            {
                                mainCatgory.Name = name;
                                mainCatgory.Url = url;
                            }
                            StockCategory child = new StockCategory { Name = name, Url = url };
                            return child;
                        });

                        mainCatgory.AddDetailRange(subCategories);
                        return mainCatgory;
                    });
                return categoryResults;
            });
        }

    }
}
