<template>
    <div>
        <div>
            <top-nav title="购物车"></top-nav>
        </div>
        <div class="order-list">
            <div class="order-table">
                  <div v-for="item0 of table" class="order-item">
                    <div class="order-title">
                        <div class="container">
                            <div class="row">
                                <div class="col-xs-12 supplier-name">
                                    <input id="selectShop" type="checkbox" v-model="item0.IsSelected"/>
                                    <label for="selectShop">{{item0.CompanyName}}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div v-for="item1 of item0.ProList" class="pro-item container-fluid">
                        <input type="checkbox" v-model="item1.IsSelected" style="float:left;" />
                        <div class="row" style="margin-left:15px;">
                            <div class="col-xs-3 ">
                                  
                                <img src="http://www.tianxiajiancai.com.cn/data/upload/shop/store/goods/58/58_05427412759844571_240.png" />
                            </div>
                            <div class="col-xs-9 p0">
                                <div class="cart-name"> 
                                    {{item1.ProName}}
                                </div>
                                <div class="cart-count">
                                    <span class="c-red">{{item1.ProPrice | money}}</span>
                                    <input type="button" value="+" class="add" />
                                    <input type="" v-model="item1.ProCount" class="count" />
                                    <input type="button" value="-" class="minus" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="width:10px">
                        &nbsp;
                    </div>
                  </div>
             </div>
        </div>
        <div class="cartFooter navbar navbar-default navbar-fixed-bottom">
            <div class="container">
                <input id="all" type="checkbox" v-model="selectAll"/>
                <label for="all" >全选</label>
                <input class="btn-add-cart" type="button" value="确认" v-on:click="onOrderPreview_Click()"/>
                <input class="btn-cancel-cart" type="button" value="继续采购" v-on:click="onBuy_Click()"/>
            </div>
        </div>
    </div>
</template>
<script>
import topNav from './../common/TopNav.vue';
export default {
    name: 'Cart',
    components :{
        topNav
    },
    data:function(){
        return {
            table:[],
            selectAll : true,
        }
    },
    mounted: function() {
        this.initPage();
    },
    methods : {
        initPage :function(){
            var me = this;
            var para = {};

            this.fetchData({
                cmd: '/api/Cart/GetCartInfo',
                para: para,
                callback: function(data) {
                    data.forEach(function(item){
                        item.IsSelected = true;
                        item.ProList.forEach(function(pro){
                            pro.IsSelected = true;
                        });
                    });

                    me.table = data;
                }
            });
        },
        onOrderPreview_Click:function(){
            var me = this;
            var proInfo = '';

            me.table.forEach(function(item){
                item.ProList.forEach(function(pro){
                    if(pro.IsSelected == true){
                        proInfo += proInfo == '' ? '' : ',';
                        proInfo += pro.ProId+ "|"+pro.ProCount;
                    }
                });
            });

            localStorage.setItem("PreviewProInfo", proInfo);

            var rn = this.newGuid(true);
            me.$router.push({path:'/preview/'+rn+'/1'});
        },
        onBuy_Click:function(){
            this.$router.push({path:'/home'});
        },
    },
}
</script>
