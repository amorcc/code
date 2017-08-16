import Vue from 'vue'
import Router from 'vue-router'
import Hello from '@/components/buyers/Hello'
import Home from '@/components/Home'
import Login from '@/components/buyers/Login'
import Cart from '@/components/buyers/Cart'
import Preview from '@/components/buyers/Preview'
import Pay from '@/components/buyers/Pay'
import Reg from '@/components/buyers/Reg'
import MySupplier from '@/components/buyers/MySupplier'
import OrderList from '@/components/buyers/OrderList'
import MyCenter from '@/components/MyCenter'
import NoCode from '@/components/NoCode'
import AddAddress from '@/components/buyers/AddAddress'
import Search from '@/components/buyers/Search'
import Product from '@/components/buyers/Product'
import SellerOrderList from '@/components/seller/OrderList'
import StoreOut from '@/components/seller/StoreOut'
import DeliverGoods from '@/components/seller/DeliverGoods'
import ProductMng from '@/components/seller/ProductMng'
import AddProduct from '@/components/seller/AddProduct'
import WxLogin from '@/components/WX/WxLogin'

Vue.use(Router)

export default new Router({
    routes: [
        //------------------------------------
        //微信
        {
            //微信绑定登录
            path: '/wxlogin/:openid',
            name: 'WxLogin',
            component: WxLogin
        },

        //------------------------------------
        //卖家
        {
            //卖家订单列表
            path: '/sol/:status',
            name: 'SellerOrderList',
            component: SellerOrderList
        },
        {
            //卖家出库
            path: '/storeout/:ordercode',
            name: 'StoreOut',
            component: StoreOut
        },
        {
            //订单发货
            path: '/deliver/:ordercode',
            name: 'DeliverGoods',
            component: DeliverGoods
        },
        {
            //产品管理
            path: '/pm',
            name: 'ProductMng',
            component: ProductMng
        },
        {
            //添加产品
            path: '/ap/:proid',
            name: 'AddProduct',
            component: AddProduct
        },
        //------------------------------------
        //买家
        {
            //我的供货商
            path: '/mysupplier',
            name: 'MySupplier',
            component: MySupplier
        },
        {
            //注册
            path: '/Reg',
            name: 'Reg',
            component: Reg
        },
        {
            //购物车
            path: '/Cart',
            name: 'Cart',
            component: Cart
        },
        {
            //订单预览
            path: '/Preview/:rn/:s',
            name: 'Preview',
            component: Preview
        },
        {
            //订单支付
            path: '/Pay/:rn/:oc',
            name: 'Pay',
            component: Pay
        },
        {
            //买家订单列表
            path: '/bol/:status',
            name: 'OrderList',
            component: OrderList
        },
        {
            //新增收货地址
            path: '/aa',
            name: 'AddAddress',
            component: AddAddress
        },
        {
            //个人中心
            path: '/center',
            name: 'MyCenter',
            component: MyCenter
        },
        {
            //搜索
            path: '/Search',
            name: 'Search',
            component: Search
        },
        {
            //登录页
            path: '/login/:openid/:access_token',
            name: 'login',
            component: Login
        },
        {
            //登录页
            path: '/login',
            name: 'login',
            component: Login
        },
        {
            //登录页
            path: '/p/:pid',
            name: 'Product',
            component: Product
        },
        {
            //登录页
            path: '/nocode',
            name: 'NoCode',
            component: NoCode
        },
        {
            //首页
            path: '*',
            name: 'Home',
            component: Home
        },
    ]
})