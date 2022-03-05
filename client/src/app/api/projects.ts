import client from "./client";

export async function getAllProjects() {
  return await client.get("/projects");
}

export async function getProjectById(id: string) {
  return await client.get(`/projects/${id}`);
}

export async function completeProject(id: string) {
  return await client.put(`/projects/${id}/complete`);
}
