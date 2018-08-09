using Core.Utility.Network;
using Cross.StockInfo.Common.Helper;
using Cross.StockInfo.Model.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    public class StockReportService : IStockReportService
    {
        private const string RevenueUrl = "http://mops.twse.com.tw/nas/t21/otc/t21sc03_{0}_{1}_0.html";

        /// <summary>
        /// List all of the revenue of stock
        /// </summary>
        /// <param name="year">民國</param>
        /// <param name="month"></param>
        /// <returns></returns>
        public Task<List<StockRevenue>> ListStockRevenueTaskAsync(int year, int month)
        {
            return Task.Run(async () =>
            {
                string url = string.Format(RevenueUrl, year, month);
                string html = await RestApi.GetHtmlTaskAsync(url);
                var stockRevenueList = HtmlHelper.DescendantsPath(html, "//td/table/tr",
                    node =>
                    {
                        bool? result = node.Attributes["align"]?.Value?.Equals("right");
                        return result.GetValueOrDefault(false);
                    },
                    node =>
                    {
                        string subHtml = node.InnerHtml.Replace("&nbsp;", string.Empty);
                        StockRevenue model = new StockRevenue
                        {
                            Name = HtmlHelper.ReadDocumentValue(subHtml, "//td[2]"),
                            Code = HtmlHelper.ReadDocumentValue(subHtml, "//td[1]"),
                            CurrentRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[3]"),
                            LastMonthRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[4]"),
                            LastYearRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[5]"),
                            MonthOverMonthPercentage = HtmlHelper.ReadDocumentValue(subHtml, "//td[6]"),
                            YearOnYearPercentage = HtmlHelper.ReadDocumentValue(subHtml, "//td[7]"),
                            CurrentAccumulatedRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[8]"),
                            LastYearAccumulatedRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[9]"),
                            AccumulatedRevenueComparePercentage = HtmlHelper.ReadDocumentValue(subHtml, "//td[10]")
                        };

                        return model;
                    });
                return stockRevenueList;
            });


        }
    }
}
