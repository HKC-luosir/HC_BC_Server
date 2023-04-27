using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Client.CommonClass;
using GlorySoft.UI;
using System.Windows.Input;
using System.Collections;

namespace Glorysoft.BC.Client.ViewModel
{
    public class GlassHistoryVM: PopupWindow
    {
        public GlassHistoryVM()
        {
            LoadTimeData();
        }
        #region property 
        private string eqpName;
        public string EQPName
        {
            get { return eqpName; }
            set
            {
                eqpName = value;
                RaisePropertyChanged("EQPName");
            }
        }
        private IList<GlassInfo> panelList;
        public IList<GlassInfo> PanelList
        {
            get { return panelList; }
            set
            {
                panelList = value;
                RaisePropertyChanged("PanelList");
            }
        }

        private List<string> glassHourSelect;
        public List<string> GlassHourSelect
        {
            get { return glassHourSelect; }
            set
            {
                glassHourSelect = value;
                RaisePropertyChanged("GlassHourSelect");
            }
        }

        private string panelID = "";
        public string PanelID
        {
            get { return panelID; }
            set
            {
                panelID = value;
                RaisePropertyChanged("PanelID");
            }
        }

        private string glassHourSelectFrom;
        public string GlassHourSelectFrom
        {
            get { return glassHourSelectFrom; }
            set
            {
                glassHourSelectFrom = value;
                RaisePropertyChanged("GlassHourSelectFrom");
            }
        }

        private string glassHourSelectTo;
        public string GlassHourSelectTo
        {
            get { return glassHourSelectTo; }
            set
            {
                glassHourSelectTo = value;
                RaisePropertyChanged("GlassHourSelectTo");
            }
        }

        private DateTime glassSelectDateFrom;
        public DateTime GlassSelectDateFrom
        {
            get { return glassSelectDateFrom; }
            set
            {
                glassSelectDateFrom = value;
                RaisePropertyChanged("GlassSelectDateFrom");
            }
        }

        private DateTime glassSelectDateTo;
        public DateTime GlassSelectDateTo
        {
            get { return glassSelectDateTo; }
            set
            {
                glassSelectDateTo = value;
                RaisePropertyChanged("GlassSelectDateTo");
            }
        }
        #endregion
        private void LoadTimeData()
        {
            glassHourSelect = new List<string>();
            for (var i = 0; i < 25; i++)
            {
                glassHourSelect.Add(i.ToString("00"));
            }

            glassSelectDateFrom = DateTime.Today;
            glassSelectDateTo = DateTime.Today;
            glassHourSelectFrom = "00";
            glassHourSelectTo = "24";
        }

        #region SearchCommand
        private DelegateCommand searchGlassCommand;
        public ICommand SearchGlassCommand
        {
            get
            {
                if (searchGlassCommand == null)
                    searchGlassCommand = new DelegateCommand(SearchGlass);
                return searchGlassCommand;
            }
        }
        private void SearchGlass()
        {
            var sFrom = string.Format("{0} {1}:00:00", glassSelectDateFrom.ToString("yyyy-MM-dd"), glassHourSelectFrom);
            var sTo = string.Format("{0} {1}:00:00", glassSelectDateTo.ToString("yyyy-MM-dd"), glassHourSelectTo);
            var map = new Hashtable
                          {
                              {"PanelID", panelID},
                              {"FromDate", sFrom},
                              {"ToDate", sTo}
                          };
            var list = ClientRequest.GetPanelList(map);
            PanelList = list;
        }

        #endregion
    }
}
