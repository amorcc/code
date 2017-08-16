<template>
    <div style="padding-bottom:80px;">
        <div class="mb40">
            <top-nav title="商品出库"></top-nav>
        </div>
        <div class="div-text mt60"><span class="red-line ml10">&nbsp;</span>
            出库订单信息
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
                    <div class="col-xs-8 p0">{{dataIn.DateAdded | datetime}}</div>
                </div>
            </div>
        </div>
        <div class="div-text mt10"><span class="red-line ml10">&nbsp;</span>
            出库订单商品信息
        </div>
        <div class="bg-white p10 mt10">
            <div class="container">
                <div :class="getRowClass(item1)" v-for="item1 of dataIn.Details" class="row pb10 bb1 mb10 pt10">
                     <div class="col-xs-3" v-on:click="btnStoreOutAll_Click(item1)" >
                           <img class="w100" src="http://www.tianxiajiancai.com.cn/data/upload/shop/store/goods/58/58_05427412759844571_240.png" />
                     </div>
                     <div class="col-xs-9 p0" >
                        <div class="row lh20" v-on:click="btnStoreOutAll_Click(item1)" >
                             <div class="col-xs-10 pro-name p0">
                                 {{item1.ProName}}
                             </div>
                             <div class="col-xs-2 pro-count p0 text-center ">
                                x{{item1.ProCount}}
                            </div>
                        </div>
                        <div class=" text-right mt10 pr10">
                            <div class="cart-count ">
                                <input v-on:click="addCount(item1)" value="+" class="add c-red" type="button"> 
                                <input v-on:change="changeStoreOutNum(item1)" class="count c-red" type="text" v-model="item1.ThisStoreOutNum"> 
                                <input v-on:click="minusCount(item1)" value="-" class="minus c-red" type="button">
                            </div>
                        </div>
                     </div>
                </div>
            </div>
        </div>
         <div class="bg-white mt10 preview-submit">
                   <div class="sum1">
                    <div>应出库：{{sumStoreOutNum}}件</div>
                    <div>本次出库：{{thisNum}}件</div>
                </div>
                <div class="btn p0 b0">
                    <input v-on:click="storeOut(2)" type="button"  value="出库并发货" />
                    <input v-on:click="storeOut(1)" type="button" value="出库" style="background:#cccccc;" />
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
            sumStoreOutNum : 0,
            thisNum : 0,
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
                cmd: '/api/ProductOrder/SellerStoreOutBefore',
                para: para,
                callback: function(data) {
                    var sum = 0;
                    if(data.Details && data.Details.length>0){
                        data.Details.forEach(function(item){
                            item.ThisStoreOutNum = 0;
                            item.StoreOutType = 0;

                            sum += item.ProCount - item.StoreOutNum;
                        });
                    }

                    me.sumStoreOutNum = sum;
                    me.dataIn = data;
                    me.calculationNum();
                }
            });
        },
        btnStoreOutAll_Click:function(item){
            console.log('all');

            var thisMaxCount =  item.ProCount - item.StoreOutNum;
            item.ThisStoreOutNum = thisMaxCount;
            item.StoreOutType = 2;
            this.calculationNum();
        },
        changeStoreOutNum:function(item){
            var thisMaxCount =  parseInt(item.ProCount) - parseInt(item.StoreOutNum);

            if(parseInt(item.ThisStoreOutNum) > thisMaxCount){
                item.ThisStoreOutNum = thisMaxCount;
            }

            if(parseInt(thisMaxCount) == parseInt(item.ThisStoreOutNum)){
                item.StoreOutType = 2;
                console.log(2)
            }
            else if(parseInt(item.ThisStoreOutNum) == 0){
                item.StoreOutType = 0;
                console.log(0)
            }
            else{
                item.StoreOutType = 1;
                console.log(1)
            }

            this.calculationNum();
        },
        calculationNum:function(){
            var me = this;
            me.thisNum = 0;
            if(me.dataIn.Details && me.dataIn.Details.length>0){
                me.dataIn.Details.forEach(function(item){
                   me.thisNum += parseInt(item.ThisStoreOutNum); 
                });
            }
        },
        addCount:function(item){
            var thisMaxCount =  parseInt(item.ProCount) - parseInt(item.StoreOutNum);

            if(parseInt(item.ThisStoreOutNum) +1 > thisMaxCount){
                item.ThisStoreOutNum = thisMaxCount;
            }
            else{
                item.ThisStoreOutNum++;
            }

            this.changeStoreOutNum(item);
        },
        minusCount:function(item){
            console.log('--')
            if(parseInt(item.ThisStoreOutNum) >0){
                item.ThisStoreOutNum--;
                this.changeStoreOutNum(item);
            }

        },
        getRowClass:function(item){
            if(item.StoreOutType == 1){
                //部分出库
                return "bg-ee c-white";
            }
            else if(item.StoreOutType == 2){
                return "bg-a6 c-white";
            }
            else{
                return "";
            }
        },
        storeOut:function(type){
            
            var me = this;
            var storeOutInfo = '';
           
            if(me.dataIn.Details && me.dataIn.Details.length>0){
                me.dataIn.Details.forEach(function(item){
                    if(item.ThisStoreOutNum > 0){
                        storeOutInfo += storeOutInfo == '' ? '' : ',';
                        storeOutInfo += item.ProId + "|" + item.ThisStoreOutNum;
                    }
                });
            }

            var para = {};
            para.OrderCode = me.orderCode;
            para.StoreOutInfo = storeOutInfo;

            this.fetchData({
                cmd: '/api/ProductOrder/SellerStoreOut',
                para: para,
                callback: function(data) {
                    if(type == 2){
                        //出库并发货
                        alert('出库并发货');
                    }
                    else{
                        alert('仅出库');
                        me.$router.push({path:'/sol/2'});
                    }
                }
            });
        },
        storeOutAndDeliver:function(){

        },
        jumpUrl:function(type){

        },
    },
}
</script>
