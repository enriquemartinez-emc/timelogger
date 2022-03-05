import { Button, Table } from "react-bootstrap";
import { ITimeLog } from "../types";

interface Props {
  timeLogs: ITimeLog[];
  isProjectCompleted: boolean;
  handleShowForm: () => void;
  handleDelete: (timeLogId: number) => void;
}

export default function TimeLogsList({
  timeLogs,
  isProjectCompleted,
  handleShowForm,
  handleDelete,
}: Props) {
  return (
    <>
      <p>
        <Button disabled={isProjectCompleted} onClick={handleShowForm}>
          Create New Time Log
        </Button>
      </p>

      <Table>
        <thead>
          <tr>
            <th>Description</th>
            <th>Duration</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {timeLogs.map((timeLog: ITimeLog) => (
            <tr key={timeLog.id}>
              <td>{timeLog.description}</td>
              <td>{timeLog.duration}</td>
              <td>
                <a href="#" onClick={() => handleDelete(timeLog.id)}>
                  <i className="bi-x-lg"></i>
                </a>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  );
}
