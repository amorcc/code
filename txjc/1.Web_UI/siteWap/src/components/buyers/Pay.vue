<template>
    <div>
        <div class="mb40">
            <top-nav title="订单支付"></top-nav>
        </div>
        <div class="bg-white mb10 bb1 lh40 pl20 pr20">
            订单金额（含运费）
            <span class="right c-red">{{dataIn.Total | money}}</span>
        </div>
        <div class="div-text mb10">
            <span class="red-line ml10 ">&nbsp;</span>
            选择支付方式
        </div>
        <div v-for="item of dataIn.CommonPayType" class="bg-white lh40 pl20 mb10" v-on:click="btnPayType_Click(item)">
            <input type="radio" v-model="payType" :value="item.PayType" />
            <span>{{item.PayTypeName}}</span>
        </div>
        <div class="text-center">
            <input v-on:click="submit_Click()" class="btn btn-success" type="button" value="确认支付" />
        </div>
    </div>
</template>
<script>
import topNav from './../common/TopNav.vue';
export default {
    name: 'Pay',
    components: {
        topNav,
    },
    data: function () {
        return {
            rn: "",
            OrderCodes: "",
            dataIn: {},
            payType: '1',
        }
    },
    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            me.rn = me.$route.params.rn;
            me.OrderCodes = me.$route.params.oc;

            var para = {};
            para.BatchId = me.rn;
            para.OrderCodes = me.OrderCodes;

            me.fetchData({
                cmd: '/api/ProductOrder/GetPayPreviewInfo',
                para: para,
                callback: function (data) {
                    //var a = {};
                    //a.PayType = 2;
                    //a.PayTypeName  = '支付方式2';
                    //data.CommonPayType.push(a);
                    if (data.CommonPayType && data.CommonPayType.length > 0) {
                        me.payType = data.CommonPayType[0].PayType;
                    }

                    me.dataIn = data;
                }
            });
        },
        btnPayType_Click: function (item) {
            this.payType = item.PayType;
        },
        submit_Click: function () {
            var me = this;
            var para = {};
            para.OrderCodes = me.OrderCodes;
            para.PayType = me.payType;

            if (me.payType == 2) {
                //微信支付
                if (me.isWeiXin() == true) {
                    location.href = "/static/wechat/wx_pay.html?rn=" + me.rn + "&OrderCodes=" + me.OrderCodes + "&PayType=" + me.payType;
                }
                else {
                    // alert('请在微信公众号内支付，或选择其他支付方式');
                    location.href = "/static/wechat/wx_h5_pay.html?rn=" + me.rn + "&OrderCodes=" + me.OrderCodes + "&PayType=" + me.payType;
                }
            }
            else if(me.payType==3){
                //支付宝支付
                location.href = "/static/Alipay/alipay_pay.html?rn=" + me.rn + "&OrderCodes=" + me.OrderCodes + "&PayType=" + me.payType;
            }
            else if (me.payType == 1) {
                //线下支付
                me.fetchData({
                    cmd: '/api/ProductOrder/OrderPay',
                    para: para,
                    callback: function (data) {

                        if (data == 1) {
                            me.$router.push({ path: '/bol/2' });
                        }
                        else {
                            me.showTips('支付出错，请联系管理员', 'error');
                        }
                    }
                });
            }
        },

    },

}
</script>
