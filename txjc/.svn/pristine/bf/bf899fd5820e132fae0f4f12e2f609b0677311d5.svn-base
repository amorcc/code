<template>
    <div>
        <div>
            <top-nav title="采购订单"></top-nav>
        </div>
        <div class="order-list">
             <div class="order-status-btn">
                  <div class="container">
                     <div class="row">
                         <div class="col-xs-2">
                             <a v-bind:class="orderStatus == -2 ? 'selectedClass' : ''" v-on:click="getOrderInfo(1,-2)" href="javascript:;" >全部</a>
                         </div>
                         <div class="col-xs-2">
                             <a v-bind:class="orderStatus == 1 ? 'selectedClass' : ''" v-on:click="getOrderInfo(1,1)" href="javascript:;" >待付款</a>
                         </div>
                         <div class="col-xs-2">
                             <a v-bind:class="orderStatus == 2 ? 'selectedClass' : ''" v-on:click="getOrderInfo(1,2)" href="javascript:;" >已付款</a>
                         </div>
                         <div class="col-xs-2">
                             <a v-bind:class="orderStatus == 4 ? 'selectedClass' : ''" v-on:click="getOrderInfo(1,4)" href="javascript:;" >已发货</a>
                         </div>
                         <div class="col-xs-2">
                             <a v-bind:class="orderStatus == 5 ? 'selectedClass' : ''" v-on:click="getOrderInfo(1,5)" href="javascript:;" >已完成</a>
                         </div>
                         <div class="col-xs-2">
                             <a  v-bind:class="orderStatus == -1 ? 'selectedClass' : ''" v-on:click="getOrderInfo(1,-1)" href="javascript:;" >已取消</a>
                         </div>
                      </div>
                  </div>
             </div>
             <div class="order-table">
                  <div v-for="item0 of dataIn.table" class="order-item">
                    <div class="order-title">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-xs-9 supplier-name">
                                    {{item0.Supplier}}
                                </div>
                                <div class="col-xs-3 order-status">
                                    {{item0.OrderStatus | OrderStatus }}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div v-for="item1 of item0.Details" class="pro-item container-fluid">
                        <div class="row">
                            <div class="col-xs-3">
                                <img src="http://www.tianxiajiancai.com.cn/data/upload/shop/store/goods/58/58_05427412759844571_240.png" />
                            </div>
                            <div class="col-xs-5 pro-name">
                                {{item1.ProName}}
                            </div>
                            <div class="col-xs-3 pro-count">
                                <div>
                                    {{item1.ProPrice | money}}  
                                </div>
                                <div>
                                    x{{item1.ProCount}}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="order-summary">
                        <div>
                            <span class="text-bold">订单编号：</span>{{item0.OrderCode}}
                        </div>
                        <div v-if="item0.Message != ''">
                            <span class="text-bold">卖家留言：</span><span class="c-color66">{{item0.Message}}</span>
                        </div>
                        <div>
                            共<span class="text-bold c-red">{{item0.SumProCount}}</span>件商品，合计
                            <span class="text-bold c-red">{{item0.Total | money}}</span>
                            <span>(含运费：{{item0.SumTransFee | money}})</span>
                        </div>
                        <div>
                            <input v-on:click="btnOrderCancel_Click(item0);" v-if="item0.OrderStatus == 1" type="button" value="取消订单" class="btn-small"/>
                            <input v-on:click="btnPayment_Click(item0)" v-if="item0.OrderStatus == 1" type="button" value="立即支付" class="btn-small"/>
                            <input v-on:click="btnRemindDeliver_Click(item0)" v-if="item0.OrderStatus == 2" type="button" value="提醒发货" class="btn-small"/>
                            <input v-on:click="btnGoodsReceipt_Click(item0)" v-if="item0.OrderStatus == 4" type="button" value="确认收货" class="btn-small"/>
                            <input v-on:click="btnBuyAgain_Click(item0)" v-if="item0.OrderStatus != 1" type="button" value="再次购买" class="btn-small"/>
                        </div>
                    </div>
                  </div>
             </div>
             <div v-if="dataIn.table.length == 0" class="no-order-hint">
                没有查询到订单信息 
             </div>
        </div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';

export default {
    name: 'preview',
    components: {
        buyerFooter,
        topNav
    },
    data:function(){
        return {
            orderStatus:0,
            dataIn:{
                table:[],
                TotalRows :0,
                TotalPages:1,
            },
            selectedClass:{
                'border-bottom' : "2px solid red",
            }
        }
    },
    mounted: function() {
        this.initPage(1);
    },
    methods : {
        initPage:function(iPageIndex){
            var me = this;

            me.orderStatus = me.$route.params.status;

            if(me.orderStatus == ''){
                me.orderStatus = -2;
            }

            me.getOrderInfo(iPageIndex,me.orderStatus);
        },
        getOrderInfo: function(iPageIndex, iOrderStatus){
            var me = this;
            me.orderStatus = iOrderStatus;
            var para = {};
            para.PageIndex = iPageIndex || 1;
            para.PageSize = me.PAGE_SIZE;
            para.OrderStatus = iOrderStatus;

            me.fetchData({
                cmd: '/api/ProductOrder/GetBuyersList',
                para: para,
                callback: function(data) {
                    me.dataIn.TotalRows = data.TotalRows;
                    me.dataIn.TotalPages = data.TotalPages;
                    me.dataIn.table = data.Data;
                }
            });
        },
        submit_Click:function(){
            var me = this;
            var para = {};

            this.fetchData({
                cmd: '/api/ProductOrder/OrderCreate',
                para: para,
                callback: function(data) {
                    var orderCodes = data.OrderCode;
                    me.$router.push({path:'/Pay/'+me.rn+'/'+orderCodes});
                }
            });
        },
        btnOrderCancel_Click:function(order){
            var me = this;
            var para = {};
            para.OrderCode = order.OrderCode;

            this.fetchData({
                cmd: '/api/ProductOrder/BuyersOrderCancel',
                para: para,
                callback: function(data) {
                    me.getOrderInfo(1,-1);
                }
            });
        },
        btnBuyAgain_Click:function(order){
            console.log('再次购买');
            var me  = this;
            var iPara = {};
            iPara.ProList= [];

            order.Details.forEach(function(item){
                var pro = {};
                pro.ProId = item.ProId;
                pro.ProCount = item.ProCount;
                iPara.ProList.push(pro);
            });

            me.fetchData({
                cmd: '/api/Cart/AddToCartManyPro',
                para: iPara,
                callback: function(data) {
                    me.$router.push({path:'/cart'});
                }
            });
        },
        btnRemindDeliver_Click:function(order){
            console.log('提醒发货');
        },
        btnGoodsReceipt_Click:function(order){
            console.log('确认收货');
            var me = this;
            var para = {};
            para.OrderCode = order.OrderCode;

            this.fetchData({
                cmd: '/api/ProductOrder/ConfirmReceipt',
                para: para,
                callback: function(data) {
                    me.getOrderInfo(1,4);
                }
            });
        },
        btnPayment_Click:function(order){
            console.log('立即支付');
            var me = this;
            var rn = order.BatchId;
            var orderCode = order.OrderCode;
            me.$router.push({path:'/Pay/'+rn+'/'+orderCode});
        },
    },
}
</script>
