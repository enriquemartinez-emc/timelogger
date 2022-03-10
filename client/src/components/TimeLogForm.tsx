import { useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import { ITimeLog } from "../types";

interface Props {
  show: boolean;
  handleClose: () => void;
  addTimeLog: (timeLog: ITimeLog) => void;
}

export default function TimeLogForm({ show, handleClose, addTimeLog }: Props) {
  const [timeLog, setTimeLog] = useState<ITimeLog>({} as ITimeLog);
  const [validated, setValidated] = useState(false);

  function handleChange(event: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = event.target;
    setTimeLog({ ...timeLog, [name]: value });
  }

  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const form = e.currentTarget;
    if (form.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
      return;
    }
    setValidated(false);
    addTimeLog(timeLog);
  }

  return (
    <Modal show={show} onHide={handleClose} centered>
      <Modal.Header closeButton>
        <Modal.Title>New Time Log</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form onSubmit={handleSubmit} noValidate validated={validated}>
          <Form.Group className="mb-3" controlId="formDescription">
            <Form.Label>Description</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter description"
              name="description"
              onChange={handleChange}
              required
              autoComplete="off"
            />
            <Form.Control.Feedback type="invalid">
              Please enter a description.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group className="mb-3" controlId="formDuration">
            <Form.Label>Duration</Form.Label>
            <Form.Control
              type="number"
              placeholder="Enter Duration"
              name="duration"
              onChange={handleChange}
              required
            />
            <Form.Control.Feedback type="invalid">
              Please enter a duration.
            </Form.Control.Feedback>
          </Form.Group>
          <Button variant="primary" type="submit">
            Save Changes
          </Button>
        </Form>
      </Modal.Body>
    </Modal>
  );
}
