<template>
    <div class="mb80">
    
        <div>
            <top-nav title="我收藏的商品"></top-nav>
        </div>
    
        <div class="pro-list bg-white mt40">
            <div v-for="pro in table" class="pro-item div-full bb1">
                <div class="pro-list-img" v-on:click="btnShowProduct_Click(pro);">
                    <img src="" />
                </div>
                <div class="pro-list-right">
                    <div class="pro-name" v-on:click="btnShowProduct_Click(pro);">
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
                                <div class="col-xs-10 m0 p0">
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
                尚未收藏商品
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
import topNav from './../common/TopNav.vue';

export default {
    name: 'ProductCollect',
    components: {
        buyerFooter,
        topNav
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
            var iPara = {};
            iPara.PageIndex = me.PageIndex;
            iPara.PageSize = me.PageSize;

            this.fetchData({
                cmd: '/api/product/GetMyCollectProductInfo',
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
