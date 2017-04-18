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
