import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "./registerServiceWorker";
import vuetify from "./plugins/vuetify";
import "@/assets/style/iransans.css";
import "@/assets/style/app.css";
import "./plugins/prototype";

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
} as any).$mount("#app");
