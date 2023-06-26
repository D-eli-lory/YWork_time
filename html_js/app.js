const routes=[
    {path:'/YProject',component:YProject},
    {path:'/YTask',component:YTask},
    {path:'/YAnswer',component:YAnswer}
]

const router = new VueRouter({
    routes
})

const app = new Vue({
    router
}).$mount('#app')



  