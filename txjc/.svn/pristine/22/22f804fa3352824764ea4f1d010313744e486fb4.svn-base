<template>
    <div>
        <div>
            <top-nav title="库存改价"></top-nav>
        </div>
        <div class="order-list">
            <div class="order-table">
                <div v-for="item of dataIn.table" class="order-item pb10">
                    <div class="pro-item container-fluid">
                        <div class="row">
                            <div class="col-xs-3">
                                <img src="http://www.tianxiajiancai.com.cn/data/upload/shop/store/goods/58/58_05427412759844571_240.png" />
                            </div>
                            <div class="col-xs-9 pro-name">
                                <div>
                                    {{item.ProName}}
                                </div>
                                <div class="mt5">
                                    <span class="c-red">{{item.ProPrice | money}}</span>
                                    <span class="ml20">{{item.Status == 1 ? '已上架' :'已下架'}}</span>
                                </div>
                                <div class="ChangeAmount mt5">
                                    <div class="col-xs-3 p0 " style="line-height:21px;">
                                        库存：
                                    </div>
                                    <div class="col-xs-9 p0 ">
                                        <span v-on:click="onAmountMins_Click(item)" class="minus">-</span>
                                        <input v-on:change="onAmount_Change(item)" v-model="item.Amount" type="text" class="num radius-none" />
                                        <span v-on:click="onAmountAdd_Click(item)" class="add">+</span>
                                    </div>
                                    <i class="clearfix"></i>
                                </div>
                                <div class="ChangeAmount mt5">
                                    <div class="col-xs-3 p0 " style="line-height:21px;">
                                        价格：
                                    </div>
                                    <div class="col-xs-9 p0 ">
                                        <span v-on:click="onPriceMins_Click(item)" class="minus">-</span>
                                        <input v-on:change="onPrice_Change(item)" v-model="item.ProPrice" type="text" class="num radius-none" />
                                        <span v-on:click="onPriceAdd_Click(item)" class="add">+</span>
                                    </div>
                                    <i class="clearfix"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div v-if="dataIn.table.length == 0" class="no-order-hint">
                没有查询到商品信息
            </div>
        </div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>
import buyerFooter from './../common/BuyersFooter.vue'
import topNav from './../common/TopNav.vue';

export default {
    name: 'AmountAndPrice',
    components: {
        buyerFooter,
        topNav
    },
    data: function () {
        return {
            Status: 1,
            SearchName: "",
            dataIn: {
                table: [],
                TotalRows: 0,
                TotalPages: 1,
            },
            selectedClass: {
                'border-bottom': "2px solid red",
            }
        }
    },
    mounted: function () {
        this.initPage(1);
    },
    methods: {
        initPage: function (iPageIndex) {
            var me = this;
            me.getProList(1);
        },
        getProList: function (iPageIndex) {
            var me = this;
            var para = {};
            para.PageIndex = iPageIndex || 1;
            para.PageSize = me.PAGE_SIZE;
            para.Name = me.SearchName;
            para.Status = me.Status;

            me.fetchData({
                cmd: '/api/Product/SellerProductMng',
                para: para,
                callback: function (data) {
                    if (data && data.Data && data.Data.length > 0) {
                        data.Data.forEach(function (item) {
                            item.Amount1 = item.Amount;
                            item.ProPrice1 = item.ProPrice;
                        });
                    }
                    me.dataIn.TotalRows = data.TotalRows;
                    me.dataIn.TotalPages = data.TotalPages;
                    me.dataIn.table = data.Data;
                }
            });
        },
        onAmountAdd_Click: function (item) {
            var me = this;
            var amount = parseInt(item.Amount);
            item.Amount = amount + 1;
            me.changeAmount(item.ProId, item.Amount);
        },
        onAmountMins_Click: function (item) {
            var me = this;
            var amount = parseInt(item.Amount);
            if (amount > 0) {
                item.Amount = amount - 1;
            }
            else {
                me.showTips('库存必须大于等于0', 'error');
                return;
            }
            me.changeAmount(item.ProId, item.Amount);
        },
        onAmount_Change: function (item) {
            var me = this;
            if (me.checkInt(item.Amount, 0) == false) {
                me.showTips('请输入正确的库存', 'error');
                item.Amount = item.Amount1;
                return false;
            }

            me.changeAmount(item.ProId, item.Amount);
        },
        changeAmount: function (proId, amount) {
            var me = this;
            var para = {};
            para.ProId = proId;
            para.Amount = amount;

            me.fetchData({
                cmd: '/api/Product/UpdateAmount',
                para: para,
                callback: function (data) {
                }
            });
        },
        onPriceAdd_Click: function (item) {
            var me = this;
            var price = parseFloat(item.ProPrice);
            item.ProPrice = price + 1;
            me.changePrice(item.ProId, item.ProPrice);
        },
        onPriceMins_Click: function (item) {
            var me = this;
            var price = parseFloat(item.ProPrice);
            if (price - 1 > 0) {
                item.ProPrice = price - 1;
            }
            else {
                me.showTips('价格必须大于0', 'error');
                return;
            }
            me.changePrice(item.ProId, item.ProPrice);
        },
        onPrice_Change: function (item) {
            var me = this;
            if (me.checkMoney(item.ProPrice, 0.01) == false) {
                me.showTips('请输入正确的价格,价格必须大于0', 'error');
                item.ProPrice = item.ProPrice1
                return false;
            }

            me.changePrice(item.ProId, item.ProPrice);
        },
        changePrice: function (proId, price) {
            var me = this;
            var para = {};
            para.ProId = proId;
            para.Price = price;

            me.fetchData({
                cmd: '/api/Product/UpdatePrice',
                para: para,
                callback: function (data) {
                }
            });
        },
    },
}
</script>
