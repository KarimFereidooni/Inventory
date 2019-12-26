import Vue from "vue";
import Vuetify from "vuetify/lib";
import { Framework } from "vuetify";

const fa = {
  close: "بستن",
  dataIterator: {
    pageText: "{0} تا {1} از {2}",
    noResultsText: "موردی پیدا نشد",
    loadingText: "بارگذاری آیتم ها ..."
  },
  dataTable: {
    itemsPerPageText: "تعداد در صفحه:",
    sortBy: "مرتب سازی بر اساس",
    ariaLabel: {
      sortDescending: ": Sorted descending. Activate to remove sorting.",
      sortAscending: ": Sorted ascending. Activate to sort descending.",
      sortNone: ": Not sorted. Activate to sort ascending."
    }
  },
  dataFooter: {
    itemsPerPageText: "تعداد در صفحه:",
    itemsPerPageAll: "همه",
    nextPage: "صفحه بعدی",
    prevPage: "صفحه قبلی",
    firstPage: "اولین صفحه",
    lastPage: "آخرین صفحه",
    pageText: "{0} - {1} از {2}"
  },
  datePicker: {
    itemsSelected: "{0} انتخاب شده"
  },
  noDataText: "موردی برای نمایش وجود ندارد",
  carousel: {
    prev: "صفحه قبلی",
    next: "صفحه بعدی"
  },
  calendar: {
    moreEvents: "{0} بیشتر"
  }
};

Vue.use(Vuetify);

declare module "vue/types/vue" {
  interface Vue {
    $vuetify: Framework;
  }
}

export default new Vuetify({
  //   icons: { iconfont: "mdi", values: icons },
  theme: {
    dark: false,
    themes: {
      light: {
        secondary: "#C51162"
      }
    }
  },
  rtl: true,
  lang: {
    locales: {
      fa
    },
    current: "fa"
  }
});
