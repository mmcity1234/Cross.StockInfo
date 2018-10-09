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
        /// <summary>
        /// 上櫃公司營收列表
        /// </summary>
        private const string OtcRevenueUrl = "http://mops.twse.com.tw/nas/t21/otc/t21sc03_{0}_{1}_0.html";
        /// <summary>
        /// 上市公司營收列表
        /// </summary>
        private const string ListStockRevenueUrl = "http://mops.twse.com.tw/nas/t21/sii/t21sc03_{0}_{1}_0.html";
        /// <summary>
        /// 三大法人買賣超
        /// </summary>
        private const string ListInvestorBuySellUrl = "https://stock.wearn.com/fundthree.asp?mode=search";

        #region Query Revenue

        public Task<List<StockRevenue>> ListOtcRevenueTaskAsync(int year, int month)
        {
            string url = string.Format(OtcRevenueUrl, year, month);
            return ListStockRevenueTaskAsync(url);
        }

        public Task<List<StockRevenue>> ListCompaynRevenueTaskAsync(int year, int month)
        {
            string url = string.Format(ListStockRevenueUrl, year, month);
            return ListStockRevenueTaskAsync(url);

        }


        /// <summary>
        /// List all of the revenue of stock
        /// </summary>
        /// <param name="year">民國</param>
        /// <param name="month"></param>
        /// <returns></returns>
        private Task<List<StockRevenue>> ListStockRevenueTaskAsync(string url)
        {
            return Task.Run(async () =>
            {
                string html = await RestApi.GetHtmlTaskAsync(url, Encoding.GetEncoding(950));
                var stockRevenueList = HtmlHelper.DescendantsPath(html, "//td/table/tr",
                    node =>
                    {
                        bool? result = node.Attributes["align"]?.Value?.Equals("right");
                        bool isSummaryRow = node.FirstChild.Attributes["class"] != null;
                        return result.GetValueOrDefault(false) && !isSummaryRow;
                    },
                    node =>
                    {
                        string subHtml = node.InnerHtml.Replace("&nbsp;", string.Empty);
                        StockRevenue model = new StockRevenue
                        {
                            Name = HtmlHelper.ReadDocumentValue(subHtml, "//td[2]")?.Trim(),
                            Code = HtmlHelper.ReadDocumentValue(subHtml, "//td[1]")?.Trim(),
                            CurrentRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[3]")?.Trim(),
                            LastMonthRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[4]")?.Trim(),
                            LastYearRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[5]")?.Trim(),
                            MonthOverMonthPercentage = HtmlHelper.ReadDocumentValue(subHtml, "//td[6]")?.Trim(),
                            YearOnYearPercentage = HtmlHelper.ReadDocumentValue(subHtml, "//td[7]")?.Trim(),
                            CurrentAccumulatedRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[8]")?.Trim(),
                            LastYearAccumulatedRevenue = HtmlHelper.ReadDocumentValue(subHtml, "//td[9]")?.Trim(),
                            AccumulatedRevenueComparePercentage = HtmlHelper.ReadDocumentValue(subHtml, "//td[10]")?.Trim(),
                            Comment = HtmlHelper.ReadDocumentValue(subHtml, "//td[11]")?.Trim()
                        };

                        return model;
                    });
                return stockRevenueList;
            });
        }
        #endregion

        public Task<List<BuySellPriceItem>> ListInvestorBuySellTaskAsync(int year, int month, int day)
        {
            return Task.Run(async () =>
            {
                var parameters = new Dictionary<string, string>
                {
                    { "yearE", Convert.ToString(year > 1911 ? year - 1911 : year) },
                    { "monthE", Convert.ToString(month) },
                    { "dayE", Convert.ToString(day)}
                };
                string html = await RestApi.PostContentTaskAsync(ListInvestorBuySellUrl, parameters, Encoding.GetEncoding(950));
                var results = HtmlHelper.DescendantsPath(html, "//table//tr",
                    node =>
                    {
                        string classValue = node.Attributes["class"]?.Value;
                        return classValue == "stockalllistbg6" || classValue == "stockalllistbg5";
                    },
                    node =>
                    {
                        string subHtml = node.InnerHtml.Replace("&nbsp;", string.Empty);
                        string date = HtmlHelper.ReadDocumentValue(subHtml, "//td[0]")?.Trim();
                        string foreign = HtmlHelper.ReadDocumentValue(subHtml, "//td[1]//span")?.Trim();
                        string investor = HtmlHelper.ReadDocumentValue(subHtml, "//td[2]//span")?.Trim();
                        string dealer = HtmlHelper.ReadDocumentValue(subHtml, "//td[3]//span")?.Trim();
                        return new BuySellPriceItem { Date = date, ForeignBuySell = foreign, InvestmentBuySell = investor, DealerBuySell = dealer };
                    });
                return results;
            });
        }
    }
}
