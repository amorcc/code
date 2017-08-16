<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>

<script>
export default {
    name: 'app',
    data : function(){
        return {
        }
    },
    mounted: function() {
    },
    methods : {
        initPage : function(){
            var me = this;
            var iPara = {};

            this.fetchData({
                cmd: '/api/Cart/GetCartNum',
                para: iPara,
                callback: function(data) {
                    me.CartNum = data;
                }
            });
        },
    },
}
</script>
