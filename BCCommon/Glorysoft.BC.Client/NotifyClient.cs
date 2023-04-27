using Glorysoft.BC.Client.CommonClass;
using Glorysoft.BC.Client.ViewModel;
using Glorysoft.BC.Entity;
using Glorysoft.BC.WCF.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Glorysoft.BC.Client
{
    public class NotifyClient : INotifyClient
    {
        public void UpdateControlState(string ControlState)
        {
            ClientInfo.Current.OClient.ControlState = ControlState;
        }

        public void UpdateHostConnectState(bool ConnectState)
        {
            ClientInfo.Current.OClient.IsHostConnect = ConnectState;
        }
        public void UpdateFDCConnectState(bool ConnectState)
        {
            ClientInfo.Current.OClient.IsFDCConnect = ConnectState;
        }
        public void UpdateFACConnectState(bool ConnectState)
        {
            ClientInfo.Current.OClient.IsFACConnect = ConnectState;
        }
        public void ReceiceHostMsg(IList<string> list)
        {
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item)) continue;
                ClientInfo.Current.OClient.MsgList.Insert(0, DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss") + "---" + item);
            }
        }
        public void OpenLotInfoWindow(CarrierInfo oCarrier)
        {
            Controller.ShowSubWindow<LotInformationVM>(new LotInformationVM(oCarrier));
        }
        public void UpdateClientCarrierInfo(CarrierInfo oCarrier)
        {
            ClientInfo.Current.OClient.CarrierList.Remove(ClientInfo.Current.OClient.CarrierList.FirstOrDefault(f => f.EQPID == oCarrier.EQPID&&f.PortID==oCarrier.PortID));//新的Carrier可能ID已经被清除了，所以不能以CarrierID来删除
            ClientInfo.Current.OClient.CarrierList.Add(oCarrier);
        }
        public void UpdateClientDenseCurrentLotQTY(int qty)
        {
            
        }
        public void UpdateClientEQPStatus(EQPInfo oEQP)
        {
            try
            {
                //ClientInfo.Current.OClient.EQPList[oEQP.EQPName].IsConnect = oEQP.IsConnect;
                //ClientInfo.Current.OClient.EQPList[oEQP.EQPName].EQPStatus = oEQP.EQPStatus;
                //ClientInfo.Current.OClient.EQPList[oEQP.EQPName].ReasonCode = oEQP.ReasonCode;
                //if (oEQP.HasUnit)
                //{
                //    foreach (var item in oEQP.UnitList)
                //    {
                //        ClientInfo.Current.OClient.EQPList[oEQP.EQPName].UnitList[item.Key].UnitStatus = item.Value.UnitStatus;
                //        ClientInfo.Current.OClient.EQPList[oEQP.EQPName].UnitList[item.Key].ReasonCode = item.Value.ReasonCode;
                //    }
                //}
            }
            catch (Exception ex)
            {

            }

        }

        public void UpdateClientCurrentOQASamplingCount(OQASamplingRule sampingRule)
        {
            try
            {
                var oOQA = ClientInfo.Current.OClient.OQASamplingRuleList.FirstOrDefault(f => f.RevisionCode == sampingRule.RevisionCode);
                oOQA.CurrentCount = sampingRule.CurrentCount;
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateClientWorkOrderAndFGCode(string workOrder,string fgCode)
        {
            try
            {
                ClientInfo.Current.OClient.CurrentWorkOrder = workOrder;
                ClientInfo.Current.OClient.CurrentFGCode = fgCode;
            }
            catch (Exception ex)
            {

                
            }
        }
    }
}
