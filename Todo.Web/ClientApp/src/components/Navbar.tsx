import React from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { AppBar, Button, Container, Toolbar, Typography } from "@mui/material";
import axios from "axios";

export default function Navbar() {
  const location = useLocation();
  const navigate = useNavigate();

  const logout = () => {
    localStorage.removeItem("token");
    navigate("/login");
  };

  const handleRemoveUser = async () => {
    const token = localStorage.getItem("token");
    axios
      .delete("api/user", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then(() => {
        navigate("/login");
      });
  };

  return (
    <AppBar position="static" sx={{ marginBottom: 10 }}>
      <Container>
        <Toolbar disableGutters>
          <Typography
            variant="h6"
            component="a"
            href="/"
            sx={{
              flexGrow: 1,
              color: "white",
              textDecoration: "none",
              fontWeight: 700,
              ":hover": {
                color: "inherit",
                textDecoration: "none",
              },
            }}
          >
            To-Do
          </Typography>
          {location.pathname === "/" && (
            <Button
              variant="text"
              sx={{ color: "white" }}
              onClick={handleRemoveUser}
            >
              Hesabı Sil
            </Button>
          )}
          {location.pathname === "/" && (
            <Button variant="text" sx={{ color: "white" }} onClick={logout}>
              Çıkış Yap
            </Button>
          )}
          {location.pathname === "/login" && (
            <Button
              variant="text"
              sx={{ color: "white" }}
              onClick={() => navigate("/register")}
            >
              Üye Ol
            </Button>
          )}
          {location.pathname === "/register" && (
            <Button
              variant="text"
              sx={{ color: "white" }}
              onClick={() => navigate("/login")}
            >
              Giriş Yap
            </Button>
          )}
        </Toolbar>
      </Container>
    </AppBar>
  );
}
