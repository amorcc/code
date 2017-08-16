<template>
    <div>
        <div>
            <top-nav title="订单改价"></top-nav>
        </div>
        <div class="bg-white mt40 p10">
            <div class="container">
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-bold ">
                        订单编号：
                    </div>
                    <div class="col-xs-8 p0">
                        {{orderCode}}
                    </div>
                </div>
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-bold ">
                        买家：
                    </div>
                    <div class="col-xs-8 p0">
                        {{dataIn.Retailer}}
                    </div>
                </div>
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-bold ">
                        商品小计:
                    </div>
                    <div class="col-xs-8 p0">
                        ￥{{dataIn.FinalPrice}}
                    </div>
                </div>
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-bold ">
                        运费小计:
                    </div>
                    <div class="col-xs-8 p0">
                        ￥
                        <input v-on:change="onTransFee_Change()" type="text" class="text-small-money" v-model="dataIn.TransFee" />
                    </div>
                </div>
    
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-bold ">
                        订单合计:
                    </div>
                    <div class="col-xs-8 p0 text-bold c-red">
                        ￥{{dataIn.Total}}
                    </div>
                </div>
            </div>
        </div>
        <div class="div-text mt10">
            <span class="red-line ml10">&nbsp;</span>
            商品详情
        </div>
        <div class="bg-white mt10 p10">
            <table class="table">
                <thead>
                    <tr>
                        <th>商品名称</th>
                        <th>单价</th>
                        <th>小计</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item of dataIn.DetailList">
                        <td>{{item.ProName}}</td>
                        <td>
                            <div style="minWidth:100px;">
                                <input v-on:change="onPrice_Change(item)" class="text-small-money text-center" type="text" v-model="item.ProPrice" />
                                <span>*{{item.ProCount}}</span>
                            </div>
                        </td>
                        <td>￥{{item.ProCount * item.ProPrice}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="bg-white mt10 p10 text-center mb60">
            <button v-on:click="submit_Click()" class="btn btn-default" type="submit">立即改价</button>
        </div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';

export default {
    name: 'OrderModifyPrice',
    components: {
        buyerFooter,
        topNav
    },
    data: function () {
        return {
            orderCode: '',
            dataIn: {},
        }
    },
    mounted: function () {
        this.initPage();
    },
    filters: {
        getTotal: function () {
            return parseFloat(dataIn.TransFee) + parseFloat(dataIn.FinalPrice);
        },
    },
    methods: {
        initPage: function () {
            var me = this;
            me.orderCode = me.$route.params.ordercode;

            if (me.orderCode && me.orderCode != '') {
                var para = {};
                para.OrderCode = me.orderCode;

                me.fetchData({
                    cmd: '/api/ProductOrder/GetOrderInfo',
                    para: para,
                    callback: function (data) {
                        if (data && data.DetailList && data.DetailList.length > 0) {
                            data.DetailList.forEach(function (item) {
                                item.OldProPrice = item.ProPrice;
                            });
                        }
                        if (data) {
                            data.Total = parseFloat(data.TransFee) + parseFloat(data.FinalPrice);
                            data.OldTransFee = data.TransFee;
                        }
                        me.dataIn = data;
                    }
                });
            }
            else {
                me.showTips('订单编号错误', 'error');
            }
        },
        onPrice_Change: function (item) {
            var me = this;
            if (me.checkMoney(item.ProPrice, 0) == false) {
                me.showTips('请输入正确的价格,价格必须大于等于0', 'error');
                item.ProPrice = item.OldProPrice;
                return false;
            }

            this.recalculate();
        },
        onTransFee_Change: function () {
            var me = this;
            if (me.checkMoney(me.dataIn.TransFee, 0) == false) {
                me.showTips('请输入正确的运费,运费必须大于等于0', 'error');
                me.dataIn.TransFee = me.dataIn.OldTransFee;
                return false;
            }

            this.recalculate();
        },
        recalculate: function () {
            var me = this;
            var subTotal = 0;

            me.dataIn.DetailList.forEach(function (item) {
                subTotal += item.ProPrice * item.ProCount;
            });

            me.dataIn.FinalPrice = subTotal;
            me.dataIn.Total = parseFloat(me.dataIn.FinalPrice) + parseFloat(me.dataIn.TransFee);
        },
        submit_Click: function () {
            var me = this;
            var para = {};
            para.OrderCode = me.orderCode;
            var transFeeChange = '';

            if (me.dataIn.TransFee != me.dataIn.OldTransFee) {
                para.NewTransFee = me.dataIn.TransFee;
                transFeeChange = para.NewTransFee;
            }

            var priceChange = '';
            me.dataIn.DetailList.forEach(function (item) {
                if (item.ProPrice != item.OldProPrice) {
                    priceChange += priceChange == '' ? '' : ',';
                    priceChange += item.ProId + '|' + item.ProPrice;
                }
            });

            para.NewProPrice = priceChange;

            if (transFeeChange != '' || priceChange != '') {
                me.fetchData({
                    cmd: '/api/ProductOrder/OrderChangePrice',
                    para: para,
                    callback: function (data) {

                    }
                });
            }

            // alert(priceChange);
        },
    },
}
</script>
