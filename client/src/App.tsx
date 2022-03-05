import { Outlet, Route, Routes } from "react-router-dom";
import Layout from "./components/Layout";
import Projects from "./views/Projects";
import TimeRegistrations from "./views/TimeLogs";

export default function App() {
  return (
    <Layout>
      <Routes>
        <Route path="/*" element={<Outlet />}>
          <Route index element={<Projects />} />
          <Route path="projects" element={<Projects />} />
          <Route path="projects/:projectId" element={<TimeRegistrations />} />
        </Route>
      </Routes>
    </Layout>
  );
}