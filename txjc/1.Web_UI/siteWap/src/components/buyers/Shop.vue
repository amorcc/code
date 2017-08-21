<template>
    <div>
        <div>
            <top-nav title="店铺页"></top-nav>
        </div>
        <div class="mt40 pro">
            <div style="position:relative">
                <img src="/static/images/shop_ad.jpg" class="w100" />
                <div class="shop-info-area">
                    <div class="shop-info-img">
                        <img :src="companyInfo.LogoImgUrl" />
                    </div>
                    <div class="shop-info-other">
                        <div class="fs16 lh30 div-ellipsis" style="height:30px;">
                            {{companyInfo.ShopName == '' ? companyInfo.CompanyName : companyInfo.ShopName}}
                        </div>
                        <div class="fs12 lh20">
                            <span class="glyphicon glyphicon-phone" aria-hidden="true"></span>
                            <span>{{companyInfo.CompanyPhone == '' ? '未填写':companyInfo.companyPhone}}</span>
                            <span v-if="companyInfo.WechatNumber != ''">(微信：</span>
                            <span v-if="companyInfo.WechatNumber != ''">{{companyInfo.WechatNumber}})</span>
                        </div>
                        <div class="fs12 lh20 div-ellipsis" style="height:20px;div-ellipsis">
                            主营：{{companyInfo.BusinessScope}}
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="lh30 p10 bg-white fs16 text-bold bb1">
            <div class="right remove-font-weight fs12">
                共 {{TotalRows}} 款
            </div>
            全部商品
        </div>
        <div class="bg-white pb10">
            <div v-for="item in prolist" class="bb1 p10">
                <div class="left shop-product-img-div">
                    <img :src="item.Image" />
                </div>
                <div class="shop-product-info pl10 ">
                    <div class="proname fs14 lh20 ">
                        <span>{{item.ProName}}</span>
                    </div>
                    <div class="row p0">
                        <div class="lh40 price fs16 c-red col-xs-8 text-left p0">
                            <span>￥{{item.Price}}</span>
                        </div>
                        <div class="col-xs-4 lh40 p0 text-right fs18">
                            <div class="pr20">
                                <span v-on:click="btnAddToCart_Click(item)" aria-hidden=" true " class="glyphicon glyphicon-shopping-cart "></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear " />
            </div>
            <div v-if="ShowMore " v-on:click="nextPage_Click() " class="show-more m10 bg-white border text-center ">
                点击查看更多
            </div>
        </div>
        <buyer-footer ref="footer">></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';
export default {
    name: 'Shop',
    components: {
        buyerFooter,
        topNav
    },
    data: function () {
        return {
            UserSN_S: '',
            companyInfo: {},
            prolist: [],
            PageIndex: 1,
            PageSize: 15,
            ShowMore: true,
            TotalRows: 0,
        }
    },
    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            me.UserSN_S = me.$route.params.usersn;

            me.getCompanyInfo();
            me.getProductList();
        },
        getCompanyInfo: function () {
            var me = this;
            var para = {};
            para.UserSN_S = me.UserSN_S;

            me.fetchData({
                cmd: '/api/Company/RetailerGetCompanyInfo',
                para: para,
                callback: function (data) {
                    me.companyInfo = data;
                }
            });
        },
        getProductList: function () {
            var me = this;
            var iPara = {};
            iPara.UserSN_S = me.UserSN_S;
            iPara.PageIndex = me.PageIndex;
            iPara.PageSize = me.PageSize;
            iPara.Key = '';

            me.fetchData({
                cmd: '/api/Product/GetRetailerCanBuyProductList',
                para: iPara,
                callback: function (data) {
                    me.TotalRows = data.TotalRow;
                    if (data && data.Data) {
                        for (var key in data.Data) {
                            me.prolist.push(data.Data[key]);
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
            me.getProductList();
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
