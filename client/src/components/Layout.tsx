import { Container } from "react-bootstrap";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import NavMenu from "./NavMenu";

interface LayoutProps {
  children: React.ReactNode;
}

export default function Layout({ children }: LayoutProps) {
  return (
    <div>
      <NavMenu />
      <Container>{children}</Container>
      <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
    </div>
  );
}
