<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <v-dialog v-model="dialog" max-width="500px">
        <template v-slot:activator="{ on }">
          <v-btn text v-on="on">
            <v-icon>mdi-plus</v-icon>
            <span class="ml-2">اضافه کردن</span>
          </v-btn>
        </template>
        <v-card>
          <v-card-title>
            <span class="headline">اطلاعات محصول</span>
          </v-card-title>

          <v-card-text>
            <form @submit="save">
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      autofocus
                      v-model="item.name"
                      label="نام محصول"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      v-model="item.count"
                      label="تعداد"
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </form>
          </v-card-text>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false"
              >انصراف</v-btn
            >
            <v-btn color="blue darken-1" text @click="save">ثبت</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-btn text>
        <v-icon>mdi-excel</v-icon>
        <span class="ml-2">خروجی اکسل</span>
      </v-btn>
      <v-spacer></v-spacer>
    </v-app-bar>

    <v-content>
      <v-container>
        <v-layout text-center wrap>
          <v-flex xs12>
            <div>
              <v-data-table
                :headers="headers"
                :items="products"
                :options.sync="options"
                :server-items-length="totalProducts"
                :loading="loading"
                class="elevation-1"
              ></v-data-table>
            </div>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "App",
  data: () => ({
    dialog: false,
    item: {
      name: "",
      count: ""
    },
    totalProducts: 0,
    products: [],
    loading: true,
    options: {} as any,
    headers: [
      {
        text: "نام محصول",
        value: "name"
      },
      { text: "تعداد", value: "count" }
    ]
  }),
  methods: {
    save() {
      //
    },
    getDataFromApi() {
      this.loading = true;
      return new Promise((resolve, reject) => {
        const { sortBy, sortDesc, page, itemsPerPage } = this.options;

        let items = this.getProducts();
        const total = items.length;

        if (sortBy.length === 1 && sortDesc.length === 1) {
          items = items.sort((a, b) => {
            const sortA = a[sortBy[0]];
            const sortB = b[sortBy[0]];

            if (sortDesc[0]) {
              if (sortA < sortB) {
                return 1;
              }
              if (sortA > sortB) {
                return -1;
              }
              return 0;
            } else {
              if (sortA < sortB) {
                return -1;
              }
              if (sortA > sortB) {
                return 1;
              }
              return 0;
            }
          });
        }

        if (itemsPerPage > 0) {
          items = items.slice((page - 1) * itemsPerPage, page * itemsPerPage);
        }

        setTimeout(() => {
          this.loading = false;
          resolve({
            items,
            total
          });
        }, 1000);
      });
    },
    getProducts() {
      return [
        {
          name: "Frozen Yogurt",
          count: 159
        },
        {
          name: "Ice cream sandwich",
          count: 237
        },
        {
          name: "Eclair",
          count: 262
        },
        {
          name: "Cupcake",
          count: 305
        },
        {
          name: "Gingerbread",
          count: 356
        },
        {
          name: "Jelly bean",
          count: 375
        }
      ];
    }
  },
  watch: {
    options: {
      handler() {
        this.getDataFromApi().then((data: any) => {
          this.products = data.items;
          this.totalProducts = data.total;
        });
      },
      deep: true
    }
  }
});
</script>
