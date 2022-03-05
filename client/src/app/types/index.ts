import { toast } from "react-toastify";

export interface IProject {
  id: number;
  name: string;
  deadline: string;
  isCompleted: boolean;
}

export interface ITimeLog {
  id: number;
  description: string;
  duration: number;
}

export class ServerError extends Error {
  constructor() {
    super("A server error has occurred");
    this.name = "ServerError";
  }
}

export class ValidationError extends Error {
  errors: string[] = [];

  constructor(errors: string[]) {
    super("There was something wrong with your request body!");
    this.name = "ValidationError";
    this.errors = errors;
  }

  displayFormattedError() {
    let errorStr = "";

    const modelStateErrors: string[] = [];
    for (const key in this.errors) {
      if (this.errors[key]) {
        modelStateErrors.push(this.errors[key]);
      }
    }
    toast.error(modelStateErrors.flat().join(", "));

    return errorStr;
  }
}

export type AllErrors = ServerError | ValidationError | any;
