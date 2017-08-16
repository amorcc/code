<template>
    <div style="padding-bottom:80px;">
        <div class="mb40">
            <top-nav title="订单发货"></top-nav>
        </div>
        <div class="div-text mt60"><span class="red-line ml10">&nbsp;</span>
            订单信息
        </div>
        <div class="bg-white p10 mt10">
            <div class="container">
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-right text-bold">订单编号：</div>
                    <div class="col-xs-8 p0">{{dataIn.OrderCode}}</div>
                </div>
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-right text-bold">买家名称：</div>
                    <div class="col-xs-8 p0">{{dataIn.Retailer}}</div>
                </div>
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-right text-bold">下单时间：</div>
                    <div class="col-xs-8 p0">{{dataIn.CreateDate | datetime}}</div>
                </div>
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-right text-bold">收货人：</div>
                    <div class="col-xs-8 p0">{{dataIn.Receiver}} <span class="ml10">({{dataIn.Phone}})</span></div>
                </div>
                <div class="row lh30">
                    <div class="col-xs-4 p0 text-right text-bold">收货地址：</div>
                    <div class="col-xs-8 p0">{{dataIn.TotalAddress}}</div>
                </div>
            </div>
        </div>
        <div class="div-text mt10"><span class="red-line ml10">&nbsp;</span>
            出库单信息
        </div>
        <div class=" p10">
            <div v-for="item of dataIn.StoreOutInfo" class="bg-white mt10 store-out-item">
                <div class="ml10 mr10 pb10 pt10 lh25 bb1">
                    出库单号：{{item.StoreOutNum}}
                </div>
                <div v-for="(pro,index) of item.Detail" class="p10 ml10 mr10 " :class="index + 1 == item.Detail.length ? '':'bb1-dashed'">
                    <div class="container">
                        <div class="row">
                            <div class="col-xs-10 p0">
                                <span class="c-white bg-red">商品</span>{{pro.ProName}} 
                            </div>
                            <div class="col-xs-2 p0 text-right">
                                * {{pro.StoreNum}}
                            </div>
                        </div>
                    </div>
                </div>
                <div v-if="item.Status == 1">
                    <div class="bt1 ml10 mr10 pt10 pb10 ">
                        <select v-model="item.ExpIdSelected" class="form-control" >
                            <option v-for="exp of dataIn.ExpressList" :value="exp.ExpId" >{{exp.ExpressName}}</option>
                        </select>
                    </div>
                    <div class="bt1 ml10 mr10 pt10 pb10 ">
                        <input v-model="item.ExpNumSelected" type="text" class="form-control" placeholder="请填写物流单号"/>
                    </div>
                    <div class="bt1 ml10 mr10 pt10 pb10 ">
                        <input v-on:click="btnDeliver_Click(item)" type="button" value="立即发货" class="btn-small right"/>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <div v-if="item.Status == 2">
                    <div class="bt1 ml10 mr10 pt10 pb10 ">
                        <span class="c-red ml10">已发货</span>
                        <span class="ml10">{{item.Express}}</span>
                    </div>
                    <div class="ml10 mr10 pt10 pb10 ">
                        <span class="ml10">物流单号：</span>
                        <a href="#">{{item.ExpCode}}</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="div-text mt10"><span class="red-line ml10">&nbsp;</span>
            操作说明
        </div>
        <div class="bg-white p10 mt10">
            <div class="lh30">1、白色背景为未出库，灰色背景为全部出库，鲜黄色背景为部分出库</div>
            <div class="lh30">2、点击商品图片或者商品名称，将自动填充全部出库数量</div>
        </div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';

export default {
    name: 'StoreOut',
    components: {
        buyerFooter,
        topNav
    },
    data:function(){
        return {
            orderCode:'',
            dataIn:{
                table:[],
            },
        }
    },
    mounted: function() {
        this.initPage(1);
    },
    methods : {
        initPage:function(iPageIndex){
            var me = this;
            me.orderCode = me.$route.params.ordercode;

            var para = {};
            para.OrderCode = me.orderCode;

            this.fetchData({
                cmd: '/api/ProductOrder/GetDeliverGoodsInfo',
                para: para,
                callback: function(data) {
                    var expId = 0;
                    if(data && data.ExpressList && data.ExpressList.length >0){
                        expId = data.ExpressList[0].ExpId;
                    }

                    if(data && data.StoreOutInfo && data.StoreOutInfo.length >0){
                        data.StoreOutInfo.forEach(function(item){
                            item.ExpIdSelected = expId;                            
                            item.ExpNumSelected = '';
                        });
                    }

                    me.dataIn = data;
                }
            });
        },
        btnDeliver_Click:function(item){
            var me = this;
            var para = {};
            para.StoreOutCode = item.StoreOutNum;
            para.ExpId = item.ExpIdSelected;
            para.ExpNum = item.ExpNumSelected;

            this.fetchData({
                cmd: '/api/ProductOrder/SellerDeliverGoodsInfo',
                para: para,
                callback: function(data) {
                    item.Status = 2;
                    item.Express = '';
                    item.ExpCode = item.ExpNumSelected;
                }
            });
        },
    },
}
</script>
