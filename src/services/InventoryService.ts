import axios from "@/plugins/axios";
import { AppError } from "@/AppError";

export default class InventoryService {
  public static async getProducts(
    page: number,
    itemsPerPage: number,
    sortBy: string,
    sortDesc: boolean
  ) {
    try {
      const response = await axios.get(
        `/Inventory/GetProducts?page=${page}&itemsPerPage=${itemsPerPage}&sortBy=${sortBy}&sortDesc=${sortDesc}`
      );
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error
            ? error.response.data.error
            : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async addProduct(data) {
    try {
      await axios.post(`/Inventory/AddProduct`, data);
      return true;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error
            ? error.response.data.error
            : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }

  public static async exportExcel(sortBy: string, sortDesc: boolean) {
    try {
      const response = await axios.get(
        `/Inventory/ExportExcel?sortBy=${sortBy}&sortDesc=${sortDesc}`,
        { responseType: "blob" }
      );
      return response.data;
    } catch (error) {
      if (error.response && error.response.data) {
        throw new AppError(
          error.response.data.error
            ? error.response.data.error
            : error.response.data.toString(),
          error.response.data.errorCode || 0
        );
      } else {
        throw error;
      }
    }
  }
}
