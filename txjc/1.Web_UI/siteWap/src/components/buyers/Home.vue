<template>
    <div class="mb80">
        <div class="div-text mt10">
            <span class="red-line ml10">&nbsp;</span>
            快捷功能
        </div>
        <div class="quick-menu-home pt10 pb10">
            <div class="container div-full">
                <div class="row">
                    <div class="col-xs-3 p0 mt10">
                        <a href="/#/bol/2">
                            <img src="/static/images/icon/cgdd.png" />
                            <div class="div-text">采购订单</div>
                        </a>
                    </div>
                    <div class="col-xs-3 p0 mt10">
                        <a href="/#/sol/2">
                            <img src="/static/images/icon/xsdd.png" />
                            <div class="div-text">销售订单</div>
                        </a>
                    </div>
                    <div class="col-xs-3 p0 mt10">
                        <a href="/#/sol/2">
                            <img src="/static/images/icon/ljfh.png" />
                            <div class="div-text">立即发货</div>
                        </a>
                    </div>
                    <div class="col-xs-3 p0 mt10">
                        <a href="/#/pm">
                            <img src="/static/images/icon/cpgl.png" />
                            <div class="div-text">产品管理</div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class="div-text mb10">
            <span class="red-line ml10 ">&nbsp;</span>
            商品列表
        </div>
        <div class="pro-list bg-white">
            <div v-for="pro in table" class="pro-item div-full bb1">
                <div class="pro-list-img" v-on:click="btnShowProduct_Click(pro)">
                    <img :src="pro.Image" alt="商品图片" />
                </div>
                <div class="pro-list-right">
                    <div class="pro-name" v-on:click="btnShowProduct_Click(pro)">
                        {{pro.ProName}}
                    </div>
                    <div class="mt10">
                        <div class="p0 pro-price">
                            {{pro.Price | money}}
                        </div>
                        <div class="p0 pro-price text-right pro-stock">
                            {{pro.Amount}}件
                        </div>
                    </div>
                    <div class="pt20">
                        <div class="container">
                            <div class="row">
                                <div class="col-xs-10 m0 p0 div-ellipsis">
                                    <span class="shop-icon">店</span>
                                    {{pro.Supplier}}
                                </div>
                                <div class="col-xs-2 c-red" v-on:click="btnAddToCart_Click(pro)">
                                    <span class="glyphicon glyphicon-shopping-cart" aria-hidden="true"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div v-if="!table || table.length == 0" class="bg-white lh40 fs16 p20">
                您尚未添加供货商，或者供货商未发布商品。
                <a href="/#/mysupplier">进入供货商管理</a>
            </div>
        </div>
        <div v-if="ShowMore" v-on:click="nextPage_Click()" class="show-more m10 bg-white border text-center">
            点击查看更多
        </div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>

import buyerFooter from './../common/BuyersFooter.vue'

export default {
    name: 'Home',
    components: {
        buyerFooter
    },
    data: function () {
        return {
            ShowMore: true,
            PageIndex: 1,
            PageSize: 15,
            table: [
            ],
        }
    },

    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            this.fetchData({
                cmd: '/api/userAuth/IsLogin',
                para: {},
                callback: function (data) {

                    if (data == 1) {
                        var iPara = {};
                        iPara.PageIndex = me.PageIndex;
                        iPara.PageSize = me.PageSize;
                        iPara.Key = '';

                        // debugger;

                        this.fetchData({
                            cmd: '/api/product/GetRetailerCanBuyProductList',
                            para: iPara,
                            callback: function (data) {
                                var i = 0;
                                if (data && data.Data) {
                                    for (var key in data.Data) {
                                        me.table.push(data.Data[key]);
                                    }

                                    if (me.PageIndex == data.TotalPages) {
                                        me.ShowMore = false;
                                    }
                                }
                            }
                        });
                    }
                    else{
                        me.$router.push({ path: '/login' });
                    }
                }
            });
        },
        nextPage_Click: function () {
            var me = this;
            me.PageIndex++;
            me.initPage();
        },
        btnAddToCart_Click: function (pro) {
            var me = this;
            var iPara = {};
            iPara.ProId = pro.ProId;
            iPara.ProCount = 1;

            this.fetchData({
                cmd: '/api/product/AddToCart',
                para: iPara,
                callback: function (data) {
                    me.$emit('initPage');
                }
            });

        },
        btnShowProduct_Click: function (item) {
            this.$router.push({ path: '/p/' + item.ProId });
        },
        test: function () {
            alert('sss');
            this.$emit('CartNum', 111);
        }
    },

}
</script>
