<template>
    <div>
        <top-nav title="搜索"></top-nav>
        <div class="mt40 p10 bg-white">
            <div style="margin-right:30px; height:30px;">
                <input v-model="key" type="text" style="width:100%; padding:10px;height:35px ;border:1px solid #cccccc;border-radius:5px 0 0 5px;" placeholder="请输入要搜索的关键词" />
            </div>
            <div v-on:click="onSearch_Click(true)" style="border-radius:0 5px 5px 0;float:right;hight:30px; border-right:1px solid #cccccc;border-top:1px solid #cccccc;border-bottom:1px solid #cccccc;margin:0;width: 30px; text-align: center; margin-top: -30px; height:35px;">
                <span class="glyphicon glyphicon-search" style="font-size:20px;display:inline-block;margin-top:5px;" aria-hidden="true"></span>
            </div>
        </div>
        <div class="mt10 mb60">
            <div class="pro-list bg-white">
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
                <div v-if="(!table || table.length == 0) && SearchClicked == true" class="bg-white lh40 fs16 p20">
                    未查询到商品信息
                </div>
            </div>
            <div v-if="ShowMore" v-on:click="nextPage_Click()" class="show-more m10 bg-white border text-center">
                点击查看更多
            </div>
    
        </div>
        <buyer-footer ref="footer"></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';

export default {
    name: 'search',
    components: {
        buyerFooter,
        topNav
    },
    data: function () {
        return {
            SearchClicked: false,
            ShowMore: false,
            PageIndex: 1,
            PageSize: 15,
            table: [],
            key: '',
        }
    },
    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
        },
        onSearch_Click: function (isClear) {
            var me = this;

            if (isClear) {
                me.table = [];
            }

            var iPara = {};
            iPara.PageIndex = me.PageIndex;
            iPara.PageSize = me.PageSize;
            iPara.Key = me.key;

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
                        else {
                            me.ShowMore = true;
                        }
                    }

                    me.SearchClicked = isClear;
                }
            });
        },
        nextPage_Click: function () {
            var me = this;
            me.PageIndex++;
            me.onSearch_Click(false);
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
                    me.$refs.footer.initPage();
                }
            });

        },
    },

}
</script>
