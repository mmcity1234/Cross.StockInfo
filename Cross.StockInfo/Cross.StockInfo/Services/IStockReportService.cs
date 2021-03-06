﻿using Cross.StockInfo.Model.Stock;
using Cross.StockInfo.ViewModels.Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Services
{
    /// <summary>
    /// 查詢台股營收與財報等整體資訊服務
    /// </summary>
    public interface IStockReportService
    {

        /// <summary>
        /// List all of the OTC revenue
        /// </summary>
        /// <param name="year"民國></param>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<List<StockRevenue>> ListOtcRevenueTaskAsync(int year, int month);


        /// <summary>
        /// List all of the company revenue
        /// </summary>
        /// <param name="year">民國</param>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<List<StockRevenue>> ListCompaynRevenueTaskAsync(int year, int month);

        /// <summary>
        /// 法人買賣超金額資訊
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        Task<List<BuySellPriceItem>> ListBuySellTaskAsync(int year, int month, int day);

        /// <summary>
        /// 列出外資買賣超個股的排名資訊
        /// </summary>
        /// <param name="top">指定取得的名次數量</param>
        /// <returns></returns>
        Task<StockBuySellListModel> ListForeignStockRankTaskAsync(int top);

        /// <summary>
        /// 列出自營商買賣超個股的排名資訊
        /// </summary>
        /// <param name="top">指定取得的名次數量</param>
        /// <returns></returns>
        Task<StockBuySellListModel> ListDealerStockRankTaskAsync(int top);

        /// <summary>
        /// 列出主力五日買賣超個股排名資訊
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        Task<StockBuySellListModel> ListPrimaryStockRankTaskAsync(int top);
    }
}
