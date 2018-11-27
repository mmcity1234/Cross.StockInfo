using Core.Utility.Network;
using Cross.StockInfo.Common.Helper;
using Cross.StockInfo.Model.Stock;
using Cross.StockInfo.ViewModels.Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
        /// 三大法人買賣超金額
        /// </summary>
        private const string ListBuySellUrl = "https://stock.wearn.com/fundthree.asp?mode=search";

        /// <summary>
        /// 外資買賣超個股
        /// </summary>
        private const string ListForeignStockRankUrl = "https://stock.wearn.com/a50.asp";
        /// <summary>
        /// 自營商買賣超個股
        /// </summary>
        private const string ListInvesorStockRankUrl = "https://stock.wearn.com/c50.asp";
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

        public Task<List<BuySellPriceItem>> ListBuySellTaskAsync(int year, int month, int day)
        {
            return Task.Run(async () =>
            {
                var parameters = new Dictionary<string, string>
                {
                    { "yearE", Convert.ToString(year > 1911 ? year - 1911 : year) },
                    { "monthE", Convert.ToString(month) },
                    { "dayE", Convert.ToString(day)}
                };
                string html = await RestApi.PostContentTaskAsync(ListBuySellUrl, parameters, Encoding.GetEncoding(950));
                var results = HtmlHelper.DescendantsPath(html, "//table//tr",
                    node =>
                    {
                        string classValue = node.Attributes["class"]?.Value;
                        return classValue == "stockalllistbg6" || classValue == "stockalllistbg5";
                    },
                    node =>
                    {
                        string subHtml = node.InnerHtml.Replace("&nbsp;", string.Empty);
                        string date = HtmlHelper.ReadDocumentValue(subHtml, "//td[1]")?.Trim();
                        string foreign = HtmlHelper.ReadDocumentValue(subHtml, "//td[2]//span")?.Replace(" ", string.Empty).Replace("+", string.Empty);
                        string investor = HtmlHelper.ReadDocumentValue(subHtml, "//td[3]//span")?.Replace(" ", string.Empty).Replace("+", string.Empty);
                        string dealer = HtmlHelper.ReadDocumentValue(subHtml, "//td[4]//span")?.Replace(" ", string.Empty).Replace("+", string.Empty);
                        return new BuySellPriceItem { Date = date, ForeignBuySell = foreign, InvestmentBuySell = investor, DealerBuySell = dealer };
                    });
                return results;
            });
        }

        public Task<StockBuySellListModel> ListDealerStockRankTaskAsync(int top)
        {
            return ListStockRankTaskAsync(top, ListInvesorStockRankUrl);
        }

        /// <summary>
        /// 列出外資買賣超個股的排名資訊
        /// </summary>
        /// <param name="top">指定取得的名次數量</param>
        /// <returns></returns>
        public Task<StockBuySellListModel> ListForeignStockRankTaskAsync(int top)
        {
            return ListStockRankTaskAsync(top, ListForeignStockRankUrl);
        }

        private Task<StockBuySellListModel> ListStockRankTaskAsync(int top, string url)
        {
            return Task.Run(async () =>
            {
                string html = await RestApi.GetHtmlTaskAsync(url, Encoding.GetEncoding(950));

                var rankResults = HtmlHelper.DescendantsPath(html, "//div//div//table//tr",
                  node =>
                  {
                      string classValue = node.Attributes["class"]?.Value;
                      return classValue == "stockalllistbg1" || classValue == "stockalllistbg2";
                  },
                  node =>
                  {
                      int rank = Convert.ToInt32(node.ChildNodes[1]?.InnerText.Trim());
                      string code = node.ChildNodes[3]?.InnerText.Trim();
                      string name = node.ChildNodes[5]?.InnerText.Trim();
                      int buyValue = Convert.ToInt32(node.ChildNodes[7]?.InnerText.Trim());
                      int sellValue = Convert.ToInt32(node.ChildNodes[9]?.InnerText.Trim());

                      StockBuySellItem item = new StockBuySellItem { Rank = rank, Code = code, Name = name, BuyVaule = buyValue, SellValue = sellValue };
                      return item;
                  });

                List<StockBuySellItem> buyResult = rankResults.Where(x => x.Value > 0 && x.Rank <= top).OrderBy(x => x.Rank).ToList();
                List<StockBuySellItem> sellResult = rankResults.Where(x => x.Value < 0 && x.Rank <= top).OrderBy(x => x.Rank).ToList();
                StockBuySellListModel model = new StockBuySellListModel { OverBuyList = buyResult, OverSellList = sellResult };
                return model;
            });
        }
    }
}
