using System;
using System.Collections.Generic;
using System.Collections;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.Collections.ObjectModel;
using System.ServiceModel;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbPortService:IAutoRegister
    {
        IList<PortInfo> ViewPortList(Hashtable Hashtable);
        //IList<PortInfo> ViewPortList(string lineID);
        //ObservableCollection<CarrierInfo> ViewCarrierList(string lineID);
        bool UpdatePortInfo(PortInfo oPort);
        int UpdatePortWaitingforProcessingTime(Hashtable Hashtable);
        bool InsertHisPortInfoResult(PortInfo PortInfo);



        object InsertCassette(Cassette item);
        bool UpdateCassette(Cassette item);
        bool UpdateHisCassette(Cassette item);
        bool UpdateCassetteHasCVD(Cassette item);
         bool UpdateCassetteStartTime(Cassette item);
         bool UpdateCassetteEndTime(Cassette item);
        int DeleteCassetteList(Hashtable map);
        int DeleteCassetteByDateTime();
        IList<Cassette> GetCassetteList(Hashtable Hashtable);
        bool InsertHisCassette(Cassette item);
        //bool InsertCassetteInfo(Cassette Cassette);
        //IList<Cassette> ViewCassetteList(Hashtable Hashtable);
        //bool UpdateCassetteInfo(Cassette Cassette);
        //bool UpdateCarrierInfo(CarrierInfo oCarrier);
        //bool InsertCarrierInfo(CarrierInfo oCarrier);
        //bool InsertCarrierList(IList<CarrierInfo> oCarrier);
        //bool ClearCarrierWipByCarrier(string CarrierID);
        //bool UpdateCarrierWipCarrierID(string oldCarrierID, string newCarrierID);
        //bool ClearCarrierWipByEQP(string eqpID);
        //bool InsertCarrierWip(Hashtable map);
        //bool DeleteCarrierWip(string glassID);

        bool Insertcfg_portgradegroup(cfg_portgradegroup data);

        IList<cfg_portgradegroup> Viewcfg_portgradegroup(Hashtable Hashtable);

        bool Updatecfg_portgradegroup(cfg_portgradegroup data);

        bool Deletecfg_portgradegroup(Hashtable Hashtable);
    }
}
