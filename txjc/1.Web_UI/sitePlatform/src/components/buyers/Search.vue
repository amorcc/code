<template>
    <div>
        <top-nav title="搜索"></top-nav>
        <buyer-footer></buyer-footer>
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
    data:function(){
        return {
        }
    },
    mounted: function() {
        this.initPage();
    },
    methods : {
        initPage:function(){
       },
        submit_Click:function(){
       },
    },

}
</script>
