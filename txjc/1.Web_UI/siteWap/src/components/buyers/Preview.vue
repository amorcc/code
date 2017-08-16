<template>
    <div>
        <div class="mb40">
            <top-nav :title="title"></top-nav>
        </div>
        <div v-if="showAddressSelect == false" class="preview ">
            <!--地址 -->
            <div class="preview-address bg-white">
                <div v-for="(item,index) of dataIn.BuyerAddress" class="address-item" v-on:click="btnOpenSelectAddress_Click()">
                    <template v-if="item.IsSelected == true">
                    <div class="address-icon">
                        <img src="/static/images/address-icon.png" />
                    </div>
                    <div class="address-content container p10">
                        <div class="row text-bold">
                            <div class="col-xs-8 p0">
                                {{item.Receiver}}
                            </div>
                            <div class="col-xs-4 p0">
                                {{item.Phone}}
                            </div>
                        </div>
                        <div>
                            {{item.AddressTotal}}
                        </div>
                    </div>
                    </template>
                </div>
                <div class="address-border"></div>
            </div>
            <!-- 地址结束-->
            <!-- 商品信息开始 -->
            <div v-for="item of dataIn.Supplier" class="bg-white p10 mt10 preview-supplier">
                <div class="supplier-name lh30 bb1 text-bold">
                   <span class="shop-icon">店</span><span style="margin-left:3px;">{{item.SupplierName}}</span>
                </div>
                <div v-for="(item0,index) of item.Items" class="bb1 fs12 preview-pro mt10 pb10" >
                    <div class="pro-image">
                        <img v-bind:src="item0.ProImage" />
                    </div>
                    <div class="pro-content">
                        <div class="name">
                            {{item0.ProName}}
                        </div>
                        <div class="price-count">
                            <span>{{item0.ProPrice | money}}</span>
                            <span class="c-red ml10">x{{item0.ProCount}}</span>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="mt10">
                    <input type="text" v-model="item.Message" placeholder="可选填，给卖家留言" class="form-control"/>
                </div>
            </div>
            <!-- 商品信息结束 -->
            <!-- 提交订单 -->
            <div class="bg-white mt10 preview-submit">
                <div class="sum">
                    <div>应付：{{sumPay | money}}</div>
                    <div>共{{sumProCount}}件商品</div>
                </div>
                <div class="btn p0 b0">
                    <input class="radius-none" v-on:click="submit_Click()" type="button" value="立即下单" />
                </div>
            </div>
            <!-- 提交订单结束-->
        </div>
        <div v-if="showAddressSelect == true">
            <div class="preview-address bg-white">
                <div v-for="(item,index) of dataIn.BuyerAddress" class="address-item bb1" v-on:click="selectAddress_Click(item)">
                    <div class="address-icon">
                         <img src="/static/images/address-icon.png" />
                    </div>
                    <div class="address-content container p10">
                        <div class="row text-bold">
                            <div class="col-xs-8 p0">
                                {{item.Receiver}} 
                            </div>
                            <div class="col-xs-4 p0">
                                {{item.Phone}}
                            </div>
                        </div>
                        <div>
                           <span v-if="item.IsDefault" class="bg-red c-white fs12">默认</span> {{item.AddressTotal}}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import topNav from './../common/TopNav.vue';
export default {
    name: 'preview',
    components :{
        topNav,
    },
    data:function(){
        return {
            rn:'',
            orderSource:0,
            proInfo :'',
            dataIn:{},
            sumProCount: 10,
            sumPay : 0,
            showAddressSelect : false,
            title:'订单预览',
            addressId : 0,
        }
    },
    mounted: function() {
        this.initPage();
    },
    methods : {
        initPage:function(){
            var me = this;
            me.rn = me.$route.params.rn;
            me.orderSource = me.$route.params.s;
            me.proInfo = localStorage.getItem("PreviewProInfo");

            var para = {};
            para.batchid = me.rn;
            para.ordersource = me.orderSource;
            para.proinfo = me.proInfo;


            me.fetchData({
                cmd: '/api/ProductOrder/Preview',
                para: para,
                callback: function(data) {
                    console.log('长度'+data.BuyerAddress.length);
                    if(data.Supplier && data.Supplier.length >0){
                        data.Supplier.forEach(function(item0){
                            item0.Message = ''; 
                            me.sumProCount += item0.SumProCount;
                            me.sumPay += item0.Total;
                        });
                    }

                    if(data.BuyerAddress && data.BuyerAddress.length >0){
                        data.BuyerAddress.forEach(function(item){
                            item.IsSelected = item.IsDefault == 1 ? true : false;
                            if(item.IsSelected == true){
                                me.addressId = item.Id;
                            }
                            console.log(item.Phone + ',' + item.IsDefault +','+ item.IsSelected);
                        });
                    }

                    me.dataIn = data;
                }
            });
        },
        btnOpenSelectAddress_Click:function(){
            var me = this;
            me.showAddressSelect = true;
            me.title = "选择收货地址";
        },
        selectAddress_Click:function(item){
            var me = this;

            if(me.dataIn.BuyerAddress && me.dataIn.BuyerAddress.length>0){
                me.dataIn.BuyerAddress.forEach(function(item){
                            item.IsSelected = false;
                        });
            }

            item.IsSelected = true;
            me.addressId = item.Id;
            me.showAddressSelect = false;
            me.title = "订单预览";
        },
        submit_Click:function(){
            var me = this;
            var para = {};
            para.ProInfo = me.proInfo;
            para.OrderSource = me.orderSource;
            para.RN = me.rn;
            para.OrderInfo = [];
            para.AddressId = me.addressId;
            para.PartnerId = 200;

            if(me.dataIn.Supplier && me.dataIn.Supplier.length >0){
                me.dataIn.Supplier.forEach(function(item){
                    var orderInfo = {};
                    orderInfo.Message = item.Message;
                    orderInfo.SupplierUserSN = item.UserSN_S;
                    para.OrderInfo.push(orderInfo);
                });
            }

            this.fetchData({
                cmd: '/api/ProductOrder/OrderCreate',
                para: para,
                callback: function(data) {
                    var orderCodes = data.OrderCode;
                    me.$router.push({path:'/Pay/'+me.rn+'/'+orderCodes});
                }
            });
        },

    },

}
</script>
