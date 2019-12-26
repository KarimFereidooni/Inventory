import store from "./store";

export class AppError extends Error {
  constructor(message, code) {
    if (code === 401) {
      store.dispatch("openQuickLoginDialog");
    }
    super(message);
    (this as any).errorCode = code;
  }
}
