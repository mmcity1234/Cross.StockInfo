using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Cross.StockInfo.Helper
{
    public class ControlHelper
    {
        //public static T FindFirstChildren<T>(DependencyObject rootNode) where T : DependencyObject
        //{
        //    List<T> results = new List<T>();
        //    Stack<DependencyObject> stack = new Stack<DependencyObject>();

        //    int count = VisualTreeHelper.GetChildrenCount(rootNode);
        //    for (int i = 0; i < count; i++)
        //    {
        //        DependencyObject childControl = VisualTreeHelper.GetChild(rootNode, i);
        //        stack.Push(childControl);              
        //    }

        //    while(stack.Count > 0)
        //    {
        //        var control = stack.Pop();
        //        if ((control.GetType()).Equals(typeof(T)) || (control.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
        //        {
        //            return (T)control;                    
        //        }

        //        int childCount = VisualTreeHelper.GetChildrenCount(control);
        //        for (int i = 0; i < childCount; i++)
        //        {
        //            DependencyObject childControl = VisualTreeHelper.GetChild(rootNode, i);
        //            stack.Push(childControl);
        //        }
        //    }
        //    return null;
        //}
    }
}
