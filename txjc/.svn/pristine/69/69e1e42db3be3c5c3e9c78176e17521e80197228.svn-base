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
        <div>
            <input v-on:click="submit_Click()" type="button" value="确认支付" />
        </div>
    </div>
</template>
<script>
import topNav from './../common/TopNav.vue';
export default {
    name: 'Pay',
    components :{
        topNav,
    },
    data:function(){
        return {
            rn: "",
            OrderCodes : "",
            dataIn : {},
            payType :'1',
        }
    },
    mounted: function() {
        this.initPage();
    },
    methods : {
        initPage:function(){
            var me = this;
            me.rn = me.$route.params.rn;
            me.OrderCodes = me.$route.params.oc;

            var para = {};
            para.BatchId = me.rn;
            para.OrderCodes = me.OrderCodes;

            me.fetchData({
                cmd: '/api/ProductOrder/GetPayPreviewInfo',
                para: para,
                callback: function(data) {
                    //var a = {};
                    //a.PayType = 2;
                    //a.PayTypeName  = '支付方式2';
                    //data.CommonPayType.push(a);
                    if(data.CommonPayType && data.CommonPayType.length >0){
                        me.payType = data.CommonPayType[0].PayType;
                    }

                    me.dataIn = data;
                }
            });
        },
        btnPayType_Click:function(item){
            this.payType =item.PayType;
        },
        submit_Click:function(){
            var me = this;
            var para = {};
            para.OrderCodes = me.OrderCodes;

            me.fetchData({
                cmd: '/api/ProductOrder/OrderPay',
                para: para,
                callback: function(data){
                    
                    if(data == 1){
                        me.$router.push({path:'/bol/2'});                        
                    }
                    else{
                        me.showTips('支付出错，请联系管理员','error');
                    }
                }
            });
        },
    },

}
</script>
