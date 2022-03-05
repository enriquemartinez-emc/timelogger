import React, { useEffect, useState } from "react";
import { Col, Form, Row } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { completeProject, getProjectById } from "../api/projects";
import { createTimeLog, deleteTimeLog } from "../api/timelogs";
import Chart from "../components/Chart";
import TimeLogForm from "../components/TimeLogForm";
import TimeLogsList from "../components/TimeLogsList";
import { AllErrors, ITimeLog } from "../types";

export default function TimeLogs() {
  const [timeLogs, setTimeLogs] = useState<ITimeLog[]>([]);
  const [showForm, setShowForm] = useState(false);
  const [isProjectCompleted, setIsProjectCompleted] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const params = useParams();

  const handleCloseForm = () => setShowForm(false);
  const handleShowForm = () => setShowForm(true);

  useEffect(() => {
    async function fetchtimeLogs() {
      try {
        const { data } = await getProjectById(params.projectId!);
        setTimeLogs(data.project.timeLogs);
      } catch (error: any) {
        setError(error);
      } finally {
        setLoading(false);
      }
    }

    fetchtimeLogs();
  }, []);

  useEffect(() => {
    setChart();
  }, [timeLogs]);

  async function handleAddTimeLog(timeLog: ITimeLog) {
    try {
      const { data } = await createTimeLog(params.projectId!, timeLog);
      const newTimeLog = data.timeLog;
      setTimeLogs([...timeLogs, newTimeLog]);
      setShowForm(false);
    } catch (error: AllErrors) {
      console.log(error.displayFormattedError());
    }
  }

  async function handleDeleteTimeLog(timeLogId: number) {
    try {
      await deleteTimeLog(params.projectId!, timeLogId);
      const newTimeLogs = timeLogs.filter((t: ITimeLog) => t.id !== timeLogId);
      setTimeLogs(newTimeLogs);
    } catch (error) {
      console.log(error);
    }
  }

  async function handleSwitchChange(e: React.ChangeEvent<HTMLInputElement>) {
    const checked = e.target.checked;
    try {
      if (checked) {
        await completeProject(params.projectId!);
      }
      setIsProjectCompleted(checked);
    } catch (error) {
      console.log(error);
    }
  }

  function setChart() {
    const labels = timeLogs.map((timeLog: ITimeLog) => timeLog.description);

    const currentChartData = {
      labels,
      datasets: [
        {
          label: "Time Activities",
          data: timeLogs.map((timeLog: ITimeLog) => timeLog.duration),
          backgroundColor: "#ffbb11",
        },
      ],
    };

    setChartData(currentChartData);
  }

  const [chartData, setChartData] = useState({});

  if (loading) return <p>Loading Time Registrations...</p>;
  if (error) return <p>Unable to display Time Registrations.</p>;

  return (
    <>
      <div className="mb-3">
        <Form>
          <Form.Check
            type="switch"
            id="custom-switch"
            label="Mark this project as Completed"
            onChange={handleSwitchChange}
          />
        </Form>
      </div>

      <Row>
        <Col>
          <Chart chartData={chartData} />
        </Col>
        <Col>
          <TimeLogsList
            timeLogs={timeLogs}
            isProjectCompleted={isProjectCompleted}
            handleShowForm={handleShowForm}
            handleDelete={handleDeleteTimeLog}
          />
        </Col>
      </Row>
      <TimeLogForm
        show={showForm}
        handleClose={handleCloseForm}
        addTimeLog={handleAddTimeLog}
      />
    </>
  );
}
