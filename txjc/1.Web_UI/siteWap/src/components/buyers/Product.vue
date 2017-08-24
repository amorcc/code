<template>
    <div>
        <div>
            <top-nav title="商品详情"></top-nav>
        </div>
        <div class="mt40 pro">
            <div class="div-full pro-img p0">
                <img :src="dataIn.ProImage" alt="商品图片" />
                <div class="pro-name">
                    <span class="pro-supplier">[{{dataIn.Supplier}}]</span>{{dataIn.ProName}}
                </div>
                <div class="pro-price-amount">
                    <span class="pro-price">{{dataIn.ProPrice | money}}</span>
                    <span class="pro-amount">库存：{{dataIn.ProAmount}}</span>
                </div>
            </div>
            <div class="div-text mb10 mt20">
                <span class="red-line ml10 ">&nbsp;</span>
                商品介绍
            </div>
            <div class="bg-white p10 mt10 pro-desc" v-html="dataIn.Desc">
                {{dataIn.Desc}}
            </div>
            <div class="product-images ">
                <img v-for="img in dataIn.ProImages" :src="img" />
            </div>
            <div class="mb60">
            </div>
            <div class="cartFooter navbar navbar-default navbar-fixed-bottom">
                <div class="container">
                    <label v-on:click="collection()">
                        <span :class="isCollect == 1 ? 'c-red':''" class="glyphicon glyphicon glyphicon-heart" aria-hidden="true"></span>
                        <span :class="isCollect == 1 ? 'c-red':''">收藏</span>
                    </label>
                    <input v-on:click="addCart_Click()" class="btn-add-cart" type="button" value="加入购物车" style="width:100px;" />
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import topNav from './../common/TopNav.vue';
export default {
    name: 'Product',
    components: {
        topNav
    },
    data: function () {
        return {
            proId: 0,
            isCollect: 0,
            dataIn: {},
        }
    },
    mounted: function () {
        this.initPage();
        this.getIsCollect();
    },
    methods: {
        initPage: function () {
            var me = this;
            me.proId = me.$route.params.pid;
            var para = {};
            para.ProId = me.proId;

            me.fetchData({
                cmd: '/api/Product/GetProductInfo',
                para: para,
                callback: function (data) {
                    if (!data.ProId) {
                        me.showTips('未查询到该产品信息', 'error');
                        history.go(-1);
                    }
                    me.dataIn = data;
                }
            });
        },
        getIsCollect: function () {
            var me = this;
            me.proId = me.$route.params.pid;
            var para = {};
            para.ProId = me.proId;

            me.fetchData({
                cmd: '/api/Product/IsCollect',
                para: para,
                callback: function (data) {
                    if (data == 1) {
                        me.isCollect = 1;
                    }
                    else {
                        me.isCollect = 0;
                    }
                }
            });
        },
        addCart_Click: function () {
            var me = this;
            var iPara = {};
            iPara.ProId = me.dataIn.ProId;
            iPara.ProCount = 1;

            this.fetchData({
                cmd: '/api/product/AddToCart',
                para: iPara,
                callback: function (data) {
                    me.$emit('initPage');
                }
            });

        },
        collection: function () {
            var me = this;
            if (this.isCollect == 1) {
                this.isCollect = 0;
            }
            else {
                this.isCollect = 1;
            }

            var para = {};
            para.ProId = me.proId;

            this.fetchData({
                cmd: '/api/product/ProductCollectSwitch',
                para: para,
                callback: function (data) {
                    me.$emit('initPage');
                }
            });
        },
    },

}
</script>
