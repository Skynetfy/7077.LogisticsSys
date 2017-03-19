function checkRate(nubmer) {
    var re = /^[0-9]+.?[0-9]*$/; //判断字符串是否为数字 //判断正整数 /^[1-9]+[0-9]*]*$/ 
    if (!re.test(nubmer)) {
        return false;
    }
    return true;
}

var Status_7077 = {
    getOrderStatus: function (s) {
        switch (s) {
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
    },
    getUserStatus: function (s) {
        switch (s) {
            case 0:
                return "待定";
            case 1:
                return "管理员";
            case 2:
                return "客户";
            case 3:
                return "代理商";
            default:
                return "未知";
        }
    },
    getPickupWay: function(s) {
        switch (s) {
            case 0:
                return "自行送货";
            case 1:
                return "上午取货(9点-13点)";
            case 2:
                return "下午取货(13点-17点)";
            default:
                return "未知";
        }
    },
    getGoodsType: function(s) {
        switch (s) {
            case 0:
                return "杂货";
            case 1:
                return "电子邮件";
            default:
                return "未知";
        }
    },
    getCargoTransWay: function(s) {
        switch (s) {
            case 0:
                return "陆运";
            case 1:
                return "空运";
            case 2:
                return "海运";
            default:
                return "未知";
        }
    },
    getPackagingType: function(s) {
        switch (s) {
            case 0:
                return "不打包";
            case 1:
                return "普通打包";
            case 2:
                return "特殊泡沫打包";
            default:
                return "未知";
        }   
    },
    getExpressWay: function(s) {
        switch (s) {
            case 0:
                return "普通正付";
            case 1:
                return "顺丰正付";
            case 2:
                return "普通到付";
            case 3:
                return "顺丰到付";
            default:
                return "未知";
        }
    },
    getPayStatus: function(f) {
        switch (f) {
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
}

