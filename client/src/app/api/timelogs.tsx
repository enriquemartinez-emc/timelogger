import { ITimeLog } from "../types";
import client from "./client";

export async function createTimeLog(id: string, timeLog: ITimeLog) {
  return await client.post(`/projects/${id}/timelogs`, timeLog);
}

export async function deleteTimeLog(id: string, timeLogId: number) {
  return await client.delete(`/projects/${id}/timelogs/${timeLogId}`);
}
