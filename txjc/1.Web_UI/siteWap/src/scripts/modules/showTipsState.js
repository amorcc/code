/**
 * 操作提示，需要进一步封装为成功或失败方法，模块化
 * 使用方法：require或Webpack，单独使用时：showTips('操作成功',function(){}),showTipsState('操作失败','error',function(){})
 * @author  Zhao Liubin
 * @type {[type]}
 */

var showTips = function(content, state, callback, time) {
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

  // var opDef=0,deg=95;
  // var rotateShow=function(){
  //     opDef+=0.05,deg-=5;
  //     box.style.transform='translate(-50%,-50%) rotateX('+deg+'deg)';
  //     box.style.opacity=opDef;
  //     if(opDef<0.95){
  //         requestAnimationFrame(rotateShow);
  //     }
  // }
  // requestAnimationFrame(rotateShow);

  var _close = function() {
    document.body.removeChild(box);
  };

  setTimeout(function() {
    _close();
    typeof state === 'function' ? state() : typeof callback === 'function' && callback();
  }, parseInt(time));
};

// var exportObj = showTips;
// typeof module === 'object' && module.exports ? module.exports = exportObj : typeof define === 'function' && define.amd ? define(function() {
//   return exportObj;
// }) : window.showTipsState = exportObj;
module.exports = showTips;
