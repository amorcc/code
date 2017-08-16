<template>
<div>
    <div>
         <top-nav title="商品详情"></top-nav>
    </div>
    <div class="mt40 pro">
        <div class="div-full pro-img p0">
            <img src="http://www.tianxiajiancai.com.cn/data/upload/shop/store/goods/7/7_05300405111668905_360.jpg" />
            <div class="pro-name">
                <span class="pro-supplier">[{{dataIn.Supplier}}]</span>{{dataIn.ProName}}
            </div>
            <div class="pro-price-amount">
                <span class="pro-price">{{dataIn.ProPrice | money}}</span>
                <span class="pro-amount">库存：{{dataIn.ProAmount}}</span>
            </div>
        </div>
        <div class="div-text mb10 mt20"><span class="red-line ml10 ">&nbsp;</span>
            商品介绍
        </div>
        <div class="div-fill mt10 pro-desc" v-html="dataIn.Desc">
            {{dataIn.Desc}}
        </div>
        <div class="mb80">
        </div>
        <div class="cartFooter navbar navbar-default navbar-fixed-bottom">
            <div class="container">
                <span class="glyphicon glyphicon glyphicon-heart" aria-hidden="true"></span>
                <span>收藏</span>
                <input v-on:click="addCart_Click()" class="btn-add-cart" type="button" value="加入购物车" style="width:100px;"/>
            </div>
        </div>
    </div>
</div>
</template>
<script>
import topNav from './../common/TopNav.vue';
export default {
    name: 'Product',
    components :{
        topNav
    },
    data:function(){
        return {
            proId : 0,
            dataIn : {},
        }
    },
    mounted: function() {
        this.initPage();
    },
    methods : {
        initPage:function(){
            var me = this;
            me.proId = me.$route.params.pid;
            var para = {};
            para.ProId = me.proId;

            me.fetchData({
                cmd: '/api/Product/GetProductInfo',
                para: para,
                callback: function(data) {
                    if(!data.ProId){
                        me.showTips('未查询到该产品信息','error');
                        history.go(-1);
                    }
                    me.dataIn = data;
                }
            });
        },
        addCart_Click : function(){
            var me  = this;
            var iPara = {};
            iPara.ProId = me.dataIn.ProId;
            iPara.ProCount = 1;

            this.fetchData({
                cmd: '/api/product/AddToCart',
                para: iPara,
                callback: function(data) {
                    me.$emit('initPage');
                }
            });

        },
    },

}
</script>
