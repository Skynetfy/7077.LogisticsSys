using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Sys.BLL.Order;
using Sys.Common;
using Sys.Dal;
using Sys.Entities;

namespace Sys.WebUI.Controllers
{
    [Authorize]
    public class ExportController : Controller
    {
        // GET: Export
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            var fileName = file.FileName.Split('.')[0] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + '.' + file.FileName.Split('.')[1];
            var filepath = Path.Combine(Server.MapPath("~/ExcelFiles/Upload/Export_excels"), fileName);
            file.SaveAs(filepath);

            XSSFWorkbook workbook = new XSSFWorkbook(file.InputStream);
            ISheet sheet = workbook.GetSheetAt(0);

            var list = new List<SysExportDataInfo>();
            IRow headerRow = sheet.GetRow(0);//第一行为标题行
            int cellCount = headerRow.LastCellNum;
            var errorMsg = "";
            if (cellCount != 12)
            {
                ViewBag.errorMsg = "导入Excel表格格式不正确！";
                return View();
            }
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                var bl = true;
                var data = new SysExportDataInfo();
                if (row != null && row.Count() == 12)
                {
                    data.TPARCELNO = row.GetCell(0).ToString();
                    if (string.IsNullOrWhiteSpace(data.TPARCELNO))
                    {
                        errorMsg += $"第【{i+1}】行：“T开头订单号”值为空<br>";
                        bl = false;
                    }
                    data.PARCELNO = row.GetCell(1).ToString();
                    if (string.IsNullOrWhiteSpace(data.PARCELNO))
                    {
                        errorMsg += $"第【{i + 1}】行：“购物网站订单号”值为空<br>";
                        bl = false;
                    }
                    data.ProductName = row.GetCell(2).ToString();
                    if (string.IsNullOrWhiteSpace(data.ProductName))
                    {
                        errorMsg += $"第【{i + 1}】行：“商品名称 规格（中文）”值为空<br>";
                        bl = false;
                    }
                    data.COST = Convert.ToDecimal(value: row.GetCell(3)?.ToString() ?? "0");
                    if (data.COST < 0)
                    {
                        errorMsg += $"第【{i + 1}】行：“购买时实际价格”值为空<br>";
                        bl = false;
                    }
                    data.CURRENCY = row.GetCell(4).ToString();
                    if (string.IsNullOrWhiteSpace(data.CURRENCY))
                    {
                        errorMsg += $"第【{i + 1}】行：“货币单位”值为空<br>";
                        bl = false;
                    }
                    data.QUANTITY = row.GetCell(5).ToString();
                    if (string.IsNullOrWhiteSpace(data.QUANTITY))
                    {
                        errorMsg += $"第【{i + 1}】行：“数量”值为空<br>";
                        bl = false;
                    }
                    data.GOODSURL = row.GetCell(6).ToString();
                    if (string.IsNullOrWhiteSpace(data.GOODSURL))
                    {
                        errorMsg += $"第【{i + 1}】行：“购买网址”值为空<br>";
                        bl = false;
                    }
                    data.IdNumber = row.GetCell(7).ToString();
                    if (string.IsNullOrWhiteSpace(data.IdNumber))
                    {
                        errorMsg += $"第【{i + 1}】行：“身份证号码”值为空<br>";
                        bl = false;
                    }
                    data.recipient = row.GetCell(8).ToString();
                    if (string.IsNullOrWhiteSpace(data.recipient))
                    {
                        errorMsg += $"第【{i + 1}】行：“收件人”值为空<br>";
                        bl = false;
                    }
                    data.ADDRESS = row.GetCell(10).ToString();
                    if (string.IsNullOrWhiteSpace(data.ADDRESS))
                    {
                        errorMsg += $"第【{i + 1}】行：“地址”值为空<br>";
                        bl = false;
                    }
                    data.PHONE = row.GetCell(9).ToString();
                    if (string.IsNullOrWhiteSpace(data.PHONE))
                    {
                        errorMsg += $"第【{i + 1}】行：“电话”值为空<br>";
                        bl = false;
                    }
                    data.Courier = row.GetCell(11).ToString();
                    if (string.IsNullOrWhiteSpace(data.Courier))
                    {
                        errorMsg += $"第【{i + 1}】行：“普通快递或者顺丰到付”值为空<br>";
                        bl = false;
                    }

                    if (bl)
                    {
                        data.FileName = fileName;
                        data.CreateDate = DateTime.Now;
                        data.IsDelete = false;
                        list.Add(data);
                    }
                }
            }

            ISysExportDataInfoRepository dao = DALFactory.SysExportDataInfoRepository;
            if (list.Any()) dao.BulkInsert(list);
            ViewBag.errorMsg = $"导入成功，共导入{list.Count}条数据。";
            ViewBag.msg = errorMsg;
            return View(list);
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetListPagerData(string search,
            int offset, int limit, string order, string sort)
        {
            ISysExportDataInfoRepository dao = DALFactory.SysExportDataInfoRepository;
            var btdata = new BootstrapTableData<SysExportDataInfo>();
            var where = "";
            if (!string.IsNullOrWhiteSpace(search))
            {
                where += $" AND (PARCELNO LIKE '%{search}%'" +
                         $" OR TPARCELNO LIKE '%{search}%'" +
                         $" OR ProductName LIKE '%{search}%'" +
                         $" OR FileName LIKE '%{search}%'" +
                         $" OR PHONE LIKE '%{search}%'" +
                         $" OR ADDRESS LIKE '%{search}%'" +
                         $" OR recipient LIKE '%{search}%'" +
                         $" OR [Courier] LIKE '%{search}%'" +
                         $" OR IdNumber LIKE '%{search}%')";
            }
            btdata.rows = dao.GetPagerList(where, offset, limit, order, sort);
            btdata.total = dao.GetPagerCount(where);
            return Json(btdata, JsonRequestBehavior.AllowGet);
        }
        public void Export()
        {
            ISysExportDataInfoRepository dao = DALFactory.SysExportDataInfoRepository;
            var list = dao.GetAll();

            string filename = "export_data_export.xlsx";

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));

            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IDataFormat dataformat = workbook.CreateDataFormat();
            IRow row0 = sheet1.CreateRow(0);
            var cell0 = row0.CreateCell(0);
            cell0.SetCellValue("T开头订单号");
            cell0.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(0, 20 * 256);

            var cell1 = row0.CreateCell(1);
            cell1.SetCellValue("购物网站订单号");
            cell1.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(1, 18 * 256);

            var cell2 = row0.CreateCell(2);
            cell2.SetCellValue("商品名称 规格（中文）");
            cell2.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(2, 22 * 256);

            var cell3 = row0.CreateCell(3);
            cell3.SetCellValue("购买时实际价格");
            cell3.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(3, 15 * 256);

            var cell4 = row0.CreateCell(4);
            cell4.SetCellValue("货币单位");
            cell4.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(4, 12 * 256);

            var cell5 = row0.CreateCell(5);
            cell5.SetCellValue("数量");
            cell5.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(5, 10 * 256);

            var cell6 = row0.CreateCell(6);
            cell6.SetCellValue("购买网址");
            cell6.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(6, 80 * 256);

            var cell7 = row0.CreateCell(7);
            cell7.SetCellValue("身份证号码（下单人或者收件人都行）");
            cell7.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(7, 28 * 256);

            var cell8 = row0.CreateCell(8);
            cell8.SetCellValue("收件人");
            cell8.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(8, 10 * 256);

            var cell9 = row0.CreateCell(9);
            cell9.SetCellValue("电话");
            cell9.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(9, 18 * 256);

            var cell10 = row0.CreateCell(10);
            cell10.SetCellValue("地址");
            cell10.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(10, 38 * 256);

            var cell11 = row0.CreateCell(11);
            cell11.SetCellValue("普通快递或者顺丰到付（二选一）");
            cell11.CellStyle = SetCellStyle(workbook);
            sheet1.SetColumnWidth(11, 20 * 256);

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                IRow row = sheet1.CreateRow(i + 1);
                var vCell0 = row.CreateCell(0);
                vCell0.SetCellValue(item.TPARCELNO);
                vCell0.CellStyle = SetCellStyle(workbook);

                var vCell1 = row.CreateCell(1);
                vCell1.SetCellValue(item.PARCELNO);
                vCell1.CellStyle = SetCellStyle(workbook);

                var vCell2 = row.CreateCell(2);
                vCell2.SetCellValue(item.ProductName);
                vCell2.CellStyle = SetCellStyle(workbook);

                var vCell3 = row.CreateCell(3);
                vCell3.SetCellValue(item.COST.ToString(CultureInfo.InvariantCulture));
                var cellStyle = SetCellStyle(workbook);
                cellStyle.DataFormat = dataformat.GetFormat("0.00");
                vCell3.CellStyle = cellStyle;

                var vCell4 = row.CreateCell(4);
                vCell4.SetCellValue(item.CURRENCY);
                vCell4.CellStyle = SetCellStyle(workbook);

                var vCell5 = row.CreateCell(5);
                vCell5.SetCellValue(item.QUANTITY);
                vCell5.CellStyle = SetCellStyle(workbook);

                var vCell6 = row.CreateCell(6);
                vCell6.SetCellValue(item.GOODSURL);
                vCell6.CellStyle = SetCellStyle(workbook);

                var vCell7 = row.CreateCell(7);
                vCell7.SetCellValue(item.IdNumber);
                vCell7.CellStyle = SetCellStyle(workbook);

                var vCell8 = row.CreateCell(8);
                vCell8.SetCellValue(item.recipient);
                vCell8.CellStyle = SetCellStyle(workbook);

                var vCell9 = row.CreateCell(9);
                vCell9.SetCellValue(item.PHONE);
                vCell9.CellStyle = SetCellStyle(workbook);

                var vCell10 = row.CreateCell(10);
                vCell10.SetCellValue(item.ADDRESS);
                vCell10.CellStyle = SetCellStyle(workbook);

                var vCell11 = row.CreateCell(11);
                vCell11.SetCellValue(item.Courier);
                vCell11.CellStyle = SetCellStyle(workbook);
            }
            var path = Server.MapPath("~/ExcelFiles/export_data.xlsx");
            using (var f = System.IO.File.Create(path))
            {
                workbook.Write(f);
            }
            Response.WriteFile(path);
            Response.Flush();
            Response.End();
        }

        public ICellStyle SetCellStyle(XSSFWorkbook book)
        {
            ICellStyle style = book.CreateCellStyle();
            style.BorderTop = BorderStyle.Thin;//上
            style.BorderBottom = BorderStyle.Thin;//下
            style.BorderLeft = BorderStyle.Thin;//左
            style.BorderRight = BorderStyle.Thin;//右
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Bottom;
            IFont font = book.CreateFont();
            font.FontHeightInPoints = 10; // 字体大小  直接对应Excel中的字体大小
            font.FontName = "微软雅黑"; //跟Excel中的字体值一样，直接写对应的名称即可
            style.SetFont(font);
            return style;
        }

        [HttpGet]
        public ActionResult Weight()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Weight(HttpPostedFileBase file)
        {
            var fileName = file.FileName.Split('.')[0] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + '.' + file.FileName.Split('.')[1];
            var filepath = Path.Combine(Server.MapPath("~/ExcelFiles/Upload/Export_excels"), fileName);
            file.SaveAs(filepath);

            XSSFWorkbook workbook = new XSSFWorkbook(file.InputStream);
            ISheet sheet = workbook.GetSheetAt(0);

            var list = new List<SysAddresserInfo>();
            IRow headerRow = sheet.GetRow(0);//第一行为标题行
            int cellCount = headerRow.LastCellNum;
            var errorMsg = "";
            if (cellCount != 3)
            {
                ViewBag.errorMsg = "导入Excel表格格式不正确！";
                return View();
            }
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                var bl = true;
                if (row != null && row.Count() == 3)
                {
                    var orderNo = row.GetCell(0).ToString();
                    if (string.IsNullOrWhiteSpace(orderNo))
                    {
                        errorMsg += $"第【{i + 1}】行：“T开头订单号”值为空<br>";
                        bl = false;
                    }
                  
                    var goodWeight = Convert.ToDecimal(value: row.GetCell(1)?.ToString() ?? "0");
                    if (goodWeight < 0)
                    {
                        errorMsg += $"第【{i + 1}】行：“重量价格”值为空<br>";
                        bl = false;
                    }

                    var orderAount = Convert.ToDecimal(value: row.GetCell(2)?.ToString() ?? "0");
                    if (orderAount < 0)
                    {
                        errorMsg += $"第【{i + 1}】行：“总金额”值为空<br>";
                        bl = false;
                    }
                    if (bl)
                    {
                        var r = DALFactory.SysAddresserInfoDao.UpdateAddresserByOrderNo(orderNo, goodWeight, orderAount);

                        if (r > 0)
                        {
                            list.Add(new SysAddresserInfo()
                            {
                                GoodsWeight = goodWeight,
                                OrderFrees = orderAount,
                                LogisticsSingle = orderNo
                            });
                        }
                    }
                }
            }
            ViewBag.errorMsg = $"导入成功";
            ViewBag.msg = errorMsg;
            return View(list);
        }
        [HttpGet]
        public ActionResult Logistics()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logistics(HttpPostedFileBase file)
        {
            var fileName = file.FileName.Split('.')[0] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + '.' + file.FileName.Split('.')[1];
            var filepath = Path.Combine(Server.MapPath("~/ExcelFiles/Upload/Export_excels"), fileName);
            file.SaveAs(filepath);

            XSSFWorkbook workbook = new XSSFWorkbook(file.InputStream);
            ISheet sheet = workbook.GetSheetAt(0);

            var list = new List<SysReceiverInfo>();
            IRow headerRow = sheet.GetRow(0);//第一行为标题行
            int cellCount = headerRow.LastCellNum;
            var errorMsg = "";
            if (cellCount != 2)
            {
                ViewBag.errorMsg = "导入Excel表格格式不正确！";
                return View();
            }
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                var bl = true;
                if (row != null && row.Count() == 2)
                {
                    var orderNo = row.GetCell(0).ToString();
                    if (string.IsNullOrWhiteSpace(orderNo))
                    {
                        errorMsg += $"第【{i + 1}】行：“T开头订单号”值为空<br>";
                        bl = false;
                    }

                    var chinaCourierNumber = row.GetCell(1).ToString();
                    if (string.IsNullOrWhiteSpace(orderNo))
                    {
                        errorMsg += $"第【{i + 1}】行：“T开头订单号”值为空<br>";
                        bl = false;
                    }
                    if (bl)
                    {
                        var r = DALFactory.SysAddresserInfoDao.UpdateSysReceiverInfoByOrderNo(orderNo,
                            chinaCourierNumber);

                        if (r > 0)
                        {
                            list.Add(new SysReceiverInfo()
                            {
                                ChinaCourierNumber = chinaCourierNumber,
                                CityName = orderNo
                            });
                        }
                    }
                }
            }
            ViewBag.errorMsg = $"导入成功";
            ViewBag.msg = errorMsg;
            return View(list);
        }
    }
}