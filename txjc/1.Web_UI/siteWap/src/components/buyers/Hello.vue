<template>
    <div style="height:2000px; ">

        <a href="#" v-on:click="clickTest()">asdsf</a><br/>
        <a href="#" v-on:click="clickTest2()">测试</a><br/>
        <a href="#" v-on:click="clickTest3()">测试2</a>
        <br/>
        <a href="#" v-on:click="clickTest4()">测试3</a>
    </div>
</template>
<script>
export default {
    name: 'hello',
    data:function(){
        return {
            msg: 'Welcome to Your Vue.js App'
        }
    },
    methods : {
        clickTest4:function(){
            $('.footnav').hide();
        },
        clickTest3:function(){
            this.showTips('请选择要批量架的商品！', 'error','',20000 );
        },
        clickTest2:function(){
            dialog({
                title: '提示',
                content: '您确认要下架商品吗？',
                ok: function() {
                    alert('ddd');
                },
                cancel: function() {},
            }).showModal();
        },
        clickTest:function(){
            var iPara = {};
            iPara.username = 'b';
            iPara.password = '96e79218965eb72c92a549dd5a330112';
            iPara.ip = 'b';
            iPara.partnerId = '200';
            iPara.misc = 'b';

            this.fetchData({
                cmd: '/api/userauth/login',
                para: iPara,
                callback: function(data) {
                  alert(data.message) ;
                }
            });

           
           
            //var url = 'http://ucapi2.zmaimai.com';
            //$.ajax({
            //    url: url + '/api/userauth/login',
            //    type: 'POST',
            //    data: iPara,
            //    dataType: 'JSON',
            //    success: function(data){
            //        var i =0;
            //        alert(data);
            //    },
            //});
        },
    },

}
</script>
