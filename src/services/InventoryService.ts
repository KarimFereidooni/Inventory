import axios from "@/plugins/axios";
import { AppError } from "@/AppError";

export default class InventoryService {
  public static async getProducts(page, itemsPerPage, sortBy, sortDesc) {
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
      const response = await axios.post(`/Inventory/AddProduct`, data);
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
