
using Android.OS;

using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Content.Res;
using Cross.StockInfo.Views;
using Android.Widget;
using Android.Content;

[assembly: ExportRenderer(typeof(TabViewPage), typeof(StyledTabbedPageRenderer))]

namespace Cross.StockInfo.Views
{
    public class StyledTabbedPageRenderer : TabbedPageRenderer
    {
        private Android.Views.View formViewPager = null;
        private TabLayout tabLayout = null;


        public StyledTabbedPageRenderer(Context context): base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            this.tabLayout = (TabLayout)this.GetChildAt(1);

            changeTabsFont();
        }

        private void changeTabsFont()
        {
            //Typeface font = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "fonts/" + Constants.FontStyle);
            ViewGroup vg = (ViewGroup)tabLayout.GetChildAt(0);
            int tabsCount = vg.ChildCount;
            for (int j = 0; j < tabsCount; j++)
            {
                ViewGroup vgTab = (ViewGroup)vg.GetChildAt(j);
                int tabChildsCount = vgTab.ChildCount;
                for (int i = 0; i < tabChildsCount; i++)
                {
                    Android.Views.View tabViewChild = vgTab.GetChildAt(i);
                    if (tabViewChild is TextView)
                    {
                        //((TextView)tabViewChild).Typeface = font;
                        ((TextView)tabViewChild).TextSize = 10;
                        

                    }
                }
            }
        }
    }
}
