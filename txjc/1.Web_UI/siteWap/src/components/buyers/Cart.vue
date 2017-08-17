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
                                    <input id="selectShop" type="checkbox" v-model="item0.IsSelected" v-on:change="onSupplierSeleceClick(item0)" />
                                    <label for="selectShop">{{item0.CompanyName}}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div v-for="item1 of item0.ProList" class="pro-item container-fluid">
                        <input type="checkbox" v-model="item1.IsSelected" v-on:change="proSelectChange(item0,item1)" style="float:left;" />
                        <div class="row" style="margin-left:15px;">
                            <div class="col-xs-3 ">
                                <img :src="item1.ProImage" style="max-height:68px;max-width: 100%;vertical-align: middle;" />
                            </div>
                            <div class="col-xs-8 p0 text-left">
                                <div class="cart-name">
                                    {{item1.ProName}}
                                </div>
                                <div class="cart-count">
                                    <span class="c-red">{{item1.ProPrice | money}}</span>
                                    <input v-on:click="addCartCount(item1)" type="button" value="+" class="add" />
                                    <input type="" v-model="item1.ProCount" class="count" />
                                    <input v-on:click="minusCartCount(item1)" type="button" value="-" class="minus" />
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
        <div v-if="!table || table.length==0" class="text-center lh30">
            <span style="font-size:33px;" class="glyphicon glyphicon-shopping-cart" aria-hidden="true"></span>
            <br/>
            <span>购物车暂无商品，立即采购吧？</span>
            <br/>
            <a href="/#/home">立即去采购</a>
        </div>
        <div class="cartFooter navbar navbar-default navbar-fixed-bottom">
            <div class="container">
                <input id="all" type="checkbox" v-model="selectAll" />
                <label for="all">全选</label>
                <input class="btn-add-cart" type="button" value="确认" v-on:click="onOrderPreview_Click()" />
                <input class="btn-cancel-cart" type="button" value="继续采购" v-on:click="onBuy_Click()" />
            </div>
        </div>
    </div>
</template>
<script>
import topNav from './../common/TopNav.vue';
export default {
    name: 'Cart',
    components: {
        topNav
    },
    data: function () {
        return {
            table: [],
            selectAll: true,
        }
    },
    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            var para = {};

            this.fetchData({
                cmd: '/api/Cart/GetCartInfo',
                para: para,
                callback: function (data) {
                    data.forEach(function (item) {
                        item.IsSelected = false;
                        item.ProList.forEach(function (pro) {
                            pro.IsSelected = false;
                        });
                    });

                    me.table = data;
                }
            });
        },
        onOrderPreview_Click: function () {
            var me = this;
            var proInfo = '';

            me.table.forEach(function (item) {
                item.ProList.forEach(function (pro) {
                    if (pro.IsSelected == true) {
                        proInfo += proInfo == '' ? '' : ',';
                        proInfo += pro.ProId + "|" + pro.ProCount;
                    }
                });
            });

            localStorage.setItem("PreviewProInfo", proInfo);

            var rn = this.newGuid(true);
            me.$router.push({
                path: '/preview/' + rn + '/1'
            });
        },
        onBuy_Click: function () {
            this.$router.push({
                path: '/home'
            });
        },
        addCartCount: function (item) {
            var me = this;
            item.ProCount++;

            var para = {};
            para.ProId = item.ProId;
            para.ModifyCount = item.ProCount;

            me.fetchData({
                cmd: '/api/Cart/CartModifyCount',
                para: para,
                callback: function (data) {

                }
            });
        },
        minusCartCount: function (item) {
            var me = this;
            if (item.ProCount > 1) {
                item.ProCount--;

                var para = {};
                para.ProId = item.ProId;
                para.ModifyCount = item.ProCount;

                me.fetchData({
                    cmd: '/api/Cart/CartModifyCount',
                    para: para,
                    callback: function (data) {

                    }
                });
            }

        },
        onSupplierSeleceClick: function (item0) {
            var me = this;

            item0.ProList.forEach(function (pro) {
                pro.IsSelected = item0.IsSelected;
            });

            if (item0.IsSelected == true) {
                me.table.forEach(function (supplier) {
                    if (supplier.UserSN_S != item0.UserSN_S) {
                        supplier.IsSelected = false;
                        supplier.ProList.forEach(function (pro) {
                            pro.IsSelected = false;
                        });
                    }
                });
            }
        },
        proSelectChange: function (item0, item1) {
            var me = this;
            if (item1.IsSelected == true) {
                me.table.forEach(function (supplier) {
                    if (supplier.UserSN_S != item0.UserSN_S) {
                        supplier.IsSelected = false;
                        supplier.ProList.forEach(function (pro) {
                            pro.IsSelected = false;
                        });
                    }
                });
            }
        },
    },
}
</script>