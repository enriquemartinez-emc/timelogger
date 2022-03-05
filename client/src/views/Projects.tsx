import { useEffect, useState } from "react";
import { Badge, Button, Table } from "react-bootstrap";
import { Link } from "react-router-dom";
import { getAllProjects } from "../api/projects";
import { IProject } from "../types";

export default function Projects() {
  const [projects, setProjects] = useState<IProject[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [toggleSort, setToggleSort] = useState(false);

  useEffect(() => {
    async function fetchProjects() {
      try {
        const { data } = await getAllProjects();
        setProjects(data.projects);
      } catch (error: any) {
        setError(error);
      } finally {
        setLoading(false);
      }
    }

    fetchProjects();
  }, []);

  function getDate(date: string) {
    return date.split("T")[0];
  }

  useEffect(() => {
    const sorted = [...projects].sort((a, b) => {
      return toggleSort
        ? (new Date(a.deadline) as any) - (new Date(b.deadline) as any)
        : (new Date(b.deadline) as any) - (new Date(a.deadline) as any);
    });
    setProjects(sorted);
  }, [toggleSort]);

  if (loading) return <p>Loading projects...</p>;
  if (error) return <p>Unable to display projects.</p>;

  return (
    <>
      <Table bordered>
        <thead>
          <tr>
            <th>Project Id</th>
            <th>Project Name</th>
            <th>Status</th>
            <th>
              <Button
                variant="outline-light"
                onClick={() => setToggleSort((toggle) => !toggle)}
              >
                Deadline <i className="bi-chevron-expand"></i>
              </Button>
            </th>
          </tr>
        </thead>
        <tbody>
          {projects.map((project: IProject) => (
            <tr key={project.id}>
              <td>{project.id}</td>
              <td>
                <Link to={`/projects/${project.id}`} className="link-primary">
                  {project.name}
                </Link>
              </td>
              <td>
                {project.isCompleted ? (
                  <Badge bg="success" text="dark">
                    Completed
                  </Badge>
                ) : (
                  <Badge bg="warning" text="dark">
                    Incomplete
                  </Badge>
                )}
              </td>
              <td>{getDate(project.deadline)}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  );
}
