using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using Glorysoft.BC.Client.ViewModel;

namespace Glorysoft.BC.Client.ViewModel
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }
    }
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }
    }
    public class ViewModelManger
    {
        static ViewModelManger()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService,DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainWindowVM>();
        }
           [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainWindowVM MainVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowVM>();
            }
        }
    
    }

    public class DataItem
    {
        public DataItem(string title)
        {
            Title = title;
        }

        public string Title
        {
            get;
            private set;
        }

        public void SetTitle(string t)
        {
            Title = t;
        }
        public void AsyncSetTitle(string t)
        {
            System.Threading.Thread.Sleep(5000);
        }
    }
}
