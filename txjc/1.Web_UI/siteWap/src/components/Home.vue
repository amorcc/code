<template>
    <div>
        <div>
            <a href="javascript:;" v-on:click="test()">test</a>
            <div v-for='pro in table'>
                {{pro.ProId}} |  {{pro.ProName}}  |  {{pro.Price}}
                <a href="javascript:;" v-on:click="addCart_Click(pro)">加入购物车</a>
            </div>
            <div v-if="ShowMore">
                <a href="javascript:;" v-on:click="nextPage_Click()"> 更多商品</a>
            </div>
        </div>
        <buyer-footer></buyer-footer>
    </div>
</template>
<script>

import buyerFooter from '/components/common/BuyersFooter.vue'

export default {
    name: 'Home',
    components: {
        buyerFooter
    },
    data:function(){
        return {
            ShowMore : true,
            PageIndex:1,
            PageSize : 15,
            table :[
            ],
        }
    },

     mounted: function() {
        this.initPage();
    },
    methods : {
        initPage :function(){
            var me = this;
            var iPara = {};
            iPara.PageIndex = me.PageIndex;
            iPara.PageSize = me.PageSize;
            iPara.Key = '';

            this.fetchData({
                cmd: '/api/product/GetRetailerCanBuyProductList',
                para: iPara,
                callback: function(data) {
                    var i=0;
                    if(data && data.Data ){
                        for (var key in data.Data) {
                            me.table.push(data.Data[key]);
                        }

                        if(me.PageIndex == data.TotalPages){
                            me.ShowMore = false;
                        }
                    }
                }
            });
        },
        nextPage_Click:function(){
            var me = this;
            me.PageIndex++;
            me.initPage();
        },
        addCart_Click : function(pro){
            var me  = this;
            var iPara = {};
            iPara.ProId = pro.ProId;
            iPara.ProCount = 1;

            this.fetchData({
                cmd: '/api/product/AddToCart',
                para: iPara,
                callback: function(data) {
                    me.$emit('initPage');
                }
            });

        },
        test : function(){
            alert('sss');
            this.$emit('CartNum',111);
        }
    },

}
</script>
