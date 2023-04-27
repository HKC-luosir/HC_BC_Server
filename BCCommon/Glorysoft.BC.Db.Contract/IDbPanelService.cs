using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.Collections;
using System.ServiceModel;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbPanelService : IAutoRegister
    {
        bool InsertHisGlassInfo(GlassInfo item);
         IList<GlassInfo> GetGlassInfoList(Hashtable map);
        IList<GlassInfo> GetGlassInfoListByCstID(Hashtable map);
        IList<GlassInfo> GetGlassInfoListByAlarm(Hashtable map);
         //GlassInfo GetGlassInfo(Hashtable map);
         object InsertGlassInfo(GlassInfo item);
         bool UpdateGlassInfo(GlassInfo item);
        bool UpdateHisGlassInfo(GlassInfo item);
        bool UpdateGlassModelPosition(GlassInfo item);
        int UpdateGlassSlotSatus(Hashtable map);
        int UpdateWIPSlotSatus(Hashtable map);
        int UpdateGlassCVDFlag(Hashtable map);
       // int UpdateGlassSlotSatus(string sql);
        bool UpdateGlassInfoBeginDate(GlassInfo item);
         bool UpdateGlassInfoEndDate(GlassInfo item);
         bool UpdateGlassInfoFetchDatetime(GlassInfo item);
        //int DeleteGlassInfo(GlassInfo item);
        int DeleteGlassInfoList(Hashtable map);
        int DeleteGlassInforByDateTime();
        #region JobData 
        //IList<JobData> GetJobDataList(Hashtable map);
        //bool InsertJobData(JobData item);
        //bool UpdateJobData(JobData item);
        //int DeleteJobDataList(Hashtable map);
        #endregion

        #region  TrayInfo
        IList<TrayInfo> GetTrayInfoList(Hashtable map);

        bool InsertTrayInfo(TrayInfo item);
        bool UpdateTrayInfo(TrayInfo item);

        int DeleteTrayInfo(TrayInfo item);
        int DeleteTrayInfoList(Hashtable map);
        int DeleteTrayInfoByDateTime(string datetime);
        #endregion

        #region  MaskInfo
        IList<MaskInfo> GetMaskInfoList(Hashtable map);

        bool InsertMaskInfo(MaskInfo item);
        bool UpdateMaskInfo(MaskInfo item);

        int DeleteMaskInfo(MaskInfo item);
        int DeleteMaskInfoList(Hashtable map);
        int DeleteMaskInfoByDateTime(string datetime);
        #endregion
        //IList<SPanelInfo> GetSPanelList(Hashtable map);
        //bool InsertSPanelInfo(SPanelInfo item);
        //bool UpdateSPanelInfo(SPanelInfo item);
        //int DeleteSPanelInfo(SPanelInfo item);

        // IList<SPanelInfo> GetPanelList(Hashtable map);
        // SPanelInfo FindPanelInfo(string panelID);
        //  SPanelInfo FindPanelInfoByPalletID(string palletID);
        //  SPanelInfo FindPanelInfoByModuleID(string moduleID);
        //  IList<SPanelInfo> GetPanelListByCSTBoxID(string boxid);
        // bool UpdatePanelCSTBoxID(Hashtable map);
        // IList<CarrierWip> GetCarrierWipByCarrierID(string carrierID);
        // bool InsertPanelInfo(SPanelInfo item);
        // bool UpdatePanelToSlotInfo(Hashtable map);
        //bool UpdatePanelInfoByMESReply(SPanelInfo panelInfo);
        //bool InsertPanelInCSTBox(PanelInCSTBox panelInfo);
        //bool DeleteCSTBoxByBoxID(string boxID);
        //bool InsertPackingBoxInfo(PackingBoxInfo info);
        //bool UpdatePackingBoxInfoByMESReply(PackingBoxInfo info);
        //bool UpdatePackingBoxInfoByLabelReply(PackingBoxInfo info);
        //bool UpdatePackingBoxInfoTranID(Hashtable map);
        //bool UpdatePackingBoxWeight(Hashtable map);
        //bool UpdatePackingBoxPalletID(Hashtable map);
        //PackingBoxInfo FindPackingBoxInfo(string boxid);
        //bool UpdatePanelNGType(Hashtable map);
        //bool UpdatePrintBoxIDByBuyBox(Hashtable map);
        #region CGT
        IList<GlassInfo> GetUnitIdGlassInfoList(Hashtable map);
        #endregion
    }
}
