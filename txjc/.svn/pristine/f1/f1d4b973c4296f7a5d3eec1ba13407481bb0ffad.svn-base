<template>
    <div>
        <div>
            <top-nav title="产品管理"></top-nav>
        </div>
        <div class="order-list">
            <div class="order-status-btn">
                <div class="container bg-white">
                    <div class="row">
                        <div class="col-xs-2 tab">
                            <a v-bind:class="Status == 1 ? 'selectedClass' : ''" v-on:click="btnStatus_Click(1)" href="javascript:;">已上架</a>
                        </div>
                        <div class="col-xs-2 tab">
                            <a v-bind:class="Status == 0 ? 'selectedClass' : ''" v-on:click="btnStatus_Click(0)" href="javascript:;">已下架</a>
                        </div>
                        <div class="col-xs-8 text-right">
                            <a href="/#/ap/0" style="color:#337ab7;width:auto;">发布新品</a>
                        </div>
                    </div>
                </div>
            </div>
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
                                <div>
                                    <span class="c-red">{{item.ProPrice | money}}</span>
                                    <span class="ml20">库存：</span>
                                    <span>{{item.Amount}}</span>
                                </div>
                                <div class="text-right">
                                    <input v-if="item.Status == 1" v-on:click="btnUpdateStatus(item,0)" type="button" value="下架" class="btn-small" />
                                    <input v-if="item.Status == 0" v-on:click="btnUpdateStatus(item,1)" type="button" value="上架" class="btn-small" />
                                    <input v-on:click="btnDeletePro(item)" v-if="item.Status == 0" type="button" value="删除" class="btn-small" />
                                    <input v-on:click="btnModifyPro(item)" type="button" value="修改" class="btn-small" />
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
    name: 'productMng',
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
                    me.dataIn.TotalRows = data.TotalRows;
                    me.dataIn.TotalPages = data.TotalPages;
                    me.dataIn.table = data.Data;
                }
            });
        },
        btnStatus_Click: function (iStatus) {
            var me = this;
            me.Status = iStatus

            me.getProList(1);
        },
        btnUpdateStatus: function (item, status) {
            var me = this;
            var para = {};
            para.Status = status;
            para.ProId = item.ProId;

            me.fetchData({
                cmd: '/api/Product/UpdateStatus',
                para: para,
                callback: function (data) {
                    me.getProList(1);
                }
            });
        },
        btnModifyPro: function (item) {
            var me = this;
            me.$router.push({ path: '/ap/' + item.ProId });
        },
        btnDeletePro: function (item) {
            var me = this;
            var para = {};
            para.ProId = item.ProId;

            me.fetchData({
                cmd: '/api/Product/DeleteProduct',
                para: para,
                callback: function (data) {
                    me.getProList(1);
                }
            });
        },
    },
}
</script>
