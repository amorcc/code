

--------------------------------------
me.ProId = me.$route.params.proid;
--------------------------------------
me.$router.push({ path: '/ap/' + item.ProId });
me.$router.push({ path: '/ap/' });
--------------------------------------
var para = {};
para.Status = status;
para.ProId = item.ProId;

me.fetchData({
    cmd: '/api/Product/UpdateStatus',
    para: para,
    callback: function (data) {
        me.getProList(1);
    }
});