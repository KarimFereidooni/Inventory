import { Vue } from "vue-property-decorator";

Vue.prototype.showErrorMessage = error => {
  alert(error.message);
};

declare module "vue/types/vue" {
  interface Vue {
    showErrorMessage: (error) => void;
  }
}
