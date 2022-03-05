import axios from "axios";
import { ValidationError } from "../types";

const instance = axios.create({
  baseURL: "http://localhost:3001/api",
  headers: {
    "Content-type": "application/json",
  },
});

instance.interceptors.response.use(
  (response) =>
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data
    response,
  (error) => {
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error

    const { status, data } = error.response!;

    if (status === 400) {
      throw new ValidationError(data.errors);
    }

    return Promise.reject(error);
  }
);

export default instance;
