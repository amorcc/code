import Vue from 'vue';
//---------------------------------------------------------------------------------
//常用过滤器
//金额
Vue.filter('money', function(value) {
    if (!value) {
        value = 0;
    }
    return '￥' + parseFloat(value).toFixed(2);
});

//  2016-07-07T10:27:13
//  2016-07-07 10:27:13
//  2016-07-07 10:27
//  2016/07/07 10:27:13
//  2016-07-07
//  时间过滤器： 传入 2016-07-07T10:27:13  过滤成： 2016-07-07 10:27:13
Vue.filter('datetime', function(value) {
    if (!value) return '';
    value = (value + 'Z').replace(/\//g, '-').replace(/[\u4E00-\u9FA5]/g, '').replace(/-(\d+)-(\d+)/, function(all, a, b) {
        /^\d$/.test(a) && (a = '0' + a);
        /^\d$/.test(b) && (b = '0' + b);
        return '-' + a + '-' + b;
    }).replace(/(\d+):(\d+):(\d+)/, function(all, a, b, c) {
        let arrTemp = [];
        /^\d$/.test(a) && (a = '0' + a);
        /^\d$/.test(b) && (b = '0' + b);
        /^\d$/.test(c) && (c = '0' + c);
        arrTemp.push(a, b, c);
        return arrTemp.join(':');
    }).replace(/\d(\s+)\d/, function(all, a) {
        if (/^\s+$/.test(a)) {
            return all.replace(a, 'T');
        }
    }); //强制把时间格式加T

    var dt = new Date(new Date(value).toUTCString().replace('GMT', ''));
    var month = parseInt(dt.getMonth()) + 1;
    var day = parseInt(dt.getDate());
    var hours = parseInt(dt.getHours());
    var minutes = parseInt(dt.getMinutes());
    var seconds = parseInt(dt.getSeconds());

    month = month < 10 ? '0' + month : month;
    day = day < 10 ? '0' + day : day;
    hours = hours < 10 ? '0' + hours : hours;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    seconds = seconds < 10 ? '0' + seconds : seconds;

    return dt.getFullYear() + '-' + month + '-' + day + ' ' + hours + ':' + minutes;
});


Vue.filter('OrderStatus', function(value) {
    if (value == -1) {
        return '已取消';
    } else if (value == 1) {
        return '待支付';
    } else if (value == 2) {
        return '已付款';
    } else if (value == 3) {
        return '已出库';
    } else if (value == 31) {
        return '部分出库';
    } else if (value == 4) {
        return '已发货';
    } else if (value == 5) {
        return '已完成';
    }
});