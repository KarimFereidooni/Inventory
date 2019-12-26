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
        <form @submit="save">
          <v-card>
            <v-card-title>
              <span class="headline">اطلاعات محصول</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12">
                    <v-text-field
                      autofocus
                      v-model="item.name"
                      label="نام محصول"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6">
                    <v-text-field
                      type="number"
                      v-model="item.count"
                      label="تعداد"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6">
                    <v-autocomplete
                      v-model="item.enumerationUnit"
                      :items="['عدد', 'کیلوگرم', 'تن']"
                      label="واحد شمارش"
                    >
                    </v-autocomplete>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-btn
                type="submit"
                color="blue darken-1"
                :loading="saving"
                text
                @click="save"
                >ثبت</v-btn
              >
              <v-btn color="blue darken-1" text @click="dialog = false"
                >انصراف</v-btn
              >
            </v-card-actions>
          </v-card>
        </form>
      </v-dialog>
      <v-btn text :loading="exporting" @click="exportExcel">
        <v-icon>mdi-excel</v-icon>
        <span class="ml-2">خروجی اکسل</span>
      </v-btn>
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
import InventoryService from "./services/InventoryService";
import { download } from "@/download";

export default Vue.extend({
  name: "App",
  data: () => ({
    dialog: false,
    item: {
      name: "",
      count: "",
      enumerationUnit: ""
    },
    totalProducts: 0,
    products: [],
    loading: true,
    saving: false,
    exporting: false,
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
    async save() {
      try {
        this.saving = true;
        await InventoryService.addProduct(this.item);
        this.dialog = false;
        await this.loadData();
        this.resetForm();
      } catch (e) {
        this.showErrorMessage(e);
      } finally {
        this.saving = false;
      }
    },
    async loadData() {
      try {
        this.loading = true;
        const { sortBy, sortDesc, page, itemsPerPage } = this.options;
        const result = await InventoryService.getProducts(
          page,
          itemsPerPage,
          sortBy[0] || "",
          sortDesc[0] || false
        );
        this.products = result.items;
        this.totalProducts = result.totalCount;
      } catch (e) {
        this.showErrorMessage(e);
      } finally {
        this.loading = false;
      }
    },
    async exportExcel() {
      try {
        this.exporting = true;
        const { sortBy, sortDesc } = this.options;
        const result = await InventoryService.exportExcel(
          sortBy[0] || "",
          sortDesc[0] || false
        );
        download(
          result,
          "Export.xlsx",
          "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        );
      } catch (e) {
        this.showErrorMessage(e);
      } finally {
        this.exporting = false;
      }
    },
    resetForm() {
      this.item = {
        name: "",
        count: "",
        enumerationUnit: ""
      };
    }
  },
  watch: {
    options: {
      handler() {
        this.loadData();
      },
      deep: true
    }
  }
});
</script>
