using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Sys.Dal;
using Sys.Entities;

namespace Sys.BLL.Order
{

    public class OrderInfoProvider
    {

        private readonly ISysOrderInfoRepository orderDao = DALFactory.SysOrderInfoDao;
        private readonly ISysAddresserInfoRepository addresserInfoDao = DALFactory.SysAddresserInfoDao;
        private readonly ISysReceiverInfoRepository receiverInfoDao = DALFactory.SysReceiverInfoDao;

        public SysAddresserInfo GetAddressInfoByOid(long id)
        {
            return addresserInfoDao.GetByOrderId(id);
        }

        public SysReceiverInfo GetReceiverInfo(long id)
        {
            return receiverInfoDao.GetByOrderId(id);
        }

        public int UpdateAddressInfo(SysAddresserInfo entity)
        {
            return addresserInfoDao.UpdateAddresser(entity);
        }

        public int UpdateReceiverInfo(SysReceiverInfo entity)
        {
            return receiverInfoDao.Update(entity);
        }

        public SysOrderInfo GetOrderInfoById(long id)
        {
            return orderDao.FindByPk(id);
        }

        public OrderView GetOrderViewById(long id)
        {
            return orderDao.GetOrderViewById(id);
        }

        public int UpdateOrderInfo(SysOrderInfo entity)
        {
            return orderDao.Update(entity);
        }

        public string GetOrderNumber()
        {
            var neworder = OrderNumberRandom.GetOrderNumber();
            if (orderDao.GetOrderByNumber(neworder) != null)
            {
                neworder = GetOrderNumber();
            }
            return neworder;
        }

        public string AddOrderInfo(string username, SysOrderInfo orderinfo, SysAddresserInfo addresserinfo, SysReceiverInfo receiverInfo, ref long status)
        {
            return orderDao.AddOrderInfo(username, orderinfo, addresserinfo, receiverInfo, ref status);
        }

        public int GetOrderViewPagerCount(string search)
        {
            return orderDao.GetOrderViewPagerCount(search);
        }

        public List<OrderView> GetOrderViewPagerList(string search, int offset, int limit, string order, string sort)
        {
            return orderDao.GetOrderViewPagerList(search, offset, limit, order, sort).ToList();
        }

        public static string GetStatusString(int n)
        {
            switch (n)
            {
                case -1:
                    return "已删除";
                case 0:
                    return "未提交";
                case 1:
                    return "待处理";
                case 2:
                    return "已处理";
                case 3:
                    return "未付款";
                case 4:
                    return "已付款";
                case 5:
                    return "未收款";
                case 6:
                    return "已收款";
                case 7:
                    return "未发货";
                case 8:
                    return "已发货";
                case 9:
                    return "已完成";
                case 10:
                    return "已失败";

                default:
                    return "未知";
            }
        }

        public static string GetPickupWayString(int n)
        {
            switch (n)
            {
                case 1:
                    return "自行送货";
                case 2:
                    return "上午取货(9点-13点)";
                case 3:
                    return "下午取货(13点-17点)";
                default:
                    return "未知";
            }
        }

        public static string GetCargoTransWayString(int n)
        {
            switch (n)
            {
                case 1:
                    return "陆运";
                case 2:
                    return "空运";
                case 3:
                    return "海运";
                default:
                    return "未知";
            }
        }

        public static string GetPackagingTypeString(int n)
        {
            switch (n)
            {
                case 1:
                    return "不打包";
                case 2:
                    return "普通打包";
                case 3:
                    return "特殊泡沫打包";
                default:
                    return "未知";
            }
        }

        public static string GetExpressWayString(int n)
        {
            switch (n)
            {
                case 1:
                    return "普通正付";
                case 2:
                    return "顺丰正付";
                case 3:
                    return "普通到付";
                case 4:
                    return "顺丰到付";
                default:
                    return "未知";
            }
        }

        public static string GetPayStatusString(int n)
        {
            switch (n)
            {
                case 0:
                    return "未付款";
                case 1:
                    return "已付款";
                case 2:
                    return "未收款";
                case 3:
                    return "已收款";
                default:
                    return "未知";
            }
        }
        public void ExportOrderDataList(List<OrderView> dataList)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet s1 = workbook.CreateSheet("Sheet1");

            ICellStyle rowstyle = workbook.CreateCellStyle();
            rowstyle.FillForegroundColor = IndexedColors.Red.Index;
            rowstyle.FillPattern = FillPattern.SolidForeground;

            ICellStyle c1Style = workbook.CreateCellStyle();
            c1Style.FillForegroundColor = IndexedColors.Yellow.Index;
            c1Style.FillPattern = FillPattern.SolidForeground;

            IRow r1 = s1.CreateRow(1);
            IRow r2 = s1.CreateRow(2);
            r1.RowStyle = rowstyle;
            r2.RowStyle = rowstyle;

            ICell c1 = r2.CreateCell(2);
            c1.CellStyle = c1Style;
            c1.SetCellValue("Test");

            ICell c4 = r2.CreateCell(4);
            c4.CellStyle = c1Style;
            using (var fs = File.Create(@"E:\test.xlsx"))
            {
                workbook.Write(fs);
            }
        }
    }
}
