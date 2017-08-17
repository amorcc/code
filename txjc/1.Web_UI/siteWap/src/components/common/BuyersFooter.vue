<template>
    <nav class="navbar navbar-default navbar-fixed-bottom footnav">
        <div class="container">
            <div class="row">
                <div class="col-xs-3 footicon">
                    <a href="/#/home">
                        <i class="home"></i>
                        <p>首页</p>
                    </a>
                </div>
                <div class="col-xs-3 footicon">
                    <a href="/#/Search">
                        <i class="search"></i>
                        <p>搜索</p>
                    </a>
                </div>
                <div class="col-xs-3 footicon">
                    <a href="/#/Cart">
                        <i class="cart">
                        </i>
                        <p>购物车</p>
                        <span class="badge cartNum">{{CartNum}}</span>
                    </a>
                </div>
                <div class="col-xs-3 footicon">
                    <a href="/#/center">
                        <i class="member"></i>
                        <p>我的</p>
                    </a>
                </div>
            </div>
        </div>
    </nav>
</template>

<script>
export default {
    name: 'BuyerFooter',
    data: function () {
        return {
            CartNum: 0,
        }
    },
    props: {
        refresh: {
            type: Boolean,
            twoWay: true,
            default: false
        }
    },
    mounted: function () {
        this.initPage();
    },
    methods: {
        initPage: function () {
            var me = this;
            var iPara = {};

            this.fetchData({
                cmd: '/api/Cart/GetCartNum',
                para: iPara,
                callback: function (data) {
                    me.CartNum = data;
                }
            });
        },
    },
    watch: {
        refresh(newVal, oldVal) {
            if (newVal) {
                this.initPage();
            }
        }
    }
}
</script>
