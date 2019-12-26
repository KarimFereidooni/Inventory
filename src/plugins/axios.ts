import Vue from "vue";
import axios from "axios";
axios.defaults.withCredentials = true;
axios.defaults.xsrfHeaderName = "X-XSRF-TOKEN";
axios.defaults.xsrfCookieName = "XSRF-TOKEN";

Vue.prototype.axios = axios;

declare module "vue/types/vue" {
  interface Vue {
    axios: typeof axios;
  }
}

export default axios;
