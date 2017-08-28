using Domain.Models;
using log4net;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public class OrderAPI
    {
        ILog log = LogManager.GetLogger("log");
        public List<WaveApi> GetWaveAPI(string domainName,string Processcenterid,DateTime DateS,DateTime DateE)
        {
            try
            {
                string url = domainName + "/div/divide/getUnDividingOBPData";
                var request = new GetOrderRequest
                {
                    // Token = "5A9C85B6E068F2236A039E6157C5DF5B",
                    processCenterId = Processcenterid,
                    beginUpdateTime = DateS.ToString("yyyy-MM-dd HH:mm:ss"),
                    endUpdateTime = DateE.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                var result = new Common.HttpHelper().Post<WaveApiResponse>(url, request);
                if (result.isSuccess)
                {
                    return result.data;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("GetWaveAPI", ex);
                return null;
            }
        }
        public bool CompleteWaveAPI(string domainName,string waveNo, List<SortingDetailView> data,int OperatorID)
        {
            try
            {
                string url = domainName + "/div/divide/savelog";
                var request = new CompleteWaveRequest
                {
                    waveNo = waveNo,
                    operatorID = OperatorID,
                    operatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    list = new List<ShortageApi>()
                };
                data.ForEach(o =>
                {
                    request.list.Add(new ShortageApi
                    {
                        orderNO = o.OrderNo,
                        num = o.WaitNum,
                        pickingQuantity = o.PutNum,
                        productcode = o.ProductCode,
                        propertyCode = o.PropertyCode
                    });
                });
                var result = new Common.HttpHelper().Post<CompleteWaveResponse>(url, request);
                return result.IsSuccess;
            }catch(Exception ex)
            {
                log.Error("CompleteWaveAPI",ex);
                return false;
            }
        }
    }
}
