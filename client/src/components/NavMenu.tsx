import { Container, Nav, Navbar, NavbarBrand } from "react-bootstrap";
import { Link } from "react-router-dom";

export default function NavMenu() {
  return (
    <header>
      <Navbar className="navbar navbar-expand-lg navbar-dark bg-primary box-shadow mb-3">
        <Container>
          <NavbarBrand>Time Logger</NavbarBrand>
          <Nav className="me-auto">
            <Nav.Link as={Link} to={"/projects"}>
              Projects
            </Nav.Link>
          </Nav>
        </Container>
      </Navbar>
    </header>
  );
}
