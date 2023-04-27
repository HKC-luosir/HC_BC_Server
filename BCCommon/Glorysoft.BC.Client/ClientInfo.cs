using System;
using System.Collections.ObjectModel;
using Glorysoft.BC.Entity;
using GalaSoft.MvvmLight;
using System.Data;
using System.ComponentModel;

namespace Glorysoft.BC.Client
{
    public class ClientInfo : INotifyPropertyChanged
    {
        #region INofifyPropertyChanged Members
        public static int Quanjuid = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INofifyPropertyChanged Members

        protected void Notify(string propertyName)
        {
            if (PropertyChanged == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }
        private static readonly Lazy<ClientInfo> Lazy = new Lazy<ClientInfo>(() => new ClientInfo());
        public static ClientInfo Current
        {
            get
            {
                return Lazy.Value;
            }
        }
        public HostInfo OClient { get; set; }

        private DataTable pPIDRecipeTable = new DataTable();

        public DataTable PPIDRecipeTable
        {
            get
            {
                return pPIDRecipeTable;
            }
            set
            {
                pPIDRecipeTable = value;
                Notify("PPIDRecipeTable");
            }
        }

    }
}
