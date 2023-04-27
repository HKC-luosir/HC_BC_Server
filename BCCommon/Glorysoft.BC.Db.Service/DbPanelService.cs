using System;
using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using System.Linq;

namespace Glorysoft.BC.Db.Service
{
    public class DbPanelService : AbstractDbService, IDbPanelService
    {
        public IList<GlassInfo> GetGlassInfoList(Hashtable map)
        {
            var panelInfos = ExecuteQueryForList<GlassInfo>("GetGlassInfoList", map).ToList();
          
          
            return panelInfos;//ExecuteQueryForList<PanelInfo>("GetPanelList", map) ?? new List<PanelInfo>();
        }
        public IList<GlassInfo> GetGlassInfoListByCstID(Hashtable map)
        {
            var panelInfos = ExecuteQueryForList<GlassInfo>("GetGlassInfoListByCstID", map).ToList();

            return panelInfos;//ExecuteQueryForList<PanelInfo>("GetPanelList", map) ?? new List<PanelInfo>();
        }
        public IList<GlassInfo> GetGlassInfoListByAlarm(Hashtable map)
        {
            var panelInfos = ExecuteQueryForList<GlassInfo>("GetGlassInfoListByAlarm", map).ToList();

            return panelInfos;//ExecuteQueryForList<PanelInfo>("GetPanelList", map) ?? new List<PanelInfo>();
        }
        //public GlassInfo GetGlassInfo(Hashtable map)
        //{
        //    return ExecuteQueryForObject<GlassInfo>("GetGlassInfo", map) ?? new GlassInfo();
        //}

        public bool InsertHisGlassInfo(GlassInfo item)
        {
            return ExecuteInsert("InsertHisGlassInfo", item);
        }
        public object InsertGlassInfo(GlassInfo item)
        {
           //  ExecuteDelete("DeleteGlassInfo", item.GlassID);
            return ExecuteQueryForObject("InsertGlassInfo", item);
        }
        public bool UpdateGlassInfo(GlassInfo item)
        {
            return ExecuteUpdate("UpdateGlassInfo", item) == 1 ? true : false;
        }
        public bool UpdateHisGlassInfo(GlassInfo item)
        {
            return ExecuteUpdate("UpdateHisGlassInfo", item) == 1 ? true : false;
        }
        public bool UpdateGlassModelPosition(GlassInfo item)
        {
            return ExecuteUpdate("UpdateGlassModelPosition", item) == 1 ? true : false;
        }
        public int UpdateGlassSlotSatus(Hashtable map)
        {
            return ExecuteUpdate("UpdateGlassSlotSatus", map);
        }

        public int UpdateWIPSlotSatus(Hashtable map)
        {
            return ExecuteUpdate("UpdateWIPSlotSatus", map);
        }
        public int UpdateGlassCVDFlag(Hashtable map)
        {
            return ExecuteUpdate("UpdateGlassCVDFlag", map);
        }

        public bool UpdateGlassInfoBeginDate(GlassInfo item)
        {
            return ExecuteUpdate("UpdateGlassInfoBeginDate", item) == 1 ? true : false;
        }
        public bool UpdateGlassInfoEndDate(GlassInfo item)
        {
            return ExecuteUpdate("UpdateGlassInfoEndDate", item) == 1 ? true : false;
        }
        public bool UpdateGlassInfoFetchDatetime(GlassInfo item)
        {
            return ExecuteUpdate("UpdateGlassInfoFetchDatetime", item) == 1 ? true : false;
        }
        //public bool UpdatePanelGrade(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdatePanelGrade", map) == 1 ? true : false;
        //}
        //public int DeleteGlassInfo(GlassInfo item)
        //{
        //    return ExecuteDelete("DeleteGlassInfo", item.GLSID);
        //}
        public int DeleteGlassInfoList(Hashtable map)
        {
            return ExecuteDelete("DeleteGlassInfoList", map);
        }
        public int DeleteGlassInforByDateTime()
        {
            DateTime datetime = DateTime.Now.AddDays(-3);
            var stringTime = datetime.ToString("yyyy-MM-dd");
            return ExecuteDelete("DeleteGlassInforByDateTime", stringTime);
        }
     
        
        #region JobData 
        //public IList<JobData> GetJobDataList(Hashtable map)
        //{
        //    var panelInfos = ExecuteQueryForList<JobData>("GetJobDataList", map).ToList();            
        //    return panelInfos;
        //}
        //public bool InsertJobData(JobData item)
        //{
        //    Hashtable map = new Hashtable();
        //    map.Add("GlassID", item.GlassID);
        //    ExecuteDelete("DeleteJobDataList",map );
        //    return ExecuteInsert("InsertJobData", item);
        //}
        //public bool UpdateJobData(JobData item)
        //{
        //    return ExecuteUpdate("UpdateJobData", item) >= 1 ? true : false;
        //}
        //public int DeleteJobDataList(Hashtable map)
        //{
        //    return ExecuteDelete("DeleteJobDataList", map);
        //}
        #endregion

        #region  TrayInfo
        public IList<TrayInfo> GetTrayInfoList(Hashtable map)
        {
            var TrayInfos = ExecuteQueryForList<TrayInfo>("GetTrayInfoList", map).ToList();
            return TrayInfos;//ExecuteQueryForList<PanelInfo>("GetPanelList", map) ?? new List<PanelInfo>();
        }

        public bool InsertTrayInfo(TrayInfo item)
        {
            ExecuteDelete("DeleteTrayInfo", item.TRAYID);
            return ExecuteInsert("InsertTrayInfo", item);
        }
        public bool UpdateTrayInfo(TrayInfo item)
        {
            return ExecuteUpdate("UpdateTrayInfo", item) == 1 ? true : false;
        }
       
        public int DeleteTrayInfo(TrayInfo item)
        {
            return ExecuteDelete("DeleteTrayInfo", item.TRAYID);
        }
        public int DeleteTrayInfoList(Hashtable map)
        {
            return ExecuteDelete("DeleteTrayInfoList", map);
        }
        public int DeleteTrayInfoByDateTime(string datetime)
        {

            return ExecuteDelete("DeleteTrayInfoByDateTime", datetime);
        }
        #endregion

        #region  MaskInfo
        public IList<MaskInfo> GetMaskInfoList(Hashtable map)
        {
            var MaskInfos = ExecuteQueryForList<MaskInfo>("GetMaskInfoList", map).ToList();
            return MaskInfos;//ExecuteQueryForList<PanelInfo>("GetPanelList", map) ?? new List<PanelInfo>();
        }

        public bool InsertMaskInfo(MaskInfo item)
        {
            ExecuteDelete("DeleteMaskInfo", item.MASKID);
            return ExecuteInsert("InsertMaskInfo", item);
        }
        public bool UpdateMaskInfo(MaskInfo item)
        {
            return ExecuteUpdate("UpdateMaskInfo", item) == 1 ? true : false;
        }

        public int DeleteMaskInfo(MaskInfo item)
        {
            return ExecuteDelete("DeleteMaskInfo", item.MASKID);
        }
        public int DeleteMaskInfoList(Hashtable map)
        {
            return ExecuteDelete("DeleteMaskInfoList", map);
        }
        public int DeleteMaskInfoByDateTime(string datetime)
        {

            return ExecuteDelete("DeleteMaskInfoByDateTime", datetime);
        }
        #endregion

        //public IList<SPanelInfo> GetSPanelList(Hashtable map)
        //{
        //    return ExecuteQueryForList<SPanelInfo>("GetSPanelList", map) ?? new List<SPanelInfo>();
        //}
        //public bool InsertSPanelInfo(SPanelInfo item)
        //{
        //   // ExecuteDelete("DeleteSPanelInfo", item.SPnlID);
        //    return ExecuteInsert("InsertSPanelInfo", item);
        //}
        //public bool UpdateSPanelInfo(SPanelInfo item)
        //{
        //    return ExecuteUpdate("UpdateSPanelInfo", item) == 1 ? true : false;
        //}
        //public int DeleteSPanelInfo(SPanelInfo item)
        //{
        //    return ExecuteDelete("DeleteSPanelInfo", item.HPanelID);
        //}
        //public SPanelInfo FindPanelInfo(string panelID)
        //{
        //    return ExecuteQueryForObject<SPanelInfo>("FindPanelInfo", panelID) ?? new SPanelInfo { HPanelID = panelID };
        //}
        //public IList<SPanelInfo> GetPanelListByCSTBoxID(string cstBoxID)
        //{
        //    return ExecuteQueryForList<SPanelInfo>("GetPanelListByCSTBoxID", cstBoxID) ?? new List<SPanelInfo>();
        //}
        //public bool UpdatePanelCSTBoxID(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdatePanelCSTBoxID", map) == 1 ? true : false;
        //}

        //public bool UpdatePanelToSlotInfo(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdatePanelToSlotInfo", map) == 1 ? true : false;
        //}
        //public IList<PanelInCSTBox> GetPanelListByBoxID(string boxID)
        //{
        //    return ExecuteQueryForList<PanelInCSTBox>("GetPanelListByBoxID", boxID) ?? new List<PanelInCSTBox>();
        //}

        //public IList<CarrierWip> GetCarrierWipByCarrierID(string carrierID)
        //{
        //    return ExecuteQueryForList<CarrierWip>("GetCarrierWipByCarrierID", carrierID) ?? new List<CarrierWip>();
        //}

        //public bool UpdatePanelInfoByMESReply(SPanelInfo panelInfo)
        //{
        //    return ExecuteUpdate("UpdatePanelInfoByMESReply", panelInfo) == 1 ? true : false;
        //}
        //public bool UpdatePanelInfoByLabelReply(SPanelInfo panelInfo)
        //{
        //    return ExecuteUpdate("UpdatePanelInfoByLabelReply", panelInfo) == 1 ? true : false;
        //}

        //public bool InsertPackingBoxInfo(PackingBoxInfo info)
        //{
        //    return ExecuteInsert("InsertPackingBoxInfo", info);
        //}
        //public bool UpdatePackingBoxInfoByMESReply(PackingBoxInfo info)
        //{
        //    var ret = ExecuteUpdate("UpdatePackingBoxInfoByMESReply", info) == 1 ? true : false;
        //    if (!ret)
        //    {
        //        ret = ExecuteInsert("InsertPackingBoxInfo", info);
        //    }
        //    return ret;
        //}
        //public bool InsertPanelInCSTBox(PanelInCSTBox panelInfo)
        //{
        //    ExecuteDelete("DeletePanelInCSTBox", panelInfo.PanelID);
        //    return ExecuteInsert("InsertPanelInCSTBox", panelInfo);
        //}
        //public bool DeletePanelInCSTBox(string panelID)
        //{
        //    return ExecuteDelete("DeletePanelInCSTBox", panelID) == 1 ? true : false;
        //}
        //public bool DeleteCSTBoxByBoxID(string boxID)
        //{
        //    return ExecuteDelete("DeleteCSTBoxByBoxID", boxID) == 1 ? true : false;
        //}
        //public bool UpdatePackingBoxInfoByLabelReply(PackingBoxInfo info)
        //{
        //    var ret = ExecuteUpdate("UpdatePackingBoxInfoByLabelReply", info) == 1 ? true : false;
        //    if (!ret)
        //    {
        //        ret = ExecuteInsert("InsertPackingBoxInfo", info);
        //    }
        //    return ret;
        //}       
        //public bool UpdatePackingBoxInfoTranID(Hashtable map)
        //{
        //    var ret = ExecuteUpdate("UpdatePackingBoxInfoTranID", map) == 1 ? true : false;
        //    if (!ret)
        //    {
        //        PackingBoxInfo info = new PackingBoxInfo { BoxID = map[(object)("BoxID")].ToString(), TranID = map[(object)("TranID")].ToString() };
        //        ret = ExecuteInsert("InsertPackingBoxInfo", info);
        //    }
        //    return ret;
        //}
        //public bool UpdatePackingBoxWeight(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdatePackingBoxWeight", map) == 1 ? true : false;
        //}
        //public bool UpdatePackingBoxPalletID(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdatePackingBoxPalletID", map) == 1 ? true : false;
        //}
        //public PackingBoxInfo FindPackingBoxInfo(string boxid)
        //{
        //    return ExecuteQueryForObject<PackingBoxInfo>("FindPackingBoxInfo", boxid) ?? new PackingBoxInfo { BoxID = boxid };
        //}
        //public bool UpdatePanelNGType(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdatePanelNGType", map) == 1 ? true : false;
        //}
        //public bool UpdatePanelBLUID(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdatePanelBLUID", map) == 1 ? true : false;
        //}

        //public bool UpdatePanelTCONID(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdatePanelTCONID", map) == 1 ? true : false;
        //}
        //public bool UpdatePanelPalletID(Hashtable map)
        //{
        //    ExecuteUpdate("ClearPalletID", map);
        //    return ExecuteUpdate("UpdatePanelPalletID", map) == 1 ? true : false;
        //}
        //public bool UpdatePrintBoxIDByBuyBox(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdatePrintBoxIDByBuyBox", map) == 1 ? true : false;
        //}
        //public bool UpdateOQAResult(Hashtable map)
        //{
        //    return ExecuteUpdate("UpdateOQAResult", map) == 1 ? true : false;
        //}
        #region CGT
        public IList<GlassInfo> GetUnitIdGlassInfoList(Hashtable map)
        {
            return ExecuteQueryForList<GlassInfo>("GetUnitIdGlassInfoList", map);
        }
        #endregion
    }
}
