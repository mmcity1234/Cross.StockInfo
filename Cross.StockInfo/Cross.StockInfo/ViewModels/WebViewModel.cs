using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cross.StockInfo.ViewModels
{
    public class WebViewModel : BaseViewModel
    {
        private string _htmlContext;
        private string _url;       
        /// <summary>
        /// WebView元件的網頁內容載入來源事件
        /// </summary>
        public event Func<string, Task<string>> HtmlLoadingSourceEvent;

        #region ViewModel

        /// <summary>
        /// 取得網頁顯示的URL路徑或是Html文件內文
        /// </summary>
        public WebViewSource Source
        {
            get
            {
                if (!string.IsNullOrEmpty(Url))
                    return Url;
                else if (!string.IsNullOrEmpty(HtmlContent))
                {
                    var htmlSource = new HtmlWebViewSource { Html = HtmlContent };
                    return htmlSource;
                }
                return null;
            }
        }
        public string HtmlContent
        {
            get => _htmlContext;
            set
            {
                _url = null;
                _htmlContext = value;
                OnPropertyChanged("Source");
            }

        }

        public string Url
        {
            get => _url;
            set
            {
                _htmlContext = null;
                _url = value;
                OnPropertyChanged("Source");
            }
        }
        #endregion


        public override async void OnPageLoading()
        {
            try
            {
                base.OnPageLoading();
                if (HtmlLoadingSourceEvent != null)
                {
                    IsPageLoading = true;
                    HtmlContent = await HtmlLoadingSourceEvent(Url);
                    IsPageLoading = false;
                }
            }
            catch (Exception e)
            {

            }

        }
    }
}
