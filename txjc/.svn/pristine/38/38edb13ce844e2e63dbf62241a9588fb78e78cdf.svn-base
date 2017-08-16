exports.install = function(Vue, options) {
    //--------------------------------
    //全局常量定义
    //--------------------------------
    Vue.prototype.PAGE_SIZE = 15;
    // Vue.prototype.API_URL = 'http://192.168.1.6:809';
    // Vue.prototype.API_URL = 'http://api.tianxiajiancai.com.cn';
    Vue.prototype.API_URL = 'http://100.64.3.160:809';
    // Vue.prototype.PIC_URL = 'http://192.168.1.6:809/upload/';
    Vue.prototype.PIC_URL = 'http://api.tianxiajiancai.com.cn/upload/';
    //Vue.prototype.API_URL = 'http://api.tianxiajiancai.com.cn';
    Vue.prototype.WX_REDIRECT_URL = 'http://api.tianxiajiancai.com.cn/upload/';
    //--------------------------------
    // 获取数据
    //--------------------------------
    Vue.prototype.showTips = function(content, state, callback, time) {
        content = content || '操作成功';
        time = parseInt(time) || 2500;
        var box = document.createElement('div');
        box.className = 'popup-tips';

        var htmlIcon = '<span class="icon"></span>';
        if (state === 'cancel' || state === 'error') {
            htmlIcon = '<span class="icon p-error"></span>';
        }
        box.innerHTML = htmlIcon + '<div class="content">' + content + '</div>';
        document.body.appendChild(box);

        var _close = function() {
            document.body.removeChild(box);
        };

        setTimeout(function() {
            _close();
            typeof state === 'function' ? state() : typeof callback === 'function' && callback();
        }, parseInt(time));
    };
    //--------------------------------
    // 获取数据
    //--------------------------------
    Vue.prototype.fetchData = function(arg) {
        let me = this;

        let cmd = arg.cmd;
        let para = arg.para;
        let url = this.API_URL + cmd;

        let token = localStorage.getItem('token');

        para.token = token;

        var callback = function(data) {
            let d = data;

            if (d) {
                switch (parseInt(d.ResponseID)) {
                    case 0:
                        if (d.Message) {
                            me.showTips(d.Message, 'success');
                        }
                        dataList = d.Data;
                        //执行回执操作
                        if (typeof arg.callback == 'function') {
                            if (dataList == undefined) {
                                dataList = '';
                            }
                            arg.callback(dataList);
                        } else {
                            console.info('callback 需要方法类型，请检查');
                        }
                        break;
                    case 1:
                        if (me.getToken() != '') {
                            me.showTips('登录状态异常，请重新登录!', 'error');
                        }
                        //location.href = '/#/login/1';
                        //me.$router.go('/login');
                        me.$router.push({ path: '/login' });
                        break;
                    default:
                        {
                            me.showTips(d.Message, 'error');
                        }
                }
            }
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: para,
            dataType: 'JSON',
            success: callback,
            complete() {},
            error() {
                me.showTips('系统异常！', 'error');
            },
        });
    };
    Vue.prototype.getToken = function() {
        var token = localStorage.getItem('token');

        if (token) {
            return token;
        } else {
            return '';
        }
    };
    //--------------------------------
    //GUID·
    //--------------------------------
    Vue.prototype.newGuid = function(onlyLetter) {
        var guid = "";
        for (var i = 1; i <= 32; i++) {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if (((i == 8) || (i == 12) || (i == 16) || (i == 20)) && onlyLetter == false) {
                guid += "-";
            }
        }
        return guid;
    };
    //--------------------------------
    //是否整数·
    //--------------------------------
    Vue.prototype.checkInt = function(value, min, max) {
        if (/^-?([1-9]\d*|0)$/.test(value) == false) {
            return false;
        }

        var v = parseInt(value);
        if (min == -1 && max != -1) {
            if (v > max) {
                return false;
            }
        }
        if (min != -1 && max == -1) {
            if (v < min) {
                return false;
            }
        } else if (min != -1 && max != -1) {
            if (v < min || v > max) {
                return false;
            }
        }
    };
    //----------------------------------------
    //验证Money类型
    Vue.prototype.checkMoney = function(value, min, max) {
        if (/(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/.test(value) == false) {
            return false;
        }
        var v = parseFloat(value);
        if (min == -1 && max != -1) {
            if (v > max) {
                return false;
            }
        }
        if (min != -1 && max == -1) {
            if (v < min) {
                return false;
            }
        } else if (min != -1 && max != -1) {
            if (v < min || v > max) {
                return false;
            }
        }
    };
};