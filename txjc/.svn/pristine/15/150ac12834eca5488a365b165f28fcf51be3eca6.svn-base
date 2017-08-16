// import 'commonLib/jquery.twbsPagination.js';

import showTips from 'commonScript/showTipsState.js';
/**
 * 获取数据通用方法
 * @date   2016-09
 * @author zhao.liubin@zol.com.cn
 * @param  {[Object]} 传入{target:'需要禁用当前点击按钮时传入当前按钮对象',cmd:'URL',para:{},callback:function(){}}
 * @return {[type]}
 */
function fetchData(arg) {
    let cmd = arg.cmd;
    let url = arg.url || arg.cmdurl || '/ajax/Handler.ashx';
    let para = arg.para || {};
    let dataList = [];
    let target = arg.target || '';
    let async = true;
    if (typeof(arg.async) != void 0) {
        async = arg.async;
    }
    var callback = function(data) {
        var d = data;
        if (d) {
            switch (parseInt(d.ResponseID)) {
                case 0:
                    {
                        if (d.Message) {
                            showTips(d.Message);
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
                    }
                case 1:
                    {
                        if (d.Message.indexOf('没有权限') > -1) {
                            if (typeof arg.noAuth == 'function') {
                                arg.noAuth()
                            }
                        } else {
                            showTips(d.Message, 'error');
                        }
                        break;
                    }
                case 2:
                    {
                        showTips('请求无权限！', 'error');
                        break;
                    }
                case 3:
                    {
                        showTips(d.Message, 'error', function() {
                            window.top.location = '/';
                        });
                        break;
                    }
            }
        }
    };

    //验证提交内容中是否包含反动词汇
    // var validatorResult = arg.validatorReactionaryWord(JSON.stringify(para));
    // if (validatorResult != '') {
    //   callback('{\'ResponseID\':1,\'Message\':\'提交内容中包含禁用词汇 [' + validatorResult + '] ,拒绝提交!\',\'Data\':{}}');
    //   return false;
    // }
    //arg.tips('请稍候...', 50, 'loading.gif', false, m);
    if (target.nodeType) {
        if (target.nodeType == 1) {
            target.classList.add('disabled');
        } else {
            console.warn('需要传入一个节点，检查下哦');
        }
    }
    let l1 = loading.show();
    let fnFinish = function() {
        l1.hide();
        (target.nodeType && target.nodeType == 1) && target.classList.remove('disabled');
    };
    $.ajax({
        url: url + '?tm=' + (new Date()).getTime(),
        type: 'POST',
        data: { cmd: cmd, para: JSON.stringify(para) },
        dataType: 'JSON',
        success: callback,
        complete() {
            fnFinish();
        },
        error() {
            fnFinish();
        },
        async: async
    });
    //update by wenston 20160421，没有敏感词时返回true
    return dataList;
}
module.exports = fetchData;
module.exports = checkInt;
module.exports = checkMoney;