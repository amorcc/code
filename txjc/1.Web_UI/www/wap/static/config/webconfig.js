// var UI_URL = 'http://m.tianxiajiancai.com.cn';
// var API_URL = 'http://api.tianxiajiancai.com.cn';

var G_API_URL = 'http://192.168.1.6:809';
var G_PIC_URL = 'http://192.168.1.6:809/upload/';
// G_API_URL = 'http://api.tianxiajiancai.com.cn';
// G_PIC_URL = 'http://api.tianxiajiancai.com.cn/upload/';

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}